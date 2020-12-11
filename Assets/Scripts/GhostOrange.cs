using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostOrange : MonoBehaviour
{
    public GameObject Player;

    private EnemyController ec;
    // Start is called before the first frame update
    void Start()
    {
        ec = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) > 8)
        {
            ec.Target = Player.transform.position;
        }
        else
        {
            ec.Target = new Vector2(-12, -18);
        }
    }
}
