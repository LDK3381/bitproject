using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class BombSpawn : MonoBehaviour
{
    public Transform throwPoint = null;
    public GameObject bomb = null;
    public GameObject weapon = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //마우스 좌클릭 시, 폭탄 투척
        if (Input.GetMouseButtonDown(0))
        {
            CreateBomb();
        }
    }

    private void CreateBomb()
    {
        //현재 무기가 폭탄일 때에만 투척하도록 제한
        if(weapon.activeSelf == true)
        {
            GameObject bombInstance = Instantiate(bomb, throwPoint.position, throwPoint.rotation);          
            Destroy(bombInstance, 2);
        }
    }
}
