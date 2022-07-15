using System.Collections;
using UnityEngine;

public class DestroyAfterTimeParticle : MonoBehaviour {
	[Tooltip("Time to destroy")]
	public float timeToDestroy = 0.8f;
	/*
	* Destroys gameobject after its created on scene.
	* This is used for particles and flashes.
	*/
	void Start () {
    if (this.GetComponent<MeshRenderer>() == null){
      Destroy(gameObject, timeToDestroy); 
    }
    else {
      StartCoroutine(FadeToDestroy());
    }
	}

  IEnumerator FadeToDestroy(){
    yield return new WaitForSecondsRealtime(timeToDestroy); 
    MeshRenderer mesh = this.GetComponent<MeshRenderer>(); 
    string colourType = (mesh.materials[0].HasProperty("_TintColor")) ? "_TintColor" : "_Color"; 
    Color color = mesh.materials[0].GetColor(colourType); 
    while(color.a > 0){
      color.a -= 0.1f;  
      mesh.materials[0].SetColor(colourType, color); 

      yield return new WaitForFixedUpdate(); 
    }
    Destroy(gameObject);
  }

}
