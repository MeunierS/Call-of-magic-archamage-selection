using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private static List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField] private string[] playerLayers;
    private PlayerInputManager playerInputManager;
    [SerializeField]private Transform[] spawnPoints;
    [SerializeField]private RenderTexture[] minimaps;
    public int playerId;
    // Start is called before the first frame update
    void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }
        void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }
    public void AddPlayer(PlayerInput player){
        playerId = players.Count;
        players.Add(player);
        Vector3 spawn = spawnPoints[playerId].position;
        player.transform.position = spawn;
        
        int layer = LayerMask.NameToLayer(playerLayers[playerId]);
        //set the layer
        player.GetComponentInChildren<CinemachineFreeLook>().gameObject.layer = layer;
        //add the layer (bitwise operation)
        LayerMask mask = player.camera.cullingMask |= 1 << layer;
        //set the player index in the Cinemachine Input Provider
        player.GetComponentInChildren<CinemachineInputProvider>().PlayerIndex = playerId;

        //P2+ change his scoreTarget
        if(playerId > 0){
            player.gameObject.GetComponent<PlayerControl>().scoreTarget += playerId;
        }

        //TODO separate UIs during split-screen
        //set minimap render texture to each player
        // if (player.GetComponentInChildren<Camera>().targetTexture != null){
        //     player.GetComponentInChildren<Camera>().targetTexture = minimaps[playerId];
        // }

        //*temporary fix for split-screen
        //deactivate minimap during split-screen
        if(playerId > 0){
            for (int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.GetComponent<PlayerUI>().minimap.gameObject.SetActive(false);
        }
        }

        //add RespawnPoints to player
        player.gameObject.GetComponent<PlayerControl>().spawnpoints = FindFirstObjectByType<SpawnPointsTarget>().gameObject.transform;

        //deactive audio listener of P2+ on spawn
        if(playerId > 0){
            player.GetComponentInChildren<AudioListener>().enabled = false;
        }
        //add player transform to UIs
        foreach (PlayerInput playerInput in players)
        {
            playerInput.gameObject.GetComponent<PlayerUI>().players.Add(player.transform);
        }
        //activate P1+ score to leaderboard
        for (int i = 0; i < players.Count; i++)
        {
            for (int j = 0; j <= playerId; j++)
            {
                players[i].gameObject.GetComponent<PlayerUI>().scores[j+6].gameObject.SetActive(true);
            }
        }
    }
}
