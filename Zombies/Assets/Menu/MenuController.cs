using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class MenuController : MonoBehaviour
{
  [SerializeField] GameObject LoadButton;
  private void Start() {
    LoadButton.GetComponent<Button>().interactable = false; 
  }

  public void OnStart(){
    SceneManager.LoadScene(1); 
  }
  public void OnLoadGame(){
  }

  public void OnExit(){
    Application.Quit(); 
  }
}
