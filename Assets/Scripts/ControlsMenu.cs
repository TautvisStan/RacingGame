using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    private Singleton singleton;
    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();
    private int PlayerNumber = 1;
    private GameObject currentKey;
    public Text Forwards, Backwards, Left, Right, Break, Reset, Confirm;
    private bool changed = false;
    private void Awake()
    {
        singleton = FindObjectOfType<Singleton>();
    }
    public void GetControls(int player)
    {
        if(changed)
        {
            changed = false;
            SaveControls();
        }
        PlayerNumber = player;
        controls = singleton.GetControls(player);
        Setup();
    }
    public void SaveControls()
    {
        singleton.SetControls(controls, PlayerNumber);
    }
    public void Setup()
    {
        Forwards.text = controls["Forwards"].ToString();
        Backwards.text = controls["Backwards"].ToString();
        Left.text = controls["Left"].ToString();
        Right.text = controls["Right"].ToString();
        Break.text = controls["Break"].ToString();
        Reset.text = controls["Reset"].ToString();
        Confirm.text = controls["Confirm"].ToString();
    }
    private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if(e.isKey)
            {
                controls[currentKey.name] = e.keyCode;
                RenameButton(currentKey, e.keyCode.ToString());
                currentKey = null;
                changed = true;
            }
        }
    }
    public void ClickedButton(GameObject clicked)
    {
        currentKey = clicked;
    }
    public void RenameButton(GameObject Button, string text)
    {
        Button.transform.GetChild(0).GetComponent<Text>().text = text;
    }
}
