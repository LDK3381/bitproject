using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteFlame : MonoBehaviour
{
    bool musicStart = false;
    [SerializeField] SoundManager soundManager = null;
    [SerializeField] AIController aiController = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(musicStart == false)
        {
            if (collision.CompareTag("Note"))
            {
                soundManager.PlayRandomBGM();
                aiController.AIStart();     //리듬 노드 한 단위마다 ai가 자동으로 한칸씩 랜덤 이동
                musicStart = true;
            }
        }
    }
}
