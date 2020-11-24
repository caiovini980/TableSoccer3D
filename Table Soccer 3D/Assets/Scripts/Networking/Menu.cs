using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] private EntryMenu _entryMenu;
    [SerializeField] private LobbyMenu _lobbyMenu;

    private void Start()
    {
        _entryMenu.gameObject.SetActive(false);
        _lobbyMenu.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        ChangeMenu(_entryMenu.gameObject);
    }

    public override void OnJoinedRoom()
    {
        ChangeMenu(_lobbyMenu.gameObject);
        _lobbyMenu.photonView.RPC("UpdateList", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        _lobbyMenu.UpdateList();
    }

    public void ChangeMenu(GameObject menu) //function to show an specific menu whenever it's called
    {
        _entryMenu.gameObject.SetActive(false);
        _lobbyMenu.gameObject.SetActive(false);

        menu.SetActive(true);
    }

    public void LeaveLobby()
    {
        NetworkAdmin.Instance.LeaveRoom();
        ChangeMenu(_entryMenu.gameObject);
    }

    public void StartGame(string SceneName)
    {
        NetworkAdmin.Instance.photonView.RPC("StartGame", RpcTarget.All, SceneName);
        
    }
}
