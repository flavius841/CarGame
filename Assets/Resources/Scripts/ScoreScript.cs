using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] int Lifes;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DownHorrizontalCollider" || other.tag == "UpHorrizontalCollider"
         || other.tag == "LeftVerticalCollider" || other.tag == "RightVerticalCollider")
        {
            Lifes--;

            if (Lifes == 0)
            {
                transform.position = new Vector3(-5.4f, -1.3f, -0.2f);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                Lifes = 3;
            }
        }

    }

    
    
    void Start()
    {
        Lifes = 3;
    }


    void Update()
    {
        
    }
}
