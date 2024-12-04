using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public TMP_Text healthText;

    void Update()
    {
        healthText.text = GameManager.Instance.PlayerController.healthSystem.ShowHUD();
    }
}