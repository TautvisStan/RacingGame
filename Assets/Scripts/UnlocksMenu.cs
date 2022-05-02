using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UnlocksMenu : MonoBehaviour
{
    private AbstractUnlockTab UnlockTab = null;
    [SerializeField] private TextMeshProUGUI CostText;
    [SerializeField] private GameObject ErrorText;
    private int Price;
    [SerializeField] private GameObject UnlockButton;
    [SerializeField] private GameObject Buttons;
    [SerializeField] private CareerMenu careerMenu;

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
        if (careerMenu.Points >= Price)
        {
            ErrorText.SetActive(false);
            careerMenu.Purchased(Price);
            UnlockTab.UnlockItem();
            UnlockButton.SetActive(false);
        }
        else
        {
            ErrorText.SetActive(true);
        }
    }
}
