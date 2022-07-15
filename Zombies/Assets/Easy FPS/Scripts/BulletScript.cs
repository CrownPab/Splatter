using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
	RaycastHit hit;
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	[Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
	public float floatInfrontOfWall;
	[Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
	public GameObject bloodEffect;
	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask ignoreLayer;
  GameObject firePostion;

  private void Start() {
    //firePostion = GameObject.FindGameObjectWithTag("BulletSpawn");
  }

	/*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
	void Update () {

		if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0)), out hit, maxDistance, ~ignoreLayer)){
      //Debug.DrawRay(transform.position, transform.forward, Color.green);
      Debug.Log(hit.transform.tag);
      if (hit.collider.GetComponent<HitDetector>() != null){
        hit.collider.GetComponent<HitDetector>().OnHit(10);
        Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(gameObject);
      }
      if (hit.transform.tag == "Destroyable"){
        hit.transform.GetComponent<DestructableObject>().onHit(10);
      }
      if (hit.transform.tag == "Forcable"){
        hit.transform.GetComponent<Rigidbody>().AddForce(Vector3.forward*10);
      }
			if(decalHitWall){
				if(hit.transform.gameObject.layer == 10){
					Instantiate(decalHitWall, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
					Destroy(gameObject);
				}
				if(hit.transform.tag == "Dummie"){
					Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
					Destroy(gameObject);
				}
			}		
			Destroy(gameObject);
		}
		Destroy(gameObject, 0.1f);
	}

}
