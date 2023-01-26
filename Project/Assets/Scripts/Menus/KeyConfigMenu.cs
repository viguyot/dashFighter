using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyConfigMenu : MonoBehaviour
{
    private Dictionary<string, KeyCode> Keys = new Dictionary<string, KeyCode>();

    [SerializeField]
    private Text left, right, jump, pause, dash, flash, shield, grab;

    private GameObject currentKey;

    ///<summary>
    /// Add every couple of string and keys to the dictionary and add the text to the button
    ///<summary>
    void Start()
    {
        Keys.Add("LeftButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton","LeftArrow")));
        Keys.Add("RightButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton","RightArrow")));
        Keys.Add("JumpButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton","Space")));
        Keys.Add("PauseButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("PauseButton","Escape")));

        left.text = Keys["LeftButton"].ToString();
        right.text = Keys["RightButton"].ToString();
        jump.text = Keys["JumpButton"].ToString();
        pause.text = Keys["PauseButton"].ToString();
        
        Keys.Add("DashButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DashButton","A")));
        Keys.Add("FlashButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("FlashButton","Z")));
        Keys.Add("ShieldButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ShieldButton","E")));
        Keys.Add("GrabButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("GrabButton","R")));

        dash.text = Keys["DashButton"].ToString();
        flash.text = Keys["FlashButton"].ToString();
        shield.text = Keys["ShieldButton"].ToString();
        grab.text = Keys["GrabButton"].ToString();
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
