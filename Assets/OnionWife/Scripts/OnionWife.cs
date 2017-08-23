using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionWife : MonoBehaviour {
  public OnionWife Initialize() {
    Health health = GetComponent<Health>();
    health.SetOnDeath(() => {
      Transform particles = Instantiate(deathParticles).transform;
      particles.position = transform.position;
      GetComponent<SpriteRenderer>().enabled = false;
    });
    return this;
  }

  [SerializeField]
  public GameObject deathParticles;
}