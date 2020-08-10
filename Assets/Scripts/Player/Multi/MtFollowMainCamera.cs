using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MtFollowMainCamera : MonoBehaviourPun
{
    public float offsetX =  0f;
    public float offsetY =  3f;
    public float offsetZ =  -0.5f;
    GameObject isMy;     //본인

    Vector3 cameraPosition;

    private void Start()
    {
        try
        {
            isMy = FindObjectOfType<GameObject>();
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
            if (isMy.CompareTag("Player"))
            {
                cameraPosition.x = isMy.transform.position.x + offsetX;
                cameraPosition.y = isMy.transform.position.y + offsetY;
                cameraPosition.z = isMy.transform.position.z + offsetZ;

                transform.position = cameraPosition;
            }
        }
        catch
        {
            Debug.Log("MtFollowMainCamera.LateUpdate Error");
        }
    }
}
