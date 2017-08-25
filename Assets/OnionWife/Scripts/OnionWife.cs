using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionWife : MonoBehaviour {
  public OnionWife Initialize(Action<int> onGrowth, Action onDeath) {
    Health health = GetComponent<Health>();
    health.SetOnDeath(() => {
      if(immortal){
        return;
      }
      Transform particles = Instantiate(deathParticles).transform;
      particles.position = transform.position;
      GetComponent<SpriteRenderer>().enabled = false;
      dead = true;
      StopWatering();
      onDeath();
    });
    this.onGrowth = onGrowth;
    return this;
  }

  public OnionWife StartWatering() {
    if (dead || immortal) {
      return this;
    }
    if (wateringCorutine != null) {
      Debug.LogError("shit shit shit");
      return this;
    }
    wateringCorutine = wateringProcess();
    StartCoroutine(wateringCorutine);
    return this;
  }

  public OnionWife StopWatering() {
    if (wateringCorutine == null) {
      Debug.Log("nop sorry already stopped");
      return this;
    }
    StopCoroutine(wateringCorutine);
    wateringCorutine = null;
    return this;
  }

  private IEnumerator wateringProcess() {
    while (true && growth < 100) {
      growth++;
      onGrowth(growth);
      yield return new WaitForSeconds(0.4f);
    }
    immortal = true;
  }

  private bool immortal = false;
  private bool dead;
  private int growth = 0;

  [SerializeField]
  public GameObject deathParticles;
  private Action<int> onGrowth;

  private IEnumerator wateringCorutine;
}