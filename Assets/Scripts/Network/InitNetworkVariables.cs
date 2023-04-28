using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Unity.Netcode;
using UnityEngine;

public class InitNetworkVariables : NetworkBehaviour
{
    [SerializeField] private GameObject toggleContainer;

    private NetworkVariable<Quaternion> diffRotation = new NetworkVariable<Quaternion>(Quaternion.identity);

    private void Start()
    {
        if (!IsServer) return;

        NetworkManager.Singleton.OnClientConnectedCallback += (clientID) =>
        {
            InitToggles(clientID);
            ManageSlider manageSliderComponent = this.GetComponent<ManageSlider>();

            if (manageSliderComponent != null)
                manageSliderComponent.ChangeColor();

            InitLabels(clientID);
        };
    }

    private void InitToggles(ulong clientID)
    {
        foreach (Transform toggle in toggleContainer.transform)
        {
            bool isToggle = toggle.GetComponent<Interactable>().IsToggled;
            ActivateToggle activateToggle = toggle.GetComponent<ActivateToggle>();

            if (!isToggle && activateToggle != null)
            {
                GameObject objToActivate = activateToggle.GetObjToActivate();
                this.GetComponent<ManageToggle>().ActivateToggleSpecificClient(isToggle, objToActivate, clientID);
            }
        }
    }

    private void InitLabels(ulong clientID)
    {
        this.GetComponent<ManageTooltips>().ActivateToggleSpecificClient(clientID);
    }

    public void SetModelDiffRotat(Quaternion newRotationToShare)
    {
        diffRotation.Value *= newRotationToShare;
    }

    public Quaternion GetDiffRotation()
    {
        return diffRotation.Value;
    }
}