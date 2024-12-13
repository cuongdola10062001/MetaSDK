using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityGLTF;
using UnityGLTF.Loader;

public class ModelLoader : MonoBehaviour
{
    private bool multithreaded = true;
    private bool hideSceneObjDuringLoad = false;
    private ImporterFactory factory = null;
    private GLTFSceneImporter.ColliderType colliderModel = GLTFSceneImporter.ColliderType.None;
    private GLTFImporterNormals importNormals = GLTFImporterNormals.Import;
    private GLTFImporterNormals importTangents = GLTFImporterNormals.Import;
    private bool swapUVs = false;
    private Shader shaderOverride = null;

    private string nameModel;
    private Vector3 pos = Vector3.zero;
    private Vector3 rot = Vector3.zero;
    private Vector3 scale = Vector3.zero;

    public Shader ShaderOverride
    {
        get => shaderOverride;
        set
        {
            shaderOverride = value;
            ApplyOverrideShader();
        }
    }
    public GameObject LastLoadedScene /*{ get; private set; }*/ = null;
    public UnityAction<string> OnLoadError;
    public UnityAction<GameObject> OnLoadComplete;

    public virtual void SetupModelLoader(ModelLoaderInfo modelLoaderInfo)
    {
        multithreaded = modelLoaderInfo.GLTFSetting.Multithreaded;
        hideSceneObjDuringLoad = modelLoaderInfo.GLTFSetting.HideSceneObjDuringLoad;
        factory = modelLoaderInfo.GLTFSetting.Factory;
        colliderModel = modelLoaderInfo.GLTFSetting.ColliderModel;
        shaderOverride = modelLoaderInfo.GLTFSetting.ShaderOverride;
        importNormals = modelLoaderInfo.GLTFSetting.ImportNormals;
        importTangents = modelLoaderInfo.GLTFSetting.ImportTangents;
        swapUVs = modelLoaderInfo.GLTFSetting.SwapUVs;

        pos = modelLoaderInfo.Position;
        rot = modelLoaderInfo.Rotation;
        scale = modelLoaderInfo.Scale;
    }

    public async Task LoadModel(string url)
    {
        var importOptions = new ImportOptions
        {
            AsyncCoroutineHelper = gameObject.GetComponent<AsyncCoroutineHelper>() ?? gameObject.AddComponent<AsyncCoroutineHelper>(),
            ImportNormals = importNormals,
            ImportTangents = importTangents,
            SwapUVs = swapUVs
        };

        GLTFSceneImporter sceneImporter = null;
        try
        {
            if (!factory) factory = ScriptableObject.CreateInstance<DefaultImporterFactory>();

            string dir = URIHelper.GetDirectoryName(url);
            importOptions.DataLoader = new UnityWebRequestLoader(dir);
            sceneImporter = factory.CreateSceneImporter(
                Path.GetFileName(url),
                importOptions
            );

            sceneImporter.SceneParent = gameObject.transform;
            sceneImporter.Collider = colliderModel;
            sceneImporter.IsMultithreaded = multithreaded;
            sceneImporter.CustomShaderName = shaderOverride ? shaderOverride.name : null;

            await sceneImporter.LoadSceneAsync(
                showSceneObj: !hideSceneObjDuringLoad,
                onLoadComplete: LoadCompleteAction
                            , progress: new Progress<ImportProgress>(p => Debug.Log($"Progress: {p}"))

            );

            ApplyOverrideShader();

            LastLoadedScene = sceneImporter.LastLoadedScene;
            LastLoadedScene.transform.position = new Vector3(pos.x, pos.y, pos.z);
            LastLoadedScene.transform.rotation = Quaternion.Euler(new Vector3(rot.x, rot.y, rot.z));
            LastLoadedScene.transform.localScale = new Vector3(scale.x, scale.y, scale.z);

            if (hideSceneObjDuringLoad)
                LastLoadedScene.SetActive(true);
        }
        catch (Exception ex)
        {
            OnLoadError?.Invoke(ex.Message);
            throw;
        }
        finally
        {
            if (importOptions.DataLoader != null)
            {
                sceneImporter?.Dispose();
                sceneImporter = null;
                importOptions.DataLoader = null;
            }
        }
    }

    private void LoadCompleteAction(GameObject obj, ExceptionDispatchInfo exceptionDispatchInfo)
    {
        if (obj != null)
        {
            OnLoadComplete?.Invoke(obj);
        }
    }

    public void ApplyOverrideShader()
    {
        if (shaderOverride != null)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.sharedMaterial.shader = shaderOverride;
            }
        }
    }
}

public class ModelLoaderInfo
{
    public GLTFSetting GLTFSetting;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
}
public class GLTFSetting
{
    public bool Multithreaded;
    public bool HideSceneObjDuringLoad;
    public ImporterFactory Factory;
    public GLTFSceneImporter.ColliderType ColliderModel;
    public Shader ShaderOverride;
    public GLTFImporterNormals ImportNormals;
    public GLTFImporterNormals ImportTangents;
    public bool SwapUVs;
}