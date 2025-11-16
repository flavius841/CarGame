using UnityEngine;

public class ControlsMenuScrpt : MonoBehaviour
{

    [SerializeField] GameObject controlsMenu;
    [SerializeField] bool active;
    
        
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!active)
            {
                controlsMenu.SetActive(true);
                active = true;
            }
            else
            {
                controlsMenu.SetActive(false);
                active = false;
            }
        }
    }
}
