using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class OnlyOneLabelToggle : MonoBehaviour
{
    [SerializeField] private List<Interactable> toggleList;

    public void CheckToggle()
    {
        bool isSomeoneActive = false;
        foreach (var toggle in toggleList)
        {
            if (toggle.IsToggled)
            {
                isSomeoneActive = true;
                DisableOtherToggle(toggle);
            }
        }

        if (!isSomeoneActive)
        {
            EnableAllToggle();
        }
    }

    private void DisableOtherToggle(Interactable toggleActivated)
    {
        foreach (var toggle in toggleList)
        {
            if (toggle != toggleActivated)
            {
                toggle.enabled = false;
            }
        }
    }

    private void EnableAllToggle()
    {
        foreach (var toggle in toggleList)
        {
            toggle.enabled = true;
        }
    }
}