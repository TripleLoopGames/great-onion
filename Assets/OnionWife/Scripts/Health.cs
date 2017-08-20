using System;
using UnityEngine;

public class Health : MonoBehaviour {

  public Health Substract(int amount) {
    if (dead) {
      return this;
    }
    current -= amount;
		onSubstract();
    if (current <= 0) {
      onDeath();
      dead = true;
    }
    return this;
  }

	public Health SetOnDeath(Action onDeath){
		this.onDeath = onDeath;
		return this;
	}

	public Health SetOnSubstract(Action onSubstract){
		this.onSubstract = onSubstract;
		return this;
	}

  [SerializeField]
  private int current = 5;
  private bool dead = false;
  private Action onDeath = () => { };
	private Action onSubstract = () => { };
}