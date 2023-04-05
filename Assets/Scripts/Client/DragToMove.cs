using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToMove : MonoBehaviour
{
    private Touch touch;
    private float speedModifier = 0.001f;
    private GameObject spawnedObject;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = GameObject.FindGameObjectWithTag("SpawnedModel");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                spawnedObject.transform.position = new Vector3(spawnedObject.transform.position.x + touch.deltaPosition.x * speedModifier,
                    spawnedObject.transform.position.y, spawnedObject.transform.position.z + touch.deltaPosition.y * speedModifier);
            }
        }
    }
}