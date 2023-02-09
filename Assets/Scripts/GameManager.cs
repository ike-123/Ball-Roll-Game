using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject Player;
    [SerializeField] Transform KillZone;

    [SerializeField] Transform RespawnPosition;

    [SerializeField] int framerate;
    // Start is called before the first frame update

    void Awake(){
        instance = this;
    }
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = framerate;

        if(Player!= null){
            if(Player.transform.position.y < KillZone.position.y){

           StartCoroutine(Respawn());
        }
        }
       
    }

    private IEnumerator Respawn(){

        Player.gameObject.SetActive(false);

       yield return new WaitForSeconds(1f);

       Player.gameObject.transform.position = RespawnPosition.position;
       Player.gameObject.SetActive(true);
    }
}
