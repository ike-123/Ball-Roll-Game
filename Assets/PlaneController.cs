using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{   

    [Header("Plane Statitstics")]


    [Tooltip("How much the throttle ramps up or down")]
    [SerializeField] float throttleIncrement = 0.1f;

     [Tooltip("Maximum engine thrust when at 100% throttle")]
    [SerializeField] float MaxThrust = 200f;

    [SerializeField] float responsiveness = 10f;


    
    public Rigidbody rb;

    private float throttle;
    private float roll;

    private float pitch;
    private float yaw;

    private float responseModifier{

        get{
            return (rb.mass/10f) * responsiveness;
        }
    }



    private void Awake(){

        rb = GetComponent<Rigidbody>();

    }

    private void HandleInputs(){

        roll = Input.GetAxis("Horizontal");
        pitch = Input.GetAxis("Vertical");
        yaw = Input.GetAxis("Yaw");


        if(Input.GetKey(KeyCode.Space)){
            throttle += throttleIncrement;
        }

        else if(Input.GetKey(KeyCode.LeftControl)){
            throttle -= throttleIncrement;
        }

        throttle = Mathf.Clamp(throttle,0f,100f);

    }

    void Update(){

        HandleInputs();

    }

    private void FixedUpdate(){

        rb.AddForce(transform.forward * MaxThrust * throttle);
        //rb.AddTorque(transform.up * yaw * responseModifier);
        rb.AddTorque(transform.right * -pitch * responseModifier);
        rb.AddTorque(transform.up * roll * responseModifier);
        
    }


}
