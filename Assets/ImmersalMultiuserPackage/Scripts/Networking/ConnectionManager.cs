using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    private bool isConnected = false;
    public void CreateRoom()
    {
        if (!isConnected) NetworkManager.Instance.CreateSession(inputField.text);
        isConnected = true;
    }

    public void JoinRoom()
    {
        if (!isConnected) NetworkManager.Instance.JoinSession(inputField.text);
        isConnected = true;
    }
}
