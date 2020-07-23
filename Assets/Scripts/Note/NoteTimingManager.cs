using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTimingManager : MonoBehaviour
{
    public List<GameObject> noteList = new List<GameObject>();

    [SerializeField] Transform center = null;
    [SerializeField] RectTransform timingRect = null;
    [SerializeField] GameObject[] dove = null;
    [SerializeField] NoteComboManager noteComboManager = null;
    [SerializeField] SgAIManager sgaiManager = null;

    Vector2 timingBoxes;
    NoteEffectManager noteEffectManager = null;
    
    void Start()
    {
        noteEffectManager = FindObjectOfType<NoteEffectManager>();
        timingBoxes = new Vector2();

        timingBoxes.Set(center.localPosition.x - timingRect.rect.width / 2,
            center.localPosition.x + timingRect.rect.width / 2);
    }

    public bool CheckTiming()
    {
        for(int i = 0; i < noteList.Count; i++)
        {
            float t_notePosX = noteList[i].transform.localPosition.x;

            if (timingBoxes.x <= t_notePosX && t_notePosX <= timingBoxes.y)
            {
                //노트 제거
                noteList[i].GetComponent<Note>().HideNote();
                noteList.RemoveAt(i);
                //노트 이펙트
                noteEffectManager.NoteHitEffect();
                noteEffectManager.DoveBounce();
                //노트 콤보
                noteComboManager.IncreaseCombo();

                //AI 움직임 작동
                //sgaiManager.AIMove();

                Debug.Log("Hit");

                dove[2].SetActive(false);
                DoveFly();
                return true;
            }
        }
        DoveStop();
        return false;
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

        dove[0].SetActive(false);
        dove[1].SetActive(false);
        dove[2].SetActive(true);
        Debug.Log("Bad or Miss");
    }
}
