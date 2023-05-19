using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class Server : NetworkBehaviour
{
    [SerializeField] float Timetostart, TimeTostop;
    [SerializeField] float timer;
    [SerializeField] bool cancount;

    
    [SerializeField] GameObject platform;
    public override void OnStartServer()
    {
        base.OnStartServer();

        cancount = true;

        // GameObject go = Instantiate(platform);
        // ServerManager.Spawn(go, null);
        
       
    }
    // Update is called once per frame
    void Update()
    {
        if (cancount)
        {
           timer += Time.deltaTime;
        }
       

        if(timer > Timetostart)
        {

            enablegotext();
        }
    }

    [ObserversRpc]
    private void enablegotext()
    {
      //  UiManager.instance.enablegotext();
    }
}
