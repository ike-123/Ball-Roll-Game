using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using FishNet.Transporting;

public class FallingSpiketrap : NetworkBehaviour
{
    private Vector3 startingPos;

    [SerializeField] Vector3 Endingpos;
    [SerializeField] float maxdistance;

    [SerializeField] float Movespeed;

    [SerializeField] float MoveUpspeed;

    [SerializeField] float counter;

    [SerializeField] bool down,up;

    [SerializeField] float timetogoup, timetogodown;


    private RaycastHit Hit;

    public override void OnStartServer(){

        base.OnStartServer();
        if (base.IsServer)
        {
            startingPos = transform.position;

            if(Physics.Raycast(startingPos,Vector3.down,out Hit,maxdistance)){
            Endingpos =  Hit.point;
            down = true;
            }
        }
       
    }
    void FixedUpdate()
    {   
        //lerp down
        // counter += Time.deltaTime;

       if(base.IsServer)
       {
        MoveDown();
        MoveUp();
       }
       

        
        
    }

    void MoveDown(){
         
         if(down){

            timetogodown += Time.deltaTime;
             transform.position =  Vector3.Lerp(transform.position,Endingpos,Time.deltaTime * Movespeed);   

            if(transform.position == Endingpos){
                down = false;
                up = true;

                
                timetogoup= 0;
            }
         }
           
    }

    void MoveUp(){
        
        if(up){
            
            timetogoup += Time.deltaTime;
             transform.position =  Vector3.Lerp(transform.position, startingPos,Time.deltaTime * MoveUpspeed);   
            {
                if(transform.position == startingPos){
                up = false;
                down = true;

                timetogodown = 0;
            }
            }
            
        }
    }
}
