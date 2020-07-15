using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    [SerializeField] Animator noteCheckAnimator = null;
    [SerializeField] Animator doveAnimator = null;

    string notehit = "NoteHit";

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(notehit);
    }

    public void NoteBounce()
    {
        noteCheckAnimator.SetTrigger(notehit);
    }

    public void DoveBounce()
    {
        doveAnimator.SetTrigger(notehit);
    }
}
