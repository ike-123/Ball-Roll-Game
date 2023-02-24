using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
public class StaticLazer : MonoBehaviour
{
    [SerializeField] Transform LazerPoint;
    [SerializeField] Vector3 Dimensions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(InstanceFinder.IsClient){
                Collider[] Objects = Physics.OverlapBox(LazerPoint.position,Dimensions,Quaternion.identity);


            for(int i = 0; i < Objects.Length; i++ ){

                if(Objects[i].tag == "Player"){

                    
                    GameObject Player = Objects[i].gameObject;
                    if(Player.GetComponent<NetworkObject>().IsOwner){
                        GameManager.instance.StartCoroutine(GameManager.instance.Respawn());
                    }

                    
                }
            }
        }


          if(InstanceFinder.IsServer){
                Collider[] Objects = Physics.OverlapBox(LazerPoint.position,Dimensions,Quaternion.identity);


            for(int i = 0; i < Objects.Length; i++ ){

                if(Objects[i].tag == "Player"){

                    
                    GameObject Player = Objects[i].gameObject;
                    
                    GameManager.instance.StartCoroutine(GameManager.instance.ServerRespawn(Player));
                    

                    
                }
            }
        }
        
        
        


        
    }


    void OnDrawGizmos(){

        Gizmos.color = Color.cyan; 
        Gizmos.DrawCube(LazerPoint.position,Dimensions *2);
    }


}
