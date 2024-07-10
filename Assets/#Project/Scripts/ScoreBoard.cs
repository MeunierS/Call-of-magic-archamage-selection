using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    GameObject[] scoreObj;
    bool isVisible= false;
    [SerializeField] private PlayerUI scoreBoard;
    // Start is called before the first frame update
    void Start()
    {
        scoreObj = GameObject.FindGameObjectsWithTag("ScoreBoard");
        ExitScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            if(!isVisible){
				ShowScore();
                scoreBoard.UpdateScore();
			} else {
				ExitScore();
			}
        }
    }
    public void ShowScore(){
        foreach (GameObject obj in scoreObj)
        {
            obj.SetActive(true);
        }
        isVisible = true;
    }
    public void ExitScore(){
        foreach (GameObject obj in scoreObj)
        {
            obj.SetActive(false);
        }
        isVisible = false;
    }
}
