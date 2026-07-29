using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private UIWindow currentWindow;

    void Awake()
    {
        Instance = this;
    }

    public bool MenuOpen => currentWindow != null;

    public void Open(UIWindow window)
    {
        if (currentWindow != null)
            currentWindow.Close();

        currentWindow = window;
        currentWindow.Open();
    }

    public void CloseCurrent()
    {
        if (currentWindow == null)
            return;

        currentWindow.Close();
        currentWindow = null;
    }
}