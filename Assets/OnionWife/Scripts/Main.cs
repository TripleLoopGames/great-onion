using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

  private Main Initialize() {
    InitalizeBunnySpawner()
      .InitalizeOnionWife()
      .InitalizePlayer();
    bunnySpawner.StartSpawnRoutine();
    return this;
  }

  private Main InitalizeBunnySpawner() {
    bunnySpawner = FindAndGetComponent<BunnySpawner>("Bunny_Spawner");
    Transform bunniesGroup = FindGameobject("Bunnies").GetComponent<Transform>();
    bunnySpawner.Initialize((bunny) => bunny.SetParent(bunniesGroup));
    return this;
  }

  private Main InitalizeOnionWife() {
    onionWife = FindAndGetComponent<OnionWife>("Onion_Wife");
    onionWife.Initialize();
    return this;
  }

  private Main InitalizePlayer() {
    player = FindAndGetComponent<Player>("Player");
    player.Initialize();
    return this;
  }

  private T FindAndGetComponent<T>(string nameToFind) {
    GameObject findResult = FindGameobject(nameToFind);
    T gotComponent = findResult.GetComponent<T>();
    if (gotComponent == null) {
      Debug.LogError("Could not find: " + gotComponent.ToString() + " in " + nameToFind);
    }
    return gotComponent;
  }

  private GameObject FindGameobject(string nameToFind) {
    GameObject findResult = GameObject.Find(nameToFind);
    if (findResult == null) {
      Debug.LogError("Could not find: " + nameToFind);
    }
    return findResult;
  }

  void Start() {
    Initialize();
  }

  private BunnySpawner bunnySpawner;
  private OnionWife onionWife;
  private Player player;
}