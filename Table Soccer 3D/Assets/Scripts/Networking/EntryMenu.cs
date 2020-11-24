using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryMenu : MonoBehaviour
{
    [SerializeField] private Text _playerName;
    [SerializeField] private Text _roomName;

    public void CreateRoom()
    {
        NetworkAdmin.Instance.ChangeNickname(_playerName.text);
        NetworkAdmin.Instance.CreateRoom(_roomName.text);
    }

    public void JoinRoom()
    {
        NetworkAdmin.Instance.ChangeNickname(_playerName.text);
        NetworkAdmin.Instance.JoinRoom(_roomName.text);
    }
}
