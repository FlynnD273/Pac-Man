using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class GhostPink : MonoBehaviour
{
    public GameObject Player;

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
        ec.Target = (Vector2)Player.transform.position.Round() + pc.Direction * 4;
    }
}
