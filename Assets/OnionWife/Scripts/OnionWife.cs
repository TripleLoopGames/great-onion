using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionWife : MonoBehaviour {
  private void Start() {
    Health health = GetComponent<Health>();
    health.SetOnDeath(() => {
      Transform particles = Instantiate(deathParticles).transform;
      particles.position = transform.position;
      GetComponent<SpriteRenderer>().enabled = false;
    });
  }

  [SerializeField]
  public GameObject deathParticles;
}