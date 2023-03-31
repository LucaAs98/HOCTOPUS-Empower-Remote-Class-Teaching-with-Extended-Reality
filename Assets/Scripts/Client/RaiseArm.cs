using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class RaiseArm : NetworkBehaviour
{
    public void CallRaiseArm()
    {
        Debug.Log("Manda notifica da client");
        RaiseArmServerRpc(this.GetComponent<InitClient>().GetPlayerName());
    }

    [ServerRpc]
    public void RaiseArmServerRpc(string playerName)
    {
        Debug.Log("RIchiesta al server di notifica");
        NetworkManager.Singleton.GetComponent<ManageNotification>().AddNotification(playerName);
    }
}