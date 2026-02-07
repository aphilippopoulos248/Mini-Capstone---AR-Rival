using Fusion;
using Fusion.Sockets;
using Immersal.XR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDragonSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private List<GameObject> dragons;
    [SerializeField] private DragonController dragonController;

    private XRSpace m_XRSpace;
    private Transform cameraTransform;
    private Dictionary<PlayerRef, NetworkObject> _spawnedDragon = new Dictionary<PlayerRef, NetworkObject>();

    private void Awake()
    {
        if (m_XRSpace == null)
        {
            m_XRSpace = GameObject.FindObjectOfType<Immersal.XR.XRSpace>();
        }
        cameraTransform = Camera.main.transform;
    }

    public void SpawnDragon()
    {
        NetworkManager.Instance.Runner.AddCallbacks(this);
    }

    #region INetworkRunnerCallbacks

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (player == runner.LocalPlayer)
        {
            NetworkObject networkDragonObject = NetworkManager.Instance.Runner.Spawn(
                                                dragons[UnityEngine.Random.Range(0, dragons.Count)],
                                                cameraTransform.position + cameraTransform.forward * 0.5f,
                                                Quaternion.identity,
                                                NetworkManager.Instance.Runner.LocalPlayer,
                                                InitializeObjBeforeSpawn);

            dragonController.Rigidbody = networkDragonObject.GetComponent<Rigidbody>();

            _spawnedDragon.Add(player, networkDragonObject);
        }
    }

    private void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
    {
        obj.GetComponent<Transform>().parent = m_XRSpace.transform;
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedDragon.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedDragon.Remove(player);
        }
    }

    #endregion INetworkRunnerCallbacks

    #region Unsed INetworkRunnerCallbacks

    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    #endregion Unsed INetworkRunnerCallbacks
}
