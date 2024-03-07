using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerRotateState : MonoBehaviour, IPlayerState
{
    private bool isRotating;
    private bool isFirstRotateClick;
    public void Handle(PlayerController controller)
    {/*
        if (Input.GetKey(KeyCode.RightArrow) && isFirstRotateClick)
        {
            controller.DuckAnimator.SetTrigger("isRotateLeftStart");
            isFirstRotateClick = false;
            StartCoroutine(DelayedRotate());
        }*/

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //if (!isRotating) return;
            controller.DuckAnimator.SetBool("isRotateLeft",true);
            controller.playerRigid.AddTorque(controller.torqueForce);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            controller.DuckAnimator.SetBool("isRotateLeft", false);
            isRotating = false;
            isFirstRotateClick = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            controller.DuckAnimator.SetBool("isRotateRight", true);
            controller.playerRigid.AddTorque(-controller.torqueForce);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            controller.DuckAnimator.SetBool("isRotateRight", false);
            isRotating = false;
            isFirstRotateClick = true;
        }
    }
    
    private IEnumerator DelayedRotate()
    {
        yield return new WaitForSeconds(0.2f);
        isRotating = true;

    }
}
