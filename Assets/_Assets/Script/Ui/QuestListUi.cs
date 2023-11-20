using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestListUi : MonoBehaviour
{
   
     [SerializeField] private TextMeshProUGUI catFound;

    private void Start() {
        Player.Instance.OnHasFoundCat += Player_OnHasFoundCat;

    }

    private void Player_OnHasFoundCat(object sender, System.EventArgs e) {
       catFound.text = "1";
    }

}
