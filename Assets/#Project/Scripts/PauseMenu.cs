using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    GameObject[] pausedObj;
    void Start(){
        Time.timeScale = 1;
        pausedObj = GameObject.FindGameObjectsWithTag("Pause");
        EndPause();
	}
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(Time.timeScale == 1)
			{
				Time.timeScale = 0;
				ShowPause();
			} else if (Time.timeScale == 0){
				Continue();
			}
        }
    }
    public void Continue(){
        EndPause();
        Time.timeScale = 1;
    }
    public void QuitToMain(){
        SceneManager.LoadScene("MainMenu");
    }
    public void ShowPause(){
        foreach (GameObject obj in pausedObj)
        {
            obj.SetActive(true);
        }
    }
    public void EndPause(){
        foreach (GameObject obj in pausedObj)
        {
            obj.SetActive(false);
        }
    }
}