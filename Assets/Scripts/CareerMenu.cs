using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CareerMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PointsText;
    public int Points;

    void Start()
    {
        Points = PlayerPrefs.GetInt("Points", 0);
        PointsText.text = Points.ToString();
    }

    public void Purchased(int price)
    {
        Points -= price;
        PlayerPrefs.SetInt("Points", Points);
        PointsText.text = Points.ToString();
    }
}