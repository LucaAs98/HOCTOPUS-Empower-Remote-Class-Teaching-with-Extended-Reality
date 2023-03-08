using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuToSpawn;
    public void GoToMenu()
    {
        Transform oldTransform = this.transform;
        this.gameObject.SetActive(false);
        menuToSpawn.transform.rotation = oldTransform.rotation;
        menuToSpawn.transform.position = oldTransform.position;
        Instantiate(menuToSpawn);
    }
}
