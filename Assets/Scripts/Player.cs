using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
  public GameObject bulletPrefab;

  float movementSpeed = 0.2f;
  float rotationSpeed = 4f;

  [SyncVar(hook = "UpdateLifeBar")] public float life = 7;

  void Start()
  {
    HandleCameras();
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

  void HandleCameras()
  {
    GameObject menuCamera = GameObject.Find("MenuCamera");
    Destroy(menuCamera);
    if (!isLocalPlayer)
    {
      GameObject playerCamera = transform.Find("Main Camera").gameObject;
      playerCamera.SetActive(false);
    }
  }

  void UpdateLifeBar(float life)
  {
    Transform lifeBar = transform.Find("Canvas").transform.Find("LifeBar");
    lifeBar.GetComponent<RectTransform>().sizeDelta = new Vector2(life, 0.5f);
  }

  void HandleMovement()
  {
    if (Input.GetKey("left"))
    {
      transform.Translate(-movementSpeed, 0, 0);
    }
    if (Input.GetKey("right"))
    {
      transform.Translate(movementSpeed, 0, 0);
    }
    if (Input.GetKey("up"))
    {
      transform.Translate(0, 0, movementSpeed);
    }
    if (Input.GetKey("down"))
    {
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
    if (Input.GetKey("space"))
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
