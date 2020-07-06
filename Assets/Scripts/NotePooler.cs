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

    Queue<GameObject> InsertQueue(NoteInfo _noteInfo)
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();
        for (int i = 0; i < _noteInfo.count; i++)
        {
            GameObject t_clone = Instantiate(_noteInfo.goNote[Random.Range(0,3)], transform.position, Quaternion.identity);
            t_clone.SetActive(false);
            if (_noteInfo.tfPoolParent != null)
                t_clone.transform.SetParent(_noteInfo.tfPoolParent);
            else
                t_clone.transform.SetParent(this.transform);
            t_queue.Enqueue(t_clone);
        }
        return t_queue;
    }
}
