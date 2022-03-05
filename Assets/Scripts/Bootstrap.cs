using RoboRyanTron.SceneReference;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SceneReference menuScene;
    [SerializeField] private Camera cameraPrefab;
    
    private void Start()
    {
        menuScene.LoadSceneAsync().completed += GenerateInitialData;
    }

    private void GenerateInitialData(AsyncOperation _)
    {
        DontDestroyOnLoad(Instantiate(cameraPrefab));
    }
}