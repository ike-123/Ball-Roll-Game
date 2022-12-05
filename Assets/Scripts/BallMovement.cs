using UnityEngine;
using Mirror;

public class BallMovement : NetworkBehaviour
{
    [SerializeField] float MoveSpeed;
    

    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] int framerate;
    [SerializeField] Vector3 Movement;

    [SerializeField] int Downwardsray;


    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer)
        {
            Rigidbody = GetComponent<Rigidbody>();
            GameManager.instance.Player = this.gameObject;
            BallPos.instance.Ball = this.gameObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            Move();
            GroundCheck();
            Application.targetFrameRate = framerate;
        }
        
    }
    

    void Move(){
        
         float Horizontal = Input.GetAxis("Horizontal");
         float Vertical = Input.GetAxis("Vertical");

         Movement = new Vector3(Horizontal,0,Vertical);
        //Movement.Normalize();

         Movement = Vector3.ClampMagnitude(Movement, 1f);

        Rigidbody.AddForce(Movement * Time.deltaTime * MoveSpeed);
    }


    
    private RaycastHit RaycastHit;

    void GroundCheck(){

        Debug.DrawRay(transform.position,Vector3.down * Downwardsray, Color.black);
       if(Physics.Raycast(transform.position,Vector3.down,out RaycastHit,Downwardsray)){

        if(RaycastHit.transform.tag == "RotatingPlatform"){

            transform.SetParent(RaycastHit.transform.parent);

        }
       }

       else{
        transform.SetParent(null);
       }

    }
}
