using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject multiUserUI;
    [SerializeField] private GameObject lobbyManagerUI;
    public void CreateRoom()
    {
        NetworkManager.Instance.CreateSession(inputField.text);
        LobbyManager.Instance.AddRoomCode(inputField.text);
    }

    public void JoinRoom()
    {
        //NetworkManager.Instance.JoinSession(inputField.text);
        multiUserUI.SetActive(false); // for testing, will move this to a ui manager later
        LobbyManager.Instance.DisplayLobbies();
    }
}
