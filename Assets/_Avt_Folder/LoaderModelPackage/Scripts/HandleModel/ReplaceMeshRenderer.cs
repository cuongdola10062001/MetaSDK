using UnityEngine;

public class ReplaceMeshRenderer : MonoBehaviour
{
    public MeshRenderer newMeshRenderer;

    void Start()
    {
        GameObject targetObject = gameObject;

        if (newMeshRenderer != null)
        {
            MeshRenderer oldMeshRenderer = targetObject.GetComponent<MeshRenderer>();
            if (oldMeshRenderer != null)
            {
                Destroy(oldMeshRenderer);
            }

            MeshRenderer addedMeshRenderer = targetObject.AddComponent<MeshRenderer>();

            addedMeshRenderer.materials = newMeshRenderer.materials;
            addedMeshRenderer.enabled = newMeshRenderer.enabled;
            addedMeshRenderer.shadowCastingMode = newMeshRenderer.shadowCastingMode;
            addedMeshRenderer.receiveShadows = newMeshRenderer.receiveShadows;
            addedMeshRenderer.lightProbeUsage = newMeshRenderer.lightProbeUsage;
            addedMeshRenderer.reflectionProbeUsage = newMeshRenderer.reflectionProbeUsage;
        }
        else
        {
            Debug.LogError("New MeshRenderer is not assigned!");
        }
    }
}

