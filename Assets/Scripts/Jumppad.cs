using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumppad : MonoBehaviour
{
    [SerializeField] float Force;
    [SerializeField] GameObject Player;


     void OnTriggerEnter(Collider Other)
     {
        if(Other.tag == "Player"){

            Rigidbody PlayerRigidbody = Player.GetComponent<Rigidbody>();

            PlayerRigidbody.velocity = new Vector3( PlayerRigidbody.velocity.x,0, PlayerRigidbody.velocity.z);

            PlayerRigidbody.AddForce(Force * Vector3.up, ForceMode.Impulse);
        }
    }
}
