using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
public class MovingLazer : MonoBehaviour
{

    [SerializeField] float LazerDistance;

    [SerializeField] Vector3 StartPos;

    [SerializeField] float SphereRadius;

    [SerializeField] LayerMask mask;

    [SerializeField] Vector3 boxsize;

    [SerializeField] Vector3 midpoint;
    
    [SerializeField] float Length;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        StartPos = transform.position;
        
        if(InstanceFinder.IsClient){

                if(Physics.SphereCast(StartPos,SphereRadius,transform.right,out RaycastHit Hit,LazerDistance,mask)){

                    Debug.Log("hitting");

                    Vector3 hitpos = Hit.point;

                    Length = (hitpos.x - transform.position.x);
                    
                    float xmid = (transform.position.x + hitpos.x) /2;

                    midpoint = new Vector3(xmid,StartPos.y,StartPos.z);

                    Collider[] Objects = Physics.OverlapBox(midpoint,new Vector3 (Length/2,boxsize.y/2,boxsize.z/2),Quaternion.identity);


                    for(int i = 0; i < Objects.Length; i++ ){

                        if(Objects[i].tag == "Player"){

                            
                            GameObject Player = Objects[i].gameObject;
                            if(Player.GetComponent<NetworkObject>().IsOwner){
                                GameManager.instance.StartCoroutine(GameManager.instance.Respawn());
                            }   
                        }

                    }

                    for(int i = 0; i < Objects.Length; i++ ){

                        if(Objects[i].tag == "Player"){

                        
                            GameObject Player = Objects[i].gameObject;
                            if(Player.GetComponent<NetworkObject>().IsOwner){
                                GameManager.instance.StartCoroutine(GameManager.instance.Respawn());
                            }
                        }

                    }
                }

            else{


                Debug.Log("not hitting");

                    //Length = ( LazerDistance - transform.position.x) ;
                    Length = LazerDistance ;
                    
                    float xmid = (transform.position.x + transform.position.x + LazerDistance) /2;

                    midpoint = new Vector3(xmid,StartPos.y,StartPos.z);

                
                    Collider[] Objects = Physics.OverlapBox(midpoint,new Vector3 (Length/2,boxsize.y/2,boxsize.z/2),Quaternion.identity);


                    for(int i = 0; i < Objects.Length; i++ ){

                        if(Objects[i].tag == "Player"){

                            
                            GameObject Player = Objects[i].gameObject;
                            if(Player.GetComponent<NetworkObject>().IsOwner){
                                GameManager.instance.StartCoroutine(GameManager.instance.Respawn());
                            }   
                        }

                    }

                    for(int i = 0; i < Objects.Length; i++ ){

                        if(Objects[i].tag == "Player"){

                        
                            GameObject Player = Objects[i].gameObject;
                            if(Player.GetComponent<NetworkObject>().IsOwner){
                                GameManager.instance.StartCoroutine(GameManager.instance.Respawn());
                            }
                        }

                    }
                
            }


            
        }
    }


     void OnDrawGizmos(){

        Gizmos.color = Color.magenta; 
        Gizmos.DrawCube(midpoint,new Vector3(Length,boxsize.y,boxsize.z));
        Gizmos.DrawSphere(midpoint,SphereRadius);
    }

}
