using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeliveryUI : MonoBehaviour
{

    private const string POPUP = "Popup";

    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failColor;
    [SerializeField] private Sprite successIcon;
    [SerializeField] private Sprite failIcon;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliveredSuccess += DeliveryManager_OnDeliveredSuccess;
        DeliveryManager.Instance.OnDeliveredFail += DeliveryManager_OnDeliveredFail;
        Hide();
    }

    private void DeliveryManager_OnDeliveredFail(object sender, System.EventArgs e)
    {
        Show();
        animator.SetTrigger(POPUP);
        backgroundImage.color = failColor;
        iconImage.sprite = failIcon;
        messageText.text = "DELIVERD\nFAIL";
    }

    private void DeliveryManager_OnDeliveredSuccess(object sender, System.EventArgs e)
    {
        Show();
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successIcon;
        messageText.text = "DELIVERD\nSUCCESS";
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
