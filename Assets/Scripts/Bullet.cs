using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
  [HideInInspector]
  public Vector3 direction;

  float speed = 1.2f;

  void Update()
  {
    transform.position += direction * speed;
  }

  void OnCollisionEnter(Collision other)
  {
    if (isServer)
    {
      if (other.gameObject.tag == "Player")
      {
        DecreaseLife(other.gameObject);
      }
    }
    CmdDestroy(gameObject);
  }

  void DecreaseLife(GameObject otherPlayer)
  {
    otherPlayer.GetComponent<Player>().life--;
  }

  [Command]
  void CmdDestroy(GameObject destroyable)
  {
    NetworkServer.Destroy(destroyable);
  }
}
