using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

  public Player Initialize(Action<Transform> group, Action<int> onAmmunitionChange) {
    this.group = group;
    this.onAmmunitionChange = onAmmunitionChange;
    ownRigidbody = GetComponent<Rigidbody>();
    return this;
  }

  private void Update() {
    // Big hack to shot, should be located inside input controller and configurable
    if (Input.GetMouseButtonDown(0) && ammunition > 0) {
      var center = new Vector2(Screen.width / 2, Screen.height / 2);
      Vector3 heading = Input.mousePosition - (Vector3) center;
      Vector3 translatedHeading = new Vector3(heading.x, 0, heading.y);
      Transform bulletTransform = Instantiate(bulletPrefab).transform;
      bulletTransform.position = transform.position;
      bulletTransform.GetComponent<Bullet>().Initialize().Shoot(translatedHeading.normalized, 20, 3);
      this.group(bulletTransform);
      ammunition--;
      this.onAmmunitionChange(ammunition);
    }
    if (Input.GetKeyDown(KeyCode.E)) {
      if (inPickup) {
        bool pickedUp = currentPickup.PickUp();
        if (pickedUp) {
          currentPickup = null;
          inPickup = false;
          ammunition++;
          this.onAmmunitionChange(ammunition);
        }
      }
      watering = true;
      if (inOnionWife) {
        onionWife.StartWatering();
      }
    }
    if (Input.GetKeyUp(KeyCode.E)) {
      watering = false;
      if (inOnionWife) {
        onionWife.StopWatering();
      }
    }
    currentDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
  }

  private void FixedUpdate() {
    ownRigidbody.velocity = currentDirection * speed;
  }

  private void OnTriggerEnter(Collider collision) {
    if (collision.CompareTag("Plant")) {
      currentPickup = collision.GetComponent<Plant>();
      inPickup = true;
    }
    if (collision.CompareTag("Onion")) {
      inOnionWife = true;
      onionWife = collision.GetComponent<OnionWife>();
      if (watering) {
        onionWife.StartWatering();
      }
    }
  }

  private void OnTriggerExit(Collider collision) {
    if (collision.CompareTag("Plant")) {
      currentPickup = null;
      inPickup = false;
    }
    if (collision.CompareTag("Onion")) {
      inOnionWife = false;
      if (watering) {
        onionWife.StopWatering();
      }
      onionWife = null;
    }
  }

  public GameObject bulletPrefab;

  private Vector3 currentDirection;
  private Rigidbody ownRigidbody;
  private Plant currentPickup;

  private OnionWife onionWife;

  private const float speed = 20;
  private const float maxSpeed = 4;

  private bool inPickup = false;
  private bool inOnionWife = false;

  private bool watering = false;
  private Action<Transform> group;
  private Action<int> onAmmunitionChange;

  private int ammunition = 0;
}