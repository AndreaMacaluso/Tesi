using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

//namespace Assets._Assets.Script;

public class NpcLookAt : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private Transform HeadLookAtTransform;

    private bool isLookingAtPosition;

    private void update(){
        float targetWeight = isLookingAtPosition ? 1f : 0f;
        float lerpSpeed = 2f;
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * lerpSpeed);
    }

    public void LookAtPosition(Vector3 lookAtPosition){
        isLookingAtPosition = true;
        HeadLookAtTransform.position = lookAtPosition;
    }

}
