using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ManageOutline : NetworkBehaviour
{
    private GameObject[] interactableObj;

    void Start()
    {
        interactableObj = GameObject.FindGameObjectsWithTag("Interactable");
    }

    public void EnableDisableComponent(GameObject objToOutline)
    {
        int index = GetIndexFromObj(objToOutline);
        EnableDisableBase(index);
        EnableDisableClientRpc(index);
    }

    [ClientRpc]
    private void EnableDisableClientRpc(int index)
    {
        EnableDisableBase(index);
    }

    private void EnableDisableBase(int index)
    {
        MonoBehaviour objOutline = GetObjFromIndex(index).GetComponent<Outline>();
        objOutline.enabled = !objOutline.enabled;
    }


    //Simple functions for taking the index of the obj from the list and viceversa
    private int GetIndexFromObj(GameObject obj)
    {
        return Array.IndexOf(interactableObj, obj);
    }

    private GameObject GetObjFromIndex(int index)
    {
        return interactableObj[index];
    }

}