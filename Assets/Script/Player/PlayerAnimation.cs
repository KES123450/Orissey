using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    public void Play(PlayerAnimationType type,float force=0)
    {
        switch (type)
        {
            case PlayerAnimationType.JumpAni:
                StartCoroutine(JumpAnimation());
                break;
            case PlayerAnimationType.LandAni:
                StartCoroutine(LandAnimation(force));
                break;

        }
    }

    private IEnumerator JumpAnimation()
    {
        transform.DOScale(new Vector3(1.2f, 0.7f, 1f), 0.2f).SetEase(Ease.OutBack);
        transform.DOLocalMoveY(-0.35f, 0.2f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.25f);
        transform.DOLocalMoveY(0.25f, 0.2f).SetEase(Ease.OutBack);
        transform.DOScale(new Vector3(0.8f, 1.3f, 1f), 0.4f).SetEase(Ease.OutBack);
    }

    private IEnumerator LandAnimation(float force=0)
    {
        
        force= force / 20;
        force = Mathf.Lerp(1, 1.2f, force);
        transform.DOLocalMoveY(0f, 0.2f).SetEase(Ease.OutBack);
        transform.DOScale(new Vector3(1.1f*force, 0.8f, 1f), 0.2f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.2f);
        transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.OutBack);
    }
}
