using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthDisplay : MonoBehaviour
{
    public int health = 100;
    public Text healthText;
    public tbunit enemyUnit;

    public void SetHUD(tbunit unit)
    {
        healthText.text = "Enemy HP : " + enemyUnit.currentHP;
    }

    public void SetHP(int hp)
    {
        health = enemyUnit.currentHP;
    }

    private void Update()
    {
        healthText.text = "Enemy HP : " + health;
    }

    
}
