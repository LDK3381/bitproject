using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoteFlame : MonoBehaviour
{
    bool musicStart = false;
    [SerializeField] SoundManager soundManager = null;
    [SerializeField] AIController aiController = null;
    private Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(musicStart == false)
        {
            if (collision.CompareTag("Note"))
            {
                soundManager.PlayRandomBGM();
                CheckCurrentScene();
                musicStart = true;
            }
        }
    }

    public void CheckCurrentScene()
    {
        if (currentScene.name == "SgMap1" || currentScene.name == "SgMap2")
            aiController.AIStart();     //리듬 노드 한 단위마다 ai가 자동으로 한칸씩 랜덤 이동
    }

}
