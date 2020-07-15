using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MtFollowMainCamera : MonoBehaviourPun
{
    public float offsetX = 0f;
    public float offsetY = 3f;
    public float offsetZ = -0.5f;
    GameObject obj;

    Vector3 cameraPosition;

    private void Start()
    {
        obj = FindObjectOfType<GameObject>();
    }

    void LateUpdate()
    {
        if (obj.CompareTag("Player"))
        {
            cameraPosition.x = obj.transform.position.x + offsetX;
            cameraPosition.y = obj.transform.position.y + offsetY;
            cameraPosition.z = obj.transform.position.z + offsetZ;

            transform.position = cameraPosition;
        }
    }
}
