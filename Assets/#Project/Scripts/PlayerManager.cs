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
    [SerializeField]private Bot[] bots;

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

        //! TO DEBUG
        //set minimap render texture to each player
        // if (GetComponentInChildren<Camera>().CompareTag("MinimapCam")){
        //     GetComponentInChildren<Camera>().targetTexture = minimaps[playerId];
        // }

        //add RespawnPoints to player
        player.gameObject.GetComponent<PlayerControl>().spawnpoints = FindFirstObjectByType<SpawnPointsTarget>().gameObject.transform;

        //Add P2 to leaderboard

    }
}
