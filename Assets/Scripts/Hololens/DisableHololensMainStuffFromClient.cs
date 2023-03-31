using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableHololensMainStuffFromClient : MonoBehaviour
{
    private GameObject notificationGroup;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("NotificationGroup").SetActive(false);
    }
}
