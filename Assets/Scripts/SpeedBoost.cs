using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
public class SpeedBoost : NetworkBehaviour
{

    [SerializeField] float Force;
    void OnTriggerEnter(Collider Other){

    if(Other.tag == "Player")
    {
        
        if(PredictionManager.IsReplaying() == false){

        Debug.Log("detected player on speed boost");
        BallMovement Player = Other.gameObject.GetComponent<BallMovement>();
        Player.SpeedBoost = true;
        Player.BoostForce = Force;
        Player.boostVector = transform.forward;
        }

    }
   }
}
