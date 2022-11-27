using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] Transform KillZone;

    [SerializeField] Transform RespawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.transform.position.y < KillZone.position.y){

           StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn(){

        Player.gameObject.SetActive(false);

       yield return new WaitForSeconds(1f);

       Player.gameObject.transform.position = RespawnPosition.position;
       Player.gameObject.SetActive(true);
    }
}
