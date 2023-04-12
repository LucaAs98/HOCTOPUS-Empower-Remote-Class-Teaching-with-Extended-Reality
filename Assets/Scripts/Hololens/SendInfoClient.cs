using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SendInfoClient : NetworkBehaviour
{
    [ClientRpc]
    public void RemoveNotificationClientRpc(ClientRpcParams clientRpcParams = default)
    {
        GameObject student = GameObject.Find("Student(Clone)");
        if (student != null)
            student.GetComponent<ClientHandler>().CallRaiseArm(false);
        else
            Debug.Log("Non trovato uffa!");
    }

    [ClientRpc]
    public void EnableDisableClientRpc(bool enable, ClientRpcParams clientRpcParams = default)
    {
        GameObject student = GameObject.Find("Student(Clone)");
        if (student != null)
            student.GetComponent<ClientHandler>().EnableDisableNotificationButton(enable);
        else
            Debug.Log("Non trovato di nuovo uffa!");
    }
}
