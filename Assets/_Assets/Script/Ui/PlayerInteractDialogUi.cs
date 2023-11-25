using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OpenAI;


namespace OpenAI
{
    public class PlayerInteractDialogUi : MonoBehaviour
    {
        [SerializeField] private TMP_InputField inputField;// inputField;
        [SerializeField] private Button send;
        [SerializeField] private Button exit; // da gestire uscita da scermata di dialogo
        [SerializeField] private TextMeshProUGUI textArea; // da gestire uscita da scermata di dialogo
        [SerializeField] private Player player;
        [SerializeField] private GameObject containerGameObject;
        private OpenAIApi openai = new OpenAIApi();

        private string userInput;
        private string instruction = "Act as a family member who doesnt know where the cat is, but wants to know about it. the player will ask you some question "; 

        private void Start(){
           
            string text = GameSettings.Instance.GetOpenAiKey();
           
            if((text != null)&&(text != "")){
                openai = new OpenAIApi(text);
            }
           
            send.onClick.AddListener(SendReply);
            exit.onClick.AddListener(CloseChat);
        }

         private void Update(){

            if(player.IsTalking()){
                 setInstructionBasedOnNPC(player);
                 Show();
             }else{
                Hide();
            }
         }


        private async void SendReply(){
            //inputField
            userInput = inputField.text;
            instruction += $"{userInput}\nA: ";//A: is for qAnswer

            textArea.text= "...";
            inputField.text = "";

            send.enabled = false;
            inputField.enabled = false;

            var Request = new CreateCompletionRequest(){
                Prompt = instruction,
                Model = "text-davinci-003",
                MaxTokens = 128
            };

            //textArea.text = "hi, have a good day";
            //@toDo inserire catch errori auth api non valida  
            var Response = await openai.CreateCompletion(Request); 

            textArea.text = Response.Choices[0].Text;
            instruction +=  $"{Response.Choices[0].Text}\nQ: ";
           
            send.enabled = true;
            inputField.enabled = true;
        }

        private void setInstructionBasedOnNPC( Player player){
            var interactableObject = player.GetInteractableObject();

            string name = interactableObject.GetNpcName();
            instruction = "Act as a family member who doesnt know where the cat is, but wants to know about it.";

            //Q: is for question

             switch (name) {
                    case "Mom":
                    instruction += "Act Like you are the Mother of the player and ask him to look in the bathroom for the cat. \nQ:";
                    break;
                    case "Dad":
                    instruction += "Act Like you are the Dad of the player, you are shaving your beard and have no idea where the cat is keep asking what Timmy is doing at the Arcade machine. \nQ:";
                    break;
                    case "Little brother" :
                    instruction += "Act Like you are a little child who wants to play at the Arcade machine but it's mad because the cat broke it \nQ:";    
                    break;
                    default :
                    instruction += "Ask if the cat has been found every 3 questions \nQ:";
                    break;
            }
        }  

        public void Show(){

            containerGameObject.SetActive(true);
        }   

        private void Hide(){
            containerGameObject.SetActive(false);
            inputField.text = "";
            textArea.text= "...";
        }

        private void CloseChat(){
          player.isNotTalking();
          Hide();
        }

    }
}