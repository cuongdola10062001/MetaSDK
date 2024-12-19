using Oculus.Interaction;
using UnityEngine;

public class PartHandlerCtrl : MonoBehaviour
{

    [SerializeField] private Transform rootTrans;
    [SerializeField] private MaterialPropertyBlockEditor materialPropertyBlockEditor;
    [SerializeField] private PointableUnityEventWrapper _event;
    [SerializeField] private MeshRenderer _meshRenderer;
    private void Start()
    {
        /*_event = GetComponent<PointableUnityEventWrapper>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();*/
        _event.WhenSelect.AddListener(x => WhenSelectObject());
        _event.WhenUnselect.AddListener(x => WhenUnSelectObject());
    }

    public void SetData(MeshRenderer meshRenderer)
    {
        //name
        _meshRenderer.gameObject.name = meshRenderer.gameObject.name;
        //transform
        _meshRenderer.transform.localPosition = meshRenderer.transform.localPosition;
        _meshRenderer.transform.localRotation = meshRenderer.transform.localRotation;
        _meshRenderer.transform.localScale = meshRenderer.transform.localScale;
        //mesh renderer
       
        //mesh filter
        var meshFilterOld = _meshRenderer.gameObject.GetComponent<MeshFilter>();
        var meshFilterNew = meshRenderer.gameObject.GetComponent<MeshFilter>();
        meshFilterOld.mesh = meshFilterNew.sharedMesh;
        meshFilterOld.mesh = meshFilterNew.sharedMesh;
        //mesh collider
        var meshColliderOld = _meshRenderer.gameObject.GetComponent<MeshCollider>();
        var meshColliderNew = meshRenderer.gameObject.GetComponent<MeshCollider>();
        meshColliderOld.sharedMesh = meshColliderNew.sharedMesh;
        meshColliderOld.convex = meshColliderNew.convex;
        //marterial
        Material marter = meshRenderer.material;
        _meshRenderer.material = marter;
        foreach (var mar in _meshRenderer.materials)
        {
            mar.shader = Shader.Find("Standard");
           /* mar.SetFloat("_Mode", 0); 
            mar.ChangeRenderMode(StandardShaderUtils.BlendMode.Opaque);*/
        }
       

    }

    /*private void ClearMeshRenderObj()
    {
        if (rootTrans.childCount > 0)
        {
            foreach (Transform childRootTrans in rootTrans)
            {
                Destroy(childRootTrans.gameObject);
            }
        }
        materialPropertyBlockEditor.Renderers.Clear();
    }
*/
    private void WhenSelectObject()
    {
        Outline outline = _meshRenderer.gameObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = _meshRenderer.gameObject.AddComponent<Outline>();
        }

        outline.OutlineColor = Color.red;
        outline.OutlineWidth = 10f;
        outline.enabled = true;

    }
    private void WhenUnSelectObject()
    {
        Outline outline = _meshRenderer.gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
    }
}
