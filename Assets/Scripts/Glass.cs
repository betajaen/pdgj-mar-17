//#define OLD_DRAWING_MODE

using System;
using System.Collections.Generic;
using UnityEngine;

public enum LiquidAlcohol
{
  Yes,
  No
}

public enum LiquidType
{
  None,
  Water,
  Beer,
  Vodka,
  Whiskey,
  Cola,
  Lemonade,
  Wine,
  BeerHead,

  Vermouth,
  GingerBeer,
  OrangeJuice,
  CranberryJuice,
  Liqueur,
  IrishCream,
  Schnapps,
  Rum,
  SodaWater,
  FruitCup,
  TonicWater,
  Gin,
  Tequila,

  COUNT

// vermouth
// ginger beer
// orange juice
// cranberry juice
// liqueur
// irish cream
// schnapps
// rum
// sodawater
// fruitcup
// tonic water
// gin
// tequila

}

public enum SolidType
{
  None,
  Ice,
  MixedFruit,
  Lime
}

[System.Serializable]
public class GlassSolid
{
  public SolidType type  = SolidType.Ice;

  public static object SolidTypeToString(SolidType solid)
  {
    switch(solid)
    {
      default:
      case SolidType.None:        return "?";
      case SolidType.Ice:         return "Ice";
      case SolidType.MixedFruit:  return "Fruit";
      case SolidType.Lime:        return "Lime";
    }
  }
}

[System.Serializable]
public class GlassLiquid
{
  public static bool AllowBeerFestBeers = true;
  public static bool AllowBeerFestWines = false;

  public static String[] BeerFestBeers =
  {
    "Jack Sound - Little Dragon Brewery", "M.V. Enterprise - Tenby Harbour Brewery", "Dark Heart - The Mantle Brewery", "Son of a Beach - Tenby Brewing Co", "Kings Ale - Rhymney Brewery", "Welsh Beacons - Beacon Brewing", "Blodwen - Gwaun Valley Brewery", "Juicey - Tiny Rebel Brewery", "Sleck Dust - Great Newsome Brewery", "Madog's Ale - Purple Moose", "Farne Island - Hadrian Border Brewery", "Hammer Stone - Bluestone Brewery", "Golden Ale - St Peters Brewery", "Old Thumper - Ringwood Brewery", "Saddle Tank - Marsdens Brewery"
  };

  public static String[] BeerFestRedWines =
  {
    "Red Wine"
  };

  public static LiquidType LiquidStringToEnum(string s)
  {
    switch (s.ToUpper())
    {
      default:
        return LiquidType.Water;
      case "WATER":
        return LiquidType.Water;
      case "BEER":
        return LiquidType.Beer;
      case "VODKA":
        return LiquidType.Vodka;
      case "WHISKEY":
        return LiquidType.Whiskey;
      case "COLA":
        return LiquidType.Cola;
      case "LEMONADE":
        return LiquidType.Lemonade;
      case "WINE":
        return LiquidType.Wine;

      case "VERMOUTH":
        return LiquidType.Vermouth;
      case "GINGERBEER":
        return LiquidType.GingerBeer;
      case "ORANGE":
        return LiquidType.OrangeJuice;
      case "CRANBERRY":
        return LiquidType.CranberryJuice;
      case "LIQUEUR":
        return LiquidType.Liqueur;
      case "IRISHCREAM":
        return LiquidType.IrishCream;
      case "SCHNAPPS":
        return LiquidType.Schnapps;
      case "RUM":
        return LiquidType.Rum;
      case "SODAWATER":
        return LiquidType.SodaWater;
      case "FRUITCUP":
        return LiquidType.FruitCup;
      case "TONICWATER":
        return LiquidType.TonicWater;
      case "GIN":
        return LiquidType.Gin;
      case "TEQUILA":
        return LiquidType.Tequila;
    }
  }
    public static LiquidAlcohol LiquidTypeToLiquidAlcohol(LiquidType type)
  {
    switch (type)
    {
      default:
      case LiquidType.Water:
        return LiquidAlcohol.No;
      case LiquidType.Beer:
        return LiquidAlcohol.Yes;
      case LiquidType.Vodka:
        return LiquidAlcohol.Yes;
      case LiquidType.Whiskey:
        return LiquidAlcohol.Yes;
      case LiquidType.Cola:
        return LiquidAlcohol.No;
      case LiquidType.Lemonade:
        return LiquidAlcohol.No;
      case LiquidType.Wine:
        return LiquidAlcohol.Yes;

      case LiquidType.Vermouth:
        return LiquidAlcohol.Yes;
      case LiquidType.GingerBeer:
        return LiquidAlcohol.No;
      case LiquidType.OrangeJuice:
        return LiquidAlcohol.No;
      case LiquidType.CranberryJuice:
        return LiquidAlcohol.No;
      case LiquidType.Liqueur:
        return LiquidAlcohol.Yes;
      case LiquidType.IrishCream:
        return LiquidAlcohol.Yes;
      case LiquidType.Schnapps:
        return LiquidAlcohol.Yes;
      case LiquidType.Rum:
        return LiquidAlcohol.Yes;
      case LiquidType.SodaWater:
        return LiquidAlcohol.No;
      case LiquidType.FruitCup:
        return LiquidAlcohol.Yes;
      case LiquidType.TonicWater:
        return LiquidAlcohol.No;
      case LiquidType.Gin:
        return LiquidAlcohol.Yes;
      case LiquidType.Tequila:
        return LiquidAlcohol.Yes;
    }
  }
  public static String LiquidTypeToString(LiquidType lt)
  {
    switch (lt)
    {
      case LiquidType.Water:
        return "Water";
      case LiquidType.Beer:
        return "Beer";
      case LiquidType.Vodka:
        return "Vodka";
      case LiquidType.Whiskey:
        return "Whiskey";
      case LiquidType.Cola:
        return "Cola";
      case LiquidType.Lemonade:
        return "Lemonade";
      case LiquidType.Wine:
        return "Wine";

      case LiquidType.Vermouth:
        return "Vermouth";
      case LiquidType.GingerBeer:
        return "Ginger Beer";
      case LiquidType.OrangeJuice:
        return "Orange Juice";
      case LiquidType.CranberryJuice:
        return "Cranberry Juice";
      case LiquidType.Liqueur:
        return "Liqueur";
      case LiquidType.IrishCream:
        return "Irish Cream";
      case LiquidType.Schnapps:
        return "Schnapps";
      case LiquidType.Rum:
        return "Rum";
      case LiquidType.SodaWater:
        return "Soda Water";
      case LiquidType.FruitCup:
        return "Fruit Cup";
      case LiquidType.TonicWater:
        return "Tonic Water";
      case LiquidType.Gin:
        return "Gin";
      case LiquidType.Tequila:
        return "Tequila";
    }
    return "?";
  }

