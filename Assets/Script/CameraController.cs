using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 cameraPos;

   
    void Update()
    {
        cameraPos = player.position+new Vector3(0,3.6f,0);
        cameraPos.z = -10;
        transform.position = cameraPos;
    }
}
