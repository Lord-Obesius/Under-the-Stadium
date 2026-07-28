using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int CleanMoney;
    [SerializeField] private int DirtyMoney;
    [SerializeField] private int Exposure;


    [SerializeField] private TextMeshProUGUI CleanMoneyText;
    [SerializeField] private TextMeshProUGUI DirtyMoneyText;
    [SerializeField] private TextMeshProUGUI ExposureText;

    void Start()
    {
        UpdateCleanMoney(0);
        UpdateDirtyMoney(0);
        UpdateExposure(0);
    }

    public void UpdateCleanMoney(int toAdd)
    {
        CleanMoney += toAdd;
        CleanMoneyText.text = $"Money: ${CleanMoney}";
    }
    public void UpdateDirtyMoney(int toAdd)
    {
        DirtyMoney += toAdd;
        DirtyMoneyText.text = $"Dirty Money: ${DirtyMoney}";
    }
    public void UpdateExposure(int toAdd)
    {
        Exposure += toAdd;
        ExposureText.text = $"Exposure: {Exposure}";
    }
}
