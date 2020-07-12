using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMainCamera : MonoBehaviour
{
    public float offsetX = 0f;
    public float offsetY = 25f;
    public float offsetZ = -35f;

    public GameObject player = null;
    Vector3 cameraPosition = new Vector3();

    // Update is called once per frame
    void LateUpdate()
    {
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = cameraPosition;
    }
}
