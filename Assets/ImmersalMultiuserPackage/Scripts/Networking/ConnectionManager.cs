using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    }

    public void JoinRoom()
    {
        multiUserUI.SetActive(false);
        NetworkManager.Instance.JoinLobby();
    }
}
