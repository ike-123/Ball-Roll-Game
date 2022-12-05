using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPos : MonoBehaviour
{
    public static BallPos instance;
    public Transform Ball;

    void Awake(){
        instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Ball.position;
    }
}
