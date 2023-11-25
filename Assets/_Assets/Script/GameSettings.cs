using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

   public static GameSettings Instance { get; private set; }

   private string openAiKey;

   private void Awake(){
        if(Instance != null){
            Destroy(gameObject);
        }else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
   }

   public void SetOpenAiKey(string userInput){
        openAiKey = userInput;
   }
   
   public string GetOpenAiKey(){
        return openAiKey; 
   }

}
