using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

  public Plant Initialize(Action onPickup) {
    this.onPickup = onPickup;
    animator = GetComponent<Animator>();
    return this;
  }

  public Plant PickUp() {
    if (growth == Growth.InProgress) {
      return this;
    }
    if (growth == Growth.None) {
      animator.SetInteger("Growth", 1);
      growth = Growth.InProgress;
      return this;
    }
    this.onPickup();
    Destroy(gameObject);
    return this;
  }

  public Plant onGrowthComplete() {
    growth = Growth.Done;
		return this;
  }

  private Action onPickup;
  private Animator animator;
  private Growth growth = Growth.None;

  enum Growth {
    None,
    InProgress,
    Done
    };
}