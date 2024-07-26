using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ToggleCamera : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    // Start is called before the first frame update
    void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    void OnEnable(){
        playerInputManager.onPlayerJoined += ToggleThis;
    }
    void OnDisable(){
        playerInputManager.onPlayerJoined -= ToggleThis;
    }
    void ToggleThis(PlayerInput player){
        this.gameObject.SetActive(false);
    }
}
