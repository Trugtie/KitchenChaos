using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        Gameplaying,
        GameOver,
    }

    private State state;
    private float countDownTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 90f;
    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
        this.state = State.WaitingToStart;
    }
    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnPauseAction += Instance_OnPauseAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (this.state == State.WaitingToStart)
        {
            this.state = State.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Instance_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdownToStart:
                countDownTimer -= Time.deltaTime;
                if (countDownTimer < 0)
                {
                    this.state = State.Gameplaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.Gameplaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0)
                {
                    this.state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;

        }
        Debug.Log(state);
    }

    public bool IsGamePlaying()
    {
        return this.state == State.Gameplaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public bool IsGameOver()
    {
        return this.state == State.GameOver;
    }

    public bool IsWaitingToStart()
    {
        return this.state == State.WaitingToStart;
    }

    public float GetCountdownTimer()
    {
        return this.countDownTimer;
    }

    public float GetGamePlayingTimerNormalize()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void TogglePauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public void AddMorePlayTime(float time)
    {
        this.gamePlayingTimer += time;
    }
}
