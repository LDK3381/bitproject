using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MtWeaponManager : MonoBehaviourPun
{
    public int selectedWeapon = 0;

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
