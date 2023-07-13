using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health = 100;
    public Text healthText;
    public tbunit playerUnit;

    public void SetHUD(tbunit unit)
    {
        healthText.text = "Player HP : " + health;
    }

    public void SetHP(int hp)
    {
        playerUnit.currentHP = health;
    }

    private void Update()
    {
        healthText.text = "Player HP : " + health;
    }
}
