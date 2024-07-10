using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    //*Order : [0 - 5] Bot 0 -> Bot 5, [6]  Player
    [SerializeField] TMP_Text[] scores;
    [SerializeField] Transform[] players;
    
    // Start is called before the first frame update
    void Start()
    {
        //initiliaze ScoreBoard
        for (int i = 0; i < scores.Length-1; i++)
        {
            scores[i].SetText($"Bot {i} : {0} kill(s) {0} death(s)");         
        }
        scores[scores.Length-1].SetText($"You : {0} kill(s) {0} death(s)");
    }

    public void UpdateScore(){
        for (int i = 0; i < scores.Length-1; i++)
        {
            scores[i].SetText($"Bot {i} : {players[i].transform.GetComponent<Bot>().personnalKill} kill(s) {players[i].transform.GetComponent<Bot>().personnalDeath} death(s)");         
        }
        scores[scores.Length-1].SetText($"You : {players[scores.Length-1].transform.GetComponent<PlayerControl>().personnalKill} kill(s) {players[scores.Length-1].transform.GetComponent<PlayerControl>().personnalDeath} death(s)");
    }
}
