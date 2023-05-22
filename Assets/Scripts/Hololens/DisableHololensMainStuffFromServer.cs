using Unity.Netcode;
using UnityEngine;

public class DisableHololensMainStuffFromServer : NetworkBehaviour
{
    [SerializeField] private GameObject handMenuToggles;
    [SerializeField] private GameObject menuDeepSkeleton;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            handMenuToggles.SetActive(false);
        }
        else {

            Transform parentUIHololens = GameObject.Find("UIHololens").transform;
            handMenuToggles.transform.SetParent(parentUIHololens, true);
            menuDeepSkeleton.transform.SetParent(parentUIHololens, true);
        }
    }
}
