using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private float movementZ;
    private int count; //hold count
    private bool doubleJumpAvailable;
    [SerializeField] private float jumpHeight = 1; //puts in inspector anyway 
    private bool onGround;
    Vector3 movementV = new Vector3(0.0f, 1.0f, 0.0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winTextObject.SetActive(false);
        SetCountText();
    }


    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y; 
        //movementZ = movementVector.z;

    }

    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject.CompareTag("pickup"))
        {
        //with this command alone (below), we can't filter which object to diable
        other.gameObject.SetActive(false); //set active to disable
        //so...use tag system, compares tag value to a string (above, line 37)
        count += 1;
        SetCountText();
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 12){
            winTextObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Win!";

            //destory enemy
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            if(onGround == false){
                if(doubleJumpAvailable == true){
                    rb.AddForce(movementV * jumpHeight, ForceMode.Impulse);
                    doubleJumpAvailable = false;
                    Debug.Log("did second jump");
                }
            }      
            if(onGround == true){
                rb.AddForce(movementV * jumpHeight, ForceMode.Impulse); 
                onGround = false;
                Debug.Log("did first jump");
            }
            
        }
        //set var for fixed update
    }

    private void FixedUpdate()
    {
        //0.0f for movement only on the x and y plane
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY); //need vector three for 3D movement
        rb.AddForce(movement * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
        if(collision.gameObject.CompareTag("Ground")){ //make ground tag
            doubleJumpAvailable = true;
            onGround = true;
            Debug.Log("resetting jump vars");
        }
     

    }


        
}

