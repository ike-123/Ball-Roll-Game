using UnityEngine;
using FishNet;
using FishNet.Object.Prediction;
using FishNet.Transporting;
using FishNet.Object;


public class BallMovement : NetworkBehaviour
{
    [SerializeField] float MoveSpeed;
    

    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] int framerate;
    [SerializeField] Vector3 Movement;

    [SerializeField] int Downwardsray;

    [SerializeField] Vector3 DragForce;

    [SerializeField] Vector3 rawSpd;

    [SerializeField] Vector3 horiDrag;

    [SerializeField] float dragamount;


    [SerializeField] float regulardrag;
    [SerializeField] float slopedragamount;
   [SerializeField] bool isOverSlope;


    [SerializeField]float rotationSpeed = 1f; // Speed of rotation in degrees per second
    [SerializeField] float radius = 1f; // Radius of the circle

    [SerializeField] float angle = 0f;

    public bool Jump;
    public float JumpForce;
    public bool Canjump;

     public bool SpeedBoost;
     public float BoostForce;

     public Vector3 boostVector;

     [SerializeField] GameObject ConveyerRoot;

    [SerializeField] float jumptimer;
    [SerializeField] float jumptime;
    public struct MoveData : IReplicateData
    {

        public bool Jump;

        public bool SpeedBoost;
        public float Horizontal;
        public float Vertical;
       
        public MoveData(bool jump,bool speedboost,float horizontal, float vertical)
            {   
                Jump = jump;
                SpeedBoost = speedboost;
                Horizontal = horizontal;
                Vertical = vertical;
                _tick = 0;
            }
        private uint _tick;

        public void Dispose() {}
        public uint GetTick() => _tick;
        public void SetTick(uint value) => _tick = value;
    }

     public struct ReconcileData : IReconcileData
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public Vector3 Velocity;
            public Vector3 AngularVelocity;
            public ReconcileData(Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity)
            {
                Position = position;
                Rotation = rotation;
                Velocity = velocity;
                AngularVelocity = angularVelocity;
                _tick = 0;
            }

            private uint _tick;
            public void Dispose() { }
            public uint GetTick() => _tick;
            public void SetTick(uint value) => _tick = value;
        }




    public override void OnStartClient()
    {
        base.OnStartClient();

        
        if(base.IsOwner)
        {
            dragamount = regulardrag;
        GameManager.instance.Player = this.gameObject;
        BallPos.instance.Ball = this.gameObject.transform;
        }
    }
    // Start is called before the first frame update
    public override void OnStartNetwork()
    {
        
        
        base.OnStartNetwork();
        base.TimeManager.OnTick += TimeManager_OnTick;
        base.TimeManager.OnPostTick += TimeManager_OnPostTick;
        Rigidbody = GetComponent<Rigidbody>();

    }


    public override void OnStopNetwork()
    {
        base.OnStopNetwork();
        base.TimeManager.OnTick -= TimeManager_OnTick;
        base.TimeManager.OnPostTick += TimeManager_OnPostTick;
    }


    private void TimeManager_OnTick()
    {   
        if(base.IsOwner)
        {
            Reconciliation(default, false);
            CheckInput(out MoveData md);
            Move(md,false);
        }     

        if(base.IsServer){
            Move(default,true);
        }
       
    }

    private void TimeManager_OnPostTick()
    {
        if(base.IsServer)
        {
        ReconcileData rd = new ReconcileData(transform.position,transform.rotation,Rigidbody.velocity,Rigidbody.angularVelocity);
        Reconciliation(rd,true);
        }
        
    }


    void Update(){
        
        if(Canjump == false){

            jumptimer -= Time.deltaTime;
        }

        if(jumptimer <= 0){
            Canjump = true;
            jumptimer = jumptime;
        }
    }   

    private void CheckInput(out MoveData md){

        md = default;

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        if (Horizontal == 0f && Vertical == 0f && !Jump && !SpeedBoost)
                return;

        md = new MoveData(Jump,SpeedBoost,Horizontal,Vertical);
        Jump =false;   
        SpeedBoost =false;     
    }

    //Movement
    [Replicate]
        private void Move(MoveData md, bool asServer, Channel channel = Channel.Unreliable, bool replaying = false)
        {
            
        Vector3 Movement = new Vector3(md.Horizontal,0,md.Vertical);

        //using clamp instead of normalize so that we can
        //move inbetween values of 0 and 1
        Movement = Vector3.ClampMagnitude(Movement, 1f);

        Debug.Log("checking");
        Rigidbody.AddForce(Movement * MoveSpeed,ForceMode.Force);

        rawSpd = Rigidbody.velocity;
        Vector3 horiSpd = rawSpd;
        horiSpd.y = 0;
        horiDrag = horiSpd * dragamount;//increase the percentage amount for more drag, and vice versa.
        Rigidbody.velocity = rawSpd - horiDrag;

    
    
    //     // Current angle around the circle

    
    //   // Calculate the position on the circle based on the current angle
    //     Vector3 position = transform.position + Quaternion.Euler(0f, angle, 0f) * Vector3.right * radius;

    //     // Set the Rigidbody's position to the calculated position
    //     GetComponent<Rigidbody>().MovePosition(position);

    //     // Increment the angle based on the rotation speed and the time since the last FixedUpdate
    //     angle += rotationSpeed * Time.fixedDeltaTime;

    //     // Wrap the angle around at 360 degrees to prevent it from getting too large
    //     angle %= 360f;

    //     Rigidbody.AddForce(transform.right * 2,ForceMode.Impulse);
    
    

        Debug.DrawRay(transform.position,Vector3.down * Downwardsray, Color.black);
       if(Physics.Raycast(transform.position,Vector3.down,out RaycastHit,Downwardsray))
       
       {

        if(RaycastHit.transform.tag == "RotatingPlatform")
        {
            
           transform.SetParent(RaycastHit.transform.parent);
        }

        if(RaycastHit.transform.tag == "ConveyerBelt"){

            ConveyerBelt ConveyerBelt = RaycastHit.transform.gameObject.GetComponent<ConveyerBelt>();
            

            Rigidbody.AddForce(ConveyerBelt.speed * ConveyerBelt.transform.forward);
        }

            var floorAngle = Vector3.Angle(RaycastHit.normal, Vector3.up);


            isOverSlope = floorAngle > 0F;

            if(isOverSlope && Movement.magnitude == 0f){

                dragamount = slopedragamount;
            }

            else{
                dragamount = regulardrag;
            }

       }

       else
       {



        Debug.Log("DESTRORY");

        transform.SetParent(null);

        

       }

        if(md.Jump)
        {
            //Canjump = false;

           
                 
                
                Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, 0, Rigidbody.velocity.z);

                Rigidbody.AddForce(JumpForce * Vector3.up, ForceMode.Impulse);

       
                
        }

        if(md.SpeedBoost)
        {

            Rigidbody.AddForce(BoostForce * boostVector, ForceMode.Impulse);
        }

        }


     [Reconcile]
        private void Reconciliation(ReconcileData rd, bool asServer, Channel channel = Channel.Unreliable)
        {
            transform.position = rd.Position;
            transform.rotation = rd.Rotation;
            Rigidbody.velocity = rd.Velocity;
            Rigidbody.angularVelocity = rd.AngularVelocity;
        }



  // Update is called once per frame
    // void Update()
    // {
    //     if(base.IsOwner){

    //         //Move();
    //         GroundCheck();
    //         Application.targetFrameRate = framerate;
    //     }
       
        
        
    // }
    
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
