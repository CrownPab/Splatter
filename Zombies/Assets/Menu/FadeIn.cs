using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FadeIn : MonoBehaviour
{
  [SerializeField]
  private float duration = 1f; 

  [SerializeField]
  private float delay = 0f;

  private CanvasGroup canvasGroup;

  IEnumerator Fade(){
    float totalTime = 0;

    yield return new WaitForSecondsRealtime(delay);
    while(totalTime < duration){
      canvasGroup.alpha = totalTime/duration; 
      totalTime += Time.deltaTime; 
      yield return null; 
    }
    canvasGroup.alpha = 1;
  }

  private void Start() {
    canvasGroup = this.GetComponent<CanvasGroup>();
    canvasGroup.alpha = 0;
    StartCoroutine(Fade());
  }

}