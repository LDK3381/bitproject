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
        try
        {
            noteHitAnimator.SetTrigger(notehit);
        }
        catch
        {
            Debug.Log("NoteEffectManager.NoteHitEffect Error");
        }
    }

    public void NoteBounce()
    {
        try
        {
            noteCheckAnimator.SetTrigger(notehit);
        }
        catch
        {
            Debug.Log("NoteEffectManager.NoteBounce Error");
        }
    }

    public void DoveBounce()
    {
        try
        {
            doveAnimator.SetTrigger(notehit);
        }
        catch
        {
            Debug.Log("NoteEffectManager.DoveBounce Error");
        }
    }
}
