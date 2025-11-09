using UnityEngine;
using System;

public class ControlingThePlayer : MonoBehaviour
{
    public float Speed_Rot_Neg, Speed_Rot_Poz, Speed, MaxSpeedPoz, MaxSpeedNeg;
    public float Acc, Inerty, brake;
    public bool StartDecreasing;
    public GameObject Pivot;
    [SerializeField] bool CrashedFront;
    [SerializeField] bool CrashedBack;

    [SerializeField] bool PartialCrashed;
    [SerializeField] bool StartGoingBackwards;
    [SerializeField]  bool StartGoingForwards;

    //[SerializeField] bool TouchedTrack;

    [SerializeField] GameObject HorrizontalCollider;
    [SerializeField] float DectectingDistance;
    [SerializeField] float NewPoz_RotZ;
    [SerializeField] float Current_RotZ;
    private float z;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DownHorrizontalCollider")
        {

            z = transform.eulerAngles.z;
            if (z > 180) z -= 360;

            
            if (z > -180 && z < 0)
            {
                ColliderFunction(-140, -50, 30, 10, val => val < -140, val => val > 2, ref CrashedFront);
            }
            


        }
        
        if (other.tag == "UpHorrizontalCollider")
        {
            
            z = transform.eulerAngles.z;
            if (z > 180) z -= 360;

            if (z < 180 && z > 0)
            {
                ColliderFunction(50, 140, -30, -10, val => val > 140, val => val > 2, ref CrashedFront);
            }

            else
            {
                ColliderFunction(-120, -50, -30, -10, val => val > -40,  val => val < -2, ref CrashedBack);
            }
            
            
            
        }

    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && Speed > 0)
         || ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && Speed < 0))
        {
            StartDecreasing = true;
            Slow(brake);
        }

        if (!Input.GetKey(KeyCode.Space) && !StartGoingBackwards && !StartGoingForwards)
        {
            Slow(Inerty);
        }

        Speed_Rot_Neg = -RotationSpeed();
        Speed_Rot_Poz = RotationSpeed();

        if (!PartialCrashed)
        {
            transform.eulerAngles = Rotate(KeyCode.D, KeyCode.RightArrow,
                new Vector3(0, 0, Speed_Rot_Neg * Time.deltaTime));

            transform.eulerAngles = Rotate(KeyCode.A, KeyCode.LeftArrow,
                new Vector3(0, 0, Speed_Rot_Poz * Time.deltaTime));
        }

        

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) ||
            Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            StartDecreasing = true;
        }

        if (!CrashedFront && !StartGoingBackwards && !StartGoingForwards && !PartialCrashed)
        {
            Moving(KeyCode.W, KeyCode.UpArrow, val => val >= 0, MaxSpeedPoz);
        }

        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.UpArrow)
            && !CrashedFront && !StartGoingBackwards && !StartGoingForwards && !PartialCrashed)
        {
            Moving(KeyCode.S, KeyCode.DownArrow, val => val <= 0, -4);
        }

        else if (CrashedFront)
        {
            Speed = 0;
            CrashedFront = false;
            StartGoingBackwards = true;
        }

        else if (CrashedBack)
        {
            Speed = 0;
            CrashedBack = false;
            StartGoingForwards = true;
        }

        else if (!StartGoingBackwards)
        {
            Moving(KeyCode.S, KeyCode.DownArrow, val => val <= 0, MaxSpeedNeg);
        }

        if (StartGoingBackwards)
        {
            CrashedFrontCar();
        }
        
        if (PartialCrashed)
        {
            PartialCrashedCar();
        }

        Nitro();

        RotatePivot();

        transform.localPosition += transform.right * Speed * Time.deltaTime;

        // TouchLimit(val => val <= LeftLimit, 180 - Z_Rot, Adjustment, 0, Sphere.transform.position.x, ref TouchedLeft);
        // TouchLimit(val => val >= -LeftLimit, 180 - Z_Rot, -Adjustment, 0, Sphere.transform.position.x, ref TouchedRight);
        // TouchLimit(val => val <= BottomLimit, -Z_Rot, 0, Adjustment, Sphere.transform.position.y, ref TouchedDown);
        // TouchLimit(val => val >= -BottomLimit, -Z_Rot, 0, -Adjustment, Sphere.transform.position.y, ref TouchedUp);

        //TouchHorrizontalCollider();
    }

    public Vector3 Rotate(KeyCode KeyCode1, KeyCode KeyCode2, Vector3 NewPoz_Rot)
    {
        if (Input.GetKey(KeyCode1) || Input.GetKey(KeyCode2))
        {
            transform.eulerAngles = transform.eulerAngles + NewPoz_Rot;
            
        }
        return transform.eulerAngles;
    }

    public void Slow(float InertyOrBrake)
    {
        if (StartDecreasing == true)
        {
            Speed = Mathf.MoveTowards(Speed, 0, InertyOrBrake * Time.deltaTime);
        }
    }

    public int RotationSpeed()
    {
        if (Speed < -1f) return 50;
        if (Speed < -0.5f) return 20;
        if (Speed < 1) return 0;
        if (Speed < 3) return 50;
        if (Speed < 4) return 80;
        return 120;
    }

    public float AccValue()
    {
        if (Speed < 9) return 4;
        if (Speed < 11) return 2;
        if (Speed < 13) return 1;
        return 0.5f;
    }

    public void Moving(KeyCode KeyCode1, KeyCode KeyCode2, Func<float, bool> condition, float MaxSpeed)
    {
        if ((Input.GetKey(KeyCode1) || Input.GetKey(KeyCode2)) && condition(Speed) && !Input.GetKey(KeyCode.Space))
        {
            StartDecreasing = false;
            Speed = Mathf.MoveTowards(Speed, MaxSpeed, Acc * Time.deltaTime);
        }
    }

    public void Nitro()
    {
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) &&
         !Input.GetKey(KeyCode.Space) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.DownArrow))
        {
            MaxSpeedPoz = 14;
            Acc = AccValue();
            transform.GetChild(0).gameObject.SetActive(true);
        }

        else
        {
            MaxSpeedPoz = 9;
            Acc = 2;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void RotatePivot()
    {
        Pivot.gameObject.transform.eulerAngles = new Vector3(0, 0, Mathf.Abs(Speed) * -7.2f);
    }

    // public void TouchLimit(Func<float, bool> condition, float NewZ_Rot, float Adjustment1, float Adjustment2, float Position, ref bool Touched)
    // {
    //     if (TouchedTrack)
    //     {
    //         Sphere.transform.position = new Vector3(Sphere.transform.position.x + Adjustment1,
    //          Sphere.transform.position.y + Adjustment2, Sphere.transform.position.z);
    //         Touched = true;
    //         Z_Rot = NewZ_Rot;
    //     }
    // }

    public void TouchHorrizontalCollider()
    {
        for (int i = 0; i < HorrizontalCollider.transform.childCount; i++)
        {
            if (Vector3.Distance(transform.position, HorrizontalCollider.transform.GetChild(i).position) < DectectingDistance)
            {
                transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
            }
        }
    }

    public void CrashedFrontCar()
    {
        Speed = Mathf.MoveTowards(Speed, -4, 6 * Time.deltaTime);

        if (Speed <= -1)
        {
            StartGoingBackwards = false;
        }
    }

    public void PartialCrashedCar()
    {
        Speed = Mathf.MoveTowards(Speed, 0, 6 * Time.deltaTime);

        Current_RotZ = Mathf.MoveTowards(Current_RotZ, NewPoz_RotZ, 700 * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, 0, Current_RotZ);

        if (Speed == 0)
        {
            PartialCrashed = false;
        }
    }
    
    public void ColliderFunction(float VerificationAngle1, float VerificationAngle2, float NewAngle1,
        float NewAngle2, Func<float, bool> condition1, Func<float, bool> condition2, ref bool Crashed)
    {
        if (z > VerificationAngle1 && z < VerificationAngle2)
        {
            Crashed = true;
        }

        else if (condition2(Speed))
        {
            if (condition1(z))
            {
                NewPoz_RotZ = transform.eulerAngles.z - NewAngle1;
            }
            else
            {
                NewPoz_RotZ = transform.eulerAngles.z + NewAngle1;
            }

            Current_RotZ = transform.eulerAngles.z;
            PartialCrashed = true;
        }


        else if (!PartialCrashed)
        {
            if (condition1(z))
            {
                NewPoz_RotZ = transform.eulerAngles.z - NewAngle2;
            }
            else
            {
                NewPoz_RotZ = transform.eulerAngles.z + NewAngle2;
            }

            Current_RotZ = transform.eulerAngles.z;
            PartialCrashed = true;
        }
    }
}
