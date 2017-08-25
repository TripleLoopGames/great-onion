using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunny : MonoBehaviour {

  public Bunny Damage() {
    Die();
    return this;
  }

  private void Update() {
    if (!reachedTarget) {
      Vector3 direction = target.transform.position - transform.position;
      ownRigidbody.velocity = direction.normalized * speed;
    }
  }

  private void OnTriggerEnter(Collider collision) {
    if (collision.gameObject == target && reachedTarget == false) {
      reachedTarget = true;
      ownRigidbody.velocity = Vector2.zero;
      StartCoroutine(AttackPhase());
    }
  }

  private IEnumerator AttackPhase(){
      Health health = target.GetComponent<Health>();
      while(true){
        yield return new WaitForSeconds(2f);
        health.Substract(1);
      }
  }

  private void Start() {
    target = GameObject.FindGameObjectsWithTag("Onion")[0];
    if (target == null) {
      reachedTarget = true;
    }
    ownRigidbody = GetComponent<Rigidbody>();
  }

  private Bunny Die() {
    StopAllCoroutines();
    Transform particles = Instantiate(bunnyParticles).transform;
    particles.position = transform.position;
    Destroy(gameObject);
    return this;
  }

  public GameObject bunnyParticles;

  private GameObject target;
  private bool reachedTarget;
  private Rigidbody ownRigidbody;
  private float speed = 5;
}