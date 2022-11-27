using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    

    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] int framerate;
    [SerializeField] Vector3 Movement;
    [SerializeField] float Horizontal,Vertical;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        Application.targetFrameRate = framerate;
    }
    

    void Move(){
         Horizontal = Input.GetAxis("Horizontal");
         Vertical = Input.GetAxis("Vertical");

         Movement = new Vector3(Horizontal,0,Vertical);
        //Movement.Normalize();

         Movement = Vector3.ClampMagnitude(Movement, 1f);

        Rigidbody.AddForce(Movement * Time.deltaTime * MoveSpeed);
    }
}
