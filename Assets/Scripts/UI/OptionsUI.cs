using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI sfxText;
    [SerializeField] private TextMeshProUGUI musicText;

    private void Awake()
    {
        Instance = this;

        this.sfxButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();

            UpdateVisual();
        });

        this.musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        this.closeButton.onClick.AddListener(() =>
        {
            this.Hide();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
        UpdateVisual();
        this.Hide();
    }

    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        this.Hide();
    }

    private void UpdateVisual()
    {
        sfxText.text = $"Sound Effect: {Mathf.Round(10f * SoundManager.Instance.GetVolume()).ToString()}";
        musicText.text = $"Music: {Mathf.Round(10f * MusicManager.Instance.GetVolume()).ToString()}";
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
