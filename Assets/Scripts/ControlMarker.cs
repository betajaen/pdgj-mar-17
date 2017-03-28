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
  Right
}

public class ControlMarker : MonoBehaviour
{

  public ControlName ControlName;
  
  Sprite  On, Off;
  bool    wasDown, isDown;
  Image   image;
  void Awake()
  {
   image = GetComponent<Image>();
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
    }

    On  = Resources.Load<Sprite>(String.Format("Control_{0}_Down", img));
    Off = Resources.Load<Sprite>(String.Format("Control_{0}", img));
  }

  void Update()
  {
    wasDown = isDown;

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
    }

    if (wasDown != isDown)
    {
      if (isDown)
        image.sprite = On;
      else
        image.sprite = Off;
    }

  }
}
