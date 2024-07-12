using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    //*Order : [0 - 5] Bot 0 -> Bot 5, [6]  Player
    [SerializeField] TMP_Text[] scores;
    // Bot names: 0-Carlina, 1-Gideon, 2-Uliath, 3-Remagine, 4-Sicofla, 5-Jaygee, 6-You
    [SerializeField] public string[] botNames;
    [SerializeField] public Transform[] players;
    
    // Start is called before the first frame update
    void Start()
    {
        //initiliaze ScoreBoard
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].SetText($"{botNames[i]} : 0 kill(s) | 0 death(s)");
        }
    }

    public void UpdateScore(){
        //for kill order
        int[] killCounts = new int[scores.Length];

        for (int i = 0; i < scores.Length-1; i++)
        {
            scores[i].SetText($"{botNames[i]} : {players[i].transform.GetComponent<Bot>().personnalKill} kill(s) | {players[i].transform.GetComponent<Bot>().personnalDeath} death(s)"); 
            killCounts[i] = players[i].transform.GetComponent<Bot>().personnalKill;        
        }
        scores[scores.Length-1].SetText($"{botNames[scores.Length-1]} : {players[scores.Length-1].transform.GetComponent<PlayerControl>().personnalKill} kill(s) | {players[scores.Length-1].transform.GetComponent<PlayerControl>().personnalDeath} death(s)");
        killCounts[scores.Length-1] = players[scores.Length-1].transform.GetComponent<PlayerControl>().personnalKill;

        //*put in kill order
        //array of index ordered by kill count

        int[] indexOrder = new int[scores.Length];
        for (int i = 0; i < scores.Length; i++)
        {
            int maxValue = Mathf.Max(killCounts);
            indexOrder[i] = Array.IndexOf(killCounts, maxValue);
            killCounts[Array.IndexOf(killCounts, maxValue)] = -1;
        }
        //highest y position for first place
        int ScorePosition = 300;
        for (int i = 0; i < scores.Length; i++)
        {
            Vector3 pos = scores[indexOrder[i]].GetComponent<RectTransform>().localPosition;
            pos.y = ScorePosition;
            scores[indexOrder[i]].GetComponent<RectTransform>().localPosition = pos;
            ScorePosition -= 100;
        }
    }
}
