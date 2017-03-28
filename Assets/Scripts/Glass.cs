using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum LiquidType
{
  None,
  Water,
  Beer,
  Vodka,
  Whiskey,
  Cola,
  Lemonade,
  Wine
}

[System.Serializable]
public class GlassLiquid
{
  const int   kMeasureMlInt = 25;
  const float kMeasureMl = 25.0f;


  public static LiquidType LiquidStringToEnum(string s)
  {
    switch(s.ToUpper())
    {
      default:            return LiquidType.Water;
      case "WATER":       return LiquidType.Water;
      case "BEER":        return LiquidType.Beer;
      case "VODKA":       return LiquidType.Vodka;
      case "WHISKEY":     return LiquidType.Whiskey;
      case "COLA":        return LiquidType.Cola;
      case "LEMONADE":    return LiquidType.Lemonade;
      case "WINE":        return LiquidType.Wine;
    }
  }

  public static String __PintShotGlassMl(float mlF, String name, bool fullGlass, GlassType gt)
  {
    int ml = (int) mlF;
    int mlRounded = (int) ((mlF) / 25.0f) * 25;

    String text = String.Empty; // mlF.ToString() + ",";

    if (fullGlass && (gt == GlassType.Pint))              text += String.Format("Pint of {0}", name);
    else if (fullGlass && (gt == GlassType.HalfPint))     text += String.Format("Half Pint of {0}", name);
    else if (fullGlass)                                   text += String.Format("Glass of {0}", name);
    else if (ml <= 6)                                     text += String.Format("Drop of {0}", name);
    else if (mlF < 25)                                    text += String.Format("{0} Tsp of {1}", (int) (ml / 6.0f), name);
    else if (ml <= 25)                                    text += String.Format("Half a Shot of {0}", name);
    else if (ml <= 50)                                    text += String.Format("Shot of {0}", name);
    else if (ml == 100)                                   text += String.Format("2 Shots of {0}", name);
    else                                                  text += String.Format("{0}ml of {1}", mlRounded, name);

    return text;
  }
  
  public static String __PintHalfGlassMl(float mlF, String name, bool fullGlass, GlassType gt)
  {
    int ml = (int) mlF;
    int mlRounded = (int) ((mlF) / 25.0f) * 25;

    String text = String.Empty; // mlF.ToString() + ",";

    if (fullGlass && (gt == GlassType.Pint))              text += String.Format("Pint of {0}", name);
    else if (fullGlass && (gt == GlassType.HalfPint))     text += String.Format("Half Pint of {0}", name);
    else if (fullGlass)                                   text += String.Format("Glass of {0}", name);
    else if (ml <= 6)                                     text += String.Format("Drop of {0}", name);
    else if (mlF < 25)                                    text += String.Format("{0} Tsp of {1}", (int) (ml / 6.0f), name);
    else                                                  text += String.Format("{0}ml of {1}", mlRounded, name);

    return text;
  }
  public static String LiquidTypeAndMeasureToStringFloatAmount(float amount, LiquidType lt, GlassType gt)
  {
    return LiquidTypeAndMlToString(AmountToMeasures(amount, gt) * 25, lt, gt);
  }

  public static float  AmountToMeasures(float amount, GlassType gt)
  {
    return (GetGlassSizeInMeasures(gt) * amount);
  }

  public static String LiquidTypeAndMlToString(float ml, LiquidType lt, GlassType gt)
  {
    float glassSize  = GetGlassSizeInMeasures(gt) * 25;
    bool fullGlass   = Mathf.Approximately(ml, glassSize);
    switch(lt)
    {
      default:
      case LiquidType.None:
        return String.Format("{0}ml of Unknown", ml);
      case LiquidType.Water:
        return __PintHalfGlassMl(ml, "Water", fullGlass, gt);
      case LiquidType.Beer:
        return __PintHalfGlassMl(ml, "Beer", fullGlass, gt);
      case LiquidType.Vodka:
        return __PintShotGlassMl(ml, "Vodka", fullGlass, gt);
      case LiquidType.Whiskey:
        return __PintShotGlassMl(ml, "Whiskey", fullGlass, gt);
      case LiquidType.Cola:
        return __PintShotGlassMl(ml, "Cola", fullGlass, gt);
      case LiquidType.Lemonade:
        return __PintShotGlassMl(ml, "Lemonade", fullGlass, gt);
      case LiquidType.Wine:
        return __PintShotGlassMl(ml, "Wine", fullGlass, gt);
    }
  }

  public Ingredient ingredient;

  [SerializeField] 
  float _amount;

