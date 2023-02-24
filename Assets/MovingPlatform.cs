using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;


public class MovingPlatform : NetworkBehaviour
{
    
    Vector3 Target;


    
    Vector3 startingPos,EndPos;

    [SerializeField] float EndingPos;

    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {   
       

        EndPos = transform.position + transform.right * EndingPos;

        startingPos = transform.position;
        Target = EndPos;
        
        
       
    }

    // Update is called once per frame
    void Update()
    {       
        if(IsServer){
             Move();
        }
        
             

    }
        
    

    private void Move(){

        
        transform.position = Vector3.MoveTowards(transform.position,Target,speed * Time.deltaTime);

        if(transform.position == EndPos){

            Target = startingPos; 
        }

        if(transform.position == startingPos){

            Target = EndPos;
        }
    }   

    private void OnDrawGizmosSelected(){

        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + transform.right * EndingPos,new Vector3(1,1,1));
    }
    
}
