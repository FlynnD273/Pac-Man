using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DotController : MonoBehaviour
{
    public UnityEvent Eaten = new UnityEvent();

    private CircleCollider2D circle;
    // Start is called before the first frame update
    void Start()
    {
        circle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.IsTouching(circle) && col.CompareTag("Player"))
        {
            Eaten.Invoke();
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
