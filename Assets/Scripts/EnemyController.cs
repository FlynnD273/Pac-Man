using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum Direction { UP, DOWN, LEFT, RIGHT };

    public float Speed;
    public Vector2 Target;
    public LayerMask Filter;

    private Vector2 lastMove;
    private Vector2 move;
    private Stopwatch moveTime;

    private Vector2 start;
    private Vector2 nextTile;

    private Vector2[] noTurn;

    // Start is called before the first frame update
    void Start()
    {
        moveTime = new Stopwatch();
        noTurn = new Vector2[] { 
            new Vector2(-1, -11), 
            new Vector2(2, -11), 
            new Vector2(2, 0), 
            new Vector2(-1, 0) };
    }

    private RaycastHit2D Cast (Vector2 dir, float dist)
    {
        RaycastHit2D hit = Physics2D.Raycast(start, dir, dist, Filter);

        if (hit.collider == null)
            hit.distance = 99;

        return hit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!moveTime.IsRunning || moveTime.ElapsedMilliseconds > 1 / Speed * 1000)
        {
            if (nextTile.x > 13)
            {
                transform.position = new Vector3(-12, transform.position.y, transform.position.z);
            }
            if (nextTile.x < -12)
            {
                transform.position = new Vector3(12, transform.position.y, transform.position.z);
            }
            NextMove();
            moveTime.Restart();
        }

        transform.position = Vector2.Lerp(start, nextTile, moveTime.ElapsedMilliseconds * Speed / 1000);
    }

    private void NextMove()
    {
        Target = new Vector2(Mathf.Round(Target.x), Mathf.Round(Target.y));
        start = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

        bool[] walls = new bool[4];
        walls[0] = Cast(Vector2.up,    2).distance < 0.75f; //Up
        walls[1] = Cast(Vector2.down,  2).distance < 0.75f; //Down
        walls[2] = Cast(Vector2.left,  2).distance < 0.75f; //Left
        walls[3] = Cast(Vector2.right, 2).distance < 0.75f; //Right

        foreach (Vector2 v in noTurn)
        {
            if (v.Equals(start))
            {
                walls[0] = true;
                break;
            }
        }

        if (lastMove.Equals(Vector2.up))
        {
            walls[1] = true;
        }
        else if (lastMove.Equals(Vector2.down))
        {
            walls[0] = true;
        }
        else if (lastMove.Equals(Vector2.left))
        {
            walls[3] = true;
        }
        else if (lastMove.Equals(Vector2.right))
        {
            walls[2] = true;
        }

        //float[] dist = new float[4];
        //Direction min = 0;
        float minDist = 9999, d;

        d = Vector2.Distance(start + Vector2.right, Target);
        if (!walls[3] && d <= minDist)
        {
            minDist = d;
            move = Vector2.right;
        }

        d = Vector2.Distance(start + Vector2.down, Target);
        if (!walls[1] && d <= minDist)
        {
            minDist = d;
            move = Vector2.down;
        }

        d = Vector2.Distance(start + Vector2.left, Target);
        if (!walls[2] && d <= minDist)
        {
            minDist = d;
            move = Vector2.left;
        }

        d = Vector2.Distance(start + Vector2.up, Target);
        if (!walls[0] && d <= minDist)
        {
            minDist = d;
            move = Vector2.up;
        }

        //Vector2 p = transform.position;

        //if (Target.y > p.y && !walls[0])
        //{
        //    move = Vector2.up;
        //}
        //else if (Target.x > p.x && !walls[3])
        //{
        //    move = Vector2.right;
        //}
        //else if (Target.x < p.x && !walls[2])
        //{
        //    move = Vector2.left;
        //}
        //else if (!walls[1])
        //{
        //    move = Vector2.down;
        //}
        //else if (!walls[0])
        //{
        //    move = Vector2.up;
        //}
        //else if (!walls[1])
        //{
        //    move = Vector2.down;
        //}
        //else if (!walls[2])
        //{
        //    move = Vector2.left;
        //}
        //else if (!walls[3])
        //{
        //    move = Vector2.right;
        //}

        nextTile = start + move;

        lastMove = move;
    }
}
