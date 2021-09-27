using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
  float speed = 2f;
  public Vector3 direction;

  void Update()
  {
    transform.position += direction * speed;
  }
}
