using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; //bit per minute
    double currenTime = 0d;

    [SerializeField] Transform tfNoteAppear = null;
    [SerializeField] GameObject[] goNote = null;
    NoteTimingManager _NoteTimingManager = null;

    void Start()
    {
        _NoteTimingManager = GetComponent<NoteTimingManager>();
    }

    void Update()
    {
        //currentTime = 0.5100555~~, 약간의 오차, 0으로 초기화가 아닌 -
        currenTime += Time.deltaTime;
       
        if (currenTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote[Random.Range(0,3)], tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            t_note.transform.localScale = new Vector3(1f, 1f, 1f);
            _NoteTimingManager.NoteList.Add(t_note);
            currenTime -= 60d / bpm;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            _NoteTimingManager.NoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            GetComponent<NoteTimingManager>().DoveStop();
        }
    }
}