  public float amount
  {
    get 
    {
      return _amount;
    }

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

  static byte kAlpha = 200;
  static byte kAlphaHead = 240;

  static Color kColour_None = new Color32(1, 1, 1, kAlpha);
  static Color kColour_Water = new Color32(150, 220, 225, kAlpha);
  static Color kColour_WaterHead = new Color32(150 + 20, 220 + 20, 225 + 20, kAlpha);
  static Color kColour_Beer = new Color32(10, 10, 10, kAlpha);
  static Color kColour_Vodka = new Color32(200, 200, 200, kAlpha);
  static Color kColour_VodkaHead = new Color32(200 + 20, 200 + 20, 200 + 20, kAlphaHead);
  static Color kColour_Whiskey = new Color32(213, 154, 111, kAlpha);
  static Color kColour_WhiskeyHead = new Color32(213 + 20, 154 + 20, 111 + 20, kAlpha);
  static Color kColour_Cola = new Color32(62, 48, 36, kAlpha);
  static Color kColour_ColaHead = new Color32(62 + 20, 48 + 20, 36 + 20, kAlphaHead);
  static Color kColour_Lemonade = new Color32(240, 240, 255, kAlpha);
  static Color kColour_Wine = new Color32(1, 1, 1, kAlpha);

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
        return kColour_Beer;
      case LiquidType.Vodka:
        return kColour_VodkaHead;
      case LiquidType.Whiskey:
        return kColour_WhiskeyHead;
      case LiquidType.Cola:
        return kColour_ColaHead;
      case LiquidType.Lemonade:
        return kColour_Lemonade;
      case LiquidType.Wine:
        return kColour_Wine;
    }
  }

  public const int kPint            = 23; // 575.0f/kMeasureMl; // 23 measures
  public const int kHalfPint        = 11; // 275.0f/kMeasureMl; // 11 measures
  public const int kWineGlass       = 4;  // 100.0f/kMeasureMl; // 4 measures
  public const int kMeasure         = 1;  // 25.0f/kMeasureMl;  // 1 measure
  public const int kDoubleMeasure   = 2;  // 50.0f/kMeasureMl;  // 2 measure

  public static int GetGlassSizeInMeasures(GlassType gt)
  {
    switch(gt)
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
  None,               // 0
  Pint,               // 24 measures ->  PINT      -> Beer
  HalfPint,           // 12 measures ->  HALF      -> Beer, Lemonade, Cola
  Highball,           // 4  measures ->  HIGHBALL  -> Water, Cocktails
  Cocktail,           // 4  measures ->  COCKTAIL  -> Cocktails
  Shot,               // 2  measures ->  SHOT      -> Vodka, Whiskey
  Wine,               // 4  measures ->  WINE      -> Wine
}

public class GlassInfo
{
  public GlassType type;
  public float width, height, halfWidth, halfHeight;
  public float measures;

  public GlassInfo(GlassType type_, float width_, float height_, float measures_)
  {
    type = type_;
    width = width_;
    height = height_;
    measures = measures_;

    halfWidth  = width * 0.5f;
    halfHeight = height * 0.5f;
  }
  
  public static GlassInfo GetGlassType(GlassType type_)
  {
    switch(type_)
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
  
  public static GlassInfo kNone               = new GlassInfo(GlassType.None,     1.0f, 1.0f, 1);
  public static GlassInfo kPint               = new GlassInfo(GlassType.Pint,     2.0f, 4.0f, GlassLiquid.kPint + 1);
  public static GlassInfo kHalfPint           = new GlassInfo(GlassType.HalfPint, 2.0f, 2.0f, GlassLiquid.kHalfPint + 1);
  public static GlassInfo kCocktail_Highball  = new GlassInfo(GlassType.Highball, 1.75f, 3.2f, GlassLiquid.kWineGlass);
  public static GlassInfo kCocktail_Cocktail  = new GlassInfo(GlassType.Cocktail, 1.0f, 1.0f, GlassLiquid.kWineGlass);
  public static GlassInfo kShot               = new GlassInfo(GlassType.Shot,     0.5f, 0.5f, GlassLiquid.kDoubleMeasure);
  public static GlassInfo kWine               = new GlassInfo(GlassType.Wine,     2.0f, 1.0f, GlassLiquid.kWineGlass);
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

  static Color kColour_Glass = new Color32(255, 255, 255, 255);

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

  void Shear(float T, float B, ref Vector3 a, ref Vector3 b, ref Vector3 c, ref Vector3 d)
  {
    a.x += T;
    b.x += T;
    c.x += B;
    d.x += B;
  }

  void AddGlass()
  {
    switch(GlassType)
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
  }

  void AddLiquids()
  {
    if (liquids != null)
    {
      float y0 = height - glassWidth;

      for (int liquidIi = 0; liquidIi < liquids.Count; liquidIi++)
      {
        var liquid = liquids[liquidIi];
        bool isTop = (liquidIi == liquids.Count - 1);

        float y1 = y0 - (height*liquid.amount);

        Color col = GlassLiquid.GetColour(liquid.type);
        Color colHead = GlassLiquid.GetColourHead(liquid.type);

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

          PushQuad(new Vector3(-halfWidth + glassWidth, y0), new Vector3(halfWidth - glassWidth, y1), GlassLiquid.GetColour(liquid.type));

          float triangleWidth = (width - glassWidth*2.0f)/nbTriangles;

          liquid.animationTimer += Time.deltaTime;
          updateNeeded = true;

          float h = 0.0f; //-liquid.visualWeights[0]*0.125f;
          float ly = y1 + h;

          float lastOffset = offset;

          for (int i = 0; i < nbTriangles; i++)
          {
            float tx0 = -halfWidth + glassWidth + (i*triangleWidth);
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
        else
        {
          liquid.centerY = y1;
          PushQuad(new Vector3(-halfWidth + glassWidth, y0), new Vector3(halfWidth - glassWidth, y1), col);
          if (isTop)
          {
            PushQuad(new Vector3(-halfWidth + glassWidth, y1), new Vector3(halfWidth - glassWidth, y1 - 0.05f), colHead);
          }
        }

        if (liquid.pourSize > 0)
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
      }
    }
  }

  void Draw()
  {
    idx = 0;

    vertices.Clear();
    indexes.Clear();
    colours.Clear();

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
}
