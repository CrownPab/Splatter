using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
  public GameObject destroyedReplacement;
  public float maxDamage = 100f; 
  private float currentDamage = 0; 
    // Start is called before the first frame update
  void Start()
  {
      
  }
  public void onHit(float damage){
    Debug.Log("Hit Destroyable");
    currentDamage += damage;
    if (currentDamage > maxDamage){
      onDestroy();
    }
  }

  private void onDestroy(){
    destroyedReplacement.SetActive(true);
    Destroy(this.gameObject);
  }

  // Update is called once per frame
  void Update()
  {
      
  }
}
