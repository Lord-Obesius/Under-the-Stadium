using UnityEngine;

public class BettingUIHandler : MonoBehaviour
{

    public BettingCounter counter;

    public void OpenMenu()
    {

    }

    public void CloseMenu() 
    {
        counter.MenuIsOpen = false;
        Debug.Log("Closed");
        counter.BettingUI.SetActive(false);

        GameManager.Instance.ChangeState(GameState.MainMenu);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
