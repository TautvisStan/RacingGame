using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ControlsStrings
{
    public const string Up = "Forwards";
    public const string Down = "Backwards";
    public const string Left = "Left";
    public const string Right = "Right";
    public const string Brake = "Brake";
    public const string Reset = "Reset";
    public const string Confirm = "Confirm";

}
public class ControlsKeeper : MonoBehaviour
{

    List<Dictionary<string, KeyCode>> controlsList = new List<Dictionary<string, KeyCode>>();
    public void SetupControls()
    {
        Dictionary<string, KeyCode> controlsP1 = new Dictionary<string, KeyCode>();
        Dictionary<string, KeyCode> controlsP2 = new Dictionary<string, KeyCode>();
        Dictionary<string, KeyCode> controlsP3 = new Dictionary<string, KeyCode>();
        Dictionary<string, KeyCode> controlsP4 = new Dictionary<string, KeyCode>();
        controlsList.Add(controlsP1);
        controlsList.Add(controlsP2);
        controlsList.Add(controlsP3);
        controlsList.Add(controlsP4);
        if (PlayerPrefs.GetInt("ControlsSet", 0) == 0)
        {
            SetDefaultControlsP1();
            SetDefaultControlsP2();
            SetDefaultControlsP3();
            SetDefaultControlsP4();
            SaveControls();
        }
        else
        {
            PullControls();
        }
    }
    public List<Dictionary<string, KeyCode>> GetControls()
    {
        return controlsList;
    }
    public Dictionary<string, KeyCode> GetControls(int player)
    {
        return controlsList[player];
    }
    public void SetControls(Dictionary<string, KeyCode> controls, int player)
    {
        controlsList[player] = controls;
        SaveControls();
    }
    private void SetDefaultControlsP1()
    {
        controlsList[0].Add(ControlsStrings.Up, KeyCode.W);
        controlsList[0].Add(ControlsStrings.Down, KeyCode.S);
        controlsList[0].Add(ControlsStrings.Left, KeyCode.A);
        controlsList[0].Add(ControlsStrings.Right, KeyCode.D);
        controlsList[0].Add(ControlsStrings.Brake, KeyCode.Space);
        controlsList[0].Add(ControlsStrings.Reset, KeyCode.R);
        controlsList[0].Add(ControlsStrings.Confirm, KeyCode.E);
    }
    private void SetDefaultControlsP2()
    {
        controlsList[1].Add(ControlsStrings.Up, KeyCode.UpArrow);
        controlsList[1].Add(ControlsStrings.Down, KeyCode.DownArrow);
        controlsList[1].Add(ControlsStrings.Left, KeyCode.LeftArrow);
        controlsList[1].Add(ControlsStrings.Right, KeyCode.RightArrow);
        controlsList[1].Add(ControlsStrings.Brake, KeyCode.RightControl);
        controlsList[1].Add(ControlsStrings.Reset, KeyCode.RightShift);
        controlsList[1].Add(ControlsStrings.Confirm, KeyCode.Keypad0);
    }
    private void SetDefaultControlsP3()
    {
        controlsList[2].Add(ControlsStrings.Up, KeyCode.None);
        controlsList[2].Add(ControlsStrings.Down, KeyCode.None);
        controlsList[2].Add(ControlsStrings.Left, KeyCode.None);
        controlsList[2].Add(ControlsStrings.Right, KeyCode.None);
        controlsList[2].Add(ControlsStrings.Brake, KeyCode.None);
        controlsList[2].Add(ControlsStrings.Reset, KeyCode.None);
        controlsList[2].Add(ControlsStrings.Confirm, KeyCode.None);
    }
    private void SetDefaultControlsP4()
    {
        controlsList[3].Add(ControlsStrings.Up, KeyCode.None);
        controlsList[3].Add(ControlsStrings.Down, KeyCode.None);
        controlsList[3].Add(ControlsStrings.Left, KeyCode.None);
        controlsList[3].Add(ControlsStrings.Right, KeyCode.None);
        controlsList[3].Add(ControlsStrings.Brake, KeyCode.None);
        controlsList[3].Add(ControlsStrings.Reset, KeyCode.None);
        controlsList[3].Add(ControlsStrings.Confirm, KeyCode.None);
    }
    public void SaveControls()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerPrefs.SetInt(ControlsStrings.Up + i, (int)controlsList[i][ControlsStrings.Up]);
            PlayerPrefs.SetInt(ControlsStrings.Down + i, (int)controlsList[i][ControlsStrings.Down]);
            PlayerPrefs.SetInt(ControlsStrings.Left + i, (int)controlsList[i][ControlsStrings.Left]);
            PlayerPrefs.SetInt(ControlsStrings.Right + i, (int)controlsList[i][ControlsStrings.Right]);
            PlayerPrefs.SetInt(ControlsStrings.Brake + i, (int)controlsList[i][ControlsStrings.Brake]);
            PlayerPrefs.SetInt(ControlsStrings.Reset + i, (int)controlsList[i][ControlsStrings.Reset]);
            PlayerPrefs.SetInt(ControlsStrings.Confirm + i, (int)controlsList[i][ControlsStrings.Confirm]);
            PlayerPrefs.SetInt("ControlsSet", 1);
        }
        PlayerPrefs.Save();
    }
    private void PullControls()
    {
        for (int i = 0; i < 4; i++)
        {
            controlsList[i].Add(ControlsStrings.Up, (KeyCode)PlayerPrefs.GetInt(ControlsStrings.Up + i, 0));
            controlsList[i].Add(ControlsStrings.Down, (KeyCode)PlayerPrefs.GetInt(ControlsStrings.Down + i, 0));
            controlsList[i].Add(ControlsStrings.Left, (KeyCode)PlayerPrefs.GetInt(ControlsStrings.Left + i, 0));
            controlsList[i].Add(ControlsStrings.Right, (KeyCode)PlayerPrefs.GetInt(ControlsStrings.Right + i, 0));
            controlsList[i].Add(ControlsStrings.Brake, (KeyCode)PlayerPrefs.GetInt(ControlsStrings.Brake + i, 0));
            controlsList[i].Add(ControlsStrings.Reset, (KeyCode)PlayerPrefs.GetInt(ControlsStrings.Reset + i, 0));
            controlsList[i].Add(ControlsStrings.Confirm, (KeyCode)PlayerPrefs.GetInt(ControlsStrings.Confirm + i, 0));
        }
    }
   
}
