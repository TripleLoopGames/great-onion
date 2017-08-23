using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BunnySpawner : MonoBehaviour {

  public BunnySpawner Initialize(Action<Transform> group) {
    this.group = group;
    Transform ownTransform = GetComponent<Transform>();
    spawnPoints = GetComponentsInChildren<Transform>()
      .Where(transform => transform != ownTransform)
      .ToArray();
    return this;
  }

  public BunnySpawner StartSpawnRoutine() {
    if (spawning) {
      return this;
    }
    spawning = true;
    spawnCorutine = spawnBunnies(spawnPoints);
    StartCoroutine(spawnCorutine);
    return this;
  }

  public BunnySpawner StopSpawnRoutine() {
    if (!spawning) {
      return this;
    }
    spawning = false;
    StopCoroutine(spawnCorutine);
    spawnCorutine = null;
    return this;
  }

  private IEnumerator spawnBunnies(Transform[] spawnPoints) {
    while (true) {
      Transform bunny = Instantiate(bunnyPrefab).transform;      
      bunny.position = getRandomSpawnPoint(spawnPoints);
      group(bunny);
      yield return new WaitForSeconds(Random.Range(1f, 3f));
    }
  }

  private Vector3 getRandomSpawnPoint(Transform[] spawnPoints) {
    int amount = spawnPoints.Length;
    int randomIndex = Random.Range(0, amount - 1);
    return spawnPoints[randomIndex].position;
  }

  public GameObject bunnyPrefab;
  private Transform[] spawnPoints;
  private bool spawning = false;
  private IEnumerator spawnCorutine;
  private Action<Transform> group;
}