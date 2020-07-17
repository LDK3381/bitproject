using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFlame : MonoBehaviour
{
    bool musicStart = false;
    [SerializeField] SoundManager soundManager = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(musicStart == false)
        {
            if (collision.CompareTag("Note"))
            {
                soundManager.PlayRandomBGM();
                musicStart = true;
            }
        }
    }
}
