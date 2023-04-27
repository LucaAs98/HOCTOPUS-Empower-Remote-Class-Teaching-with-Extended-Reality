using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InitNetworkVariables : NetworkBehaviour
{
    private GameObject model;
    private NetworkVariable<Quaternion> diffRotation = new NetworkVariable<Quaternion>(Quaternion.identity);

    private void Start()
    {
        model = GameObject.FindGameObjectWithTag("SpawnedModel");
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer) return;

        diffRotation.OnValueChanged += (Quaternion previousValue, Quaternion newValue) =>
        {
            Debug.Log(OwnerClientId + "; DiffRotationIsChanged: " + diffRotation.Value);
        };
    }

    public void SetModelDiffRotat(Quaternion newRotationToShare)
    {
        Debug.Log("---------------------------------- DiffRotationIsSetted: " + newRotationToShare);
        diffRotation.Value *= newRotationToShare;
    }

    public Quaternion GetDiffRotation()
    {
        return diffRotation.Value;
    }
}