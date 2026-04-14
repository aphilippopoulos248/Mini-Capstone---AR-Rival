using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbySessionUIPrefab;
    [SerializeField] private GameObject lobbyManagerCanvas;
    [SerializeField] private Transform lobbyGridParent;

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

    public void DisplayLobbies()
    {
        lobbyManagerCanvas.SetActive(true);

        for (int i = lobbyGridParent.childCount - 1; i >= 0; i--)
        {
            Destroy(lobbyGridParent.GetChild(i).gameObject);
        }

        foreach (string roomName in NetworkManager.Instance.ActiveRooms)
        {
            GameObject lobbySession = Instantiate(lobbySessionUIPrefab, lobbyGridParent);
            lobbySession.GetComponent<LobbySessionUI>().Initialize(roomName);
        }
    }

    public void HideLobbies()
    {
        lobbyManagerCanvas.SetActive(false);
    }
}