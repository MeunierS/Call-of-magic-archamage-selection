using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.SceneManagement;

public class MatchTime : MonoBehaviour
{
    public float timeElapsed;
    public TMP_Text timerText;
    [SerializeField] PlayerUI playerUI;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        timeElapsed -= Time.deltaTime;
        timerText.SetText($"{timeElapsed:0.00} s");
        if(timeElapsed < 0){
            EndMatch();
        }
        
    }
    void EndMatch(){
        SaveMatchData();
        SceneManager.LoadScene("VictoryScreen");
    }
    void SaveMatchData(){
        for (int i = 0; i < 6; i++)
        {
            MatchData._kills[i] = playerUI.players[i].transform.GetComponent<Bot>().personnalKill;
            MatchData._deaths[i]= playerUI.players[i].transform.GetComponent<Bot>().personnalDeath;    
        }
        MatchData._kills[6]= playerUI.players[6].transform.GetComponent<PlayerControl>().personnalKill;
        MatchData._deaths[6]= playerUI.players[6].transform.GetComponent<PlayerControl>().personnalDeath;
    }
}
