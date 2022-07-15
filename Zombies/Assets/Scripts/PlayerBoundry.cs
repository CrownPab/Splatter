using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundry : MonoBehaviour
{
  private void OnCollisionEnter(Collision other) {
    if (other.gameObject.tag == "Zombie"){
      Physics.IgnoreCollision(other.collider, this.GetComponent<BoxCollider>());
    }
  }
}
