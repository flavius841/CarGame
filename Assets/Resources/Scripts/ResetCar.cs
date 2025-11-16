using UnityEngine;

public class ResetCar : MonoBehaviour
{

    [SerializeField] GameObject Car;
    
    void Start()
    {
        
    }

    
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.R))
       {
            Car.transform.position = new Vector3(0, -2, 0);
            Car.transform.rotation = Quaternion.Euler(0, 0, 0);
       }   
    }


}
