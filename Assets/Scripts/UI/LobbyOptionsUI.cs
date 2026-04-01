using UnityEngine;

public class LobbyOptionsUI : MonoBehaviour
{
    public void OnClick_KickPlayer()
    {
        Debug.Log("Kicking Player");
    }
    public void OnClick_LeaveLobby()
    {
        Debug.Log("Leaving Lobby");
        NetworkManager.Instance.LeaveSession();
    }
}
