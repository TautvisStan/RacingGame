using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UnlocksMenu : MonoBehaviour
{
    private AbstractUnlockTab UnlockTab = null;
    [SerializeField] private TextMeshProUGUI CostText;
    [SerializeField] private TextMeshProUGUI PointsText;
    [SerializeField] private GameObject ErrorText;
    private int Price;
    [SerializeField] private GameObject UnlockButton;
    private int Points;
    [SerializeField] private GameObject Buttons;
    private void Start()
    {
        Points = PlayerPrefs.GetInt("Points", 1000);
        PointsText.text = Points.ToString();
    }
    public void SetTab(AbstractUnlockTab tab)
    {
        Buttons.SetActive(true);
        if(UnlockTab != null)
        {
            UnlockTab.gameObject.SetActive(false);
        }
        tab.gameObject.SetActive(true);
        UnlockTab = tab;
        UnlockTab.SetItemToStage();
        ErrorText.SetActive(false);
        SetItem();
    }
    public void LeftClicked()
    {
        UnlockTab.LeftItem();
        SetItem();
    }
    public void RightClicked()
    {
        UnlockTab.RightItem();
        SetItem();
    }
    public void SetItem()
    {
        ErrorText.SetActive(false);
        Price = UnlockTab.Price;
        CostText.text = Price.ToString();
        if (UnlockTab.Unlocked)
        {
            UnlockButton.SetActive(false);
        }
        else
        {
            UnlockButton.SetActive(true);
        }
    }
    public void UnlockItem()
    {
        if (Points >= Price)
        {
            ErrorText.SetActive(false);
            Points -= Price;
            PlayerPrefs.SetInt("Points", Points);
            PointsText.text = Points.ToString();
            UnlockTab.UnlockItem();
            UnlockButton.SetActive(false);
        }
        else
        {
            ErrorText.SetActive(true);
        }
    }
}
