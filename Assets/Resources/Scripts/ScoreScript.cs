using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    [SerializeField] int Lifes;
    [SerializeField] int Score;
    [SerializeField] Image Life1;
    [SerializeField] Image Life2;
    [SerializeField] Image Life3;
    [SerializeField] float MinAlpha = 0.5f;
    [SerializeField] float MaxAlpha = 1f;
    [SerializeField] bool Tg1Touched;
    [SerializeField] bool Tg2Touched;
    [SerializeField] bool Tg3Touched;
    [SerializeField] bool Tg4Touched;
    [SerializeField] bool Tg5Touched;



    [SerializeField] TextMeshProUGUI ScoreText;

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

        // if (other.tag == "Target1" && Tg1Touched == false)
        // {
        //     Score += 10;
        //     Tg1Touched = true;
        //     ScoreText.text = "Score: " + Score.ToString();
        // }

        AddScore("Target1", ref Tg1Touched, other);
        AddScore("Target2", ref Tg2Touched, other);
        AddScore("Target2", ref Tg3Touched, other);
        AddScore("Target4", ref Tg4Touched, other);
        AddScore("Target5", ref Tg5Touched, other);
        

    }

    
    
    void Start()
    {
        Lifes = 3;
        Score = 0;
    }


    void Update()
    {
        
        
    }

    public void ResetFunction()
    {
        transform.position = new Vector3(-5.4f, -1.3f, -0.2f);
        transform.rotation = Quaternion.Euler(0, 0, 0);

        Lifes = 3;

        Score = 0;
        ScoreText.text = "Score: " + Score.ToString();

        Color c = Life1.color;
        c.a = MaxAlpha;
        Life1.color = c;

        c = Life2.color;
        c.a = MaxAlpha;
        Life2.color = c;    

        c = Life3.color;
        c.a = MaxAlpha;
        Life3.color = c;

        Tg1Touched = false;
        Tg2Touched = false;
        Tg3Touched = false;
        Tg4Touched = false;
        Tg5Touched = false;

    }

    public void AddScore(string targetTag, ref bool targetTouched, Collider2D other)
    {
       if (other.tag == targetTag && targetTouched == false)
        {
            Score += 10;
            targetTouched = true;
            ScoreText.text = "Score: " + Score.ToString();
        }

       
    }

    
}
