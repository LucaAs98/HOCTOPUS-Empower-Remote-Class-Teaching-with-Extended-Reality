using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageToggle : MonoBehaviour
{
    [SerializeField] private GameObject objToActivate;

    public void ActiveDeactivateObj()
    {
        objToActivate.gameObject.SetActive(!objToActivate.activeSelf);
    }
}
