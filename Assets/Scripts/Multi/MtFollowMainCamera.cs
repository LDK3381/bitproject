using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MtFollowMainCamera : MonoBehaviourPun
{
    public float offsetX =  0f;
    public float offsetY =  3f;
    public float offsetZ =  -0.5f;
    GameObject obj;

    Vector3 cameraPosition;

    private void Start()
    {
        try
        {
            obj = FindObjectOfType<GameObject>();
        }
        catch
        {
            Debug.Log("MtFollowMainCamera.Start Error");
        }
    }

    void LateUpdate()
    {
        try
        {
            if (obj.CompareTag("Player"))
            {
                cameraPosition.x = obj.transform.position.x + offsetX;
                cameraPosition.y = obj.transform.position.y + offsetY;
                cameraPosition.z = obj.transform.position.z + offsetZ;

                transform.position = cameraPosition;
            }
        }
        catch
        {
            Debug.Log("MtFollowMainCamera.LateUpdate Error");
        }
    }
}
