using Oculus.Interaction;
using UnityEngine;

public class PartHandlerCtrl : MonoBehaviour
{
    private PointableUnityEventWrapper _event;
    private MeshRenderer _meshRenderer;
    private void Start()
    {
        _event= GetComponent<PointableUnityEventWrapper>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();

        _event.WhenSelect.AddListener(x => WhenSelectObject());
        _event.WhenUnselect.AddListener(x => WhenUnSelectObject());
    }


    /*[SerializeField] Transform rootTrans;
    [SerializeField] MaterialPropertyBlockEditor materialPropertyBlockEditor;

    private GameObject itemGrab;

    public void SetData(MeshRenderer meshRenderer)
    {
        ClearMeshRenderObj();
        string nameMesh= meshRenderer.gameObject.name;
        Vector3 posMesh = meshRenderer.transform.localPosition;
        Quaternion rotMesh = meshRenderer.transform.localRotation;
        Vector3 scaleMesh = meshRenderer.transform.localScale;

        GameObject meshObj = Instantiate(meshRenderer.gameObject);

        materialPropertyBlockEditor.Renderers.Add(meshRenderer);
        meshObj.transform.SetParent(rootTrans);
        meshObj.name = nameMesh;
        meshObj.transform.localPosition = posMesh;
        meshObj.transform.localRotation = rotMesh;
        meshObj.transform.localScale = scaleMesh;

        itemGrab = meshObj.gameObject;
    }

    private void ClearMeshRenderObj()
    {
        if (rootTrans.childCount > 0)
        {
            foreach (Transform childRootTrans in rootTrans)
            {
                Destroy(childRootTrans.gameObject);
            }
        }
        materialPropertyBlockEditor.Renderers.Clear();
    }*/

    public void WhenSelectObject()
    {
        Debug.LogWarning("WhenSelectObject");
        Outline outline = _meshRenderer.gameObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = _meshRenderer.gameObject.AddComponent<Outline>();
        }

        outline.OutlineColor = Color.red;
        outline.OutlineWidth = 10f;
        outline.enabled = true;

    }
    public void WhenUnSelectObject()
    {
        Outline outline = _meshRenderer.gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
        Debug.LogWarning("WhenUnSelectObject");
    }
}
