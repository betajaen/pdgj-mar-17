using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class PixelPerfectCamera : MonoBehaviour
{
  public GameObject BoundsObject;
  Texture2D texture;
  Bounds bounds;
  void Start()
  {
    texture = new Texture2D(1, 1);
    texture.SetPixel(0,0, Color.black);
    texture.Apply();

    Bounds bounds = BoundsObject.GetComponent<SpriteRenderer>().bounds;
    Debug.Log(bounds.size);
  }

  void DrawQuad(Rect position)
  {
    GUI.DrawTexture(position, texture);
  }

  void LateUpdate()
  {
    float screenRatio = (float)Screen.width / (float)Screen.height;
    float targetRatio = bounds.size.x / bounds.size.y;
 
    if (screenRatio >= targetRatio)
    {
      Camera.main.orthographicSize = bounds.size.y / 2;
    }
    else
    {
      float differenceInSize = targetRatio / screenRatio;
      Camera.main.orthographicSize = bounds.size.y / 2 * differenceInSize;
    }
 
    transform.position = new Vector3(bounds.center.x, bounds.center.y, -1f);
  }
 
}
