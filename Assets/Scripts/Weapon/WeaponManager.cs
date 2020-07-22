using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponManager : MonoBehaviourPun
{
    public int selectedWeapon = 0;

    void Start()
    {
        photonView.RPC("SelectWeapon", RpcTarget.All);
    }

    void Update()
    {
        #region 무기 변환 1,2,3
        int previousSelectedWeapon = selectedWeapon;

        #region 스크롤로 무기 변환 기능(쓸지 말지 일단 보류)
        //if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        //{
        //    if (selectedWeapon >= transform.childCount - 1)
        //        selectedWeapon = 0;
        //    else
        //        selectedWeapon++;
        //}

        //if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        //{
        //    if (selectedWeapon <= 0)
        //        selectedWeapon = transform.childCount - 1;
        //    else
        //        selectedWeapon--;
        //}
        #endregion

        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedWeapon = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
            selectedWeapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
            selectedWeapon = 2;
        if (previousSelectedWeapon != selectedWeapon)
            photonView.RPC("SelectWeapon", RpcTarget.All);
        #endregion
    }

    // 무기 선택함수
    [PunRPC]
    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
