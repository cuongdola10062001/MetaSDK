using System.Collections.Generic;
using UnityEngine;

public class TestModelLoader : MonoBehaviour
{
    public BatchModelLoader batchLoader;

    private List<string> modelUrls = new List<string>()
        {
            "https://modelviewer.dev/shared-assets/models/Astronaut.glb",
            "https://modelviewer.dev/shared-assets/models/Astronaut.glb",
            "https://modelviewer.dev/shared-assets/models/Astronaut.glb",
            "https://modelviewer.dev/shared-assets/models/Astronaut.glb",
            "https://modelviewer.dev/shared-assets/models/Astronaut.glb",
            "https://modelviewer.dev/shared-assets/models/Astronaut.glb",
        };
    void Start()
    {

        batchLoader.OnAllLoadCompleted = (models) =>
        {
            Debug.Log($"All models loaded: {models.Count}");
        };

        batchLoader.OnAnyModelError = (error) =>
        {
            Debug.LogError($"Error loading model: {error}");
        };

        batchLoader.LoadModelsCallback(modelUrls, () =>
        {
            Debug.Log("callback load model !");
        });
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            batchLoader.ClearCache();
        }
    }


}
