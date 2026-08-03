using UnityEngine;

public class PlaceBetMenu : UIWindow
{

    [SerializeField] private BettingMenu betMenu;

    public void CancelBet()
    {
        UIManager.Instance.Open(betMenu);
    }
}
