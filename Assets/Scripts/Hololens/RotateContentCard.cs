using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateContentCard : MonoBehaviour
{
    [SerializeField] private GameObject objToRotate;
    [SerializeField] private float speed = 100;
    [SerializeField] public Transform model2Spawn;

    void OnEnable()
    {
        foreach (var component in GameObject.FindObjectsOfType<RotateContentCard>())
        {
            if (component != this.GetComponent<RotateContentCard>())
            {
                component.enabled = false;
                component.objToRotate.gameObject.transform.rotation = new Quaternion(0,0,0,0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        objToRotate.transform.Rotate(Vector3.up * Time.deltaTime * speed);
    }
}