using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRed : MonoBehaviour
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
        ec.Target = Player.transform.position;
    }
}
