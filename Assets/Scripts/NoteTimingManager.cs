using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTimingManager : MonoBehaviour
{
    public List<GameObject> NoteList = new List<GameObject>();

    [SerializeField] Transform Center = null;
    //[SerializeField] RectTransform[] TimingRect = null;
    [SerializeField] RectTransform TimingRect = null;
    [SerializeField] GameObject[] Dove = null;

    //Vector2[] TimingBoxes = null;
    Vector2 TimingBoxes;
    NoteEffectManager _NoteEffectManager = null;

    void Start()
    {
        _NoteEffectManager = FindObjectOfType<NoteEffectManager>();
        TimingBoxes = new Vector2();

        TimingBoxes.Set(Center.localPosition.x - TimingRect.rect.width / 2,
            Center.localPosition.x + TimingRect.rect.width / 2);

        //for(int i = 0; i < TimingRect.Length; i++)
        //{
        //    TimingBoxes[i].Set(Center.localPosition.x - TimingRect[i].rect.width / 2,
        //        Center.localPosition.x + TimingRect[i].rect.width / 2);
        //}

    }

    public void CheckTiming()
    {
        for(int i = 0; i < NoteList.Count; i++)
        {
            float t_notePosX = NoteList[i].transform.localPosition.x;

            if (TimingBoxes.x <= t_notePosX && t_notePosX <= TimingBoxes.y)
            {
                NoteList[i].GetComponent<Note>().HideNote();
                _NoteEffectManager.NoteHitEffect();
                NoteList.RemoveAt(i);

                Debug.Log("Hit");
                Dove[2].SetActive(false);
                DoveFly();
                return;
            }

            //for(int x = 0; x < TimingBoxes.Length; x++) //TimingBoxes가 1개
            //{
            //    if(TimingBoxes[x].x <= t_notePosX && t_notePosX <= TimingBoxes[x].y)
            //    {
            //        NoteList[i].GetComponent<Note>().HideNote();
            //        _NoteEffectManager.NoteHitEffect();
            //        NoteList.RemoveAt(i);

            //        Debug.Log("Hit" + x);
            //        Dove[2].SetActive(false);
            //        DoveFly();
            //        return;
            //    }
            //}
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
        Dove[0].SetActive(false);
        Dove[1].SetActive(false);
        Dove[2].SetActive(true);
        Debug.Log("Miss");
    }
}
