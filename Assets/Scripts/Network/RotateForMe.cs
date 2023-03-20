using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RotateForMe : NetworkBehaviour
{


    public void Rotate(ulong clientId)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { clientId }
            }
        };

        RotateClientRpc(clientRpcParams);
    }

    [ClientRpc]
    public void RotateClientRpc(ClientRpcParams clientRpcParams = default)
    {
        Debug.Log("ESEGUITO SOLO PER TE!");
    }
}
