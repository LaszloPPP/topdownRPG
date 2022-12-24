using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Fighter
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //reset moveDelta
        moveDelta = new Vector3(x, y, 0);

        //swap sprite direction right or left
        if (moveDelta.x > 0)
        {
            //transform.localScale = Vector3.one;
            transform.localScale = new Vector3(1, 1, 1);

        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //hit detection for Y. check if we can move in Y direction by casting a box to the destination first. if null (no hit) we can move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            //move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        //hit detection for X. check if we can move in X direction by casting a box to the destination first. if null (no hit) we can move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime),
            LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            //move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }


    }
}
