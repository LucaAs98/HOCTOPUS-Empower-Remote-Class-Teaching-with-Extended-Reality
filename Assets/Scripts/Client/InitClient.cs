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
        if (!IsOwner) return;

        this.gameObject.GetComponent<Camera>().enabled = true;
        this.gameObject.GetComponent<AudioListener>().enabled = true;
        this.gameObject.GetComponent<ARPoseDriver>().enabled = true;
        this.gameObject.GetComponent<ARCameraBackground>().enabled = true;
    }
}