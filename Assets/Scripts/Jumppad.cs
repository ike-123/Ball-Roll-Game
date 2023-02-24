using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;

public class Jumppad : NetworkBehaviour
{
    [SerializeField] float Force;


     void OnTriggerEnter(Collider Other)
     {
        

            if (Other.tag == "Player" )
            {
                if(PredictionManager.IsReplaying() == false){

                  

                BallMovement Player = Other.gameObject.GetComponent<BallMovement>();

                if(Player.Canjump){

                Player.Jump = true;
                Player.JumpForce = Force;
                }
              
                }
                

                // PlayerRigidbody.velocity = new Vector3(PlayerRigidbody.velocity.x, 0, PlayerRigidbody.velocity.z);

                // PlayerRigidbody.AddForce(Force * Vector3.up, ForceMode.Impulse);
            }
        
        
            
        
       
    }
}
