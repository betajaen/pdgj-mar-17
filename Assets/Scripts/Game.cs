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

public enum GameState
{
  None,
  Menu,
  Pour,
  Submit,
  Loose
}

public class ControlInfo
{
  public SolidType   Solid    = SolidType.None;
  public LiquidType  Liquid   = LiquidType.None;

  public void Reset()
  {
    Solid  = SolidType.None;
    Liquid = LiquidType.None;
  }

  public void Set(SolidType solid)
  {
    Solid = solid;
  }

  public void Set(LiquidType liquid)
  {
    Liquid = liquid;
  }

}

public class Game : MonoBehaviour
{
  
  public Glass Glass;
  public Tap   Tap;
  public Text  Text;
  public float lastSum = -1.0f;
  public TextAsset CocktailsTxt;
  public Text  OrderText;
  public Text  BreweryText;
  
  public List<Ingredient>  ingredients; 
  public List<Cocktail> Cocktails; 

  public static GameState State = 0, NextState = 0;

  Cocktail Cocktail;

  [SerializeField] public GameObject UI_IngredientsList;
  [SerializeField] public GameObject UI_ControlsRight;
  [SerializeField] public GameObject UI_ControlsLeft;
  [SerializeField] public GameObject UI_ControlsLeftThumb;
  [SerializeField] public GameObject UI_OrderName;
  [SerializeField] public Text       UI_OrderResult;
  [SerializeField] public Stars      UI_Stars;
  [SerializeField] public GameObject UI_ControlsSubmit;
  [SerializeField] public GameObject UI_BreweryName; 
  
  public static ControlInfo Tap_Action2 = new ControlInfo();
  public static ControlInfo Tap_Action3 = new ControlInfo();
  public static ControlInfo Tap_Action4 = new ControlInfo();
  public static ControlInfo Tap_Up      = new ControlInfo();
  public static ControlInfo Tap_Down    = new ControlInfo();
  public static ControlInfo Tap_Left    = new ControlInfo();
  public static ControlInfo Tap_Right   = new ControlInfo();

  public ControlMarker Control_Action2;
  public ControlMarker Control_Action3;
  public ControlMarker Control_Action4;
  public ControlMarker Control_Up;
  public ControlMarker Control_Right;
  public ControlMarker Control_Down;
  public ControlMarker Control_Left;



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

