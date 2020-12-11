using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static Camera Main;

    public CompositeCollider2D Bounds;
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = GetComponent<Camera>();
        if (Main == null)
        {
            Main = cam;
        }

        transform.position = new Vector3(Bounds.bounds.center.x, Bounds.bounds.center.y, -10);

        float camH = cam.orthographicSize * 2;
        float camW = camH * cam.aspect;
        float h = Bounds.bounds.size.y;
        float w = Bounds.bounds.size.x;

        if (h / camH > w / camW)
        {
            cam.orthographicSize *= h / camH;
        }
        else
        {
            cam.orthographicSize *= w / camW;
        }
    }
}
