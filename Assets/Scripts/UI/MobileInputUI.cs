using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Show();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Hide();
        }
        else if (GameManager.Instance.IsWaitingToStart())
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