  public static String __PintShotGlassMl(float mlF, String name, bool fullGlass, GlassType gt)
  {
    int ml = (int) mlF;
    int mlRounded = (int) ((mlF)/25.0f)*25;

    String text = String.Empty; // mlF.ToString() + ",";

    if (fullGlass && (gt == GlassType.Pint)) text += String.Format("Pint of {0}", name);
    else if (fullGlass && (gt == GlassType.HalfPint)) text += String.Format("Half Pint of {0}", name);
    else if (fullGlass) text += String.Format("Glass of {0}", name);
    else if (ml <= 6) text += String.Format("Drop of {0}", name);
    else if (mlF < 25) text += String.Format("{0} Tsp of {1}", (int) (ml/6.0f), name);
    else if (ml <= 25) text += String.Format("Half a Shot of {0}", name);
    else if (ml <= 50) text += String.Format("Shot of {0}", name);
    else if (ml == 100) text += String.Format("2 Shots of {0}", name);
    else text += String.Format("{0}ml of {1}", mlRounded, name);

    return text;
  }

  public static String __PintHalfGlassMl(float mlF, String name, bool fullGlass, GlassType gt)
  {
    int ml = (int) mlF;
    int mlRounded = MlToScoringMeasures(mlF);

    String text = String.Empty; // mlF.ToString() + ",";

    if (fullGlass && (gt == GlassType.Pint)) text += String.Format("Pint of {0}", name);
    else if (fullGlass && (gt == GlassType.HalfPint)) text += String.Format("Half Pint of {0}", name);
    else if (fullGlass) text += String.Format("Glass of {0}", name);
    else if (ml <= 6) text += String.Format("Drop of {0}", name);
    else if (mlF < 25) text += String.Format("{0} Tsp of {1}", (int) (ml/6.0f), name);
    else text += String.Format("{0}ml of {1}", mlRounded, name);

    return text;
  }

  public static String LiquidTypeAndMeasureToStringFloatAmount(float amount, LiquidType lt, GlassType gt)
  {
    return LiquidTypeAndMlToString(AmountToMeasures(amount, gt)*25, lt, gt);
  }

  public static float AmountToMeasures(float amount, GlassType gt)
  {
    return (GetGlassSizeInMeasures(gt)*amount);
  }

  public static float AmountToScoringMeasures(float amount, GlassType gt)
  {
    return Mathf.CeilToInt(GetGlassSizeInMeasures(gt)*amount/25.0f)*25;
  }

  public static int MlToScoringMeasures(float ml)
  {
    return (int) ((ml)/25.0f)*25;
  }

  public static String MakeUnit(int ml, int glassSizeMl, LiquidType lt)
  {
    switch (lt)
    {
      case LiquidType.Water:
      case LiquidType.Beer:
      case LiquidType.Cola:
      case LiquidType.Lemonade:
      case LiquidType.Wine:
      {
        /*
          if (ml <= 6)
            return "1 Tsp";
          if (ml <= 12)
            return "2 Tsp";
          if (ml <= 18)
            return "3 Tsp";
          if (ml <= 24)
            return "4 Tsp";
        */
        return String.Format("{0} ml", ml);
      }
        break;
      case LiquidType.Vodka:
      case LiquidType.Whiskey:
      {
        if (ml <= 6)
          return "1 Tsp";
        if (ml <= 12)
          return "2 Tsp";
        if (ml <= 18)
          return "3 Tsp";
        if (ml <= 24)
          return "4 Tsp";
        if (ml <= 25 + 12)
          return "1 Shot";
        if (ml <= 50 + 12)
          return "2 Shots";
        return String.Format("{0} ml", ml);
      }
        break;
    }
    return String.Format("{0} ml", ml);
  }

  public static bool MlApprox(int x, int y, int diff = 20)
  {
    return Mathf.Abs(y - x) <= diff;
  }

  public static String LiquidTypeAndMlWithMeasureToString(float amount, float measureAmount, LiquidType lt, GlassType gt)
  {
    float glassSizeMlF = GetGlassSizeInMeasures(gt)*25;
    int glassSizeMl = Mathf.CeilToInt(glassSizeMlF);
    int liquidMl = Mathf.RoundToInt(amount*glassSizeMlF);
    int targetMl = Mathf.RoundToInt(measureAmount*glassSizeMlF);
    string name = LiquidTypeToString(lt);

    switch (lt)
    {
      case LiquidType.Water:
      case LiquidType.Beer:
      case LiquidType.Cola:
      case LiquidType.Lemonade:
      case LiquidType.Wine:
      {
        if (MlApprox(targetMl, glassSizeMl))
        {
          if (MlApprox(liquidMl, targetMl))
          {
            return String.Format("Full Glass of {0}", name);
          }
          else
          {
            return String.Format("{0}/Glass of {1}", MakeUnit(liquidMl, glassSizeMl, lt), name);
          }
        }
        else
        {
          return String.Format("{0}/{1} of {2}", MakeUnit(liquidMl, glassSizeMl, lt), MakeUnit(targetMl, glassSizeMl, lt), name);
        }
      }
      case LiquidType.Vodka:
      case LiquidType.Whiskey:
      {
        if (MlApprox(targetMl, glassSizeMl))
        {
          if (MlApprox(liquidMl, targetMl))
          {
            return String.Format("Full Glass of {0}", name);
          }
          else
          {
            return String.Format("{0}/Glass of {1}", MakeUnit(liquidMl, glassSizeMl, lt), name);
          }
        }
        else
        {
          return String.Format("{0}/{1} of {2}", MakeUnit(liquidMl, glassSizeMl, lt), MakeUnit(targetMl, glassSizeMl, lt), name);
        }
      }
        break;
    }

    return String.Format("{0}ml/{1}ml of {2}", liquidMl, targetMl, name);
  }

