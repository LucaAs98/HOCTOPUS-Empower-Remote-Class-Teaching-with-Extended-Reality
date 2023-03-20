using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InitClient : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) 
            this.gameObject.SetActive(false);

        // if (!IsOwner)
        //     this.gameObject.GetComponentInChildren<Camera>().enabled = false;

    }
}