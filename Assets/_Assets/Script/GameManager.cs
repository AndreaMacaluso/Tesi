using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleton
    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State {
        WaitingToStart,
       // CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State state;
    private float gamePlayingTime = 300f;


    private bool isGamePaused = false;
   
    private void Awake() {
      Instance = this;

      state = State.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction (object sender, EventArgs e){

       TogglePauseGame();
    }

    
    private void Update() {
        switch (state) {
            case State.WaitingToStart:
            state = State.GamePlaying;//da eliminare
                break;
            case State.GamePlaying:
                 gamePlayingTime -= Time.deltaTime;
                if (gamePlayingTime < 0f) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    
    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }
    
    public float GetGamePlayingTime() {
        return gamePlayingTime;
    }

        public bool IsGameOver() {
        return state == State.GameOver;
    }

    private void TogglePauseGame(){
        isGamePaused = !isGamePaused;
        if(isGamePaused){
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }else{
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        } 
    }
}
