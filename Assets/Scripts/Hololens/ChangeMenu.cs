using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    //[SerializeField] private GameObject menuToSpawn;
    public void GoToMenu(GameObject menuToSpawn)
    {
      
        GameObject parent = GameObject.Find("UIHololens");
        Transform pTrans = parent != null ? parent.transform : null;    
        GameObject menu = Instantiate(menuToSpawn, pTrans);
        Transform tranCam = Camera.main.transform;
        menu.transform.position = tranCam.position + tranCam.forward/2;
        menu.transform.LookAt(tranCam);
        menu.transform.RotateAround(menu.transform.position, menu.transform.up, 180f);
        Destroy(gameObject);
    }
}
