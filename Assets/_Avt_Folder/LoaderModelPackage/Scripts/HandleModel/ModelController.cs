using UnityEngine;

public class ModelController : MonoBehaviour
{
    private MeshRenderer[] meshRenderers;
    [SerializeField] private GameObject partHandlerPrefab;
    private void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (var item in meshRenderers)
        {
            string nameItem = item.name;
            Transform parentItem = item.transform.parent;
            GameObject partGrabObj = Instantiate(partHandlerPrefab, Vector3.zero, Quaternion.identity);
            partGrabObj.name = "Grab_" + nameItem;
            partGrabObj.transform.SetParent(parentItem);
            var partHandler = partGrabObj.GetComponent<PartHandlerCtrl>();
            partHandler.SetData(item);
        }
    }
}
