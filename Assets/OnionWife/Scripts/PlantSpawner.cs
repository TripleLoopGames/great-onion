using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class PlantSpawner : MonoBehaviour {

  public PlantSpawner Initialize(Action<Transform> group) {
    this.group = group;
    Transform ownTransform = GetComponent<Transform>();
    plantLocations = GetComponentsInChildren<Transform>()
      .Where(transform => transform != ownTransform)
      .Select((transform, index) => new PlantLocation {
        id = index,
          transform = transform,
          used = false
      })
      .ToArray();
    return this;
  }

  public PlantSpawner StartSpawnRoutine() {
    if (spawning) {
      return this;
    }
    spawning = true;
    spawnCorutine = spawnPlants(plantLocations);
    StartCoroutine(spawnCorutine);
    return this;
  }

  public PlantSpawner StopSpawnRoutine() {
    if (!spawning) {
      return this;
    }
    spawning = false;
    StopCoroutine(spawnCorutine);
    spawnCorutine = null;
    return this;
  }

  private IEnumerator spawnPlants(PlantLocation[] plantLocations) {
    while (spawnAmount <= 8) {
      Transform plant = Instantiate(plantPrefab).transform;
      PlantLocation plantLocation = getRandomAvailablePlantLocation(plantLocations);
      plantLocation.used = true;
      plant.position = plantLocation.transform.position;
      group(plant);
      plant.GetComponent<Plant>().Initialize(() => {
        plantLocation.used = false;
        spawnAmount--;
        if (spawnAmount > 8) {
          StopSpawnRoutine();
          StartSpawnRoutine();
        }
      });
      spawnAmount++;
      yield return new WaitForSeconds(Random.Range(4f, 6f));
    }
  }

  private PlantLocation getRandomAvailablePlantLocation(PlantLocation[] plantLocations) {
    Array.Find(plantLocations, plantLocation => !plantLocation.used);
    PlantLocation[] unusedPlantLocations = plantLocations.Where(plantLocation => !plantLocation.used).ToArray();
    int amount = unusedPlantLocations.Length;
    int randomIndex = Random.Range(0, amount - 1);
    return unusedPlantLocations[randomIndex];
  }

  private bool spawning = false;
  private int spawnAmount = 0;

  [SerializeField]
  private GameObject plantPrefab;
  private PlantLocation[] plantLocations;
  private Action<Transform> group;
  private IEnumerator spawnCorutine;
}