  public static String LiquidTypeAndMlToString(float ml, LiquidType lt, GlassType gt)
  {
    float glassSizeMlF = GetGlassSizeInMeasures(gt)*25;
    int glassSizeMl = Mathf.CeilToInt(glassSizeMlF);
    int liquidMl = Mathf.RoundToInt(ml);
    string name = LiquidTypeToString(lt);

    if (MlApprox(liquidMl, glassSizeMl))
    {
      return String.Format("Glass of {0}", name);
    }
    else if (MlApprox(liquidMl, glassSizeMl/2))
    {
      return String.Format("Half a glass of {0}", name);
    }
    else
    {
      return String.Format("{0} of {1}", MakeUnit(liquidMl, glassSizeMl, lt), name);
    }
  }

  public Ingredient ingredient;

  [SerializeField] float _amount;

  public float amount
  {
    get { return _amount; }

    set
    {
      _amount = value;

      if (ingredient != null)
      {
        ingredient.RefreshNeeded = true;
      }
    }
  }


  // Relative to glass height
  [SerializeField] public float[] visualWeights = new float[17]; // weights.

  [SerializeField] public LiquidType type;
  [SerializeField] public float animationTimer;
  [SerializeField] public bool animate;
  [SerializeField] public bool animatingWeights;
  [SerializeField] public bool pouring;
  [SerializeField] public float pourSize;
  [SerializeField] public float centerX0, centerX1, centerY, centerY1;
  [SerializeField] public float idleAnimator;
  [SerializeField] public GlassLiquid head;
  [SerializeField] public GlassLiquid headLiquid;

  static byte kAlpha = 200;
  static byte kAlpha2 = 180;
  static byte kAlphaHead = 240;

  static Color kColour_None = new Color32(1, 1, 1, kAlpha);
  static Color kColour_Water = new Color32(150, 220, 225, kAlpha);
  static Color kColour_WaterHead = new Color32(150 + 20, 220 + 20, 225 + 20, kAlpha);
  static Color kColour_Beer = new Color32(216, 96, 0, kAlpha);
  static Color kColour_BeerHead = new Color32(240, 240, 240, kAlpha);
  static Color kColour_BeerHeadHead = new Color32(250, 250, 250, kAlpha);
  static Color kColour_Vodka = new Color32(220, 220, 220, kAlpha);
  static Color kColour_VodkaHead = new Color32(220 + 10, 220 + 10, 220 + 10, kAlphaHead);
  static Color kColour_Whiskey = new Color32(213, 154, 111, kAlpha);
  static Color kColour_WhiskeyHead = new Color32(213 + 20, 154 + 20, 111 + 20, kAlpha);
  static Color kColour_Cola = new Color32(62, 48, 36, kAlpha);
  static Color kColour_ColaHead = new Color32(62 + 20, 48 + 20, 36 + 20, kAlphaHead);
  static Color kColour_Lemonade = new Color32(252, 253, 245, kAlpha2);
  static Color kColour_LemonadeHead = new Color32(252, 253, 245, kAlpha);
  static Color kColour_Wine = new Color32(182, 39, 66, kAlpha);
  static Color kColour_WineHead = new Color32(182 + 20, 39 + 20, 66 + 20, kAlpha);

  public static Color GetColour(LiquidType t)
  {
    switch (t)
    {
      default:
        return kColour_None;
      case LiquidType.Water:
        return kColour_Water;
      case LiquidType.Beer:
        return kColour_Beer;
      case LiquidType.Vodka:
        return kColour_Vodka;
      case LiquidType.Whiskey:
        return kColour_Whiskey;
      case LiquidType.Cola:
        return kColour_Cola;
      case LiquidType.Lemonade:
        return kColour_Lemonade;
      case LiquidType.Wine:
        return kColour_Wine;
      case LiquidType.BeerHead:
        return kColour_BeerHead;
    }
  }

  public static Color GetColourHead(LiquidType t)
  {
    switch (t)
    {
      default:
        return kColour_None;
      case LiquidType.Water:
        return kColour_WaterHead;
      case LiquidType.Beer:
        return kColour_BeerHead;
      case LiquidType.Vodka:
        return kColour_VodkaHead;
      case LiquidType.Whiskey:
        return kColour_WhiskeyHead;
      case LiquidType.Cola:
        return kColour_ColaHead;
      case LiquidType.Lemonade:
        return kColour_LemonadeHead;
      case LiquidType.Wine:
        return kColour_WineHead;
      case LiquidType.BeerHead:
        return kColour_BeerHeadHead;
    }
  }

  public const int kPint = 23; // 575.0f/kMeasureMl; // 23 measures
  public const int kHalfPint = 11; // 275.0f/kMeasureMl; // 11 measures
  public const int kWineGlass = 4; // 100.0f/kMeasureMl; // 4 measures
  public const int kMeasure = 1; // 25.0f/kMeasureMl;  // 1 measure
  public const int kDoubleMeasure = 2; // 50.0f/kMeasureMl;  // 2 measure

  public static int GetGlassSizeInMeasures(GlassType gt)
  {
    switch (gt)
    {
      default:
      case GlassType.None:
        return 1;
      case GlassType.Pint:
        return 23;
      case GlassType.HalfPint:
        return 11;
      case GlassType.Highball:
        return 10;
      case GlassType.Cocktail:
        return 10;
      case GlassType.Shot:
        return 3;
      case GlassType.Wine:
        return 10;
    }
  }

