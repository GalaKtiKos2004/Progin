using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstSceneLoader : MonoBehaviour
{
    [SerializeField] string _sceneName;
    
    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}
