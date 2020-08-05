using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SgWeaponManager : MonoBehaviour
{
    public int selectedWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        KeySelect();
    }

    // 무기 선택함수
    private void SelectWeapon()
    {
        try
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
        catch
        {
            Debug.Log("SgWeaponManager.SelectWeapon Error");
        }
    }

    private void KeySelect()
    {
        try
        {
            #region 무기 변환 1,2,3
            int previousSelectedWeapon = selectedWeapon;

            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Weapon1"))))
                selectedWeapon = 0;
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Weapon2"))) && transform.childCount >= 2)
                selectedWeapon = 1;
            if (Input.GetKeyDown((KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Weapon3"))) && transform.childCount >= 3)
                selectedWeapon = 2;
            if (previousSelectedWeapon != selectedWeapon)
                SelectWeapon();
            #endregion
        }
        catch
        {
            Debug.Log("SgWeaponManager.KeySelect Error");
        }
    }

    private void ScrollSelect()
    {
        //미사용
        #region 스크롤로 무기 변환 기능
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        #endregion
    }
}