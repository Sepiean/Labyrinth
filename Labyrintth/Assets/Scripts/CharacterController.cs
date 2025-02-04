using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{

    [SerializeField] private float speed = 10.5f;
    [SerializeField] private float gravity = 10.0f;
    [SerializeField] private float jumpHeight = 15f;
    Vector3 velocity;
    CharacterController controller;
    public bool isGrounded;
    public LayerMask groundMask;
    public Transform groundPlace;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move*speed*Time.deltaTime);
        
        RaycastHit hit; //  Zmienna na informacje o uderzeniu
        if (Physics.Raycast(groundPlace.position,
            transform.TransformDirection(Vector3.down),
            out hit, 0.3f, groundMask
            ))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2 * gravity);
        }
        
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
    }
}
