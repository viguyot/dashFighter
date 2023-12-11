using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyConfigMenu : MonoBehaviour
{
    private Dictionary<string, KeyCode> Keys = new Dictionary<string, KeyCode>();

    [SerializeField]
    private Text left, right, jump, down, dash, grab, attack, charge;

    [SerializeField]
    private Text left2, right2, jump2, down2, dash2, grab2, attack2, charge2;

    private GameObject currentKey;

    ///<summary>
    /// Add every couple of string and keys to the dictionary and add the text to the button
    ///<summary>
    void Start()
    {
        Keys.Add("LeftButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton","Q")));
        Keys.Add("RightButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton","D")));
        Keys.Add("JumpButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton","Z")));
        Keys.Add("DownButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DownButton","S")));
        Keys.Add("DashButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DashButton","A")));
        Keys.Add("GrabButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GrabButton","F")));
        Keys.Add("AttackButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AttackButton","E")));
        Keys.Add("ChargeButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ChargeButton","R")));

        left.text = Keys["LeftButton"].ToString();
        right.text = Keys["RightButton"].ToString();
        jump.text = Keys["JumpButton"].ToString();
        down.text = Keys["DownButton"].ToString();
        dash.text = Keys["DashButton"].ToString();
        grab.text = Keys["GrabButton"].ToString();
        attack.text = Keys["AttackButton"].ToString();
        charge.text = Keys["ChargeButton"].ToString();
        
        Keys.Add("LeftButton2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton2","J")));
        Keys.Add("RightButton2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton2","L")));
        Keys.Add("JumpButton2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton2","I")));
        Keys.Add("DownButton2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DownButton2","K")));
        Keys.Add("DashButton2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DashButton2","U")));
        Keys.Add("GrabButton2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GrabButton2","M")));
        Keys.Add("AttackButton2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("AttackButton2","O")));
        Keys.Add("ChargeButton2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ChargeButton2","P")));

        left2.text = Keys["LeftButton2"].ToString();
        right2.text = Keys["RightButton2"].ToString();
        jump2.text = Keys["JumpButton2"].ToString();
        down2.text = Keys["DownButton2"].ToString();
        dash2.text = Keys["DashButton2"].ToString();
        grab2.text = Keys["GrabButton2"].ToString();
        attack2.text = Keys["AttackButton2"].ToString();
        charge2.text = Keys["ChargeButton2"].ToString();
    }

    ///<summary>
    /// if a button to link a key is clicked, wait for the user to press a key to reassign
    ///<summary>
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                Keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey = null;
            }
        }
    }

    ///<summary>
    /// call if the button link to a key is clicked
    ///<summary>
    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }

    ///<summary>
    /// save the keys assigned by the player
    ///<summary>
    public void SaveKeys()
    {
        foreach (var key in Keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }

        PlayerPrefs.Save();
        //InputManager.Instance().Keys = Keys;
    }
}
