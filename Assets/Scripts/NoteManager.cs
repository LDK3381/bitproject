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
        //currentTime = 0.5100555~~, 약간의 오차, 0으로 초기화가 아닌 -
        currenTime += Time.deltaTime;

        if (currenTime >= 60d / bpm)
        {
            GameObject t_note = NotePooler.instance.noteQueue.Dequeue();
            t_note.transform.position = tfNoteAppear.position;
            t_note.SetActive(true);

            t_note.transform.localScale = new Vector3(1f, 1f, 1f);
            noteTimingManager.NoteList.Add(t_note);
            currenTime -= 60d / bpm;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                noteTimingManager.DoveStop();
            }

            noteTimingManager.NoteList.Remove(collision.gameObject);
            NotePooler.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }
}
