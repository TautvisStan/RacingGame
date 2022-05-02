using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    private Singleton singleton;
    private Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();
    private int PlayerNumber = 1;
    private GameObject currentKey;
    [SerializeField] private Text Forwards, Backwards, Left, Right, Brake, Reset, Confirm;
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
        Forwards.text = TryGetControls(controls, ControlsStrings.Up);
        Backwards.text = TryGetControls(controls, ControlsStrings.Down);
        Left.text = TryGetControls(controls, ControlsStrings.Left);
        Right.text = TryGetControls(controls, ControlsStrings.Right);
        Brake.text = TryGetControls(controls, ControlsStrings.Brake);
        Reset.text = TryGetControls(controls, ControlsStrings.Reset);
        Confirm.text = TryGetControls(controls, ControlsStrings.Confirm);
    }

    private string TryGetControls(Dictionary<string, KeyCode> controls, string key)
    {
        controls.TryGetValue(key, out KeyCode keyCode);
        if (keyCode == KeyCode.None)
        {
            return "None";
        }
        else
        {
            return keyCode.ToString();
        }
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