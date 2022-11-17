using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] float Force;
   void OnTriggerEnter(Collider Other){
    if(Other.tag == "Player"){

        Player.GetComponent<Rigidbody>().AddForce(Force * transform.forward, ForceMode.Impulse);
    }
   }
}
