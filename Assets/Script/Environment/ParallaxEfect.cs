using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEfect : MonoBehaviour
{
    private float startposX, startposY;
    private GameObject cam;
    public float parallaxFactorX;
    public float parallaxFactorY;

    void Start()
    {
        cam = Camera.main.gameObject;
        startposX = transform.position.x;
        startposY = transform.position.y;

    }

    
    void Update()
    {
        float distanceX = cam.transform.position.x * parallaxFactorX;
        float distanceY = cam.transform.position.y * parallaxFactorY;
        Vector3 newPosition = new Vector3(startposX + distanceX, startposY+distanceY, transform.position.z);
        transform.position = newPosition;
    }

}
