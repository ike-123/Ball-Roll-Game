using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
public class spiketrap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other){

        if(InstanceFinder.IsServer){

              if(other.tag == "Player"){

            GameManager.instance.StartCoroutine(GameManager.instance.ServerRespawn(other.gameObject));
        }
        }
      
    }
}
