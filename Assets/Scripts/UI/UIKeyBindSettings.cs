﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyBindSettings : MonoBehaviour
{
    [Header("키 설정")]
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text up, down, left, right, weapon1, weapon2, weapon3;
    private GameObject currentKey = null;

    private Color32 before = new Color32(39, 171, 249, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);

    void Start()
    {
        //기본 키값
        keys.Add("Button_Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Up", "W")));
        keys.Add("Button_Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Down", "S")));
        keys.Add("Button_Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Left", "A")));
        keys.Add("Button_Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Right", "D")));
        keys.Add("Button_Weapon1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Weapon1", "Alpha1")));
        keys.Add("Button_Weapon2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Weapon2", "Alpha2")));
        keys.Add("Button_Weapon3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Button_Weapon3", "Alpha3")));

        //화면에 변경된 키를 버튼에 그대로 표시
        up.text = keys["Button_Up"].ToString();
        down.text = keys["Button_Down"].ToString();
        left.text = keys["Button_Left"].ToString();
        right.text = keys["Button_Right"].ToString();
        weapon1.text = keys["Button_Weapon1"].ToString();
        weapon2.text = keys["Button_Weapon2"].ToString();
        weapon3.text = keys["Button_Weapon3"].ToString();

        SaveKeys();
    }

    //화면에 출력
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = before;
                currentKey = null;
            }
        }
    }


    //버튼 클릭 시, 키 변경 전/후에 따라 색상 변경
    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = before;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;

        SaveKeys();
    }

    //변경한 키값 저장
    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
        Debug.Log("키 변경 완료");
    }

    //초기화 버튼 클릭 시 바뀐 키값 전부 초기화
    public void ResetKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.DeleteKey(key.Key);
        }

        //PlayerPref 삭제 후, Dictionary 삭제 처리
        keys.Remove("Button_Up");
        keys.Remove("Button_Down");
        keys.Remove("Button_Left");
        keys.Remove("Button_Right");
        keys.Remove("Button_Weapon1");
        keys.Remove("Button_Weapon2");
        keys.Remove("Button_Weapon3");

        Start();
    }
}