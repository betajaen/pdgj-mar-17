using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ControlName
{
  A,
  B,
  X,
  Y,
  Up,
  Down,
  Left,
  Right,
  ThumbLeftX,
  ThumbLeftY,
  ThumbLeftNegY,
  ThumbLeftXY
}

public class ControlMarker : MonoBehaviour
{

  public ControlName ControlName;
  public Text        Text;
  public LiquidType  Liquid;
  public SolidType   Solid;
  
  Sprite  On, Off;
  bool    wasDown, isDown;
  float   wasX, X, wasY, Y;
  float   cx, cy, dcx, dcy;
  float   ww, hh;
  Image   image;
  Transform tr;

  void Awake()
  {
   image = GetComponent<Image>();
   tr = GetComponent<Transform>();
   Vector3 p = tr.localPosition;
   cx = p.x;
   cy = p.y;

   ww = 34;
   hh = 34;
  }

  void Start ()
  {
    String img = "A";
    wasDown = false;
    isDown = false;

    switch(ControlName)
    {
      case ControlName.A:
        img = "A";
        break;
      case ControlName.B:
        img = "B";
        break;
      case ControlName.X:
        img = "X";
        break;
      case ControlName.Y:
        img = "Y";
        break;
      case ControlName.Up:
        img = "Up";
        break;
      case ControlName.Down:
        img = "Down";
        break;
      case ControlName.Left:
        img = "Left";
        break;
      case ControlName.Right:
        img = "Right";
        break;
      case ControlName.ThumbLeftX:
      case ControlName.ThumbLeftY:
      case ControlName.ThumbLeftNegY:
      case ControlName.ThumbLeftXY:
        img = "Thumb";
        break;
    }

    On  = Resources.Load<Sprite>(String.Format("Control_{0}_Down", img));
    Off = Resources.Load<Sprite>(String.Format("Control_{0}", img));
  }

  void Update()
  {
    wasDown = isDown;
    wasX = X;
    wasY = Y;

    switch(ControlName)
    {
      default:
      case ControlName.A:
        isDown = Controls.Action1Down;
        break;
      case ControlName.B:
        isDown = Controls.Action2Down;
        break;
      case ControlName.X:
        isDown = Controls.Action3Down;
        break;
      case ControlName.Y:
        isDown = Controls.Action4Down;
        break;
      case ControlName.Up:
        isDown = Controls.DPadUpDown;
        break;
      case ControlName.Down:
        isDown = Controls.DPadDown;
        break;
      case ControlName.Left:
        isDown = Controls.DPadLeft;
        break;
      case ControlName.Right:
        isDown = Controls.DPadRight;
        break;
      case ControlName.ThumbLeftX:
        X = Controls.LeftStickX;
        break;
      case ControlName.ThumbLeftY:
        Y = Controls.LeftStickY;
        break;
      case ControlName.ThumbLeftNegY:
        Y = Mathf.Min(0.0f, Controls.LeftStickY);
        break;
      case ControlName.ThumbLeftXY:
        X = Controls.LeftStickX;
        Y = Controls.LeftStickY;
        break;
    }
    
    if (wasDown != isDown)
    {
      if (isDown)
        image.sprite = On;
      else
        image.sprite = Off;
    }

    if (Mathf.Approximately(X, wasX) == false || Mathf.Approximately(Y, wasY) == false)
    {
       Vector3 p = new Vector3(cx + X * ww, cy + Y * hh);
       tr.localPosition = p;

       if (image.sprite == Off)
        image.sprite = On;
    }
    
    if (ControlName == ControlName.ThumbLeftX ||  ControlName == ControlName.ThumbLeftNegY || ControlName == ControlName.ThumbLeftY || ControlName == ControlName.ThumbLeftXY)
    {
      if (Mathf.Approximately(X, 0.0f) == false || Mathf.Approximately(Y, 0.0f) == false)
      {
         if (image.sprite == Off)
          image.sprite = On;
      }
      else
      {
         if (image.sprite == On)
          image.sprite = Off;
      }
    }

  }

  public void ControlUpdated()
  {
    if (Text == null)
    {
      Text = gameObject.GetComponentInChildren<Text>();
    }

    if (Text != null)
    {
      if (Liquid != LiquidType.None)
      {
        Text.text = GlassLiquid.LiquidTypeToString(Liquid);
      }
      else if (Solid != SolidType.None)
      {
        Text.text = "Add " + GlassSolid.SolidTypeToString(Solid);
      }
      else
      {
        Text.text = String.Empty;
      }
    }

  }

}
