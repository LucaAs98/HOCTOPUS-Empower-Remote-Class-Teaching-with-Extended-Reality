using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuInfoHandler : MonoBehaviour
{
    [SerializeField] private GameObject objToActivate;
    [SerializeField] private GameObject toggle;
    [SerializeField] private GameObject menuModels;
    private Transform objSpawned = null;

    // Call the true function in the root-->manageToggle
    public void ActivateToggleCall()
    {
        if (objSpawned == null)
        {
            objSpawned = Instantiate(objToActivate.transform);
        }

        //We take the boolean value of the toggle and we pass it to the function with the gameobj to activate/deactivate
        bool isToggle = toggle.GetComponent<Interactable>().IsToggled;

        if (isToggle)
        {
            Camera cam = Camera.main;
            Vector3 pos = cam.transform.position + cam.transform.forward + (cam.transform.right / 2) +
                          (cam.transform.up / 2);
            objSpawned.rotation = Quaternion.identity;
            objSpawned.position = pos;
            objSpawned.GetComponent<ManageStudentList>().UpdateStudentList();
        }

        objSpawned.gameObject.SetActive(isToggle);

    }

    public void ChooseOtherModel() {

        GameObject spawnedModel = GameObject.FindGameObjectsWithTag("SpawnedModel")[0];
        Destroy(spawnedModel);
        
        if(objSpawned!= null)
            Destroy(objSpawned.gameObject);

        this.GetComponent<ChangeMenu>().GoToMenu(menuModels);
    }
}
