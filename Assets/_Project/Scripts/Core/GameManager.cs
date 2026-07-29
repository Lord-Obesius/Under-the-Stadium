using UnityEngine;

public enum GameState
{
    MainMenu,
    Betting,
    Battle,
    Results
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState CurrentState;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        if (CurrentState == GameState.Betting)
        {
            PlayerMovementController playerCont = GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>();
            playerCont.canLookAround = false;
            playerCont.canMove = false;
        }
        else if (CurrentState == GameState.MainMenu)
        {
            PlayerMovementController playerCont = GameObject.FindWithTag("Player").GetComponent<PlayerMovementController>();
            playerCont.canLookAround = true;
            playerCont.canMove = true;
        }
    }
}