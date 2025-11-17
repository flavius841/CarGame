using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] int Lifes;
    [SerializeField] Image Life1;
    [SerializeField] Image Life2;
    [SerializeField] Image Life3;
    [SerializeField] float MinAlpha = 0.5f;
    [SerializeField] float MaxAlpha = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DownHorrizontalCollider" || other.tag == "UpHorrizontalCollider"
         || other.tag == "LeftVerticalCollider" || other.tag == "RightVerticalCollider")
        {
            Lifes--;

            if (Lifes == 2)
            {
                Color c = Life1.color;
                c.a = MinAlpha;
                Life1.color = c;
            }

            if (Lifes == 1)
            {
                Color c = Life2.color;
                c.a = MinAlpha;
                Life2.color = c;
            }

            if (Lifes == 0)
            {
                Color c = Life3.color;
                c.a = MinAlpha;
                Life3.color = c;

                Invoke("ResetFunction", 0.5f);
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

    public void ResetFunction()
    {
        transform.position = new Vector3(-5.4f, -1.3f, -0.2f);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        Lifes = 3;

        Color c = Life1.color;
        c.a = MaxAlpha;
        Life1.color = c;

        c = Life2.color;
        c.a = MaxAlpha;
        Life2.color = c;    

        c = Life3.color;
        c.a = MaxAlpha;
        Life3.color = c;

    }
}
