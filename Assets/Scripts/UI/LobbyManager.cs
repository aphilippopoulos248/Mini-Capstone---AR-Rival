using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject lobbySessionUIPrefab;
    [SerializeField] private GameObject lobbyManagerCanvas;
    [SerializeField] private Transform lobbyGridParent;

    [SerializeField] private Button leaveLobbyBtn;
    [SerializeField] private Button backToMapBtn;

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

    public void ShowLeaveLobbyBtn()
    {
        backToMapBtn.gameObject.SetActive(false);
        leaveLobbyBtn.gameObject.SetActive(true);
    }

    public void ShowBackToMapBtn()
    {
        backToMapBtn.gameObject.SetActive(true);
        leaveLobbyBtn.gameObject.SetActive(false);
    }
}