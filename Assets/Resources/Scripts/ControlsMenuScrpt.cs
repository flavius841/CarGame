using UnityEngine;

public class ControlsMenuScrpt : MonoBehaviour
{

    [SerializeField] GameObject controlsMenu;
    [SerializeField] bool active1;
    
    
        
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!active1)
            {
                controlsMenu.SetActive(true);
                active1 = true;
            }
            else
            {
                controlsMenu.SetActive(false);
                active1 = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Invoke("LoadScene0", 0.5f);
        }


    }

    public void LoadScene0()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
