using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAToStart : MonoBehaviour
{

  void Update ()
  {
    if (Controls.Action1)
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
  }

}
