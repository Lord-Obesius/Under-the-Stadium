using UnityEngine;

public class UIWindow : MonoBehaviour
{
    public virtual void Open()
    {
        gameObject.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0;
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
    }
}
