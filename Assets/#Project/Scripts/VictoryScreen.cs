using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] TMP_Text[] scores;
    // Start is called before the first frame update
    void Start()
    {
        //for kill order
        int[] killCounts = new int[7];

        for (int i = 0; i < 7; i++)
        {
            scores[i].SetText($"{MatchData._names[i]} : {MatchData._kills[i]} kill(s) | {MatchData._deaths[i]} death(s)"); 
            killCounts[i] = MatchData._kills[i];        
        }

        //*put in kill order
        //array of index ordered by kill count

        int[] indexOrder = new int[7];
        for (int i = 0; i < 7; i++)
        {
            int maxValue = Mathf.Max(killCounts);
            indexOrder[i] = Array.IndexOf(killCounts, maxValue);
            killCounts[Array.IndexOf(killCounts, maxValue)] = -1;
        }
        //highest y position for first place
        int ScorePosition = 300;
        for (int i = 0; i < 7; i++)
        {
            Vector3 pos = scores[indexOrder[i]].GetComponent<RectTransform>().localPosition;
            pos.y = ScorePosition;
            scores[indexOrder[i]].GetComponent<RectTransform>().localPosition = pos;
            ScorePosition -= 100;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReturnToMain(){

        SceneManager.LoadScene("MainMenu");
    }
}
