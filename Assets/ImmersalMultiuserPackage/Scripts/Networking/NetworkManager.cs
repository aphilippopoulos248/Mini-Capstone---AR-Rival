using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using Fusion.Sockets;
using System;
using UnityEngine.Events;
using TMPro;

public class NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private GameObject _runnerPrefab;
    [SerializeField] public TextMeshProUGUI playerJoinedDebug;

    public UnityEvent onPlayerJoinedEvent;

    //creating a singleton
    public static NetworkManager Instance { get; private set; }

    public NetworkRunner Runner { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        // fixing the server to a perticular region
        Fusion.Photon.Realtime.PhotonAppSettings.Global.AppSettings.FixedRegion = "asia";
    }

    public async void CreateSession(string roomCode)
    {
        //Create Runner
        CreateRunner();

        //ConnectSession
        await Connect(roomCode);
    }

    public async void JoinSession(string roomCode)
    {
        //Create Runner
        CreateRunner();

        //ConnectSession
        await Connect(roomCode);
    }

    public void CreateRunner()
    {
        Runner = Instantiate(_runnerPrefab, transform).GetComponent<NetworkRunner>();
        Runner.AddCallbacks(this);
    }

    private async Task Connect(string SessionName)
    {
        var args = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = SessionName,
            SceneManager = GetComponent<NetworkSceneManagerDefault>(),
        };
        await Runner.StartGame(args);
    }

    #region INetworkRunnerCallbacks

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        playerJoinedDebug.text += " Player ID: " + player.PlayerId + "has joined\n";
        onPlayerJoinedEvent.Invoke();
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log("Runner Shutdown");
    }

    #endregion INetworkRunnerCallbacks

    #region INetworkRunnerCallbacks (Unused)

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    #endregion INetworkRunnerCallbacks (Unused)
}
