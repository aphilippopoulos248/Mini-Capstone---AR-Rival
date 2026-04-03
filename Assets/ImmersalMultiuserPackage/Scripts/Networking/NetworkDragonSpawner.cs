using Fusion;
using Fusion.Sockets;
using Immersal.XR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class NetworkDragonSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private List<GameObject> dragons;
    [SerializeField] private DragonController dragonController;
    [SerializeField] private ARRaycastManager arRaycastManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private XRSpace m_XRSpace;
    private Transform cameraTransform;
    private Dictionary<PlayerRef, EnemyPair> _spawnedDragon = new Dictionary<PlayerRef, EnemyPair>();

    private struct EnemyPair
    {
        public NetworkObject boss;
        public NetworkObject enemy;

        public EnemyPair(NetworkObject _boss, NetworkObject _enemy)
        {
            boss = _boss;
            enemy = _enemy;
        }
    }

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
            Vector3 spawnPosition = GetForwardGroundPosition(); // method to get the position in front of the camera on the ground
            Vector3 lookRotation = new Vector3(cameraTransform.forward.x, 0, cameraTransform.forward.z); // rotation to face the same direction as the camera but only on the horizontal plane
            Quaternion spawnRotation = Quaternion.LookRotation(lookRotation) * Quaternion.Euler(0f, 180f, 0f);

            //TODO: Adjust position
            NetworkObject networkEnemyObject = NetworkManager.Instance.Runner.Spawn(
                                    dragons[0],
                                    spawnPosition * 1.2f, // spawn the enemy slightly closer to the player than the dragon
                                    spawnRotation,
                                    NetworkManager.Instance.Runner.LocalPlayer,
                                    InitializeObjBeforeSpawn);

            NetworkObject networkDragonObject = NetworkManager.Instance.Runner.Spawn(
                                                dragons[UnityEngine.Random.Range(1, dragons.Count)],
                                                spawnPosition,
                                                spawnRotation,
                                                NetworkManager.Instance.Runner.LocalPlayer,
                                                InitializeObjBeforeSpawn);

            dragonController.Rigidbody = networkDragonObject.GetComponent<Rigidbody>();

            _spawnedDragon.Add(player, new EnemyPair(networkDragonObject, networkEnemyObject));
        }
    }
    private Vector3 GetForwardGroundPosition()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
        {
            return hits[0].pose.position;
        }

        return cameraTransform.position + cameraTransform.forward * 1.5f;
    }

    private void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
    {
        obj.GetComponent<Transform>().parent = m_XRSpace.transform;
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedDragon.TryGetValue(player, out EnemyPair enemies))
        {
            runner.Despawn(enemies.boss);
            runner.Despawn(enemies.enemy);
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
