using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using OpenAI.PlayerInteractDialogUi;

//using Assets._Assets.Script.NpcLookAt;

public class NpcInteractable : MonoBehaviour
{
    private Animator animator;
    private NpcLookAt npcLookAt;
    
    [SerializeField]private string npc_name;

    private void Awake(){
        animator = GetComponent<Animator>();
        npcLookAt = GetComponent<NpcLookAt>();
    }

   public void InteractAction(Transform interactorTransform){
        float playerHeight = 1.4f;
        npcLookAt.LookAtPosition(interactorTransform.position + Vector3.up * playerHeight);
   }

   public string GetNpcName(){
         return npc_name;
   }
}
