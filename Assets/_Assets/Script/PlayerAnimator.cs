using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimator : MonoBehaviour
{

    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player;
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();

     // animator.setBool(IS_WALKING,)
    } 

    private void Update(){
       // animator = GetComponent<Animator>();
      animator.SetBool(IS_WALKING,player.IsWalking());
    } 
}
