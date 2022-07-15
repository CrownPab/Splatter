using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodySection{Head, Body, Legs};

public class HitDetector : MonoBehaviour
{
  [SerializeField]
  private BodySection bodySection;
  private Zombie controller;

  private void Awake() {
    controller = this.transform.root.GetComponent<Zombie>();
  }

  public void OnHit(float damage){
    if (controller == null){
      controller = this.transform.root.GetComponent<Zombie>();
    }
    controller.OnHit(bodySection, damage);
  }
}
