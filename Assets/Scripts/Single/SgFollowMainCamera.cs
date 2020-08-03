using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SgFollowMainCamera : MonoBehaviour
{
    public float offsetX = 0f;
    public float offsetY = 3f;
    public float offsetZ = -0.5f;
    public GameObject obj;

    Vector3 cameraPosition;

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
            Debug.Log("SgFollowMainCamera.LateUpdate Error");
        }
        
    }
}
