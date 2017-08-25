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
    StartCoroutine(gameTimer());
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
      int nextSpawnTime = 2;
      flowState flowState = Array.Find(timeFlow, (time) => time.timeSpent > timeSpent);
      if(flowState != null){
        nextSpawnTime = flowState.timeLimit;
      }
      yield return new WaitForSeconds(Random.Range(nextSpawnTime - 2, nextSpawnTime));
    }
  }

  private IEnumerator gameTimer() {
    while (true) {
      timeSpent++;
      yield return new WaitForSeconds(1f);
    }
  }

  private Vector3 getRandomSpawnPoint(Transform[] spawnPoints) {
    int amount = spawnPoints.Length;
    int randomIndex = Random.Range(0, amount - 1);
    return spawnPoints[randomIndex].position;
  }

  private int timeSpent = 0;

  [SerializeField]
  private GameObject bunnyPrefab;
  private Transform[] spawnPoints;
  private bool spawning = false;
  private IEnumerator spawnCorutine;
  private Action<Transform> group;

  private flowState[] timeFlow = new flowState[] {
    new flowState { timeSpent = 10, timeLimit = 15 },
    new flowState { timeSpent = 25, timeLimit = 12 },
    new flowState { timeSpent = 50, timeLimit = 10 },
    new flowState { timeSpent = 70, timeLimit = 8 },
    new flowState { timeSpent = 100, timeLimit = 5 },
  };
}