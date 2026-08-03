using UnityEngine;

public class BettingCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private BettingMenu bettingMenu;

    public void Interact()
    {
        UIManager.Instance.Open(bettingMenu);
    }
}