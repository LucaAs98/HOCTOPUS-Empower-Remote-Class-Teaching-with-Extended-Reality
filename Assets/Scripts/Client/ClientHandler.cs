using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientHandler : NetworkBehaviour
{
    //Supported devices for client
    private enum Devices
    {
        Android,
        Hololens
    }

    public string playerName; //Client's name
    [SerializeField] private GameObject startingMenu; //Starting menu we want to instantiate when user disconnects 
    [SerializeField] private GameObject raiseArmButton; //Button that client uses for making a question
    [SerializeField] private Devices device; //Client device
    [SerializeField] private GameObject labelButton; //Content text of raiseArmButton
    private bool raisedArm; //Check if arm is reised or not
    [SerializeField] private Material greenMaterialHololens;
    [SerializeField] private Material yellowMaterialHololens;

    private MeshRenderer childRaiseArmButton;

    [SerializeField] private GameObject selectPartPanel;
    [SerializeField] private Transform choosePartBtn;
    [SerializeField] private GameObject exitBtn;
    [SerializeField] private GameObject repositionBtn;
    [SerializeField] private GameObject removeOutlineBtn;
    private string lastOutline = null;

    private GameObject model;

    void Start()
    {
        if (!IsOwner)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            //On start we want to add user to the "connected user list" in server side
            CallAddUserServerRpc(OwnerClientId, playerName);
            if (device == Devices.Android)
                raiseArmButton.GetComponent<Image>().color = new Color32(43, 180, 45, 255);
            else
            {
                //Allow manipulation of the model
                model = GameObject.FindGameObjectWithTag("SpawnedModel");
                if (model != null)
                {
                    model.GetComponent<NearInteractionGrabbable>().enabled = true;
                    model.GetComponent<ObjectManipulator>().enabled = true;
                    model.GetComponent<CursorContextObjectManipulator>().enabled = true;
                    model.GetComponent<ObjectManipulator>().ManipulationType = ManipulationHandFlags.TwoHanded;
                    model.GetComponent<ObjectManipulator>().TwoHandedManipulationType =
                        TransformFlags.Move | TransformFlags.Scale;
                }


                //Init RaiseArmButton
                childRaiseArmButton = NetworkManager.Singleton.GetComponent<StartLesson>()
                    .FindDeepChild(raiseArmButton.transform, "BackgroundQuestionBtn").GetComponent<MeshRenderer>();
                childRaiseArmButton.material = greenMaterialHololens;
            }

            raisedArm = false;
        }
    }

    public void CallRaiseArm(bool flagCall = true)
    {
        raisedArm = !raisedArm;

        if (raisedArm)
        {
            if (device == Devices.Android)
            {
                raiseArmButton.GetComponent<Image>().color = new Color32(227, 224, 50, 255);
                labelButton.GetComponent<TextMeshProUGUI>().text = "Hand down";
            }
            else
            {
                childRaiseArmButton.material = yellowMaterialHololens;
                labelButton.GetComponent<TextMeshPro>().text = "Hand down";
            }
        }
        else
        {
            if (device == Devices.Android)
            {
                raiseArmButton.GetComponent<Image>().color = new Color32(43, 180, 45, 255);
                labelButton.GetComponent<TextMeshProUGUI>().text = "Raise arm";
            }
            else
            {
                childRaiseArmButton.material = greenMaterialHololens;
                labelButton.GetComponent<TextMeshPro>().text = "Raise arm";
            }
        }

        if (flagCall)
            RaiseArmServerRpc(OwnerClientId, raisedArm);
    }

    public void EnableDisableNotificationButton(bool enable)
    {
        raiseArmButton.gameObject.SetActive(enable);
    }

    public string GetPlayerName()
    {
        return playerName;
    }


    public void Exit()
    {
        //We delete the user from server "connected user list" and we disconnect it
        DeleteStudentServerRpc(OwnerClientId);

        if (device == Devices.Android)
        {
            //Then we instantiate again the canvas of the client. Now is ready to put again the name and the lesson code
            Instantiate(startingMenu);
        }
        else if (device == Devices.Hololens)
        {
            //If we are on Hololens we use the ChangeMenu function
            NetworkManager.Singleton.GetComponent<ChangeMenu>().GoToMenu(startingMenu, null, false);
        }
    }

    public void Permission(bool enable)
    {
        exitBtn.SetActive(!enable);
        raiseArmButton.SetActive(!enable);
        choosePartBtn.gameObject.SetActive(enable);
        selectPartPanel.SetActive(false);
        removeOutlineBtn.SetActive(false);

        if (!enable)
        {
            RemoveOutline();
            ResetChoosePartBtn();
        }
    }


    //Every time we click the "ChoosePartBtn"
    public void ChoosePartClick()
    {
        if (!selectPartPanel.activeSelf)
        {
            InitChoosePartBtn();
        }
        else
        {
            ResetChoosePartBtn();
        }
    }

    private void InitChoosePartBtn()
    {
        selectPartPanel.SetActive(true);

        Transform container = selectPartPanel.transform.GetChild(1).transform.GetChild(0);
        LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());

        choosePartBtn.GetComponent<Image>().color = Color.red;
        choosePartBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Close";

        this.GetComponent<MoveSpawnedObj>().enabled = false;
    }

    private void ResetChoosePartBtn()
    {
        selectPartPanel.SetActive(false);
        choosePartBtn.GetComponent<Image>().color = new Color(0, 0.71f, 1);
        choosePartBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Choose part";

        this.GetComponent<MoveSpawnedObj>().enabled = true;
    }

    public void ShowOutline(string partName)
    {
        //Check dell'ultimo outline attivato
        if (lastOutline != null)
        {
            RemoveOutlineServerRpc(lastOutline);
        }

        lastOutline = partName;

        removeOutlineBtn.SetActive(true);


        //Aggiungi bottone per disabilitarla da android


        //Chiediamo al server di mostrarla a tutti
        ShowOutlineServerRpc(partName);
    }

    public void RemoveOutline()
    {
        if (lastOutline != null)
        {
            RemoveOutlineServerRpc(lastOutline);
            lastOutline = null;
            removeOutlineBtn.SetActive(false);
        }
    }

    [ServerRpc]
    private void ShowOutlineServerRpc(string partName)
    {
        GameObject model = GameObject.FindGameObjectWithTag("SpawnedModel");
        GameObject objToOutline = GameObject.Find(partName);

        model.GetComponent<ManageOutline>().AddOutline(objToOutline);
    }

    [ServerRpc]
    private void RemoveOutlineServerRpc(string partName)
    {
        GameObject model = GameObject.FindGameObjectWithTag("SpawnedModel");
        GameObject objToOutline = GameObject.Find(partName);
        model.GetComponent<ManageOutline>().RemoveOutline(objToOutline);
    }

    [ServerRpc]
    private void RaiseArmServerRpc(ulong playerID, bool flagRaisedArm)
    {
        NetworkManager.Singleton.GetComponent<StartLesson>().ModifyUserArm(playerID, flagRaisedArm);
    }

    [ServerRpc]
    private void CallAddUserServerRpc(ulong clientID, string studentName)
    {
        NetworkManager.Singleton.GetComponent<StartLesson>().AddUser(clientID, studentName);
    }

    [ServerRpc]
    public void DeleteStudentServerRpc(ulong clientId)
    {
        NetworkManager.Singleton.GetComponent<StartLesson>().RemoveUser(clientId);
        NetworkManager.Singleton.DisconnectClient(clientId);
    }
}