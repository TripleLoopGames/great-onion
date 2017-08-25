using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCounter : MonoBehaviour {

  public WaterCounter Initialize() {
    text = GetComponentInChildren<Text>();
    return this;
  }

  public WaterCounter setAmount(int amount) {
    text.text = amount + " %";
    return this;
  }

  private Text text;
}