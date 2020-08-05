using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteInfo
{
    public GameObject[] goNote;
    public int count;
    public Transform tfPoolParent;
}

public class NotePooler : MonoBehaviour
{
    [SerializeField] NoteInfo[] noteInfo = null;

    public static NotePooler instance;

    public Queue<GameObject> noteQueue = new Queue<GameObject>();

    void Start()
    {
        instance = this;
        noteQueue = InsertQueue(noteInfo[0]);
    }

    //노트를 큐를 사용해서 관리.
    //노트클론 여러개를 만들고, 순차대로 enable로 보이고 숨기는 것을 반복.
    Queue<GameObject> InsertQueue(NoteInfo _noteInfo)
    {
        try
        {
            Queue<GameObject> t_queue = new Queue<GameObject>();

            for (int i = 0; i < _noteInfo.count; i++)
            {
                GameObject t_clone = Instantiate(_noteInfo.goNote[Random.Range(0, 3)], transform.position, Quaternion.identity);
                t_clone.SetActive(false);
                if (_noteInfo.tfPoolParent != null)
                    t_clone.transform.SetParent(_noteInfo.tfPoolParent);
                else
                    t_clone.transform.SetParent(this.transform);
                t_queue.Enqueue(t_clone);
            }
            return t_queue;
        }
        catch
        {
            Debug.Log("NotePooler.InsertQueue Error");
            return null;
        }

    }
}
