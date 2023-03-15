using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ManageTooltips : NetworkBehaviour
{
    [SerializeField] private GameObject containerTooltipsToggle;
    private GameObject[] listOfObjWithTooltips;
    private bool wasActive = false;

    void Start()
    {
        listOfObjWithTooltips = GameObject.FindGameObjectsWithTag("Layer");
    }

    //Called by server in ActivateToggle, we dont call it in client
    public void ActiveDeactivateTooltips(GameObject objWithTooltips)
    {
        wasActive = objWithTooltips.transform.Find("Tooltips").gameObject.activeSelf;

        containerTooltipsToggle.GetComponent<OnlyOneLabelToggle>().CheckToggle(wasActive);

        //We need to take the index because we cant pass the gameobj to the clientrpc function
        int objIndex = GetIndexFromObj(objWithTooltips);

        //Main function to activate/deactivate the obj
        ActivateDeactivateBase(objIndex);

        //We excecute the code in every client
        ActivateToggleClientRpc(objIndex);
    }

    //Code excecuted in every client
    [ClientRpc]
    private void ActivateToggleClientRpc(int objIndex)
    {
        //It calls the main function directly
        ActivateDeactivateBase(objIndex);
    }

    private void ActivateDeactivateBase(int objIndex)
    {
        //We take the true obj from the serialized list and we activate/deactivate it
        GameObject obj = GetObjFromIndex(objIndex);
        Transform tooltipsContainer = obj.transform.Find("Tooltips");

        if (!wasActive)
        {
            foreach (var objWithTooltip in listOfObjWithTooltips)
            {
                if (objWithTooltip != obj)
                {
                    Debug.Log(objWithTooltip);
                    Transform tooltip = objWithTooltip.transform.Find("Tooltips");
                    if (tooltip != null)
                        tooltip.gameObject.SetActive(false);
                }
            }
        }

        tooltipsContainer.gameObject.SetActive(!wasActive);
    }


    //Simple functions for taking the index of the obj from the list and viceversa
    private int GetIndexFromObj(GameObject obj)
    {
        return Array.IndexOf(listOfObjWithTooltips, obj);
    }

    private GameObject GetObjFromIndex(int index)
    {
        return listOfObjWithTooltips[index];
    }
}