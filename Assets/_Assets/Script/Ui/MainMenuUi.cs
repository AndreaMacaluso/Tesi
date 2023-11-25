using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuUi : MonoBehaviour {


    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TMP_InputField inputField;// inputField;
    [SerializeField] private Button confirmButton;    
   // [SerializeField] private Player player;


    private void Awake() {
        
        /*
        send.onClick.AddListener(() =>{
            userInput = inputField.text;
            player.SetOpenAiKey(userInput);
        }); */
   
        confirmButton.onClick.AddListener(() => {
            GameSettings.Instance.SetOpenAiKey(inputField.text);
        });

        playButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

}