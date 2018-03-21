using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : Unit
{
    public float speedMove = 5;
    public float speedRotation = 10;
    Rigidbody rb;
    BoxCollider box;
    public Slider healthBar;

    

    // Use this for initialization
    void Awake ()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        box = GetComponent<BoxCollider>();
        healthBar.value = Calculatehealth();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Get inputs from Input manager (axes range from 0 to 1)
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        float moveInput = Input.GetAxisRaw("Horizontal");

        //Player Movement
        Vector3 moveV3 = new Vector3(-horizontalInput, 0.0f, -verticalInput);
        transform.Translate(moveV3 * speedMove * Time.deltaTime, Space.World);

        //Player Rotation 
        var newRot = Quaternion.LookRotation(Vector3.Lerp(moveV3, transform.forward, Time.deltaTime));
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * speedRotation);

        /*
        anim.SetFloat("moveHorizontal", horizontalInput);
        anim.SetFloat("moveVertical", verticalInput);
        */

        //Play Run Animation 
         if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            anim.SetFloat("Move", 1);
        }
         else
            anim.SetFloat("Move", 0);
        //rb.transform.eulerAngles = Vector3.Lerp(rb.transform.eulerAngles, new Vector3(0, rotation, 0), Time.deltaTime * speed);
        //transform.rotation = Quaternion.LookRotation(Vector3.Lerp(moveV3, transform.forward, Time.deltaTime * speed));
        //transform.rotation = Quaternion.FromToRotation(moveV3, transform.forward);


        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("Attack");
            box.isTrigger = true;
        }
        else
            box.isTrigger = false;

        healthBar.value = Calculatehealth();

        print(Calculatehealth());
    }

    float Calculatehealth()
    {
       return health / maxHealth;
    }
    
}
