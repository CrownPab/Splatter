using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
  public GameObject[] zombies;
  public int maxZombies = 10; 
  public float spawnTime = 100; 

  private Transform[] spawnLocations; 
  int spawnIndex = 0; 
  
  private int index;

  private void Start() {
    index = 0; 
    spawnLocations = this.transform.GetComponentsInChildren<Transform>(); 
    StartCoroutine(SpawnZombies());
  }

  IEnumerator SpawnZombies(){
    float deltaSpawnTime = spawnTime/ (float) maxZombies;

    for (int i = 0; i < maxZombies; i++){
      GameObject zombie = Instantiate(zombies[index]);
      zombie.transform.position = spawnLocations[spawnIndex].position;

      index++;
      if (index >= zombies.Length){
        index = 0;
      }

      spawnIndex++; 
      if (spawnIndex >= spawnLocations.Length){
        spawnIndex = 0; 
      }

      yield return new WaitForSeconds(deltaSpawnTime);
    }
  }

}

