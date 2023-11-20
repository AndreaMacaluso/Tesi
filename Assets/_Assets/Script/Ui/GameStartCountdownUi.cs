using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUi : MonoBehaviour {



    private const string NUMBER_POPUP = "NumberPopup";


    [SerializeField] private TextMeshProUGUI countdownText;
    


    //private Animator animator;
    private int previousCountdownNumber;

    
    private void Awake() {
        //animator = GetComponent<Animator>();
    }

     private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGamePlaying()) {
            Show();
        } else {
            Hide();
        }
    }
 /**/
    private void Update() {
        //@toDo rivedere per renderla abilitata, al momento non funziona
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetGamePlayingTime());
        countdownText.text = countdownNumber.ToString();

        if (previousCountdownNumber != countdownNumber) {
            previousCountdownNumber = countdownNumber;
           // animator.SetTrigger(NUMBER_POPUP); //@toDo da settare
          //  SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    

}