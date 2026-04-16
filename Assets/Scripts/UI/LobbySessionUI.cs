using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbySessionUI : MonoBehaviour
{
    [SerializeField] private TMP_Text lobbyCodeText;
    [SerializeField] private Button joinButton;

    private string lobbyCode;

    public void Initialize(string code)
    {
        lobbyCode = code;
        lobbyCodeText.text = code;

        //joinButton.onClick.RemoveAllListeners();
        //joinButton.onClick.AddListener(JoinLobby);
    }

    public void JoinLobby()
    {
        NetworkManager.Instance.JoinSession(lobbyCode);
        LobbyManager.Instance.HideLobbies();

        LobbyManager.Instance.ShowLeaveLobbyBtn();
    }
}