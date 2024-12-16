using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartHandlerCtrl : MonoBehaviour
{
    [SerializeField] Transform rootTrans;
    [SerializeField] MaterialPropertyBlockEditor materialPropertyBlockEditor;

    private GameObject itemGrab;

    public void SetData(MeshRenderer meshRenderer)
    {
        if (rootTrans.childCount > 0)
        {
            foreach (Transform childRootTrans in rootTrans)
            {
                Destroy(childRootTrans.gameObject);
            }
        }
        materialPropertyBlockEditor.Renderers.Clear();

        materialPropertyBlockEditor.Renderers.Add(meshRenderer);
        meshRenderer.transform.SetParent(rootTrans);

        itemGrab = meshRenderer.gameObject;
    }

    public void WhenSelectObject()
    {
        Debug.LogWarning("WhenSelectObject");
    }
    public void WhenUnSelectObject()
    {
        Debug.LogWarning("WhenUnSelectObject");
    }
}
