using UnityEngine;

public class ModelController : MonoBehaviour
{
    [SerializeField] private GameObject partHandlerPrefab;
    private void Start()
    {
        /*var meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (var item in meshRenderers)
        {
            string nameItem = item.name;
            Transform parentItem = item.transform.parent;
            GameObject partGrabObj = Instantiate(partHandlerPrefab, Vector3.zero, Quaternion.identity);
            partGrabObj.transform.SetParent(parentItem);
            partGrabObj.name = "Grab_" + nameItem;
            partGrabObj.transform.localPosition = Vector3.zero;
            partGrabObj.transform.localScale = Vector3.one;
            var partHandler = partGrabObj.GetComponent<PartHandlerCtrl>();
            partHandler.SetData(item);
        }*/
    }
}
