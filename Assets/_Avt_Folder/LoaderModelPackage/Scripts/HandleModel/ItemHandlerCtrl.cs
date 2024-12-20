using Oculus.Interaction;
using UnityEngine;

public class ItemHandlerCtrl : MonoBehaviour
{
    [SerializeField] private Transform rootTrans;
    [SerializeField] private MaterialPropertyBlockEditor materialPropertyBlockEditor;
    [SerializeField] private PointableUnityEventWrapper _event;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ItemAssemble _itemAssemble;

    /*private Vector3 forceDirection = new Vector3(0, 1, 0);
    private float forceMagnitude = 100f;
    private Rigidbody rb;*/

    private bool isGrab = false;
    public bool IsGrab => isGrab;

    private void Start()
    {
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
        }

        //ItemAssemble itemAssemble= _meshRenderer.gameObject.GetComponent<ItemAssemble>();
        _itemAssemble.SetTargetObj(meshRenderer.gameObject);

    }

    private void WhenSelectObject()
    {
        isGrab = true;

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
        isGrab = false;

        Outline outline = _meshRenderer.gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }

        float distance = _itemAssemble.DistanceObjGrabAndItemModel();
        if (distance < 0.2f)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
