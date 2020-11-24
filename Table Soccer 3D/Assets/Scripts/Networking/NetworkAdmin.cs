using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkAdmin : MonoBehaviourPunCallbacks
{
    public static NetworkAdmin Instance { get; private set; }

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
        PhotonNetwork.ConnectUsingSettings(); //connect to the master server
    }


    public override void OnConnectedToMaster()
    {
        Debug.Log("Connection well done");
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ChangeNickname(string nickname)
    {
        PhotonNetwork.NickName = nickname;
    }

    public string GetPlayerList()
    {
        var list = "";

        foreach(var player in PhotonNetwork.PlayerList)
        {
            list += player.NickName + "\n";
        }

        return list;
    }

    public bool MasterPlayer()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    public void StartGame(string SceneName)
    {
        PhotonNetwork.LoadLevel(SceneName);
    }
}
