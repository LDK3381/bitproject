using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;
    UnityEngine.UI.Image noteImage;

    void OnEnable()
    {
        //노트 enable = true
        if(noteImage == null)
            noteImage = GetComponent<UnityEngine.UI.Image>();
        noteImage.enabled = true;
    }

    void Update()
    {
        //노트 이동.
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }

    public void HideNote()
    {
        //노트 enable = false
        noteImage.enabled = false;
    }

    public bool GetNoteFlag()
    {
        //노트에 플래그 추가
        return noteImage.enabled;
    }
}
