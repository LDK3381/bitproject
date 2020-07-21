using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class BombSpawn : MonoBehaviour
{
    [Header("현재 장착된 총")]
    public Transform throwPoint = null;
    public GameObject bomb = null;
    public GameObject weapon = null;

    GameObject bombInstance = null;
    
    [SerializeField] Text txt_Bomb = null;

    [SerializeField] int bombCount = 1;

    void Start()
    {
        BombUiSetting();
    }

        void Update()
    {
        //마우스 좌클릭 시, 폭탄 투척
        if (Input.GetMouseButtonDown(0))
        {
            CreateBomb();
        }
    }
    public void BombCountUp(int extra)
    {
        bombCount += extra;
    }

    public void BombUiSetting()
    {
        txt_Bomb.text = "x " + bombCount;
    }

    private void CreateBomb()
    {
        //현재 무기가 폭탄일 때에만 투척하도록 제한
        if (weapon.activeSelf == true && bombCount > 0)
        {
            bombCount--;
            BombUiSetting();
            bombInstance = Instantiate(bomb, throwPoint.position, throwPoint.rotation);
            Destroy(bombInstance, 2);
        }
    }
}
