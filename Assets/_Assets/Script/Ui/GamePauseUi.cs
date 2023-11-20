using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUi : MonoBehaviour
{

   [SerializeField] private Button quitButton;

    private void Start() {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }

   private void Awake() {

        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

   }

   private void Show() {
        gameObject.SetActive(true);

       // resumeButton.Select();
   }

   private void Hide() {
        gameObject.SetActive(false);
   }
}
