using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        Gameplaying,
        GameOver,
    }

    private State state;
    private float waitingToStartTimer = 1f;
    private float countDownTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 10f;

    private void Awake()
    {
        Instance = this;
        this.state = State.WaitingToStart;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0)
                {
                    this.state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
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

    public float GetCountdownTimer()
    {
        return this.countDownTimer;
    }

    public float GetGamePlayingTimerNormalize()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
}
