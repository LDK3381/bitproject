using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; //bit per minute
    double currenTime = 0d;

    [SerializeField] Transform tfNoteAppear = null;
    NoteTimingManager noteTimingManager = null;

    void Start()
    {
        noteTimingManager = GetComponent<NoteTimingManager>();
    }

    void Update()
    {
        BpmCheck();
    }

    private void BpmCheck()
    {
        try
        {
            //currentTime = 0.5100555~~, 약간의 오차, 0으로 초기화가 아닌 -
            currenTime += Time.deltaTime;

            //bpm에 맞추어, 큐를 호출해서 노트를 보여줌.
            if (currenTime >= 60d / bpm)
            {
                GameObject t_note = NotePooler.instance.noteQueue.Dequeue();
                t_note.transform.position = tfNoteAppear.position;
                t_note.SetActive(true);

                t_note.transform.localScale = new Vector3(1f, 1f, 1f);
                noteTimingManager.noteList.Add(t_note);
                currenTime -= 60d / bpm;
            }
        }
        catch
        {
            Debug.Log("NoteManager.BpmCheck Error");
        }
    }

    //노트가 컨테이너 끝에 닿으면 비활성화
    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.CompareTag("Note"))
            {
                if (collision.GetComponent<Note>().GetNoteFlag())
                {
                    noteTimingManager.DoveStop();
                }

                noteTimingManager.noteList.Remove(collision.gameObject);
                NotePooler.instance.noteQueue.Enqueue(collision.gameObject);
                collision.gameObject.SetActive(false);
            }
        }
        catch
        {
            Debug.Log("NoteManager.OnTriggerEnter2D Error");
        }
    }
}
