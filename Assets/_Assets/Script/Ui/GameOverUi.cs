using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUi : MonoBehaviour {


    [SerializeField] private TextMeshProUGUI catFound;

    string catsNumeber = "0";

    private void Start() {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Player.Instance.OnHasFoundCat += Player_OnHasFoundCat;
        Hide();
    }

    private void Player_OnHasFoundCat(object sender, System.EventArgs e) {
          catsNumeber = "1";
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGameOver()) {
            catFound.text = catsNumeber;
            Show();
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }


}