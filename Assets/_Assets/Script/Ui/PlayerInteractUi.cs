using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteractUi : MonoBehaviour
{
   [SerializeField] private GameObject containerGameObject;
   [SerializeField] private Player player;
   [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;

   private void Update(){
        if(player.GetInteractableObject() != null){
            Show(player.GetInteractableObject());
          if(player.IsTalking()){
               Hide();
           }
        }else{
            Hide();
        }
   }

   private void Show(NpcInteractable npcInteractable){

        containerGameObject.SetActive(true);
        string name = npcInteractable.GetNpcName();
        string interactText = "Talk to " + name;
        interactTextMeshProUGUI.text = interactText;/* */
   }

    private void Hide(){
         containerGameObject.SetActive(false);
   }
}
