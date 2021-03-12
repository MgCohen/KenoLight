using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLoader : MonoBehaviour
{

  public static SpriteLoader Main;
  private void Awake()
  {
    Main = this;
  }

  public Sprite[] sprites = new Sprite[90];
}
