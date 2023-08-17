using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class JoystickUI : MonoBehaviour
{
    [SerializeField] private GameObject joyStickUI;
    private bool lockJoystick = false;

    private void Start()
    {
        TouchManager.Instance.OnTouchBegan += TouchManager_OnTouchBegan;
        TouchManager.Instance.OnTouchEnded += TouchManager_OnTouchEnded;
        Hide();
    }

    private void TouchManager_OnTouchEnded(object sender, EventArgs e)
    {
        Hide();
    }

    private void TouchManager_OnTouchBegan(object sender, TouchManager.OnTouchPositionChangedEventArgs e)
    {
        bool isConflic = CheckConFlic();
        if (!isConflic && !lockJoystick)
        {
            Show();
            UpdateTransform(e.touchPosition);
        }
    }

    private void UpdateTransform(Vector2 targetTransform)
    {
        transform.position = targetTransform;
    }

    private bool CheckConFlic()
    {
        return TouchManager.Instance.IsHasConflictWithOtherButton() || TouchManager.Instance.IsStayInZeroPosition();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        SetLockJoystick(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        SetLockJoystick(false);
    }

    public void SetLockJoystick(bool isLock)
    {
        this.lockJoystick = isLock;
    }
}
