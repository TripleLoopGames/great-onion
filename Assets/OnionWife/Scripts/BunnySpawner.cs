using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BunnySpawner : MonoBehaviour {

  IEnumerator spawnBunnies(Transform[] spawnPoints) {
    while (true) {
      Transform bunny = Instantiate(bunnyPrefab).transform;
      bunny.position = getRandomSpawnPoint(spawnPoints);
      yield return new WaitForSeconds(Random.Range(1f, 3f));
    }
  }

  private Vector3 getRandomSpawnPoint(Transform[] spawnPoints) {
    int amount = spawnPoints.Length;
    int randomIndex = Random.Range(0, amount - 1);
    return spawnPoints[randomIndex].position;
  }

  void Start() {
    Transform ownTransform = GetComponent<Transform>();
    Transform[] spawnPoints = GetComponentsInChildren<Transform>()
      .Where(transform => transform != ownTransform)
      .ToArray();
    spawnCorutine = spawnBunnies(spawnPoints);
    StartCoroutine(spawnCorutine);
  }

  public GameObject bunnyPrefab;

  private IEnumerator spawnCorutine;
}