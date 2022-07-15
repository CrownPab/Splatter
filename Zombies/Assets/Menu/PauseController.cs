using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
  [SerializeField] GameObject menu; 
  [SerializeField] GameObject UICamera; 
  private bool isOpen;
  private GameObject[] weapons;  
    // Start is called before the first frame update
  void Start()
  {
    menu.SetActive(false); 
    isOpen = false; 
  }

  public void OnSaveGame(){

  }

  public void OnReturnToMenu(){
    StartCoroutine(MainMenu());
  }

  // singleton for URP disables the start function when loading a new scene.
  // need to wait a frame for singleton to be destroyed
  // before loading new scene. 
  IEnumerator MainMenu(){
    GameObject debugUpdater = GameObject.Find("[Debug Updater]");
    if(debugUpdater != null)
    {
      Destroy(debugUpdater);
    }
    yield return new WaitUntil(() => GameObject.Find("[Debug Updater]") == null);
    Time.timeScale = 1; 
    SceneManager.LoadScene(0, LoadSceneMode.Single); 
  }

  private void ToggleWeapons(bool isActive){
    if (!isActive){
      weapons = GameObject.FindGameObjectsWithTag("Weapon"); 
    }
    foreach(GameObject weapon in weapons){
      weapon.SetActive(isActive); 
    }

    if (isActive){
      weapons = null; 
    }
  }

  public void ReturnToGame(){
    isOpen = false; 
    menu.SetActive(false); 
    Time.timeScale = 1; 
    UICamera.SetActive(true);
    ToggleWeapons(true); 
		Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape)){
      if (isOpen){
        ReturnToGame(); 
      }
      else {
        isOpen = true; 
        menu.SetActive(true); 
        Time.timeScale = 0;
        UICamera.SetActive(false); 
        ToggleWeapons(false); 
        Cursor.lockState = CursorLockMode.None;
      }
    }
  }
}
