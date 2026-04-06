using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbySessionUIPrefab;
    [SerializeField] private GameObject lobbyManagerCanvas;
    [SerializeField] private Transform lobbyGridParent;

    private List<string> roomCodes = new List<string>();

    public static LobbyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddRoomCode(string roomCode)
    {
        if (string.IsNullOrWhiteSpace(roomCode)) return;
        roomCodes.Add(roomCode);
    }

    public void DisplayLobbies()
    {
        lobbyManagerCanvas.SetActive(true);

        for (int i = lobbyGridParent.childCount - 1; i >= 0; i--)
        {
            Destroy(lobbyGridParent.GetChild(i).gameObject);
        }

        foreach (string roomCode in roomCodes)
        {
            GameObject lobbySession = Instantiate(lobbySessionUIPrefab, lobbyGridParent);
            lobbySession.GetComponent<LobbySessionUI>().Initialize(roomCode);
        }
    }

    public void HideLobbies()
    {
        lobbyManagerCanvas.SetActive(false);
    }
}