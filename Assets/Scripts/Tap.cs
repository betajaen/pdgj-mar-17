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
  
  bool CheckControls(bool update)
  {
    
    //const float flowRate = 0.005f;
    
    float flowRate = Glass.GlassInfo.flowRate;
    
    // BEER - Left Thumb Stick, Pull Down

    float beerTap = Mathf.Abs(Mathf.Min(0.0f, Controls.LeftStickY));
    
    if (beerTap > 0.0f)
    {
      float beerFlowRate = beerTap * 0.5f;
      float headFlowRate = beerTap * 0.25f;
      
      GlassLiquid beer = Pour(LiquidType.Beer,     flowRate * beerFlowRate);
      GlassLiquid head = Pour(LiquidType.BeerHead, flowRate * headFlowRate);
      
      update = true;
      Glass.BeerHeadTimer = 0.0f;
    }
    else
    {
      if (Unpour(LiquidType.Beer) || Unpour(LiquidType.BeerHead))
      {
        update = true;
      }
    }

    // Action and DPad buttons

    // Action2
    if (Controls.Action2Down && Game.Tap_Action2.Liquid != LiquidType.None)
    {
        Pour(Game.Tap_Action2.Liquid, flowRate);
        update = true;
    }
    else if (Game.Tap_Action2.Liquid != LiquidType.None)
    {
      if (Unpour(Game.Tap_Action2.Liquid))
      {
        update = true;
      }
    }
    else if (Controls.Action2 && Game.Tap_Action2.Solid != SolidType.None)
    {
      AddSolid(Game.Tap_Action2.Solid);
    }
    
    
    // Action3
    if (Controls.Action3Down && Game.Tap_Action3.Liquid != LiquidType.None)
    {
        Pour(Game.Tap_Action3.Liquid, flowRate);
        update = true;
    }
    else if (Game.Tap_Action3.Liquid != LiquidType.None)
    {
      if (Unpour(Game.Tap_Action3.Liquid))
      {
        update = true;
      }
    }
    else if (Controls.Action3 && Game.Tap_Action3.Solid != SolidType.None)
    {
      AddSolid(Game.Tap_Action3.Solid);
    }
    
    
    // Action4
    if (Controls.Action4Down && Game.Tap_Action4.Liquid != LiquidType.None)
    {
        Pour(Game.Tap_Action4.Liquid, flowRate);
        update = true;
    }
    else if (Game.Tap_Action4.Liquid != LiquidType.None)
    {
      if (Unpour(Game.Tap_Action4.Liquid))
      {
        update = true;
      }
    }
    else if (Controls.Action4 && Game.Tap_Action4.Solid != SolidType.None)
    {
      AddSolid(Game.Tap_Action4.Solid);
    }
    
    // Up
    if (Controls.DPadUpDown && Game.Tap_Up.Liquid != LiquidType.None)
    {
        Pour(Game.Tap_Up.Liquid, flowRate);
        update = true;
    }
    else if (Game.Tap_Up.Liquid != LiquidType.None)
    {
      if (Unpour(Game.Tap_Up.Liquid))
      {
        update = true;
      }
    }
    else if (Controls.DPadUp && Game.Tap_Up.Solid != SolidType.None)
    {
      AddSolid(Game.Tap_Up.Solid);
    }
    
    // Right
    if (Controls.DPadRightDown && Game.Tap_Right.Liquid != LiquidType.None)
    {
        Pour(Game.Tap_Right.Liquid, flowRate);
        update = true;
    }
    else if (Game.Tap_Right.Liquid != LiquidType.None)
    {
      if (Unpour(Game.Tap_Right.Liquid))
      {
        update = true;
      }
    }
    else if (Controls.DPadRight && Game.Tap_Right.Solid != SolidType.None)
    {
      AddSolid(Game.Tap_Right.Solid);
    }
    
    // Down
    if (Controls.DPadDownDown && Game.Tap_Down.Liquid != LiquidType.None)
    {
        Pour(Game.Tap_Down.Liquid, flowRate);
        update = true;
    }
    else if (Game.Tap_Down.Liquid != LiquidType.None)
    {
      if (Unpour(Game.Tap_Down.Liquid))
      {
        update = true;
      }
    }
    else if (Controls.DPadDown && Game.Tap_Down.Solid != SolidType.None)
    {
      AddSolid(Game.Tap_Down.Solid);
    }
    
    // Left
    if (Controls.DPadLeftDown && Game.Tap_Left.Liquid != LiquidType.None)
    {
        Pour(Game.Tap_Left.Liquid, flowRate);
        update = true;
    }
    else if (Game.Tap_Left.Liquid != LiquidType.None)
    {
      if (Unpour(Game.Tap_Left.Liquid))
      {
        update = true;
      }
    }
    else if (Controls.DPadLeft && Game.Tap_Left.Solid != SolidType.None)
    {
      AddSolid(Game.Tap_Left.Solid);
    }

    return update;
  }

  private void AddSolid(SolidType solid)
  {
  }

  void Update()
  {
    switch(Game.State)
    {
      case GameState.Menu:
        break;
      case GameState.Pour:
        UpdatePour();
        break;
      case GameState.Submit:
        break;
      case GameState.Loose:
        break;
    }
  }

  void UpdatePour()
  {
    bool update = CheckControls(false);

    foreach (var pour in pours)
    {
      if (pour.deleted)
        continue;

      bool doPour = false;
      bool doDelete = false;
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
          t = pour.downTime/maxTime;
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
        t = pour.upTime/maxTime;
        pour.liquid.pourSize = 1 - t;
        doPour = true;
      }

      if (doPour)
      {
        t = t*t*(3f - 2f*t);

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

  GlassLiquid Pour(LiquidType lt, float pourSize)
  {
    GlassLiquid glassLiquid = null;
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

      glassLiquid = null;

      foreach (var liquid in Glass.liquids)
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

        GlassLiquid topLiquid = Glass.GetTopLiquid();

        bool hasHead = topLiquid != null && topLiquid.type == LiquidType.BeerHead;
        bool isHead = (lt == LiquidType.BeerHead);

        if (isHead)
        {
          glassLiquid.animate = true;
          Glass.liquids.Add(glassLiquid);
          Glass.BeerHead = glassLiquid;
          Glass.BeerHeadTimer = 0.0f;

          Debug.Log("Beer Head Add");
        }
        else if (hasHead || Glass.Sum >= 1.0f)
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

      pour.downTime = 0.0f;
      pour.isDown = true;
      pour.pourRate = pourSize;
    }

    return glassLiquid;
  }

  bool Unpour(LiquidType lt)
  {
    foreach (var pour in pours)
    {
      if (pour.liquidType == lt)
      {
        pour.liquid.pouring = false;
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
    foreach (var l in Glass.liquids)
    {
      lastLiquidType = l.type;
    }

    return lastLiquidType == lt;
  }

  
  public void GameStart()
  {
    foreach (var pour in pours)
    {
      pour.deleted = true;
      pour.isDown = false;
      pour.liquid = null;
      pour.downTime = 0.0f;
      pour.upTime = 0.0f;
      pour.liquidType = LiquidType.None;
    }
  }

}
