using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

[Serializable]
public class CocktailMeasure
{
  [SerializeField]
  public LiquidType              LiquidType;

  [SerializeField]
  public int                     Measure;

  [SerializeField]
  public String                  Text;

  [SerializeField]
  public float                   Amount;
}

[Serializable]
public class Cocktail
{
  [SerializeField]
  public String                       Name;

  [SerializeField]
  public GlassType                    Glass;

  [SerializeField]
  public List<CocktailMeasure>        Measures;
}

public class Game : MonoBehaviour
{
  
  public Glass Glass;
  public Tap   Tap;
  public Text  Text;
  public float lastSum = -1.0f;
  public TextAsset CocktailsTxt;
  public Text  OrderText;
  
  public List<Ingredient>  ingredients; 
  public List<Cocktail> Cocktails; 

  Cocktail Cocktail;

  void Start()
  {
    if (ingredients != null)
    {
      Vector3 p;
      for (int ingIi = 0; ingIi < ingredients.Count; ingIi++)
      {
        var ing = ingredients[ingIi];
        p = ing.transform.localPosition;
        p.y = ingIi * -40;
        ing.transform.localPosition = p;
      }
    }

    ReadCocktails();

    SetupCocktail(Cocktails[4]);
  }

  void Update()
  {
    Glass.Sum = 0.0f;

    bool reorderIngredients = false;

    // UI
    for (int liquidIi = 0; liquidIi < Glass.liquids.Count; liquidIi++)
    {
      var liquid = Glass.liquids[liquidIi];
      Glass.Sum += liquid.amount;

      if (liquid.ingredient == null)
      {
        liquid.ingredient = FindIngredientUi(liquid.type);

        if (liquid.ingredient == null)
        {
          liquid.ingredient = MakeIngredientUi(liquid.type);
          if (liquid.ingredient)
          {
            liquid.ingredient.Liquid = liquid;
            liquid.ingredient.Glass = Glass;
            liquid.ingredient.Enable();
            liquid.ingredient.DoUpdate();
            liquid.ingredient.Update();
          }
        }
        else
        {
          liquid.ingredient.Liquid = liquid;
          liquid.ingredient.Glass = Glass;
        }
      }
    }

    // Spilling
    if (Glass.Sum > 1.0f)
    {
      float upSum = 0.0f;
      GlassLiquid topLiquid = null;

      for (int liquidIi = 0; liquidIi < Glass.liquids.Count; liquidIi++)
      {
        GlassLiquid gl = Glass.liquids[liquidIi];

        if (liquidIi == Glass.liquids.Count - 1)
        {
          topLiquid = gl;
        }
        else
        {
          upSum += gl.amount;
        }
      }

      if (topLiquid != null)
      {
        topLiquid.amount = 1.0f - upSum;

        if (topLiquid.amount <= 0.0f)
        {
          if (topLiquid.ingredient.HasMeasure() == false)
          {
            topLiquid.ingredient.LiquidType = LiquidType.None;
            topLiquid.ingredient.Disable();
          }
          
          topLiquid.ingredient.Liquid = null;
          topLiquid.ingredient.RefreshNeeded = true;

          Glass.liquids.Remove(topLiquid);
          Glass.liquidsOrderChanged = true;
          reorderIngredients = true;
        }
        else
        {
          topLiquid.animate = true;
        }

        Glass.updateNeeded = true;
      }
    }
    
    // Win Conditions
    if (Glass.liquidsOrderChanged || Mathf.Approximately(lastSum, Glass.Sum) == false)
    {
      Glass.liquidsOrderChanged = false;
      CheckWinCondition();
    }

    if (reorderIngredients)
    {
      OrderMeasures();
    }

    lastSum = Glass.Sum;
    
  }
  
  void CheckWinCondition()
  {
    bool haveWon = false, haveLost = false;
    
    foreach(var ingredient in ingredients)
    {
      if (ingredient.Won == IngredientWin.Ignore)
        continue;
    }
  }
  
