// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using Mono.CSharp;
using UnityEngine;


public class TouchInteractable : MonoBehaviour, IMixedRealityTouchHandler
{
    #region Event handlers

    public TouchEvent OnTouchCompleted;
    public TouchEvent OnTouchStarted;
    public TouchEvent OnTouchUpdated;

    #endregion

    

    private void Start()
    {
    }

    void IMixedRealityTouchHandler.OnTouchCompleted(HandTrackingInputEventData eventData)
    {
        OnTouchCompleted.Invoke(eventData);
        GameObject.FindGameObjectWithTag("SpawnedModel").GetComponent<ManageOutline>().RemoveOutline(this.gameObject);
    }

    void IMixedRealityTouchHandler.OnTouchStarted(HandTrackingInputEventData eventData)
    {
        OnTouchStarted.Invoke(eventData);
        GameObject.FindGameObjectWithTag("SpawnedModel").GetComponent<ManageOutline>().AddOutline(this.gameObject);
    }

    void IMixedRealityTouchHandler.OnTouchUpdated(HandTrackingInputEventData eventData)
    {
        OnTouchUpdated.Invoke(eventData);
        Debug.Log("Sto toccando: " + this.gameObject.name);
    }
}