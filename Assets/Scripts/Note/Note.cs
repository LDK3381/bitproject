using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;
    UnityEngine.UI.Image noteImage;

    void Update()
    {
        //노트 이동.
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }

    void OnEnable()
    {
        try
        {
            //노트 enable = true
            if (noteImage == null)
                noteImage = GetComponent<UnityEngine.UI.Image>();
            noteImage.enabled = true;
        }
        catch
        {
            Debug.Log("Note.OnEnable Error");
        }
    }

    public void HideNote()
    {
        try
        {
            //노트 enable = false
            noteImage.enabled = false;
        }
        catch
        {
            Debug.Log("Note.HideNote Error");
        }
      
    }

    public bool GetNoteFlag()
    {
        //노트에 플래그 추가
        return noteImage.enabled;
    }
}
