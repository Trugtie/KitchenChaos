using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class KnobUI : MonoBehaviour
{
    [SerializeField] private MyOnScreenStick myOnScreenStick;

    private void Start()
    {
        TouchManager.Instance.OnTouchMoved += TouchManager_OnTouchMoved;
    }

    private void TouchManager_OnTouchMoved(object sender, TouchManager.OnTouchPositionChangedEventArgs e)
    {
        myOnScreenStick.SimulateDrag(e.touchPosition);
    }
}
