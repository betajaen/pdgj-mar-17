using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stars : MonoBehaviour {

  public Image[] Star;
  
  Sprite On, Off;

  void Awake()
  {
    On = Resources.Load<Sprite>("starGold");
    Off = Resources.Load<Sprite>("starSilver");
  }
  
  void _Set(int n, bool value)
  {
    Star[n].sprite = value ? On : Off;
  }

  public void Set(int score)
  {
    if (score > 5)
      score = 5;
    else if (score < 0)
      score = 0;

    switch(score)
    {
      case 0:
        _Set(0, false);
        _Set(1, false);
        _Set(2, false);
        _Set(3, false);
        _Set(4, false);
      break;
      case 1:
        _Set(0, true);
        _Set(1, false);
        _Set(2, false);
        _Set(3, false);
        _Set(4, false);
      break;
      case 2:
        _Set(0, true);
        _Set(1, true);
        _Set(2, false);
        _Set(3, false);
        _Set(4, false);
      break;
      case 3:
        _Set(0, true);
        _Set(1, true);
        _Set(2, true);
        _Set(3, false);
        _Set(4, false);
      break;
      case 4:
        _Set(0, true);
        _Set(1, true);
        _Set(2, true);
        _Set(3, true);
        _Set(4, false);
      break;
      case 5:
        _Set(0, true);
        _Set(1, true);
        _Set(2, true);
        _Set(3, true);
        _Set(4, true);
      break;

    }

  }
}
