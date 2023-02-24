using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Transporting;
using FishNet.Object;
public class ConveyerBelt : MonoBehaviour
{
    public int speed;
    [SerializeField] List<GameObject> Players = new List<GameObject>();

    [SerializeField] GameObject root;

    [SerializeField] bool addforce;

    [SerializeField] GameObject Player;

    [SerializeField] GameObject Parent;

    [SerializeField] GameObject Player2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // // Update is called once per frame
    // void Update()
    // {   
    //    if(InstanceFinder.IsServer){
        
    //         for(int i = 0; i < Players.Count; i ++){

    //         Debug.Log("server");
    //         Players[i].transform.Translate(transform.forward * speed* Time.deltaTime);

    //     }
    //    }

    //    if(InstanceFinder.IsClient){

    //     if(addforce){
    //         Debug.Log("client");
    //         root.transform.Translate(transform.forward * speed* Time.deltaTime);
    //     }
    //    }
      
           

        
          
    // }

    // private void OnCollisionEnter(Collision collision){

        
    //     if(collision.gameObject.tag == "Player")
        
    //     {
           
    //     if( InstanceFinder.IsServer)
    //     {
    //         Debug.Log("adding");
    //         GameObject root = new GameObject();

    //         collision.gameObject.transform.SetParent(root.transform);

    //         Players.Add(root);
    //     }
        
    //     if(collision.gameObject.GetComponent<NetworkObject>().IsOwner)
    //     {

    //         root = new GameObject();
            
    //         Player = collision.gameObject;

    //         Player.transform.SetParent(root.transform);

    //         addforce = true;


    //     }

              
    //     }
    // }

    //  private void OnCollisionExit(Collision collision){
    //     if(collision.gameObject.tag == "Player"){
            

    //     if( InstanceFinder.IsServer)
    //     {   
    //         Player2 = collision.gameObject;
    //         Debug.Log($"player name: {Player2.name}");
    //         Debug.Log($"transform name: {Player2.transform.name}");

    //           Debug.Log($"parent name: {Player2.transform.parent.name}");
    //           Debug.Log($"parent GO name: {Player2.transform.parent.gameObject.name}");

            
    //             Parent = Player2.transform.parent.gameObject;
            
            


    //         Debug.Log("saving parent");
     

    //          Player2.transform.SetParent(null);

    //          Debug.Log("done");
    //         Players.Remove(Parent);
    //          Destroy(Parent);
    //     }
        
    //     if(collision.gameObject.GetComponent<NetworkObject>().IsOwner)
    //     {
    //         Debug.Log("isownerexit");
            

    //         addforce = false;
    //         Player.transform.SetParent(null);
    //         Destroy(root);


            



    //     }
       
    //     }
    // }


}
