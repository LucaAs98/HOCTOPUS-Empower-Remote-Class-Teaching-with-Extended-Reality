using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class ClientHandler : NetworkBehaviour
{
    private enum Devices
    {
        Android,
        Hololens
    }

    public string playerName;
    [SerializeField] private GameObject androidCanvas;
    [SerializeField] private GameObject raiseArmButton;
    [SerializeField] private Devices device;
    [SerializeField] private TextMeshProUGUI labelButton;
    private bool raisedArm;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            CallAddUserServerRpc(OwnerClientId, playerName);
            raiseArmButton.GetComponent<Image>().color = new Color32(43, 180, 45, 255);
            raisedArm = false;
        }
    }

    public void CallRaiseArm(bool flagCall = true)
    {
        raisedArm = !raisedArm;
        
        if (device == Devices.Android)
        {
            if (raisedArm) { 
                raiseArmButton.GetComponent<Image>().color = new Color32(227, 224, 50, 255);
                labelButton.text = "Ritira";
            }
            else
            {
                raiseArmButton.GetComponent<Image>().color = new Color32(43, 180, 45, 255);
                labelButton.text = "Domanda";
            }
        }
        else
        {
            //Disattiva hololens button
        }

        if(flagCall)
            RaiseArmServerRpc(OwnerClientId, raisedArm);
    }
    public void EnableDisableNotificationButton(bool enable) {

        raiseArmButton.gameObject.SetActive(enable);
    
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void Exit()
    {
        DeleteStudentServerRpc(OwnerClientId);
        Instantiate(androidCanvas);
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