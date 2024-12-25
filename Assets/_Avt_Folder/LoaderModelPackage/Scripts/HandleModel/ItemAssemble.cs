using UnityEngine;

public class ItemAssemble : MonoBehaviour
{
    [SerializeField] private GameObject targetObj;
    public void SetTargetObj(GameObject obj)
    {
        targetObj = obj;
    }

    [SerializeField] private float distanceGobal;

    [SerializeField] private float angleForward;
    [SerializeField] private float angleRight;
    [SerializeField] private float angleUp;

    private float valueAngleDesired = 60f;


    void Start()
    {

    }

    void Update()
    {
        distanceGobal = DistanceObjGrabAndItemModel();

        AngleSatisfiedObjGrabAndItemModel();
        /*if (targetObj == null) return;

        Renderer renderer1 = gameObject.GetComponent<Renderer>();
        Renderer renderer2 = targetObj.GetComponent<Renderer>();

        Vector3 positionA = renderer1.transform.position;
        Vector3 positionB = renderer2.transform.position;

        Vector3 directionA = positionA.normalized; 
        Vector3 directionB = positionB.normalized;

        angle = Vector3.Angle(directionA, directionB);
        Debug.Log("Góc giữa Object A và Object B: " + angle + " độ");*/
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

            return distance;
        }
        return 999999999999999;
    }

    public bool AngleSatisfiedObjGrabAndItemModel()
    {
        if (targetObj == null) return false;

        Renderer renderer1 = gameObject.GetComponent<Renderer>();
        Renderer renderer2 = targetObj.GetComponent<Renderer>();

        if (renderer1 != null && renderer2 != null)
        {
            /*float angleForward = Vector3.Angle(renderer1.transform.forward, renderer2.transform.forward);
            float angleRight= Vector3.Angle(renderer1.transform.right, renderer2.transform.right);
            float angleUp= Vector3.Angle(renderer1.transform.up, renderer2.transform.up);*/

            angleForward = Vector3.Angle(renderer1.transform.forward, renderer2.transform.forward);
            angleRight = Vector3.Angle(renderer1.transform.right, renderer2.transform.right);
            angleUp = Vector3.Angle(renderer1.transform.up, renderer2.transform.up);

            if (angleForward <= valueAngleDesired && angleRight <= valueAngleDesired && angleUp <= valueAngleDesired)
            {
                return true;
            }
        }

        return false;
    }
}
