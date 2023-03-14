using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Unity.Netcode;
using UnityEngine;

public class ManageToggle : NetworkBehaviour
{
    [SerializeField] private List<GameObject> listOfObjToActivate;


    //Called by server in ActivateToggle, we dont call it in client
    public void ActiveDeactivateObj(bool isToggle, GameObject objToActivate)
    {
        //We need to take the index because we cant pass the gameobj to the clientrpc function
        int objIndex = GetIndexFromObj(objToActivate);

        //Main function to activate/deactivate the obj
        ActivateDeactivate(isToggle, objIndex);

        //We excecute the code in every client
        ActivateToggleClientRpc(isToggle, objIndex);
    }

    //Code excecuted in every client
    [ClientRpc]
    private void ActivateToggleClientRpc(bool isToggle, int objIndex)
    {
        //It calls the main function directly
        ActivateDeactivate(isToggle, objIndex);
    }

    private void ActivateDeactivate(bool isToggle, int objIndex)
    {
        //We take the true obj from the serialized list and we activate/deactivate it
        GameObject obj = GetObjFromIndex(objIndex);
        obj.gameObject.SetActive(isToggle);
    }


    //Simple functions for taking the index of the obj from the list and viceversa
    private int GetIndexFromObj(GameObject obj)
    {
        return listOfObjToActivate.IndexOf(obj);
    }

    private GameObject GetObjFromIndex(int index)
    {
        return listOfObjToActivate[index];
    }
}