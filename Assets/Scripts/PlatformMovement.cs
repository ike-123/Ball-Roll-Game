using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{

    public float movespeed;
    [SerializeField] float min,max;
    [SerializeField] float Horizontal, Vertical;
    public GameObject Player;

    [SerializeField ] int framerate;  
    [SerializeField] float value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Application.targetFrameRate = framerate;
         Horizontal = Input.GetAxis("Horizontal") * movespeed;
         Vertical = Input.GetAxis("Vertical") * movespeed;

        

        
         //transform.RotateAround(Player.transform.position, Vector3.left, Vertical  * Time.deltaTime);
       // transform.RotateAround(Player.transform.position, Vector3.forward, Horizontal * Time.deltaTime);

        // if(transform.localRotation.eulerAngles.x > 360 + min && transform.localRotation.eulerAngles.x < max)
        // {
        //   Debug.Log(transform.localRotation.eulerAngles.x);
        //   transform.Rotate(-Vertical * Time.deltaTime,0f,0f);
        // }
         
        //  if(transform.localRotation.eulerAngles.z > 360 + min && transform.localRotation.eulerAngles.z < max){
        //   transform.Rotate(0f,0f,Horizontal * Time.deltaTime);
        //  }

        transform.Rotate(-Vertical * Time.deltaTime,0f,Horizontal* Time.deltaTime);

        //   Quaternion newquaternion = new Quaternion();
        //   newquaternion.Set(Mathf.Clamp(transform.rotation.x,min,max),transform.rotation.y,Mathf.Clamp(transform.rotation.z,min,max),1);

        //  transform.rotation = newquaternion;
        //transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x,min,max),transform.eulerAngles.y,Mathf.Clamp(transform.eulerAngles.z,min,max));
        
        
    }
}
