using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;

    public Vector2 Direction { get; private set; }


    public LayerMask Filter;

    private Vector2 move;

    private Animator anim;
    //private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
    }

    private RaycastHit2D Cast(Vector2 dir, float dist)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dist, Filter);

        if (hit.collider == null)
            hit.distance = 99;

        return hit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int x = (int)Mathf.Round(Input.GetAxisRaw("Horizontal"));
        int y = (int)Mathf.Round(Input.GetAxisRaw("Vertical"));

        Vector2 prevMove = move;

        if (y != 0)
        {
            move = new Vector2(0, y);
        }
        else if (x != 0)
        {
            move = new Vector2(x, 0);
        }

        RaycastHit2D c = Cast(move, 2);
        if (!move.Equals(Vector2.zero))
        {
            if (c.distance > 0.5f)
            {
                if (move.x != 0)
                {
                    transform.position = new Vector2(transform.position.x, Mathf.Round(transform.position.y));
                }
                else if (move.y != 0)
                {
                    transform.position = new Vector2(Mathf.Round(transform.position.x), transform.position.y);
                }

                transform.rotation = Quaternion.Euler(0, 0, (float)(Math.Atan2(-move.x, move.y) * Mathf.Rad2Deg) + 90);

                Direction = move;
                anim.SetBool("IsMoving", true);
            }
            else
            {
                move = prevMove;
            }
        }

        c = Cast(move, 2);
        if (c.distance <= 0.5f)
        {
            move = Vector2.zero;
            anim.SetBool("IsMoving", false);
        }


        transform.position += (Vector3)move * Speed * Time.deltaTime;

        if (transform.position.x > 13.5)
        {
            transform.position = new Vector2(-12, transform.position.y);
        }
        if (transform.position.x < -12.5)
        {
            transform.position = new Vector2(12, transform.position.y);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, -2);
    }
}
