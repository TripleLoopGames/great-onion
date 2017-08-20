using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      var center = new Vector2(Screen.width / 2, Screen.height / 2);
      Vector3 heading = Input.mousePosition - (Vector3)center;
      Vector3 translatedHeading = new Vector3(heading.x, 0, heading.y);
      Transform bulletTransform = Instantiate(bulletPrefab).transform;
      bulletTransform.position = transform.position;
      bulletTransform.GetComponent<Bullet>().Initialize().Shoot(translatedHeading.normalized, 20, 3);
    }
    currentDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
  }

  private void FixedUpdate()
  {
    ownRigidbody.velocity = currentDirection * speed;
  }

  private void Start()
  {
    ownRigidbody = GetComponent<Rigidbody>();
  }

  public GameObject bulletPrefab;
  private Vector3 currentDirection;
  private Rigidbody ownRigidbody;

  private const float speed = 10;
  private const float maxSpeed = 4;
}
