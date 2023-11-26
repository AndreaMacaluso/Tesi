using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
   public static Player Instance { get; private set; }
   public event EventHandler OnHasFoundCat;
   
   [SerializeField] private float moveSpeed = 7f;
   [SerializeField] private GameInput gameInput;

   
   
   private bool isWalking;
   private bool isTalking;
   private Vector3 lastInteractDir;
   
    private void Awake() {
        Instance = this;
     }

    private void Update() {
        if (GameManager.Instance.IsGamePlaying()){
            if(!isTalking){
                HandleMovement();
            }
            HandleInteractionsObjects();
            HandleInteractionsNPCS();
        }
        else{
            return;
        }
    }

     private void HandleInteractionsObjects() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }

        float interactDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance)) {
            //da definire cosa fare per interazioni oggetti
        }
        else{
     
        }
    
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
   
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .4f;
        float playerHeight = 1f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // Cannot move towards moveDir

            // Attempt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = (moveDir.x < -.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can move only on the X
                moveDir = moveDirX;
            } else {
                // Cannot move only on the X

                // Attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = (moveDir.z < -.5f || moveDir.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove) {
                    // Can move only on the Z
                    moveDir = moveDirZ;
                } else {
                    // Cannot move in any direction
                }
            }
        }

        if (canMove) {
            transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        
        // rotation
        float rotateSpeed = 1f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
      
    }

    private void HandleInteractionsNPCS(){
        if(Input.GetKeyDown(KeyCode.F)){
            float interactRange = 3f; 
            Collider[] colliderArray = Physics.OverlapSphere(transform.position,interactRange);
            foreach(Collider collider in colliderArray){
                if(collider.TryGetComponent(out NpcInteractable npcInteractable)){
                    npcInteractable.InteractAction(transform);// += Pause_performed;
                    isTalking = true;
                   
                } 
            }
        }
    }

    /* private void Pause_performed (UnityEngine.InputSystem.InputAction.CallbackContext obj){

        OnPauseAction?.Invoke(this, EventArgs.Empty);
    } */
   
    public NpcInteractable GetInteractableObject(){
       
        List<NpcInteractable> npcInteractableList = new List<NpcInteractable>();
        float interactRange = 3f; 
        Collider[] colliderArray = Physics.OverlapSphere(transform.position,interactRange);
        foreach(Collider collider in colliderArray){

            if(collider.TryGetComponent(out NpcInteractable npcInteractable)){
                npcInteractableList.Add(npcInteractable);   
            } 

            if(collider.TryGetComponent(out CatInteractable catInteractable)){
                   OnHasFoundCat?.Invoke(this, EventArgs.Empty);
            }
        }
        NpcInteractable closestNpcInteractable = null;
        foreach(NpcInteractable npcInteractable in npcInteractableList ){
            if(closestNpcInteractable == null){
                closestNpcInteractable = npcInteractable;
            }else{
                if(Vector3.Distance(transform.position, npcInteractable.transform.position) < 
                Vector3.Distance(transform.position, closestNpcInteractable.transform.position)){
                   //closer
                    closestNpcInteractable = npcInteractable;
                }
            }

        }
        return closestNpcInteractable;     
    }
    

    public bool IsWalking(){
            return isWalking;
    }
    public bool IsTalking(){
            return isTalking;
    }
    public void isNotTalking(){
        isTalking = false;
    }   

}
