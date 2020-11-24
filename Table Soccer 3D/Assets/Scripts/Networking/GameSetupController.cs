using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    public static GameSetupController Instance { get; private set; }
    public List<PlayerController> Players { get => _players; private set => _players = value; }

    [SerializeField] private string _prefabLocation;
    [SerializeField] private Transform[] _spawns;

    private int _playersOnline = 0;
    private List<PlayerController> _players;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
       photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
        Players = new List<PlayerController>();
    }

    [PunRPC]
    private void AddPlayer()
    {
        _playersOnline++;

        if(_playersOnline == PhotonNetwork.PlayerList.Length)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        var playerObject = PhotonNetwork.Instantiate(
            _prefabLocation, 
            _spawns[Random.Range(0, _spawns.Length)].position, 
            Quaternion.identity);
        var player = playerObject.GetComponent<PlayerController>();

        player.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
