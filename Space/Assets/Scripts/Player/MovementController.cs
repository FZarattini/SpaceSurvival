using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float speed;
    public GameObject character;
    public Animator animator;

    private Rigidbody2D rb;
    private float moveHorizontal;
    private float moveVertical;
    private Vector2 movement;


	// Use this for initialization
	void Start () {
        rb = character.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("horizontal", moveHorizontal);
        animator.SetFloat("vertical", moveVertical);


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("isMoving", true);
        }
        else if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D)) {
            animator.SetBool("isMoving", false);
        }


        movement = new Vector2(moveHorizontal, moveVertical);

        rb.velocity = movement * speed;
        
	}
}
