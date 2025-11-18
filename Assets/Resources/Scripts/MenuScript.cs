using UnityEngine;

public class MenuScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Scene1Load()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Scene2Load()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void InvokeScene1Load()
    {
        Invoke("Scene1Load", 0.5f);
    }

    public void InvokeScene2Load()
    {
        Invoke("Scene2Load", 0.5f);
    }
}
