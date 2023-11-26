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
        GameEnding,
        GameOver,
    }

    private State state;
    private float gamePlayingTime = 2f;


    private bool isGamePaused = false;
   
    private void Awake() {
      Instance = this;

      state = State.WaitingToStart;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        Player.Instance.OnHasFoundCat += Player_OnHasFoundCat;
    }

    private void GameInput_OnPauseAction (object sender, EventArgs e){

       TogglePauseGame();
    }


    private void Player_OnHasFoundCat(object sender, System.EventArgs e) {

        if((state != State.GameEnding)&&(gamePlayingTime > 10f)){
            gamePlayingTime = 10f;
            state = State.GameEnding;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    
    private void Update() {
        Debug.Log(gamePlayingTime);
        Debug.Log(state);
        switch (state) {
            case State.WaitingToStart:
                gamePlayingTime -= Time.deltaTime;
                if (gamePlayingTime < 0f) {
                        gamePlayingTime = 300f;
                        state = State.GamePlaying;
                        OnStateChanged?.Invoke(this, EventArgs.Empty);
                    }
                break;
            case State.GamePlaying:
                gamePlayingTime -= Time.deltaTime;
                if (gamePlayingTime < 0f) {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameEnding:
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
        return ((state == State.GamePlaying)||(state == State.GameEnding));
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