  // A 'measure' is considered 25ml, all liquid types are variants of this; including pints.
  public static int GetMeasure(LiquidType t)
  {
    switch (t)
    {
      default:
      case LiquidType.None:
        return 0;
      case LiquidType.Water:
        return kWineGlass;
      case LiquidType.Beer:
        return kPint;
      case LiquidType.Vodka:
        return kMeasure;
      case LiquidType.Whiskey:
        return kMeasure;
      case LiquidType.Cola:
        return kHalfPint;
      case LiquidType.Lemonade:
        return kHalfPint;
      case LiquidType.Wine:
        return kWineGlass;
    }
    return 1;
  }

  public static GlassType GlassStringToEnum(string s)
  {
    switch (s.ToUpper())
    {
      default:
        return GlassType.Pint;
      case "PINT":
        return GlassType.Pint;
      case "HALF":
        return GlassType.HalfPint;
      case "HIGHBALL":
        return GlassType.Highball;
      case "COCKTAIL":
        return GlassType.Cocktail;
      case "SHOT":
        return GlassType.Shot;
      case "WINE":
        return GlassType.Wine;
    }
  }
}

public enum GlassType
{
  None, // 0
  Pint, // 24 measures ->  PINT      -> Beer
  HalfPint, // 12 measures ->  HALF      -> Beer, Lemonade, Cola
  Highball, // 4  measures ->  HIGHBALL  -> Water, Cocktails
  Cocktail, // 4  measures ->  COCKTAIL  -> Cocktails
  Shot, // 2  measures ->  SHOT      -> Vodka, Whiskey
  Wine, // 4  measures ->  WINE      -> Wine
}

public class GlassInfo
{
  public GlassType type;
  public float width, height, halfWidth, halfHeight;
  public float measures, flowRate;

  public GlassInfo(GlassType type_, float width_, float height_, float measures_, float flowRate_)
  {
    type = type_;
    width = width_;
    height = height_;
    measures = measures_;
    flowRate = flowRate_;

    halfWidth = width*0.5f;
    halfHeight = height*0.5f;
  }

  public static GlassInfo GetGlassType(GlassType type_)
  {
    switch (type_)
    {
      default:
      case GlassType.None:
        return kNone;
      case GlassType.Pint:
        return kPint;
      case GlassType.HalfPint:
        return kHalfPint;
      case GlassType.Highball:
        return kCocktail_Highball;
      case GlassType.Cocktail:
        return kCocktail_Cocktail;
      case GlassType.Shot:
        return kShot;
      case GlassType.Wine:
        return kWine;
    }
  }

  public static GlassInfo kNone = new GlassInfo(GlassType.None, 1.0f, 1.0f, 1, 0.005f);
  public static GlassInfo kPint = new GlassInfo(GlassType.Pint, 2.0f, 4.0f, GlassLiquid.kPint + 1, 0.0075f);
  public static GlassInfo kHalfPint = new GlassInfo(GlassType.HalfPint, 2.0f, 2.0f, GlassLiquid.kHalfPint + 1, 0.005f);
  public static GlassInfo kCocktail_Highball = new GlassInfo(GlassType.Highball, 1.75f, 3.2f, GlassLiquid.kWineGlass, 0.005f);
  public static GlassInfo kCocktail_Cocktail = new GlassInfo(GlassType.Cocktail, 1.0f, 1.0f, GlassLiquid.kWineGlass, 0.005f);
  public static GlassInfo kShot = new GlassInfo(GlassType.Shot, 0.65f, 0.95f, GlassLiquid.kDoubleMeasure, 0.035f);
  public static GlassInfo kWine = new GlassInfo(GlassType.Wine, 2.0f, 2.0f, GlassLiquid.kWineGlass, 0.02f);
}


[RequireComponent(typeof (MeshRenderer))]
[RequireComponent(typeof (MeshFilter))]
public class Glass : MonoBehaviour
{
  public Mesh mesh;
  public MeshRenderer mr;
  public MeshFilter mf;
  public Transform tr;
  public List<GlassLiquid> liquids;
  public List<Vector3> vertices;
  public List<Color> colours;
  public List<int> indexes;
  public bool updateNeeded = false;
  public bool liquidsOrderChanged = false;
  int idx;
  float z;

  class Polyline
  {
    public List<Vector3> points;

    public Polyline(Vector3 a, Vector3 b, params Vector3[] p)
    {
      points = new List<Vector3>(2 + p.Length);
      points.Add(a);
      points.Add(b);
      foreach (var pt in p)
      {
        points.Add(pt);
      }
    }
  }

  List<Polyline> GlassShape = new List<Polyline>();
  List<Vector4> GlassCollider = new List<Vector4>();

  static Color kColour_Glass = new Color32(255, 255, 255, 100);

  public GlassInfo glassInfo;
  public float glassWidth = 0.1f;

  public float Sum = 0;

  public GlassInfo GlassInfo;

  public GlassType GlassType
  {
    get { return GlassInfo.type; }
  }

  public float width
  {
    get { return GlassInfo.width; }
  }

  public float height
  {
    get { return GlassInfo.height; }
  }

  public float halfWidth
  {
    get { return GlassInfo.halfWidth; }
  }

  public float halfHeight
  {
    get { return GlassInfo.halfHeight; }
  }

  public GlassLiquid BeerHead;
  public float BeerHeadTimer;

//  GlassLiquid beerHead;
//
//  public GlassLiquid BeerHead
//  {
//    get
//    { 
//      if (beerHead != null && beerHead.type == LiquidType.None)
//        return null;
//      
//      return beerHead; 
//    }
//    set
//    {
//      beerHead = value;
//    }
//  }

  Glass()
  {
    GlassInfo = GlassInfo.kNone;
  }

  void Start()
  {
    mr = GetComponent<MeshRenderer>();
    mf = GetComponent<MeshFilter>();
    tr = GetComponent<Transform>();

    if (liquids == null)
    {
      liquids = new List<GlassLiquid>(10);
    }

    mesh = new Mesh();
    mf.sharedMesh = mesh;
    mesh.MarkDynamic();

    vertices = new List<Vector3>(100*3);
    colours = new List<Color>(100*3);
    indexes = new List<int>(100*3);

    updateNeeded = true;
    liquidsOrderChanged = true;

    GlassInfo = GlassInfo.kPint;
  }

