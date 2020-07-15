using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    bool musicStart = false;
    [SerializeField] SoundManager _soundManager = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(musicStart == false)
        {
            if (collision.CompareTag("Note"))
            {
                _soundManager.PlayRandomBGM();
                musicStart = true;
            }
        }
    }
}
