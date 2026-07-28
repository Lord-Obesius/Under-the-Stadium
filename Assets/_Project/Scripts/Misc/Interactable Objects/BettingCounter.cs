using UnityEngine;

public class BettingCounter : MonoBehaviour, IInteractable
{

    public bool MenuIsOpen = false;

    public void Interact()
    {
        if (MenuIsOpen)
            return;

        MenuIsOpen = true;
        Debug.Log("Opened");
    }
}