  void Update()
  {
    if (updateNeeded)
    {
      updateNeeded = false;
      Draw();
    }
  }

  void Quad(Vector3 tl, Vector3 br, out Vector3 a, out Vector3 b, out Vector3 c, out Vector3 d)
  {
    /*
        A ------ B
        |    /   |
        |   /    |
        |  /     |
        C--------D
     */

    a = new Vector3(tl.x, -tl.y, z);
    b = new Vector3(br.x, -tl.y, z);
    c = new Vector3(tl.x, -br.y, z);
    d = new Vector3(br.x, -br.y, z);
  }

  void PushQuad(Vector3 tl, Vector3 br, Color col, float shearT = 0.0f, float shearB = 0.0f)
  {
    Vector3 a, b, c, d;
    Quad(tl, br, out a, out b, out c, out d);
    Shear(shearT, shearB, ref a, ref b, ref c, ref d);

    vertices.Add(a);
    vertices.Add(b);
    vertices.Add(c);
    vertices.Add(d);

    colours.Add(col);
    colours.Add(col);
    colours.Add(col);
    colours.Add(col);

    indexes.Add(idx + 0);
    indexes.Add(idx + 1);
    indexes.Add(idx + 2);

    indexes.Add(idx + 2);
    indexes.Add(idx + 1);
    indexes.Add(idx + 3);

    indexes.Add(idx + 0);
    indexes.Add(idx + 2);
    indexes.Add(idx + 1);

    indexes.Add(idx + 2);
    indexes.Add(idx + 3);
    indexes.Add(idx + 1);

    idx += 4;
  }

  void PushQuad(Vector3 a, Vector3 b, Vector3 c, Vector3 d, Color col)
  {
    vertices.Add(a);
    vertices.Add(b);
    vertices.Add(c);
    vertices.Add(d);

    colours.Add(col);
    colours.Add(col);
    colours.Add(col);
    colours.Add(col);

    indexes.Add(idx + 0);
    indexes.Add(idx + 1);
    indexes.Add(idx + 2);

    indexes.Add(idx + 2);
    indexes.Add(idx + 1);
    indexes.Add(idx + 3);

    indexes.Add(idx + 0);
    indexes.Add(idx + 2);
    indexes.Add(idx + 1);

    indexes.Add(idx + 2);
    indexes.Add(idx + 3);
    indexes.Add(idx + 1);

    idx += 4;
  }

  void PushQuad2(Vector3 tl, Vector3 br, Color col, float T0, float T1, float B0, float B1)
  {
    Vector3 a, b, c, d;
    Quad(tl, br, out a, out b, out c, out d);
    Shear(T0, T1, B0, B1, ref a, ref b, ref c, ref d);

    vertices.Add(a);
    vertices.Add(b);
    vertices.Add(c);
    vertices.Add(d);

    colours.Add(col);
    colours.Add(col);
    colours.Add(col);
    colours.Add(col);

    indexes.Add(idx + 0);
    indexes.Add(idx + 1);
    indexes.Add(idx + 2);

    indexes.Add(idx + 2);
    indexes.Add(idx + 1);
    indexes.Add(idx + 3);

    indexes.Add(idx + 0);
    indexes.Add(idx + 2);
    indexes.Add(idx + 1);

    indexes.Add(idx + 2);
    indexes.Add(idx + 3);
    indexes.Add(idx + 1);

    idx += 4;
  }

  void PushFadeDownQuad(Vector3 tl, Vector3 br, Color col, float shearT = 0.0f, float shearB = 0.0f)
  {
    Vector3 a, b, c, d;
    Quad(tl, br, out a, out b, out c, out d);
    Shear(shearT, shearB, ref a, ref b, ref c, ref d);

    vertices.Add(a);
    vertices.Add(b);
    vertices.Add(c);
    vertices.Add(d);

    Color fade = new Color(col.r, col.g, col.b, 0.0f);

    colours.Add(col);
    colours.Add(col);
    colours.Add(fade);
    colours.Add(fade);

    indexes.Add(idx + 0);
    indexes.Add(idx + 1);
    indexes.Add(idx + 2);

    indexes.Add(idx + 2);
    indexes.Add(idx + 1);
    indexes.Add(idx + 3);

    indexes.Add(idx + 0);
    indexes.Add(idx + 2);
    indexes.Add(idx + 1);

    indexes.Add(idx + 2);
    indexes.Add(idx + 3);
    indexes.Add(idx + 1);

    idx += 4;
  }

  void PushTriangle(Vector3 a, Vector3 b, Vector3 c, Color col)
  {
    a.y = -a.y;
    b.y = -b.y;
    c.y = -c.y;

    vertices.Add(a);
    vertices.Add(b);
    vertices.Add(c);

    colours.Add(col);
    colours.Add(col);
    colours.Add(col);

    indexes.Add(idx + 0);
    indexes.Add(idx + 1);
    indexes.Add(idx + 2);

    indexes.Add(idx + 0);
    indexes.Add(idx + 2);
    indexes.Add(idx + 1);

    idx += 3;
  }

  void PushTriangleNoTrans(Vector3 a, Vector3 b, Vector3 c, Color col)
  {
    vertices.Add(a);
    vertices.Add(b);
    vertices.Add(c);

    colours.Add(col);
    colours.Add(col);
    colours.Add(col);

    indexes.Add(idx + 0);
    indexes.Add(idx + 1);
    indexes.Add(idx + 2);

    indexes.Add(idx + 0);
    indexes.Add(idx + 2);
    indexes.Add(idx + 1);

    idx += 3;
  }

  void PushTriangle2(Vector3 a, Vector3 b, Vector3 c, Color colA, Color colB, Color colC)
  {
    a.y = -a.y;
    b.y = -b.y;
    c.y = -c.y;

    vertices.Add(a);
    vertices.Add(b);
    vertices.Add(c);

    colours.Add(colA);
    colours.Add(colB);
    colours.Add(colC);

    indexes.Add(idx + 0);
    indexes.Add(idx + 1);
    indexes.Add(idx + 2);

    indexes.Add(idx + 0);
    indexes.Add(idx + 2);
    indexes.Add(idx + 1);

    idx += 3;
  }

