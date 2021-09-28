using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
  public GameObject bulletPrefab;
  float movementSpeed = 0.2f;
  float rotationSpeed = 4f;

  [SyncVar(hook = "UpdateLifeBar")] public float life = 7;

  void Start()
  {
    if (!isLocalPlayer)
    {
      transform.Find("Main Camera").gameObject.SetActive(false);
    }
  }

  void FixedUpdate()
  {
    if (isLocalPlayer)
    {
      HandleMovement();
      HandleRotation();
      HandleShoot();
    }
  }

  void UpdateLifeBar(float life)
  {
    transform.Find("Canvas").transform.Find("LifeBar").GetComponent<RectTransform>().sizeDelta = new Vector2(life, 0.5f);
  }

  void HandleMovement()
  {
    if (Input.GetKey("left"))
    {
      //transform.position += new Vector3(-movementSpeed, 0, 0);
      transform.Translate(-movementSpeed, 0, 0);
    }
    if (Input.GetKey("right"))
    {
      //transform.position += new Vector3(movementSpeed, 0, 0);
      transform.Translate(movementSpeed, 0, 0);
    }
    if (Input.GetKey("up"))
    {
      //transform.position += new Vector3(0, 0, movementSpeed);
      transform.Translate(0, 0, movementSpeed);
    }
    if (Input.GetKey("down"))
    {
      //transform.position += new Vector3(0, 0, -movementSpeed);
      transform.Translate(0, 0, -movementSpeed);
    }
  }

  void HandleRotation()
  {
    if (Input.GetKey("a"))
    {
      transform.Rotate(0, -rotationSpeed, 0);
    }
    if (Input.GetKey("d"))
    {
      transform.Rotate(0, rotationSpeed, 0);
    }
  }

  void HandleShoot()
  {
    if (Input.GetKeyDown("space"))
    {
      CmdShoot();
    }
  }

  [Command]
  void CmdShoot()
  {
    GameObject bullet = Instantiate(bulletPrefab, transform.position + (transform.forward * 2), Quaternion.identity);
    bullet.GetComponent<Bullet>().direction = transform.forward;
    NetworkServer.Spawn(bullet);
  }
}
