using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class TouchManager : MonoBehaviour
{
    public event EventHandler<OnTouchPositionChangedEventArgs> OnTouchBegan;
    public event EventHandler<OnTouchPositionChangedEventArgs> OnTouchMoved;
    public event EventHandler OnTouchEnded;

    public class OnTouchPositionChangedEventArgs : EventArgs
    {
        public Vector2 touchPosition;
    }

    public static TouchManager Instance { get; private set; }

    private Touchscreen touchscreen;
    private TouchControl touch;
    private Vector2 touchPosition;

    [SerializeField] private List<Transform> buttonPostionList;

    private void Awake()
    {
        Instance = this;
        touchscreen = Touchscreen.current;
    }

    private void Update()
    {
        UpdateTouchPosition();
    }

    private void UpdateTouchPosition()
    {
        if (touchscreen != null && touchscreen.touches.Count > 0)
        {
            touch = touchscreen.touches[0];

            Vector2 newTouchPosition = touch.position.ReadValue();

            UnityEngine.InputSystem.TouchPhase touchPhase = touch.phase.ReadValue();

            if (touchPhase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                if (newTouchPosition != touchPosition)
                {

                    touchPosition = newTouchPosition;
                    OnTouchBegan?.Invoke(this, new OnTouchPositionChangedEventArgs
                    {
                        touchPosition = newTouchPosition
                    });
                }
            }
            else if (touchPhase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                touchPosition = newTouchPosition;

                OnTouchMoved?.Invoke(this, new OnTouchPositionChangedEventArgs
                {
                    touchPosition = touchPosition
                });

            }
            else if (touchPhase == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                OnTouchEnded?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public bool IsHasConflictWithOtherButton()
    {
        foreach (Transform buttonPosition in buttonPostionList)
        {
            float boundPlusRange = 100f;

            bool isInBoundX = touchPosition.x <= buttonPosition.position.x + boundPlusRange && touchPosition.x >= buttonPosition.position.x - boundPlusRange;

            bool isInBoundY = touchPosition.y <= buttonPosition.position.y + boundPlusRange && touchPosition.y >= buttonPosition.position.y - boundPlusRange;

            if (isInBoundX && isInBoundY)
                return true;
        }
        return false;
    }

    public bool IsStayInZeroPosition()
    {
        return touchPosition.Equals(Vector2.zero);
    }
}
