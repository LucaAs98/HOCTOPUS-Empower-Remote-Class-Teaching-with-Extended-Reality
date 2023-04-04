using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ManageOutline : NetworkBehaviour
{
    [SerializeField] private List<GameObject> interactableObj;

    private void EnableDisableComponent(GameObject objToOutline, bool activate)
    {
        int index = GetIndexFromObj(objToOutline);
        EnableDisableBase(index, activate);
        EnableDisableClientRpc(index, activate);
    }

    [ClientRpc]
    private void EnableDisableClientRpc(int index, bool activate)
    {
        EnableDisableBase(index, activate);
    }

    private void EnableDisableBase(int index, bool activate)
    {
        GetObjFromIndex(index).GetComponent<Outline>().enabled = activate;
    }


    //Simple functions for taking the index of the obj from the list and viceversa
    private int GetIndexFromObj(GameObject obj)
    {
        return interactableObj.IndexOf(obj);
    }

    private GameObject GetObjFromIndex(int index)
    {
        return interactableObj[index];
    }


    public void RemoveOutline(GameObject obj)
    {
        EnableDisableComponent(obj, false);
    }

    public void AddOutline(GameObject obj)
    {
        EnableDisableComponent(obj, true);
    }
}