  static void Shear(float T, float B, ref Vector3 a, ref Vector3 b, ref Vector3 c, ref Vector3 d)
  {
    a.x += T;
    b.x += T;
    c.x += B;
    d.x += B;
  }

  static void Shear(float T0, float T1, float B0, float B1, ref Vector3 a, ref Vector3 b, ref Vector3 c, ref Vector3 d)
  {
    a.x += T0;
    b.x += T1;
    c.x += B0;
    d.x += B1;
  }

  void AddGlass()
  {
#if OLD_DRAWING_MODE
    switch (GlassType)
    {
      default:
      {
        // Left
        PushQuad(new Vector3(-halfWidth, -glassWidth*2.0f), new Vector3(-halfWidth + glassWidth, height - glassWidth), kColour_Glass, -0.0f, 0.0f);
        // Right
        PushQuad(new Vector3(halfWidth - glassWidth, -glassWidth*2.0f), new Vector3(halfWidth, height - glassWidth), kColour_Glass, 0.0f, 0.0f);
        // Bottom
        PushQuad(new Vector3(-halfWidth, height - glassWidth), new Vector3(halfWidth, height), kColour_Glass, 0.0f, 0.0f);
      }
      break;
      case GlassType.Shot:
      {
        // Left
        PushQuad2(new Vector3(-halfWidth, -glassWidth*2.0f), new Vector3(-halfWidth + glassWidth, height - glassWidth), kColour_Glass, -0.1f, 0.0f, 0.0f, 0.0f);
        // Right
        PushQuad2(new Vector3(halfWidth - glassWidth, -glassWidth*2.0f), new Vector3(halfWidth, height - glassWidth), kColour_Glass, 0.0f, 0.1f, 0.0f, 0.0f);
        // Bottom
        PushQuad(new Vector3(-halfWidth, height - glassWidth), new Vector3(halfWidth, height), kColour_Glass, 0.0f, 0.0f);
      }
      break;
      case GlassType.Pint:
      {
        // Left
        PushQuad(new Vector3(-halfWidth, -glassWidth*2.0f), new Vector3(-halfWidth + glassWidth, height - glassWidth), kColour_Glass, -0.5f, 0.0f);
        // Right
        PushQuad(new Vector3(halfWidth - glassWidth, -glassWidth*2.0f), new Vector3(halfWidth, height - glassWidth), kColour_Glass, 0.5f, 0.0f);
        // Bottom
        PushQuad(new Vector3(-halfWidth, height - glassWidth), new Vector3(halfWidth, height), kColour_Glass, 0.0f, 0.0f);
      }
      break;
    }
    #else
    DrawGlassShape();
#endif
  }

