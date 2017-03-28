using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Pour
{
  [SerializeField]
  public GlassLiquid liquid;

  [SerializeField]
  public LiquidType  liquidType;

  [SerializeField]
  public float       pourRate;

  [SerializeField]
  public bool        isDown;

  [SerializeField]
  public float       downTime;

  [SerializeField]
  public float       upTime;

  [SerializeField]
  public bool        deleted;
}

public class Tap : MonoBehaviour 
{
  public Glass Glass;

  public List<Pour> pours; 

  void Start()
  {
    pours = new List<Pour>(8);
  }

  Pour GetNewPour()
  {
    Pour pour = null;

    if (pours != null && pours.Count > 0)
    {
      foreach(var p in pours)
      {
        if (p.deleted)
        {
          pour = p;
          pour.deleted = false;
          break;
        }
      }
    }

    if (pour == null)
    {
      pour = new Pour();
      pours.Add(pour);
    }

    return pour;
  }

  void DeletePour(Pour pour)
  {
    pour.deleted = true;
    pour.isDown = false;
    pour.liquid = null;
    pour.downTime = 0.0f;
    pour.upTime = 0.0f;
    pour.liquidType = LiquidType.None;
  }

  void Update()
  {
    const float flowRate = 0.005f;

    bool update = false;

    if (Controls.Action1Down)
    {
      Pour(LiquidType.Cola, flowRate);
      update = true;
    }
    else
    {
      if (Unpour(LiquidType.Cola))
      {
        update = true;
      }
    }
    
    if (Controls.Action2Down)
    {
      Pour(LiquidType.Vodka, flowRate);
      update = true;
    }
    else
    {
      if (Unpour(LiquidType.Vodka))
      {
        update = true;
      }
    }
    
    if (Controls.Action3Down)
    {
      Pour(LiquidType.Whiskey, flowRate);
      update = true;
    }
    else
    {
      if (Unpour(LiquidType.Whiskey))
      {
        update = true;
      }
    }
    
    if (Controls.LeftBumperDown)
    {
      Pour(LiquidType.Water, flowRate);
      update = true;
    }
    else
    {
      if (Unpour(LiquidType.Water))
      {
        update = true;
      }
    }
    
    if (Controls.RightBumperDown)
    {
      Pour(LiquidType.Lemonade, flowRate);
      update = true;
    }
    else
    {
      if (Unpour(LiquidType.Lemonade))
      {
        update = true;
      }
    }
    
    if (Controls.DPadUpDown)
    {
      Pour(LiquidType.Beer, flowRate);
      update = true;
    }
    else
    {
      if (Unpour(LiquidType.Beer))
      {
        update = true;
      }
    }
    foreach(var pour in pours)
    {
      if (pour.deleted)
        continue;
      
      bool  doPour = false;
      bool  doDelete = false;
      float pour0 = 0, pour1 = 0, t = 0;

      if (pour.isDown)
      {
        const float maxTime = 0.5f;
        const float minTime = 0.25f;
        
        if (pour.downTime >= minTime)
        {
          if (pour.downTime >= maxTime)
          {
            pour.downTime = maxTime;
          }
          
          pour0 = 0.0f;
          pour1 = pour.pourRate;
          t = pour.downTime / maxTime;
          doPour = true;
          pour.liquid.visualWeights[7] = t;
          pour.liquid.visualWeights[8] = t;
          pour.liquid.pouring = true;
          pour.liquid.pourSize = 1.0f;

          if (IsTop(pour.liquidType))
          {
            pour.liquid.animate = true;
          }

        }
      }
      else
      {
        const float maxTime = 0.5f;
        
        if (pour.upTime >= maxTime)
        {
          doDelete = true;
          pour.upTime = maxTime;
        }
        
        pour0 = pour.pourRate;
        pour1 = 0.0f;
        t = pour.upTime / maxTime;
        pour.liquid.pourSize = 1 - t;
        doPour = true;
      }
      
      if (doPour)
      {
        t = t*t * (3f - 2f*t);

        float pourAmount = Mathf.Lerp(pour0, pour1, t);

        update = true;
        pour.liquid.amount += pourAmount;
      }

      if (doDelete)
      {
        DeletePour(pour);
      }

    }
    
    if (update)
    {
      Glass.updateNeeded = true;
    }
  }

  bool Unpour(LiquidType lt)
  {
    foreach(var pour in pours)
    {
      if (pour.liquidType == lt)
      {
        pour.liquid.pouring = false; // Debug.Log("Pouring False");
        pour.isDown = false;
        pour.upTime += Time.deltaTime;
        return true;
      }
    }

    return false;
  }

  bool IsTop(LiquidType lt)
  {
    LiquidType lastLiquidType = LiquidType.None;
    foreach(var l in Glass.liquids)
    {
      lastLiquidType = l.type;
    } 

    return lastLiquidType == lt;
  }

  void Pour(LiquidType lt, float pourSize)
  {
    Pour pour = null;

    pourSize *= 0.35f; // Difficulty modifier

    for (int ii = 0; ii < pours.Count; ii++)
    {
      var p = pours[ii];
      if (p.liquidType == lt)
      {
        pour = p;
        break;
      }
    }

    if (pour != null)
    {
      pour.isDown = true;
      pour.downTime += Time.deltaTime;
      pour.pourRate = pourSize;
    }
    else
    {
      pour = GetNewPour();
      pour.liquidType = lt;
        
      GlassLiquid glassLiquid = null;
      foreach(var liquid in Glass.liquids)
      {
        if (liquid.type == lt)
        {
          glassLiquid = liquid;
          break;
        }
      }

      if (glassLiquid == null)
      {
        for (int ii = 0; ii < Glass.liquids.Count; ii++)
        {
          var liquid = Glass.liquids[ii];
          liquid.animate = false;
        }

        glassLiquid = new GlassLiquid();
        glassLiquid.amount = 0.0f;
        glassLiquid.type = lt;
        glassLiquid.animationTimer = Time.time;
        Glass.liquidsOrderChanged = true;
        pour.liquid = glassLiquid;

        if (Glass.Sum >= 1.0f)
        {
          glassLiquid.animate = false;
          Glass.liquids.Insert(0, glassLiquid);
        }
        else
        {
          glassLiquid.animate = true;
          Glass.liquids.Add(glassLiquid);
        }
      }
      else
      {
        pour.liquid = glassLiquid;
      }
      
      pour.downTime   = 0.0f;
      pour.isDown     = true;
      pour.pourRate   = pourSize;
    }
  }
  
}
