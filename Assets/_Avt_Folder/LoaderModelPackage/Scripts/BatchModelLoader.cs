using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityGLTF;

public class BatchModelLoader : MonoBehaviour
{
    public UnityAction<List<GameObject>> OnAllLoadCompleted;
    public UnityAction OnlyLoadCompleted;
    public UnityAction<string> OnAnyModelError;

    private Dictionary<string, GameObject> modelCache = new Dictionary<string, GameObject>();
    private List<GameObject> loadedModels = new List<GameObject>();
    private int totalToLoad = 0;
    private int totalLoaded = 0;
    private int totalErrors = 0;

    public async void LoadModelsCallback(List<string> urls, UnityAction action)
    {
        await LoadModels(urls);
        action?.Invoke();
    }

    public async Task LoadModels(List<string> urls, ModelLoaderInfo modelLoaderInfo = null)
    {
        totalToLoad = urls.Count;
        totalLoaded = 0;
        totalErrors = 0;
        loadedModels.Clear();
        int x = 0;

        foreach (var url in urls)
        {
            //setting pos,rot,scale
            Vector3 pos = new Vector3(x, 0, 0);
            Vector3 rot = Vector3.zero;
            Vector3 scale = Vector3.one;
            x++;


            if (modelCache.ContainsKey(url))
            {
                GameObject modelLoaderObj = Instantiate(modelCache[url], pos, Quaternion.identity);
                var modelLoader = modelLoaderObj.GetComponent<ModelLoader>();
                modelLoader.LastLoadedScene = modelLoaderObj.transform.GetChild(0).gameObject;
                modelLoaderObj.transform.SetParent(transform);
                loadedModels.Add(modelLoaderObj);
                totalLoaded++;
                CheckAllLoaded();
                continue;
            }

            var loaderObject = new GameObject("ModelLoader");
            loaderObject.transform.parent = gameObject.transform;

            var loader = loaderObject.AddComponent<ModelLoader>();

            loader.OnLoadComplete = (model) =>
            {
                modelCache[url] = model.transform.parent.gameObject;
                loadedModels.Add(model.transform.parent.gameObject);
                totalLoaded++;

                OnlyLoadCompleted?.Invoke();

                CheckAllLoaded();
            };

            loader.OnLoadError = (error) =>
            {
                totalErrors++;
                OnAnyModelError?.Invoke(error);
                CheckAllLoaded();
            };

            if (modelLoaderInfo == null)
                modelLoaderInfo = new ModelLoaderInfo
                {
                    GLTFSetting = new GLTFSetting
                    {
                        Multithreaded = true,
                        HideSceneObjDuringLoad = true,
                        Factory = null,
                        ColliderModel = GLTFSceneImporter.ColliderType.Box,
                        ShaderOverride = null,
                        ImportNormals = GLTFImporterNormals.Import,
                        ImportTangents = GLTFImporterNormals.Import,
                        SwapUVs = false,
                    },
                    Position = pos,
                    Rotation = rot,
                    Scale = scale,
                };

            loader.SetupModelLoader(modelLoaderInfo);

            try
            {
                await loader.LoadModel(url);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load model at {url}: {ex.Message}");
            }

        }
    }

    private void CheckAllLoaded()
    {
        if (totalLoaded + totalErrors >= totalToLoad)
        {
            if (totalErrors == 0)
            {
                OnAllLoadCompleted?.Invoke(loadedModels);
            }
            else
            {
                Debug.LogError("Some models failed to load.");
            }
        }
    }

    public void ClearCache()
    {
        foreach (var model in loadedModels)
        {
            Destroy(model);
        }
        modelCache.Clear();
        totalLoaded = 0;
        totalErrors = 0;
        loadedModels.Clear();
    }
}
