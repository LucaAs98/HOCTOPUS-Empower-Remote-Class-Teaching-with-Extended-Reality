using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class DisableButtonTotSeconds : MonoBehaviour
{
    private enum Devices
    {
        Android,
        Hololens
    }

    [SerializeField] private Devices device;

    private Button button;
    private Interactable interactableBtn;

    public void StartTimer() //Call this from OnClick
    {
        if (device == Devices.Android)
        {
            button = this.GetComponent<Button>();
        button.interactable = false;
        }
        else
        {
            interactableBtn = this.GetComponent<Interactable>();
            interactableBtn.enabled = false;
        }
        
        Invoke("EndTimer", 5f);
    }

    private void EndTimer()
    {
        if (device == Devices.Android)
        {
            button.interactable = true;
        }
        else
        {
            interactableBtn.enabled = true;
        }
    }
}