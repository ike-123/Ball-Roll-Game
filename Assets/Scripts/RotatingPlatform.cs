using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;

public class RotatingPlatform : NetworkBehaviour
{

    [SerializeField] float RotateSpeed; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (base.IsServer)
        {
            transform.Rotate(0, RotateSpeed, 0);
        }
   
    }
}
