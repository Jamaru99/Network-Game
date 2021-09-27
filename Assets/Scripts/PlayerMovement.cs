using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
  public GameObject bulletPrefab;
  float movementSpeed = 0.1f;
  float rotationSpeed = 2f;

  void Start()
  {

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

  void HandleMovement()
  {
    if (Input.GetKey("left"))
    {
      transform.position += new Vector3(-movementSpeed, 0, 0);
    }
    if (Input.GetKey("right"))
    {
      transform.position += new Vector3(movementSpeed, 0, 0);
    }
    if (Input.GetKey("up"))
    {
      transform.position += new Vector3(0, 0, movementSpeed);
    }
    if (Input.GetKey("down"))
    {
      transform.position += new Vector3(0, 0, -movementSpeed);
    }
  }

  void HandleRotation()
  {
    if (Input.GetKey("a"))
    {
      transform.Rotate(0, rotationSpeed, 0);
    }
    if (Input.GetKey("d"))
    {
      transform.Rotate(0, -rotationSpeed, 0);
    }
  }

  void HandleShoot()
  {
    if (Input.GetKey("space"))
    {
      GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
      bullet.GetComponent<Bullet>().direction = transform.forward;
    }
  }
}
