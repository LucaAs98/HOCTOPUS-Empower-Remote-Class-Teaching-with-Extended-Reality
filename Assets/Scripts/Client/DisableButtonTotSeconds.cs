using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class DisableButtonTotSeconds : MonoBehaviour
{
    private Button button;

    public void StartTimer() //Call this from OnClick
    {
        button = this.GetComponent<Button>();
        button.interactable = false;
        Invoke("EndTimer", 5f);
    }

    private void EndTimer()
    {
        button.interactable = true;
    }
}