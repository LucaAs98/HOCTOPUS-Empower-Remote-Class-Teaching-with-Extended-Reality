using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Spawner : NetworkBehaviour
{
    [SerializeField] public List<GameObject> listClientPrefabs;

    [ServerRpc(RequireOwnership = false)]
    public void JoinServerRpc(ulong clientId, int platform)
    {
        Debug.Log("Sono entrato serverrpc - IsServer: " + IsServer);
        GameObject tempGO = (GameObject)Instantiate(listClientPrefabs[platform]);
        NetworkObject netObj = tempGO.GetComponent<NetworkObject>();

        netObj.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
    }

    public override void OnNetworkSpawn()
    {
        Debug.Log("Sono entrato OnNetworkSpawn - IsServer: " + IsServer);
        if (Application.platform == RuntimePlatform.Android)
            JoinServerRpc(NetworkManager.Singleton.LocalClientId, 0);
        else
            JoinServerRpc(NetworkManager.Singleton.LocalClientId, 1);
    }
}