  void AddLiquids()
  {
    float hgw = glassWidth*0.5f;
    float gwAmount = glassWidth*(1.0f/height);

    if (liquids != null)
    {
      float y0 = height - hgw;

      float amountY0 = 0.0f;

      for (int liquidIi = 0; liquidIi < liquids.Count; liquidIi++)
      {
        var liquid = liquids[liquidIi];
        bool isTop = (liquidIi == liquids.Count - 1);

        float amountY1 = amountY0 + liquid.amount;

        float y1 = y0 - (height*liquid.amount);

        Color col = GlassLiquid.GetColour(liquid.type);
        Color colHead = GlassLiquid.GetColourHead(liquid.type);

        #region Animation

        if (liquid.animate)
        {
          const int nbTriangles = 17;
          const float tSpeed = 10.0f;
          const float offset = 0.05f;

          liquid.animatingWeights = true;
          {
            float n = liquid.visualWeights[0];
            float tx = tSpeed*n*Time.deltaTime;
            n -= tx;
            if (n < 0.01f)
              n = 0.0f;
            liquid.visualWeights[0] = n;
          }

          {
            float n = liquid.visualWeights[nbTriangles - 1];
            float tx = tSpeed*Time.deltaTime;
            n -= tx;
            if (n < 0.01f)
              n = 0.0f;
            liquid.visualWeights[nbTriangles - 1] = n;
          }

          for (int i = 0; i < 8; i++)
          {
            float n = liquid.visualWeights[i];
            int other = i + 1;

            float m = liquid.visualWeights[other];
            float tx = tSpeed*m*Time.deltaTime;

            m -= tx;
            if (m < 0.01f)
              m = 0.0f;

            n += tx;
            if (n > 1.0f)
              n = 1.0f;

            liquid.visualWeights[i] = n;
            liquid.visualWeights[other] = m;
          }

          for (int i = 16; i > 8; i--)
          {
            float n = liquid.visualWeights[i];
            int other = i - 1;

            float m = liquid.visualWeights[other];
            float tx = tSpeed*m*Time.deltaTime;

            m -= tx;
            if (m < 0.01f)
              m = 0.0f;

            n += tx;
            if (n > 1.0f)
              n = 1.0f;

            liquid.visualWeights[i] = n;
            liquid.visualWeights[other] = m;
          }

          if (Mathf.Approximately(liquid.pourSize, 0))
          {
            float sumWeights = 0.0f;

            for (int i = 0; i < 17; i++)
            {
              sumWeights += liquid.visualWeights[i];
            }

            if (sumWeights < 0.05f)
            {
              liquid.idleAnimator += Time.deltaTime;
            }
            else
            {
              liquid.idleAnimator = 0.0f;
            }

            if (liquid.idleAnimator > 0.50f)
            {
              liquid.animate = false;
            }
          }

#if OLD_DRAWING_MODE
          PushQuad(new Vector3(-halfWidth + glassWidth, y0), new Vector3(halfWidth - glassWidth, y1), GlassLiquid.GetColour(liquid.type));
  #else
          //float xx = FindGlassInner(halfWidth, liquid.amount);
          //PushQuad(new Vector3(-xx * width, y0), new Vector3(halfWidth - glassWidth * 0.5f, y1), GlassLiquid.GetColour(liquid.type));


          float xx0 = Mathf.Abs(FindGlassX(amountY0))*width;
          float xx1 = Mathf.Abs(FindGlassX(amountY1 - gwAmount))*width;
          // PushQuad(new Vector3(-yy1 + glassWidth * 0.5f, y0), new Vector3(yy1 - glassWidth * 0.5f, y1), GlassLiquid.GetColour(liquid.type));

          PushQuad(new Vector3(-xx1 + hgw, -y1), new Vector3(+xx1 - hgw, -y1), new Vector3(-xx0 + hgw, -y0), new Vector3(+xx0 - hgw, -y0), GlassLiquid.GetColour(liquid.type));

#endif

          float triangleWidth = ((xx1*2.0f) - (hgw*2.0f))/nbTriangles;

          liquid.animationTimer += Time.deltaTime;
          updateNeeded = true;

          float h = 0.0f; //-liquid.visualWeights[0]*0.125f;
          float ly = y1 + h;

          float lastOffset = offset;

          // PushQuad(new Vector3(-xx1 + hgw, y1 - 0.05f), new Vector3(xx1 - hgw, y1 - 0.10f), Color.green);

          for (int i = 0; i < nbTriangles; i++)
          {
            float tx0 = -xx1 + hgw + (i*triangleWidth);
            float tx1 = tx0 + triangleWidth;
            float ty = y1;

            h = -liquid.visualWeights[i]*0.125f;

            PushTriangle2(new Vector3(tx0, ly), new Vector3(tx1, y1), new Vector3(tx0, y1), col, col, col);
            PushTriangle2(new Vector3(tx0, ly), new Vector3(tx1, ty + h), new Vector3(tx1, y1), col, col, col);

            if (i == 8)
            {
              liquid.centerY = ty + (h - offset);
            }

            PushTriangle(new Vector3(tx0, ly - lastOffset), new Vector3(tx1, ty + (h - offset)), new Vector3(tx0, ly), colHead);
            PushTriangle(new Vector3(tx0, ly), new Vector3(tx1, ty + h), new Vector3(tx1, ty + (h - offset)), colHead);

            ly = ty + h;
          }
        }
          #endregion

        else
        {
          float xx0 = Mathf.Abs(FindGlassX(amountY0))*width;
          float xx1 = Mathf.Abs(FindGlassX(amountY1))*width;

          liquid.centerY = y1;

#if OLD_DRAWING_MODE
            PushQuad(new Vector3(-halfWidth + glassWidth, y0), new Vector3(halfWidth - glassWidth, y1), col);
          #else
          PushQuad(new Vector3(-xx1 + hgw, -y1), new Vector3(+xx1 - hgw, -y1), new Vector3(-xx0 + hgw, -y0), new Vector3(+xx0 - hgw, -y0), GlassLiquid.GetColour(liquid.type));
#endif

          if (isTop)
          {
#if OLD_DRAWING_MODE
              PushQuad(new Vector3(-halfWidth, y1), new Vector3(halfWidth, y1 - 0.05f), colHead);
            #else
            PushQuad(new Vector3(-xx1 + hgw, y1), new Vector3(xx1 - hgw, y1 - 0.05f), colHead);
#endif
          }
        }

        // Pouring Liquid
        if (Game.State == GameState.Pour && liquid.type != LiquidType.BeerHead && liquid.pourSize > 0)
        {
          int nbSplashes = 10;

          float hh = Mathf.Lerp(-height, liquid.centerY, 1.0f - liquid.pourSize);
          float ww = Mathf.Lerp(0.25f, 0.0f, 1.0f - liquid.pourSize);

          float splashWidth = (ww)/nbSplashes;
          float x0 = -ww*0.5f;

          float yy = (liquid.centerY) + 0.5f;

          if (yy > height - glassWidth)
          {
            yy = height - glassWidth;
          }

          for (int i = 0; i < nbSplashes; i++)
          {
            Color c = col;
            if (Mathf.Tan(liquid.animationTimer*2.0f + (i*233.3292f)) >= 0.0f)
              c = colHead;

            PushQuad(new Vector3(x0, hh), new Vector3(x0 + splashWidth, liquid.centerY), c);
            PushFadeDownQuad(new Vector3(x0, liquid.centerY), new Vector3(x0 + splashWidth, yy), c);
            x0 += splashWidth;
          }
        }

        y0 = y1;
        amountY0 = amountY1;
      }
    }
  }

  void Draw()
  {
    idx = 0;

    vertices.Clear();
    indexes.Clear();
    colours.Clear();

    if (GlassShape == null)
    {
      GlassShape = new List<Polyline>(4);
      UpdateGlassShape();
    }
    else if (GlassShape.Count == 0)
    {
      UpdateGlassShape();
    }

    z = 0.0f;
    AddLiquids();
    z = -1.0f;
    AddGlass();

    RefreshMesh();

    tr.localPosition = new Vector3(0.0f, halfHeight*100.0f);
  }

  void RefreshMesh()
  {
    mesh.Clear();
    mesh.SetVertices(vertices);
    mesh.SetColors(colours);
    mesh.SetTriangles(indexes, 0);
    mesh.RecalculateBounds();
  }

  public GlassLiquid GetTopLiquid()
  {
    if (liquids == null)
      return null;

    if (liquids.Count == 0)
      return null;

    return liquids[liquids.Count - 1];
  }

  public GlassLiquid FindLiquid(LiquidType lt)
  {
    for (int liquidIi = 0; liquidIi < liquids.Count; liquidIi++)
    {
      var liquid = liquids[liquidIi];
      if (liquid.type == lt)
        return liquid;
    }
    return null;
  }

  public void DeleteLiquid(GlassLiquid liquid)
  {
    if (liquid.ingredient != null)
    {
      liquid.ingredient.Liquid = null;
      if (liquid.ingredient.Measure == null)
      {
        liquid.ingredient.LiquidType = LiquidType.None;
        liquid.ingredient.Disable();
      }
    }

    liquids.Remove(liquid);
    liquidsOrderChanged = true;
  }

