using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    //*Order : [0 - 5] Bot 0 -> Bot 5, [6]  Player1, [7] Player2
    [SerializeField] public TMP_Text[] scores;
    // Bot names: 0-Carlina, 1-Gideon, 2-Uliath, 3-Remagine, 4-Sicofla, 5-Jaygee, 6-Player 1, 7-Player 2
    [SerializeField] public List<Transform> players;
    
    // Start is called before the first frame update
    void Start()
    {
        //initiliaze ScoreBoard
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].SetText($"{MatchData._names[i]} : 0 kill(s) | 0 death(s)");
        }
    }

    public void UpdateScore(){
        //for kill order
        int[] killCounts = new int[scores.Length];

        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].SetText($"{MatchData._names[i]} : {MatchData._kills[i]} kill(s) | {MatchData._deaths[i]} death(s)");
            killCounts[i] = MatchData._kills[i];        
        }
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
