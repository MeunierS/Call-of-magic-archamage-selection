using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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
        SceneManager.LoadScene("VictoryScreen");
    }
}
