using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ManageTooltips : NetworkBehaviour
{
    [SerializeField] private GameObject containerTooltipsToggle;
    [SerializeField] private List<GameObject> listOfObjWithTooltips;


    //Called by server in ActivateToggle, we dont call it in client
    public void ActiveDeactivateTooltips(GameObject objWithTooltips)
    {
        //We need to take the index because we cant pass the gameobj to the clientrpc function
        int objIndex = GetIndexFromObj(objWithTooltips);

        //Main function to activate/deactivate the obj
        bool wasActive = CheckActive(objIndex);

        ActivateDeactivateBase(objIndex, wasActive);

        //We excecute the code in every client
        ActivateToggleClientRpc(objIndex, wasActive);
    }

    //Code excecuted in every client
    [ClientRpc]
    private void ActivateToggleClientRpc(int objIndex, bool wasActive)
    {
        //It calls the main function directly
        ActivateDeactivateBase(objIndex, wasActive);
    }

    private bool CheckActive(int objIndex)
    {
        //We take the true obj from the serialized list and we activate/deactivate it
        GameObject obj = GetObjFromIndex(objIndex);
        bool wasActive = obj.transform.Find("Tooltips").gameObject.activeSelf;
        containerTooltipsToggle.GetComponent<OnlyOneLabelToggle>().CheckToggle(wasActive);
        return wasActive;
    }

    private void ActivateDeactivateBase(int objIndex, bool wasActive)
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
                    Transform tooltip = objWithTooltip.transform.Find("Tooltips");
                    tooltip.gameObject.SetActive(false);
                }
            }
        }

        tooltipsContainer.gameObject.SetActive(!wasActive);
    }

    //Simple functions for taking the index of the obj from the list and viceversa
    private int GetIndexFromObj(GameObject obj)
    {
        return listOfObjWithTooltips.IndexOf(obj);
    }

    private GameObject GetObjFromIndex(int index)
    {
        return listOfObjWithTooltips[index];
    }
}