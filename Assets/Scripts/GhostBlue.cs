using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBlue : MonoBehaviour
{
    public GameObject Player;
    public GameObject Blinky;

    private PlayerController pc;
    private EnemyController ec;
    // Start is called before the first frame update
    void Start()
    {
        ec = GetComponent<EnemyController>();
        pc = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 t = 2 * ((Vector2)Player.transform.position.Round() + pc.Direction * 2) - (Vector2)Blinky.transform.position.Round();
        ec.Target = t;
    }
}
