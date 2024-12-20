using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssemble : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;
    public void SetTargetObj(GameObject obj)
    {
        targetObj = obj;
    }

    void Start()
    {
        
    }

    void Update()
    {

        
    }

    public float DistanceObjGrabAndItemModel()
    {
        if (targetObj == null) return 999999999999999;

        Renderer renderer1 = gameObject.GetComponent<Renderer>();
        Renderer renderer2 = targetObj.GetComponent<Renderer>();

        if (renderer1 != null && renderer2 != null)
        {
            Vector3 center1 = renderer1.bounds.center;
            Vector3 center2 = renderer2.bounds.center;

            float distance = Vector3.Distance(center1, center2);

            Debug.LogWarning("distance: " + distance);
            return distance;
        }
        return 999999999999999;
    }
}
