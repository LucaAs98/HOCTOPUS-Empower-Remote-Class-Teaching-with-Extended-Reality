using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InitClient : NetworkBehaviour
{
    public string playerName;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            CallAddUserServerRpc(playerName);
        }
    }


    [ServerRpc]
    private void CallAddUserServerRpc(string studentName)
    {
        NetworkManager.Singleton.GetComponent<StartLesson>().AddUser(studentName);
    }
}