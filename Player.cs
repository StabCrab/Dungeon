using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private Rigidbody2D rigidBody;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Reset MoveDelta
        moveDelta = new Vector3(x,y,0);
        
        // Swap sprite direction, wether you are going right or left
        if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(2, 2, 1);
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-2, 2, 1);
        }

        //make move
        transform.Translate(moveDelta * Time.deltaTime);
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
