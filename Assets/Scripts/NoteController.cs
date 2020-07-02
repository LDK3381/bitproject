using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    NoteTimingManager _TimingManager;

    // Start is called before the first frame update
    void Start()
    {
        _TimingManager = FindObjectOfType<NoteTimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //판정
            _TimingManager.CheckTiming();
        }
    }
}
