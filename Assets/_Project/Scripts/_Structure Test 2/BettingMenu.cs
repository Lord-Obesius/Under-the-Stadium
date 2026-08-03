using UnityEngine;

public class BettingMenu : UIWindow
{
    [SerializeField] private PlaceBetMenu placeBetMenu;

    public void PlaceBet()
    {
        UIManager.Instance.Open(placeBetMenu);
    }
}