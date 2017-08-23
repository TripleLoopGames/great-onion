using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public Player Initialize() {
    ownRigidbody = GetComponent<Rigidbody>();
    return this;
  }

  private void Update() {
    // Big hack to shot, should be located inside input controller and configurable
    if (Input.GetMouseButtonDown(0)) {
      var center = new Vector2(Screen.width / 2, Screen.height / 2);
      Vector3 heading = Input.mousePosition - (Vector3) center;
      Vector3 translatedHeading = new Vector3(heading.x, 0, heading.y);
      Transform bulletTransform = Instantiate(bulletPrefab).transform;
      bulletTransform.position = transform.position;
      bulletTransform.GetComponent<Bullet>().Initialize().Shoot(translatedHeading.normalized, 20, 3);
    }
    if (Input.GetKeyDown(KeyCode.E)) {
      if (inPickup) {
        //currentPickup.PickUp();
      }
    }
    currentDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
  }

  private void FixedUpdate() {
    ownRigidbody.velocity = currentDirection * speed;
  }

  private void OnTriggerEnter2D(Collider2D collision) {
    if (collision.CompareTag("PickUp")) {
      //currentPickup = collision.GetComponent<Plant>();
      inPickup = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision) {
    if (collision.CompareTag("PickUp")) {
      //currentPickup = null;
      inPickup = false;
    }
  }

  public GameObject bulletPrefab;

  private Vector3 currentDirection;
  private Rigidbody ownRigidbody;
  //private Plant currentPickup;

  private const float speed = 10;
  private const float maxSpeed = 4;

  private bool inPickup = false;
}