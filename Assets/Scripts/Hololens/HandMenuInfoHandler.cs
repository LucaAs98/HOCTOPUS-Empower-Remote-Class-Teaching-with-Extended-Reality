using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class HandMenuInfoHandler : MonoBehaviour
{
    [SerializeField] private GameObject objToActivate;
    [SerializeField] private Interactable toggleStudentListInteractable;
    [SerializeField] private Interactable toggleManipulateModelInteractable;
    [SerializeField] private GameObject menuModels;
    private Transform studentList = null;
    private GameObject model;

    private void Start()
    {
        model = GameObject.FindGameObjectWithTag("SpawnedModel");
    }

    //Function called from the click of the toggle "ManipulatePermission" in HandMenuInfo
    public void AllowManipulation()
    {
        //We take the boolean value of the toggle
        bool isToggle = toggleManipulateModelInteractable.IsToggled;

        //We enable/disable the components for the manipulation of the model
        model.GetComponent<NearInteractionGrabbable>().enabled = isToggle;
        model.GetComponent<ObjectManipulator>().enabled = isToggle;
        model.GetComponent<CursorContextObjectManipulator>().enabled = isToggle;
    }

    public void DisableManipulation()
    {
        bool isToggle = toggleManipulateModelInteractable.IsToggled;

        if (isToggle)
        {
            toggleManipulateModelInteractable.IsToggled = false;

            AllowManipulation();
        }
    }

    // Call the true function in the root-->manageToggle
    public void ActivateToggleCall()
    {
        if (studentList == null)
        {
            studentList = Instantiate(objToActivate.transform);
        }

        //We take the boolean value of the toggle and we pass it to the function with the gameobj to activate/deactivate
        bool isToggle = toggleStudentListInteractable.IsToggled;

        if (isToggle)
        {
            Transform camTransform = Camera.main.transform;
            Vector3 pos = camTransform.position + camTransform.forward / 2 + (camTransform.right / 4);
            studentList.position = pos;
            studentList.transform.LookAt(camTransform);
            studentList.GetComponent<ManageStudentList>().UpdateStudentList();
            studentList.transform.RotateAround(studentList.transform.position, studentList.transform.up, 180f);
        }

        studentList.gameObject.SetActive(isToggle);
    }

    public void ChooseOtherModel()
    {
        Destroy(model);

        if (studentList != null)
            Destroy(studentList.gameObject);

        this.GetComponent<ChangeMenu>().GoToMenu(menuModels);
    }

    public void CloseMenuButtonClicked()
    {
        toggleStudentListInteractable.IsToggled = false;
        ActivateToggleCall();
    }
}