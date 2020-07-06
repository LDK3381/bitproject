using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTimingManager : MonoBehaviour
{
    public List<GameObject> NoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform TimingRect = null;
    [SerializeField] GameObject[] Dove = null;
    [SerializeField] NoteComboManager noteComboManager = null;

    Vector2 TimingBoxes;
    NoteEffectManager noteEffectManager = null;
    

    void Start()
    {
        noteEffectManager = FindObjectOfType<NoteEffectManager>();
        TimingBoxes = new Vector2();

        TimingBoxes.Set(Center.localPosition.x - TimingRect.rect.width / 2,
            Center.localPosition.x + TimingRect.rect.width / 2);
    }

    public void CheckTiming()
    {
        for(int i = 0; i < NoteList.Count; i++)
        {
            float t_notePosX = NoteList[i].transform.localPosition.x;

            if (TimingBoxes.x <= t_notePosX && t_notePosX <= TimingBoxes.y)
            {
                //노트 제거
                NoteList[i].GetComponent<Note>().HideNote();
                NoteList.RemoveAt(i);
                //노트 이펙트
                noteEffectManager.NoteHitEffect();
                noteEffectManager.DoveBounce();
                //노트 콤보
                noteComboManager.IncreaseCombo();

                Debug.Log("Hit");

                Dove[2].SetActive(false);
                DoveFly();
                return;
            }
        }
        DoveStop();
    }
    private void DoveFly()
    {
        if(!GameObject.Find("Dove1"))
        {
            GameObject.Find("Dove").transform.Find("Dove1").gameObject.SetActive(true);
            GameObject.Find("Dove").transform.Find("Dove2").gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("Dove").transform.Find("Dove1").gameObject.SetActive(false);
            GameObject.Find("Dove").transform.Find("Dove2").gameObject.SetActive(true);
        }
    }
    public void DoveStop()
    {
        noteComboManager.ResetCombo();
        noteEffectManager.DoveBounce();

        Dove[0].SetActive(false);
        Dove[1].SetActive(false);
        Dove[2].SetActive(true);
        Debug.Log("Bad or Miss");
    }
}
