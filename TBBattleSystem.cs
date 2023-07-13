using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class TBBattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public tbunit playerUnit;
    public tbunit enemyUnit;

    public EnemyHealthDisplay eHealth;
    public HealthDisplay pHealth;

    public Text dialogueText;

    public BattleState state;
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());

    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<tbunit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<tbunit>();

        dialogueText.text = "An enemy appears!";

        pHealth.SetHUD(playerUnit);
        eHealth.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        if (enemyUnit.TakeDamage(playerUnit.damage))
        {
            eHealth.health = playerUnit.damage;
        }

        eHealth.SetHP(enemyUnit.currentHP);
        dialogueText.text = "Got 'em!";
        //damage enemy
        yield return new WaitForSeconds(1f);

        //check if the enemy is dead
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
            //end battle
        }

        //change state based on outcome
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            //enemy turn
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "The enemy attacked!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "Enemy defeated!";
        }

        else if(state == BattleState.LOST)
        {
            dialogueText.text = "You lose!";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "What will you do?";
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        pHealth.SetHP(playerUnit.currentHP);
        dialogueText.text = "You gained 5 HP!";

        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

}