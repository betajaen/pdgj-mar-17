using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum IngredientWin
{
  Ignore, //
  None,   // Doesn't have the liquid or not enough
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

  public void Update()
  {
    if (RefreshNeeded)
    {
      RefreshNeeded = false;
      
//      if (isCorrect)
//      {
//        if (Mathf.Approximately(progress, 0))
//        {
//          Image.sprite = TexNone;
//        }
//        else if (Mathf.Approximately(progress, 1.0f) || (progress > 0.95f && progress < 1.15f) )
//        {
//          Image.sprite = TexCorrect;
//        }
//        else if (progress > 1.15f)
//        {
//          Image.sprite = TexPlus;
//        }
//        else
//        {
//          Image.sprite = TexMiddle;
//        }
//      }
//      else
//      {
//        Image.sprite = TexIncorrect;
//      }

      bool hasMeasure = HasMeasure();
      bool hasLiquid  = HasLiquid();

      
      if (hasMeasure)
      {
        
        
        int measures = 0;

        if (hasLiquid)
        {
          measures = (int) GlassLiquid.AmountToMeasures(Liquid.amount, Glass.GlassType);
        }

        if (measures == 0)
        {
          Image.sprite = TexNone;
          Won = IngredientWin.None;
        }
        else if (measures == Measure.Measure)
        {
          Image.sprite = TexCorrect;
          Won = IngredientWin.Exact;
        }
        else if (measures >= Measure.Measure)
        {
          Image.sprite = TexPlus;
          Won = IngredientWin.Over;
        }
        else
        {
          Image.sprite = TexMiddle;
          Won = IngredientWin.None;
        }
        
        Text.text = String.Format("{0}", Measure.Text);
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

}
