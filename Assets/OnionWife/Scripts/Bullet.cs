using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

  public Bullet Initialize(Collider playeCollider) {
    ownRigidbody = GetComponent<Rigidbody>();
    timerComponent = GetComponent<TimerComponent>();
    Physics.IgnoreCollision(GetComponents<Collider>()[1], playeCollider);
    return this;
  }

  public Bullet Shoot(Vector3 direction, float magnitude, int lifeTime) {
    ownRigidbody.AddForce(direction * magnitude, ForceMode.Impulse);
    timerComponent.StartTimer(lifeTime, () => Destroy(gameObject));
    return this;
  }

  private void OnTriggerEnter(Collider collision) {
    if (collision.CompareTag("Enemy")) {
      collision.GetComponentInParent<Bunny>().Damage();
      ownRigidbody.useGravity = true;
      ownRigidbody.velocity = new Vector3(0, 0, 0);
      ownRigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
      timerComponent.StopTimer();
    }
    if (collision.CompareTag("Player")) {
      inPlayer = true;
      playerCollider = collision;
      if (inGround) {
        collision.SendMessage("AddAmunition", 1);
        Destroy(gameObject);
      }
    }
  }

  private void OnCollisionEnter(Collision collision) {
    Debug.Log(collision.gameObject.tag);
    if (collision.gameObject.CompareTag("Ground") && !inGround) {
      inGround = true;
      timerComponent.StartTimer(1, () => Destroy(gameObject));
      if (inPlayer) {
        playerCollider.SendMessage("AddAmunition", 1);
        Destroy(gameObject);
      }
    }
  }

  private void OnTriggerExit(Collider collision) {
    if (collision.CompareTag("Player")) {
      inPlayer = false;
      playerCollider = null;
    }
  }

  private Collider playerCollider;
  private bool inGround = false;
  private bool inPlayer = false;
  private Rigidbody ownRigidbody;
  private TimerComponent timerComponent;
}