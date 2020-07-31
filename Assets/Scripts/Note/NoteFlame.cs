using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteFlame : MonoBehaviour
{
    [SerializeField] SoundManager soundManager = null;
    [SerializeField] AIController aiController = null;

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
            soundManager.PlayRandomBGM();
            CheckCurrentScene();
            musicStart = true;
        }
    }

    public void CheckCurrentScene()
    {
        try
        {
            if (currentScene.name == "SgMap1" || currentScene.name == "SgMap2")
                aiController.AIStart();     //리듬 노드 한 단위마다 ai가 자동으로 한칸씩 랜덤 이동
        }
        catch
        {
            Debug.Log("NoteFlame.CheckCurrentScene Error");
        }
    }
}
