using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float MoveSpeed;
    

    [SerializeField] Rigidbody Rigidbody;
    [SerializeField] int framerate;
    
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
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        Vector3 Movement = new Vector3(Horizontal,0,Vertical);
        Movement.Normalize();

        Rigidbody.AddForce(Movement * Time.deltaTime * MoveSpeed);
    }
}
