using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    string notehit = "NoteHit";

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(notehit);
    }
}
