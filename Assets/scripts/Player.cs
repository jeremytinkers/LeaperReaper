using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpKeyPressedDown;
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    [SerializeField] private Transform playerBottomTransform;
    [SerializeField] private LayerMask layerMask;
    public int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressedDown = true;
            //Debug.Log("update");
        }

        horizontalInput = Input.GetAxis("Horizontal");
  
    }

    //deals with updates made by the physics engine , runs once very physics update
    private void FixedUpdate()
    {
        //If the player is still in the air, return -> do nothin
        if(Physics.OverlapSphere(playerBottomTransform.position, 0.1f, layerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyPressedDown)
        {
            //Debug.Log("fixedupdate");
            rigidBodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            jumpKeyPressedDown = false;
        }

        rigidBodyComponent.velocity = new Vector3(horizontalInput*4, rigidBodyComponent.velocity.y, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            score++;
        }
    }

}

