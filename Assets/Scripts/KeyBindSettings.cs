using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindSettings : MonoBehaviour
{
    [Header("키 설정")]
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    public Text up, down, left, right, fire, weapon1, weapon2, weapon3;
    private GameObject currentKey = null;

    private Color32 before = new Color32(39, 171, 249, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);

    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "W")));
        keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "S")));
        keys.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A")));
        keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D")));
        //keys.Add("Fire", KeyCode.W);
        //keys.Add("Weapon1", KeyCode.1);
        //keys.Add("Weapon2", KeyCode.W);
        //keys.Add("Weapon3", KeyCode.W);

        up.text = keys["Up"].ToString();
        down.text = keys["Down"].ToString();
        left.text = keys["Left"].ToString();
        right.text = keys["Right"].ToString();
    }

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


    //키 변경
    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = before;
        }

        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    //변경한 키값 저장
    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
    }


}
