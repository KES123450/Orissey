using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovementController : MonoBehaviour
{
    public float speed;
    public float endPosX;
    public Vector3 startPos;


    void Update()
    {
        if (transform.localPosition.x >= endPosX)
        {
            transform.localPosition = startPos;
        }
        transform.Translate(speed, 0, 0);
    }
}
