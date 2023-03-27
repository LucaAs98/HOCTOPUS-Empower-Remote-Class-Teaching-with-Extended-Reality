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
        RaiseArmServerRpc(this.GetComponent<InitClient>().GetPlayerName());
    }

    [ServerRpc]
    public void RaiseArmServerRpc(string playerName)
    {
        NetworkManager.Singleton.GetComponent<ManageNotification>().AddNotification(playerName);
    }
}