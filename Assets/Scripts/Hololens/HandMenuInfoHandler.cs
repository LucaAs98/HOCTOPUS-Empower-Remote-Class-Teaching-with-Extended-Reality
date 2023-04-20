using Microsoft.MixedReality.Toolkit.UI;
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
            Transform camTransform = Camera.main.transform;
            Vector3 pos = camTransform.position + camTransform.forward / 2 + (camTransform.right / 4) +
                          (camTransform.up / 4);
            objSpawned.position = pos;
            objSpawned.transform.LookAt(camTransform);
            objSpawned.GetComponent<ManageStudentList>().UpdateStudentList();
            objSpawned.transform.RotateAround(objSpawned.transform.position, objSpawned.transform.up, 180f);
        } 

        objSpawned.gameObject.SetActive(isToggle);
    }

    public void ChooseOtherModel()
    {
        GameObject spawnedModel = GameObject.FindGameObjectsWithTag("SpawnedModel")[0];
        Destroy(spawnedModel);

        if (objSpawned != null)
            Destroy(objSpawned.gameObject);

        this.GetComponent<ChangeMenu>().GoToMenu(menuModels);
    }

    public void CloseMenuButtonClicked()
    {
        toggle.GetComponent<Interactable>().IsToggled = false;
        ActivateToggleCall();
    }
}