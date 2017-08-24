using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmunitionCounter : MonoBehaviour {
  public AmmunitionCounter Initialize() {
		text = GetComponentInChildren<Text>();
    return this;
  }

	public AmmunitionCounter setAmount(int amount){
		text.text = amount.ToString();
		return this;
	}

	private Text text;
}