using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ExitAndroid : NetworkBehaviour
{

    [SerializeField] private GameObject androidCanvas;

    public void Exit()
    {
        DeleteStudentServerRpc(this.GetComponent<InitClient>().playerName, OwnerClientId);
        Instantiate(androidCanvas);
    }

    [ServerRpc]
    public void DeleteStudentServerRpc(string playerName, ulong clientId)
    {
        Debug.Log("1: " + playerName);
        NetworkManager.Singleton.GetComponent<StartLesson>().RemoveUser(playerName);
        NetworkManager.Singleton.DisconnectClient(clientId);
    }
}