  void ReadCocktails()
  {
    Cocktails = new List<Cocktail>(40);
    
    String txt = CocktailsTxt.text;
    using (StringReader reader = new StringReader(txt))
    {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          line = line.Trim();
          if (String.IsNullOrEmpty(line))
            continue;
          
          if (line[0] == '#')
            continue;
          
          String[] p = line.Split(';');

          if (p.Length < 3)
            continue;

          Cocktail cocktail = new Cocktail();
          Cocktails.Add(cocktail);

          cocktail.Name      = p[0];
          cocktail.Glass     = GlassLiquid.GlassStringToEnum(p[1]);
          cocktail.Measures  = new List<CocktailMeasure>(3);

          int   left =  GlassLiquid.GetGlassSizeInMeasures(cocktail.Glass);;
          
          float glassMeasures = left;

          for(int i=2;i < p.Length;i++)
          {
            string[] measureText            = p[i].Split(new char[]{' '}, 2);
            
            if (measureText.Length != 2)
            {
              Debug.LogErrorFormat("Bad Measure: '{0}', in {1}", measureText, line);
              continue;
            }
            
            int         measure             = 0; 
            Int32.TryParse(measureText[0], out measure);

            if (measure == 0)
            {
              measure = left;
            }

            String      ingredientName      = measureText[1];
            LiquidType  liquidType          = GlassLiquid.LiquidStringToEnum(ingredientName);
            String      humanText           = GlassLiquid.LiquidTypeAndMlToString(measure * 25, liquidType, cocktail.Glass);
            
            CocktailMeasure cocktailMeasure = new CocktailMeasure();
            cocktailMeasure.LiquidType      = liquidType;
            cocktailMeasure.Measure         = measure;
            cocktailMeasure.Text            = humanText;
            cocktailMeasure.Amount          = measure / glassMeasures;
            
            left -= measure;

            cocktail.Measures.Add(cocktailMeasure);
          }
        }
    }
  }
  
  void SetupCocktail(Cocktail cocktail_)
  {
    Cocktail = cocktail_;
    Glass.GlassInfo = GlassInfo.GetGlassType(Cocktail.Glass);
    Glass.Sum = 0.0f;
    OrderText.text = Cocktail.Name;
    SetupMeasures();

    Debug.LogFormat("Cocktail Setup: Glass = {0}, Name = {1}, GlassMeasures = {2}", Glass.GlassInfo.type, Cocktail.Name, Glass.GlassInfo.measures);
  }
  
  Ingredient FindIngredientUi(LiquidType lt)
  {
    for(int i=0;i < ingredients.Count;i++)
    {
      if (ingredients[i].LiquidType == lt)
        return ingredients[i];
    }
    return null;
  }

  Ingredient MakeIngredientUi(LiquidType lt)
  {
    for(int i=0;i < ingredients.Count;i++)
    {
      if (ingredients[i].LiquidType == LiquidType.None)
      {
        ingredients[i].LiquidType = lt;
        return ingredients[i];
      }
    }
    return null;
  }

  void SetupMeasures()
  {
    for (int ingredientIi = 0; ingredientIi < ingredients.Count; ingredientIi++)
    {
      Ingredient ingredient = ingredients[ingredientIi];
      ingredient.LiquidType = LiquidType.None;
      ingredient.Won        = IngredientWin.Ignore;
      ingredient.Disable();
    }

    for(int measuresIi = 0;measuresIi < Cocktail.Measures.Count;measuresIi++)
    {
      CocktailMeasure measure = Cocktail.Measures[measuresIi];
      Ingredient ingredient   = FindIngredientUi(measure.LiquidType);
      
      if (ingredient == null)
      {
        ingredient = MakeIngredientUi(measure.LiquidType);
        if (ingredient == null)
          continue;
      }

      ingredient.DoSetup(measure.LiquidType, measure, Glass);
    }

    OrderMeasures(true);
  }
  
  void OrderMeasures(bool force = false)
  {
    int ingredientY = 0;
    bool didGap = false;

    for (int ingredientIi = 0; ingredientIi < ingredients.Count; ingredientIi++)
    {
      Ingredient ingredient = ingredients[ingredientIi];
      
      if ((ingredient.Measure == null || ingredient.Measure.LiquidType == LiquidType.None) && !didGap)
      {
        ingredientY++;
        didGap = true;
      }
      
      Vector3 p = ingredient.transform.localPosition;
      p.y = ingredientY * -40;
      ingredient.transform.localPosition = p;
      ingredientY++;
      ingredient.Enable();

      if (ingredient.LiquidType == LiquidType.None)
      {
        ingredient.Disable();
      }

    }

  }
}
