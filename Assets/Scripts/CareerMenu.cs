using TMPro;
using UnityEngine;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Points += 50;
            PlayerPrefs.SetInt("Points", Points);
            PointsText.text = Points.ToString();
        }
    }
}
