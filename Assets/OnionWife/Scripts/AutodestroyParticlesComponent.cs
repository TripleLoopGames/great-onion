using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutodestroyParticlesComponent : MonoBehaviour {

  // Use this for initialization
  void Start() {
    this.selfParticle = this.GetComponent<ParticleSystem>();
  }

  // Update is called once per frame
  void Update() {
    if (this.selfParticle) {
      if (!this.selfParticle.IsAlive()) {
        Destroy(gameObject);
      }
    }
  }

  private ParticleSystem selfParticle;
}