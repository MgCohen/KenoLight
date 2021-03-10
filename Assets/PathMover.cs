using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class PathMover : MonoBehaviour
{
  public Transform target;
  public List<Transform> waypoints = new List<Transform>();
  Vector3[] path => waypoints.Select(x => x.position).ToArray();

  public float pathTime;

  private void OnEnable()
  {
    //Move();
    Float();
  }

  public void Move()
  {
    target.DOPath(path, pathTime, PathType.CatmullRom);
  }

  public void Float()
  {
    // Debug.Log(1);
    target.DOShakePosition(pathTime, 50, 1);
  }
}
