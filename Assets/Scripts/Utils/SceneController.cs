using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, Inventory currentInventory)
    {
        GameSession.Instance.SaveInventory(currentInventory);
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Inventory newInventory = FindObjectOfType<Inventory>();

        if (newInventory != null)
        {
            GameSession.Instance.LoadInventory(newInventory);
        }
    }
}
