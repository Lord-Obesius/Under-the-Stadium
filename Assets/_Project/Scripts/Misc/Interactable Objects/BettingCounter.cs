using UnityEngine;

public class BettingCounter : MonoBehaviour, IInteractable
{

    public bool MenuIsOpen = false;

    public GameObject BettingUI;

    public void Interact()
    {
        if (MenuIsOpen)
            return;

        MenuIsOpen = true;
        Debug.Log("Opened");
        BettingUI.SetActive(true);
        BettingUI.GetComponent<BettingUIHandler>().OpenMenu();
    }
}
