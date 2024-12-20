using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGLTF;

public class ModelController : MonoBehaviour
{
    [SerializeField] private GameObject partHandlerPrefab;
    [SerializeField] private GLTFComponent gltfComponent;

    [SerializeField] private MeshRenderer[] meshRenderers;
    [SerializeField] private List<Grabbable> grabbableList = new List<Grabbable>();
    //[SerializeField] private List< Grabbable> grabbableList = new List<Grabbable>();

    private void Start()
    {
        gltfComponent = GetComponent<GLTFComponent>();
        gltfComponent.onLoadComplete += AddComponentGrabbale;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P) || OVRInput.GetUp(OVRInput.Button.One))
        {
            ResetModelToInit();
        }
    }

    private void AddComponentGrabbale()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var item in meshRenderers)
        {
            string nameItem = item.name;
            Transform parentItem = item.transform.parent;
            GameObject partGrabObj = Instantiate(partHandlerPrefab, Vector3.zero, Quaternion.identity);
            var grabbablePart = partGrabObj.GetComponent<Grabbable>();
            grabbableList.Add(grabbablePart);

            partGrabObj.transform.SetParent(parentItem);
            partGrabObj.name = "Grab_" + nameItem;
            partGrabObj.transform.localPosition = Vector3.zero;
            partGrabObj.transform.localScale = Vector3.one;
            var partHandler = partGrabObj.GetComponent<ItemHandlerCtrl>();
            partHandler.SetData(item);
            item.GetComponent<MeshCollider>().enabled = true;
            //item.GetComponent<MeshCollider>().isTrigger = true;

            foreach (Material mar in item.materials)
            {
                mar.shader = Shader.Find("Standard");
                mar.SetInt("_Surface", 1);
                mar.SetFloat("_Mode", 3);
                mar.ChangeRenderMode(StandardShaderUtils.BlendMode.Transparent);

                Color color = mar.color;
                color.a = 0.2f;
                mar.color = color;
            }
        }

        var coroutine = WaitAndTransparentMarterialModel(3.0f);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndTransparentMarterialModel(float waitTime)
    {
        yield return new WaitForSeconds(0.2f);
        foreach (var meshRender in meshRenderers)
        {
            meshRender.GetComponent<MeshCollider>().isTrigger = true;
        }
        yield return new WaitForSeconds(waitTime - 0.2f);
        foreach (var grabbable in grabbableList)
        {
            Rigidbody rigidbody = grabbable.gameObject.GetComponent<Rigidbody>();
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }

    }

    private void ResetModelToInit()
    {
        if (grabbableList.Count <= 0) return;
        foreach (var grabbale in grabbableList)
        {
            grabbale.transform.localPosition = Vector3.zero;
            grabbale.transform.localRotation = Quaternion.Euler(Vector3.zero);
            grabbale.transform.localScale = Vector3.one;
        }
    }
}