  public void GameStart()
  {
    foreach (var liquid in liquids)
    {
      if (liquid.ingredient != null)
      {
        liquid.ingredient.Liquid = null;
      }
      liquid.type = LiquidType.None;
    }

    if (GlassShape == null)
    {
      GlassShape = new List<Polyline>(4);
    }
    else
    {
      GlassShape.Clear();
    }

    Sum = 0.0f;
    BeerHead = null;
    BeerHeadTimer = 0.0f;
    liquids.Clear();
    updateNeeded = true;
  }

  void UpdateGlassShape()
  {
    GlassShape.Clear();
    GlassCollider.Clear();

    switch (GlassType)
    {
      case GlassType.None:
        break;
      case GlassType.Pint:
      {
        GlassCollider.Add(new Vector4(-0.5f, 0, -0.65f, 1.1f));

        GlassShape.Add(new Polyline(new Vector3(-0.65f, 1.1f), new Vector3(-0.5f, 0), new Vector3(+0.5f, 0), new Vector3(+0.65f, 1.1f)));
      }
        break;
      case GlassType.HalfPint:
      {
        GlassCollider.Add(new Vector4(-0.5f, 0, -0.75f, 1.1f));

        GlassShape.Add(new Polyline(new Vector3(-0.75f, 1.1f), new Vector3(-0.5f, 0), new Vector3(+0.5f, 0), new Vector3(+0.75f, 1.1f)));
      }
        break;
      case GlassType.Highball:
      {
        GlassCollider.Add(new Vector4(-0.5f, 0, -0.5f, 1.1f));

        GlassShape.Add(new Polyline(new Vector3(-0.5f, 1.1f), new Vector3(-0.5f, 0), new Vector3(+0.5f, 0), new Vector3(+0.5f, 1.1f)));
      }
        break;
      case GlassType.Cocktail:
        GlassCollider.Add(new Vector4(-0.5f, 0, -0.75f, 1.1f));

        GlassShape.Add(new Polyline(new Vector3(-0.75f, 1.1f), new Vector3(-0.5f, 0), new Vector3(+0.5f, 0), new Vector3(+0.75f, 1.1f)));
        break;
      case GlassType.Shot:
        GlassCollider.Add(new Vector4(-0.5f, 0, -0.75f, 1.1f));

        GlassShape.Add(new Polyline(new Vector3(-0.75f, 1.1f), new Vector3(-0.5f, 0), new Vector3(+0.5f, 0), new Vector3(+0.75f, 1.1f)));
        break;
      case GlassType.Wine:
        GlassCollider.Add(new Vector4(-0.5f, 0, -0.75f, 1.1f));

        GlassShape.Add(new Polyline(new Vector3(-0.75f, 1.1f), new Vector3(-0.5f, 0), new Vector3(+0.5f, 0), new Vector3(+0.75f, 1.1f)));

        GlassShape.Add(new Polyline(new Vector3(0.0f, 0.0f), new Vector3(0.0f, -1.0f)));

        GlassShape.Add(new Polyline(new Vector3(-0.45f, -1.0f), new Vector3(+0.45f, -1.0f)));
        break;
    }
  }

  void DrawGlassShape()
  {
    for (int lineIi = 0; lineIi < GlassShape.Count; lineIi++)
    {
      Polyline ln = GlassShape[lineIi];
      DrawPolyLine(ln, glassWidth, kColour_Glass);
    }
  }

  void DrawPolyLine(Polyline ln, float thickness, Color col)
  {
    thickness *= 0.5f;

    float x1 = ln.points[0].x, y1 = ln.points[0].y, x2, y2;

    x1 = (x1*width);
    y1 = (y1*height) - height;

    PushQuad(new Vector3(x1 - thickness, y1 - thickness), new Vector3(x1 + thickness, y1 - thickness), new Vector3(x1 - thickness, y1 + thickness), new Vector3(x1 + thickness, y1 + thickness), kColour_Glass);

    x1 = ln.points[0].x;
    y1 = ln.points[0].y;

    for (int i = 1; i < ln.points.Count; i++)
    {
      x2 = ln.points[i].x;
      y2 = ln.points[i].y;

      x1 = (x1*width);
      y1 = (y1*height) - height;
      x2 = (x2*width);
      y2 = (y2*height) - height;

      float angle = Mathf.Atan2(y2 - y1, x2 - x1);
      float t2sina1 = thickness*Mathf.Sin(angle);
      float t2cosa1 = thickness*Mathf.Cos(angle);
      float t2sina2 = thickness*Mathf.Sin(angle);
      float t2cosa2 = thickness*Mathf.Cos(angle);

      PushTriangleNoTrans(new Vector3(x1 + t2sina1, y1 - t2cosa1), new Vector3(x2 + t2sina2, y2 - t2cosa2), new Vector3(x2 - t2sina2, y2 + t2cosa2), col);
      PushTriangleNoTrans(new Vector3(x2 - t2sina2, y2 + t2cosa2), new Vector3(x1 - t2sina1, y1 + t2cosa1), new Vector3(x1 + t2sina1, y1 - t2cosa1), col);

      PushQuad(new Vector3(x2 - thickness, y2 - thickness), new Vector3(x2 + thickness, y2 - thickness), new Vector3(x2 - thickness, y2 + thickness), new Vector3(x2 + thickness, y2 + thickness), kColour_Glass);

      x1 = ln.points[i].x;
      y1 = ln.points[i].y;
    }
  }

  float FindGlassX(float y)
  {
    Vector4 ln = GlassCollider[0];
    Vector3 a = new Vector3(ln.x, ln.y), b = new Vector3(ln.z, ln.w);
    Vector3 dir = (b - a).normalized;
    Vector3 up = new Vector3(0, 1, 0);

    float angle = (90.0f - (Vector3.Dot(dir, up)*Mathf.Rad2Deg))*Mathf.Deg2Rad;

    float t = y/Mathf.Cos(angle);

    Vector3 pt = a + dir*t;

    return pt.x;
  }
}
