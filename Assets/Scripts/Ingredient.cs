using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum IngredientWin
{
  Ignore, //
  None,   // Doesn't have the liquid or not enough
  Partial, // Has some of the liquid
  Exact,  // Has the liquid
  Over,   // Has the liquid but is over
  Wrong   // Wrong liquid
}

public class Ingredient : MonoBehaviour
{
  public Sprite               TexCorrect, TexPlus, TexIncorrect, TexMiddle, TexNone;
  public bool                 RefreshNeeded = false;
  public LiquidType           LiquidType;
  public int                  Touched;
  public UnityEngine.UI.Image Image;
  public UnityEngine.UI.Text  Text;
  public GlassLiquid          Liquid;
  public CocktailMeasure      Measure;
  public Glass                Glass;

  bool  isCorrect;
  float progress;

  public IngredientWin        Won = IngredientWin.None;

  static bool RoughlyEquals(float a, float b, float delta)
  {
    return Mathf.Abs(b - a) < delta;
  }

  void Awake()
  {
    TexCorrect   = Resources.Load<Sprite>("UI/ingredient_good");
    TexPlus      = Resources.Load<Sprite>("UI/ingredient_plus");
    TexIncorrect = Resources.Load<Sprite>("UI/ingredient_bad");
    TexMiddle    = Resources.Load<Sprite>("UI/ingredient_ongoing");
    TexNone      = Resources.Load<Sprite>("UI/ingredient");
  }

  public void DoSetup(LiquidType lt,  CocktailMeasure measure, Glass glass)
  {
    RefreshNeeded       = true;
    LiquidType          = lt;
    Text.enabled        = true;
    Image.enabled       = true;
    Liquid              = null;
    Measure             = measure;
    Glass               = glass;

    if (Measure != null)
    {
      Image.sprite = TexNone;
      Won = IngredientWin.None;
    }
    else
    {
      Image.sprite = TexIncorrect;
      Won = IngredientWin.Wrong;
    }

  }

  public void DoUpdate()
  {
    RefreshNeeded       = true;
  }
  
  public void Enable()
  {
    Text.enabled        = true;
    Image.enabled       = true;
  }

  public void Disable()
  {
    Text.enabled        = false;
    Image.enabled       = false;
  }

  public IngredientWin GetWin()
  {
    bool hasMeasure = HasMeasure();
    bool hasLiquid  = HasLiquid();

    if (hasMeasure == false)
      return IngredientWin.Wrong;
    if (hasLiquid == false)
      return IngredientWin.None;

     int ml = 0;
     int target = Measure.Measure * 25;

      if (hasLiquid)
      {
        ml = Mathf.RoundToInt(GlassLiquid.AmountToMeasures(Liquid.amount, Glass.GlassType))  * 25;
          
        if (LiquidType == LiquidType.Beer && Glass.BeerHead != null)
        {
          ml += Mathf.RoundToInt(GlassLiquid.AmountToMeasures(Glass.BeerHead.amount, Glass.GlassType))  * 25;
        }
      }
      
      if (ml == 0)
      {
        return IngredientWin.None;
      }
      else if (ml < target)
      {
        return IngredientWin.Partial;
      }
      else if (GlassLiquid.MlApprox(ml, target))
      {
        return IngredientWin.Exact;
      }
      else if (ml >= target)
      {
        return IngredientWin.Over;
      }
      else
      {
        return IngredientWin.Wrong;
      }
  }

  public void Update()
  {
    if (RefreshNeeded)
    {
      RefreshNeeded = false;
      
      bool hasMeasure = HasMeasure();
      bool hasLiquid  = HasLiquid();

      
      if (hasMeasure && Glass != null)
      {
        
        Won = GetWin();

        switch(Won)
        {
          case IngredientWin.None:
            Image.sprite = TexNone;
            break;
          case IngredientWin.Partial:
            Image.sprite = TexMiddle;
          break;
          case IngredientWin.Exact:
            Image.sprite = TexCorrect;
            break;
          case IngredientWin.Over:
            Image.sprite = TexPlus;
            break;
          case IngredientWin.Wrong:
            Image.sprite = TexIncorrect;
            break;
        }


        if (hasLiquid && Glass != null)
        {
          float liquidAmount = Liquid.amount;

          if (Liquid.type == LiquidType.Beer && Glass.BeerHead != null)
          {
            liquidAmount += Glass.BeerHead.amount;
          }

          Text.text = GlassLiquid.LiquidTypeAndMlWithMeasureToString(liquidAmount, Measure.Amount, Liquid.type, Glass.GlassType);
        }
        else
        {
          Text.text = String.Format("{0}", Measure.Text);
        }
      }
      else if (hasLiquid)
      {
        Image.sprite = TexIncorrect;
        Text.text = GlassLiquid.LiquidTypeAndMeasureToStringFloatAmount(Liquid.amount, Liquid.type, Glass.GlassType);
        Won = IngredientWin.Wrong;
      }
      else
      {
        Image.sprite = TexIncorrect;
        Text.text = String.Format("??? {0} ", LiquidType);
      }
    }
  }

  public bool HasMeasure()
  {
    return (Measure != null && Measure.LiquidType != LiquidType.None);
  }

  public bool HasLiquid()
  {
    return (Liquid != null && Liquid.type != LiquidType.None);
  }

  public bool HasHead()
  {
    return (HasLiquid() && (Liquid != null && Liquid.type != LiquidType.BeerHead));
  }
}
