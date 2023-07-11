using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject visualSelectedObject;
    private void Start()
    {
        Player.Instance.OnSeletecedCounterChanged += Player_OnSeletecedCounterChanged;
    }

    private void Player_OnSeletecedCounterChanged(object sender, Player.OnSeletecedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        visualSelectedObject.SetActive(true);
    }

    private void Hide()
    {
        visualSelectedObject.SetActive(false);
    }
}
