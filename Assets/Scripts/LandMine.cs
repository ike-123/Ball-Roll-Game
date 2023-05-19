using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LandMine : MonoBehaviour
{
    
    [SerializeField] float Force;
    [SerializeField] float forceup;
    void OnTriggerEnter(Collider Other){
    if(Other.tag == "Player"){

        // launch player in opposite direction
        GameObject Player = Other.gameObject;
        Vector3 direction = Player.transform.position - transform.position ;
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().AddForce(Force * new Vector3(direction.x,forceup,direction.z), ForceMode.Impulse);

        //stop player from being able to move
    }
   }
}