    SetupCocktail(GetNextCocktail());
    SetState(GameState.Menu);

  }

  void Update()
  {
    if (NextState != GameState.None)
    {
      GameState newState = NextState;
      NextState = GameState.None;
      SetState(newState);
    }

    switch(State)
    {
      case GameState.Menu:
        UpdateMenu();
        break;
      case GameState.Pour:
        UpdatePour();
        break;
      case GameState.Submit:
        UpdateSubmit();
        break;
      case GameState.Loose:
        UpdateGameOver();
        break;
    }
  }
  
  void Show(GameObject go)
  {
    if (go != null)
    {
      go.SetActive(true);
    }
  }

  void Hide(GameObject go)
  {
    if (go != null)
    {
      go.SetActive(false);
    }
  }

  void SetState(GameState newState)
  {
    Debug.Log("New State = " + newState);

    Hide(UI_ControlsLeft);
    Hide(UI_ControlsLeftThumb);
    Hide(UI_ControlsRight);
    Hide(UI_IngredientsList);
    Hide(UI_OrderName);
    Hide(UI_OrderResult.gameObject);
    Hide(UI_Stars.gameObject);
    Hide(UI_ControlsSubmit);
    Hide(UI_BreweryName);

    switch(newState)
    {
      case GameState.Menu:
      break;
      case GameState.Pour:
      {
        Show(UI_ControlsLeft);
        Show(UI_ControlsLeftThumb);
        Show(UI_ControlsRight);
        Show(UI_IngredientsList);
        Show(UI_OrderName);
        Show(UI_BreweryName);
        
        SetupCocktail(GetNextCocktail());
      }
        break;
      case GameState.Submit:
      {
        Show(UI_OrderResult.gameObject);
        Show(UI_Stars.gameObject);
        Show(UI_ControlsSubmit);

        UI_Stars.Set(stars);
        UI_OrderResult.text = resultText;
      }
        break;
      case GameState.Loose:
        break;
    }

    State = newState;
  }

  void UpdateMenu()
  {
    NextState = GameState.Pour;
  }

  void UpdateSubmit()
  {
    // A - Add Drink
    if (Controls.Action1)
    {
      SetupCocktail(Cocktails[0]);
      NextState = GameState.Pour;
    }
  }

  void UpdatePour()
  {
    Glass.Sum = 0.0f;

    bool reorderIngredients = false;

    // UI
    for (int liquidIi = 0; liquidIi < Glass.liquids.Count; liquidIi++)
    {
      var liquid = Glass.liquids[liquidIi];
      Glass.Sum += liquid.amount;

      if (liquid.ingredient == null && liquid.type != LiquidType.BeerHead)
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

    Glass.BeerHeadTimer += Time.deltaTime;

    // Beer Head
    if (Glass.BeerHead != null && Glass.BeerHead.type == LiquidType.BeerHead && Glass.BeerHeadTimer >= 0.1f)
    {
      GlassLiquid beer = Glass.FindLiquid(LiquidType.Beer);
      if (beer != null && beer.amount < 0.95f)
      {
        const float flowRate = 0.05f;

        float headAmount = Glass.BeerHead.amount;
        float headFlow = flowRate*Time.deltaTime;

        bool doDeleteHead = false;

        float newHeadAmount = headAmount - headFlow;

        if (newHeadAmount <= 0.0f)
        {
          doDeleteHead = true;
          headFlow = headAmount;
        }

        beer.amount += headFlow;
        Glass.BeerHead.amount -= headFlow;

        if (doDeleteHead)
        {
          Glass.DeleteLiquid(Glass.BeerHead);
          Glass.BeerHead = null;
        }

        Glass.updateNeeded = true;
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
          if (topLiquid.ingredient != null)
          {
            if (topLiquid.ingredient.HasMeasure() == false)
            {
              topLiquid.ingredient.LiquidType = LiquidType.None;
              topLiquid.ingredient.Disable();
            }

            topLiquid.ingredient.Liquid = null;
            topLiquid.ingredient.RefreshNeeded = true;
          }

          Glass.DeleteLiquid(topLiquid);
          reorderIngredients = true;
        }
        else
        {
          topLiquid.animate = true;
        }

        Glass.updateNeeded = true;
      }
    }

    if (reorderIngredients)
    {
      OrderMeasures();
    }

//    if (reorderIngredients || Mathf.Approximately(lastSum, Glass.Sum) == false)
//    {
//     // Debug.Log(GetWin());
//    } 

    lastSum = Glass.Sum;

    // A - Add Drink
    if (Controls.Action1)
    {
      SubmitDrink();
    }
  }

  void UpdateGameOver()
  {
  }

  int stars = 0;
  String resultText = String.Empty;

  // 1-5 stars.
  public int GetWin()
  {
    bool hasIngredients = false, havePartialIngredients = false, haveMissingIngredients = false, haveCorrectAmounts = false, haveOver = false, emptyGlass = true;
    int extraIngredients = 0;
    
    foreach (var ingredient in ingredients)
    {
      if (ingredient.LiquidType == LiquidType.BeerHead || ingredient.LiquidType == LiquidType.None)
        continue;
      
      hasIngredients = true;
      
      if (ingredient.Measure == null)
      {
        extraIngredients++;
        continue;
      }
      else
      {
        IngredientWin win = ingredient.GetWin();
        switch(win)
        {
          case IngredientWin.None:
            haveMissingIngredients = true;
            break;
          case IngredientWin.Partial:
            havePartialIngredients = true;
            haveCorrectAmounts = false;
            break;
          case IngredientWin.Exact:
            if (havePartialIngredients == false)
              haveCorrectAmounts = true;
            break;
          case IngredientWin.Over:
            haveOver = true;
            break;
          case IngredientWin.Wrong:
            extraIngredients++;
            break;
        }
      }
    }

    if (haveMissingIngredients)
      haveCorrectAmounts = false;

    int score = 5;

    //Debug.LogFormat("Has={0}, Partial={1}, Missing={2}, Correct={3}, Over={4}, Empty={5}, Extra={6}", hasIngredients, havePartialIngredients, haveMissingIngredients, haveCorrectAmounts, haveOver, emptyGlass, extraIngredients);

    if (hasIngredients == false)
    {
      //Debug.Log("No Ingredients");
      return 0;
    }

    if (extraIngredients > 0)
    {
      if (extraIngredients == 1)
        score--;
      else if (extraIngredients == 2)
        score -= 2;
      else if (extraIngredients >= 3)
        score -= 3;
      
      //Debug.Log("Extra " + extraIngredients);
    }

    if (!haveCorrectAmounts)
    {
      if (haveMissingIngredients)
      {
        score -= 2;
      }

      if (havePartialIngredients || haveOver)
      {
        score--;
      }
    }

    if (score < 0)
      score = 0;
    else if (score > 5)
      score = 5;

    return score;
  }

  void SubmitDrink()
  {
    Debug.Log("Submit Drink");
    stars = GetWin();
    NextState = GameState.Submit;
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

        cocktail.Name = p[0];
        cocktail.Glass = GlassLiquid.GlassStringToEnum(p[1]);
        cocktail.Measures = new List<CocktailMeasure>(3);

        int left = GlassLiquid.GetGlassSizeInMeasures(cocktail.Glass);
        ;

        float glassMeasures = left;

        for (int i = 2; i < p.Length; i++)
        {
          string[] measureText = p[i].Split(new char[] {' '}, 2);

          if (measureText.Length != 2)
          {
            Debug.LogErrorFormat("Bad Measure: '{0}', in {1}", p[i], line);
            continue;
          }

          int measure = 0;
          Int32.TryParse(measureText[0], out measure);

          if (measure == 0)
          {
            measure = left;
          }

          String ingredientName = measureText[1];
          LiquidType liquidType = GlassLiquid.LiquidStringToEnum(ingredientName);
          String humanText = GlassLiquid.LiquidTypeAndMlToString(measure*25, liquidType, cocktail.Glass);

          CocktailMeasure cocktailMeasure = new CocktailMeasure();
          cocktailMeasure.LiquidType = liquidType;
          cocktailMeasure.Measure = measure;
          cocktailMeasure.Text = humanText;
          cocktailMeasure.Amount = measure/glassMeasures;

          left -= measure;

          cocktail.Measures.Add(cocktailMeasure);
        }
      }
    }
  }
  int turns = 0;

  Cocktail GetNextCocktail()
  {
    int id = 0;  // @TODO
    Debug.LogFormat("Turn {0}", id);
    switch(turns)
    {
      case 0: id = 0; break;  // Water
      case 1: id = 5; break;  // Water
      case 2: id = 3; break;  // Wine
      case 3: id = 5; break;  // Pint
      case 4: id = 6; break;  // Whiskey and Cola
      default:
        id = UnityEngine.Random.Range(0, Cocktails.Count - 1);
      break;
    }
    
    turns++;
    return Cocktails[id];
  }

  void SetupCocktail(Cocktail cocktail_)
  {
    Cocktail = cocktail_;
    SendMessage("GameStart");
    Glass.GlassInfo = GlassInfo.GetGlassType(Cocktail.Glass);
    Glass.Sum = 0.0f;
    
    if (GlassLiquid.AllowBeerFestBeers && Cocktail.Name == "Pint of Beer")
    {
      String[] tx =  GlassLiquid.BeerFestBeers[UnityEngine.Random.Range(0, GlassLiquid.BeerFestBeers.Length)].Split('-');

      OrderText.text = "Pint of " + tx[0].Trim();
      BreweryText.text = tx[1].Trim();
    }
    else
    {
      OrderText.text = Cocktail.Name;
      BreweryText.text = String.Empty;
    }
    SetupMeasures();

    Debug.LogFormat("Cocktail Setup: Glass = {0}, Name = {1}, GlassMeasures = {2}", Glass.GlassInfo.type, Cocktail.Name, Glass.GlassInfo.measures);
  }

  Ingredient FindIngredientUi(LiquidType lt)
  {
    for (int i = 0; i < ingredients.Count; i++)
    {
      if (ingredients[i].LiquidType == lt)
        return ingredients[i];
    }
    return null;
  }

  Ingredient MakeIngredientUi(LiquidType lt)
  {
    for (int i = 0; i < ingredients.Count; i++)
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
      ingredient.Won = IngredientWin.Ignore;
      ingredient.Disable();
    }

    for (int measuresIi = 0; measuresIi < Cocktail.Measures.Count; measuresIi++)
    {
      CocktailMeasure measure = Cocktail.Measures[measuresIi];
      Ingredient ingredient = FindIngredientUi(measure.LiquidType);

      if (ingredient == null)
      {
        ingredient = MakeIngredientUi(measure.LiquidType);
        if (ingredient == null)
          continue;
      }

      ingredient.DoSetup(measure.LiquidType, measure, Glass);
    }

    SetUpControls();
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
      p.y = ingredientY*-40;
      ingredient.transform.localPosition = p;
      ingredientY++;
      ingredient.Enable();

      if (ingredient.LiquidType == LiquidType.None)
      {
        ingredient.Disable();
      }
    }
  }

  public ControlMarker GetAlcholControl(int id)
  {
    ControlMarker ctrl = null;
    
    switch(id)
    {
      case 0: ctrl = Control_Action2; break;
      case 1: ctrl = Control_Action3; break;
      case 2: ctrl = Control_Action4; break;
    }

    return ctrl;
  }
  
  public bool RandomAddAlcholControl(LiquidType liquid, SolidType solid)
  {
    bool allUsed = true;

    for(int i=0;i < 3;i++)
    {
      ControlMarker ctrl = GetAlcholControl(i);
      if (ctrl.Liquid != LiquidType.None || ctrl.Solid != SolidType.None)
      {
        allUsed = false;
        break;
      }
    }

    if (allUsed == false)
      return false;

    int steps = 0;
    while(steps++ < 100)
    {
      int r = UnityEngine.Random.Range(0, 3);
      ControlMarker ctrl = GetAlcholControl(r);
      
      if (ctrl.Liquid != LiquidType.None || ctrl.Solid != SolidType.None)
      {
        continue;
      }

      SetAlcholControl(r, liquid, solid);
      return true;
    }
    
    return false;
  }

  public bool AddAlcholControl(LiquidType liquid, SolidType solid)
  {
    for(int i=0;i < 3;i++)
    {
      ControlMarker ctrl = GetAlcholControl(i);
      if (ctrl.Liquid == LiquidType.None && ctrl.Solid == SolidType.None)
      {
        SetAlcholControl(i, liquid, solid);
        return true;
      }
    }
    return false;
  }

  public bool SetAlcholControl(int id, LiquidType liquid, SolidType solid)
  {
    ControlMarker ctrl = GetAlcholControl(id);

    if (ctrl != null)
    {
      ctrl.Liquid = liquid;
      ctrl.Solid = solid;
      ctrl.ControlUpdated();

      ControlInfo info = null;

      switch(id)
      {
        case 0: info = Tap_Action2; break;
        case 1: info = Tap_Action3; break;
        case 2: info = Tap_Action4; break;
      }

      if (info != null)
      {
        info.Liquid = liquid;
        info.Solid  = solid;
      }

      return true;
    }

    return false;
  }
  
  public ControlMarker GetNotAlcholControl(int id)
  {
    ControlMarker ctrl = null;
    
    switch(id)
    {
      case 0: ctrl = Control_Up;    break;
      case 1: ctrl = Control_Right; break;
      case 2: ctrl = Control_Down;  break;
      case 3: ctrl = Control_Left;  break;
    }

    return ctrl;
  }
  
  public bool RandomAddNotAlcholControl(LiquidType liquid, SolidType solid)
  {
    bool allUsed = true;

    for(int i=0;i < 4;i++)
    {
      ControlMarker ctrl = GetNotAlcholControl(i);
      if (ctrl.Liquid != LiquidType.None || ctrl.Solid != SolidType.None)
      {
        allUsed = false;
        break;
      }
    }

    if (allUsed == false)
      return false;

    int steps = 0;
    while(steps++ < 100)
    {
      int r = UnityEngine.Random.Range(0, 4);
      ControlMarker ctrl = GetNotAlcholControl(r);
      
      if (ctrl.Liquid != LiquidType.None || ctrl.Solid != SolidType.None)
      {
        continue;
      }

      SetNotAlcholControl(r, liquid, solid);
      return true;
    }
    
    return false;
  }

  public bool AddNotAlcholControl(LiquidType liquid, SolidType solid)
  {
    for(int i=0;i < 3;i++)
    {
      ControlMarker ctrl = GetNotAlcholControl(i);
      if (ctrl.Liquid == LiquidType.None && ctrl.Solid == SolidType.None)
      {
        SetNotAlcholControl(i, liquid, solid);
        return true;
      }
    }
    return false;
  }

  public bool SetNotAlcholControl(int id, LiquidType liquid, SolidType solid)
  {
    ControlMarker ctrl = GetNotAlcholControl(id);

    if (ctrl != null)
    {
      ctrl.Liquid = liquid;
      ctrl.Solid = solid;
      ctrl.ControlUpdated();
      
      ControlInfo info = null;

      switch(id)
      {
        case 0: info = Tap_Up;    break;
        case 1: info = Tap_Right; break;
        case 2: info = Tap_Down;  break;
        case 3: info = Tap_Left;  break;
      }

      if (info != null)
      {
        info.Liquid = liquid;
        info.Solid  = solid;
      }


      return true;
    }

    return false;
  }

  public LiquidType GetUniqueRandomLiquid(List<LiquidType> used, LiquidAlcohol al)
  {
    int steps = 0;

    while(steps++ < 100)
    {
      int idx = UnityEngine.Random.Range(0, used.Count);
      LiquidType lt = used[idx];

      if (GlassLiquid.LiquidTypeToLiquidAlcohol(lt) != al || lt == LiquidType.BeerHead || lt == LiquidType.Beer)
        continue;
      
      used.Remove(lt);
      
      return lt;
    }

    return LiquidType.None;
  }

  public void SetUpControls()
  {
    int nbLiquids = (int) LiquidType.COUNT;

    List<SolidType>  solids   = new List<SolidType>(8);
    List<LiquidType> liquids = new List<LiquidType>(nbLiquids);
    for(int i=0;i < nbLiquids;i++)
      liquids.Add((LiquidType) i);

    liquids.Remove(LiquidType.None);
    
    SetAlcholControl(0, LiquidType.None, SolidType.None);
    SetAlcholControl(1, LiquidType.None, SolidType.None);
    SetAlcholControl(2, LiquidType.None, SolidType.None);
    
    SetNotAlcholControl(0, LiquidType.None, SolidType.None);
    SetNotAlcholControl(1, LiquidType.None, SolidType.None);
    SetNotAlcholControl(2, LiquidType.None, SolidType.None);
    SetNotAlcholControl(3, LiquidType.None, SolidType.None);
    
    for (int measuresIi = 0; measuresIi < Cocktail.Measures.Count; measuresIi++)
    {
      CocktailMeasure measure = Cocktail.Measures[measuresIi];
      
      SolidType  solid  = SolidType.None; // @TODO. Maybe part of measure?
      LiquidType liquid = LiquidType.None;
      
      liquid = measure.LiquidType;
      
      if (solid != SolidType.None)
      {
        solids.Add(solid);
      }

      if (liquid != LiquidType.None)
      {
        liquids.Remove(liquid);

        if (liquid == LiquidType.Beer)
          continue;
        
        LiquidAlcohol alcohol = GlassLiquid.LiquidTypeToLiquidAlcohol(liquid);

        if (alcohol == LiquidAlcohol.Yes)
        {
          if (RandomAddAlcholControl(liquid, solid) == false)
          {
            if (RandomAddNotAlcholControl(liquid, solid) == false)
            {
              Debug.LogError("Cannot fit in ingredients. Recipe too complicated");
            }
          }
        }
        else
        {
          
          if (RandomAddNotAlcholControl(liquid, solid) == false)
          {
            if (RandomAddAlcholControl(liquid, solid) == false)
            {
              Debug.LogError("Cannot fit in ingredients. Recipe too complicated");
            }
          }
        }

      }
    }

    // Actions
    for(int i=0;i < 3;i++)
    {
      ControlMarker ctrl = GetAlcholControl(i);
      if (!(ctrl.Liquid == LiquidType.None && ctrl.Solid == SolidType.None))
        continue;

      LiquidType lt = GetUniqueRandomLiquid(liquids, LiquidAlcohol.Yes);
      SetAlcholControl(i, lt, SolidType.None);
    }
    
    // DPad
    for(int i=0;i < 4;i++)
    {
      ControlMarker ctrl = GetNotAlcholControl(i);
      if (!(ctrl.Liquid == LiquidType.None && ctrl.Solid == SolidType.None))
        continue;

      LiquidType lt = GetUniqueRandomLiquid(liquids, LiquidAlcohol.No);
      SetNotAlcholControl(i, lt, SolidType.None);
    }
  }

}
