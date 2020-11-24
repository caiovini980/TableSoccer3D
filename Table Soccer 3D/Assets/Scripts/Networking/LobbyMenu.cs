using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _playerList;
    [SerializeField] private Button _startGame;

    [PunRPC]
    public void UpdateList()
    {
        _playerList.text = NetworkAdmin.Instance.GetPlayerList();
        _startGame.interactable = NetworkAdmin.Instance.MasterPlayer();
    }


}
