using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteFlame : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = null;

    private bool musicStart = false;
    private Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (musicStart == false)
            {
                NoteTrigger(collision);
            }
        }
        catch
        {
            Debug.Log("NoteFlame.OnTriggerEnter2D Error");
        }
    }

    private void NoteTrigger(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            soundManager.PlayBGM();
            musicStart = true;
        }
    }
}
