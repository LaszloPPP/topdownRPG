using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter //abstract means this .cs cannot be added as component to objects. it can only be inherited
{
    private Vector3 originalSize;

    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    

    protected virtual void UpdateMotor(Vector3 input)
    {
        //reset moveDelta
        moveDelta = new Vector3 (input.x * xSpeed, input.y * ySpeed, 0);

        //swap sprite direction right or left
        if (moveDelta.x > 0)
        {
            //transform.localScale = Vector3.one;
            transform.localScale = originalSize;

        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(originalSize.x*-1,originalSize.y,originalSize.z);
        }

        //add push vector if any
        moveDelta += pushDirection;

        //reduce push force every frame based on push recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

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
