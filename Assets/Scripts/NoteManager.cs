using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0; //bit per minute
    double currenTime = 0d;

    [SerializeField] Transform tfNoteAppear = null;
    [SerializeField] GameObject goNote1 = null;
    //[SerializeField] GameObject goNote2 = null;
    //[SerializeField] GameObject goNote3 = null;

    void Start()
    {

    }

    void Update()
    {
        //currentTime = 0.5100555~~, 약간의 오차, 0으로 초기화가 아닌 -
        currenTime += Time.deltaTime;

        if (currenTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote1, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            currenTime -= 60d / bpm;    
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            Destroy(collision.gameObject);
        }
    }
}
