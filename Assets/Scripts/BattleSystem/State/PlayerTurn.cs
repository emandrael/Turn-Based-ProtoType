﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

internal class PlayerTurn : State
{
    private bool isDead;
    public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        // Disables the move panels buttons.
        BattleSystem.attackChoicePanelBackButton.interactable = false;
        foreach (GameObject button in BattleSystem.attackChoiceButtons)
        {
            button.GetComponentInChildren<Button>().interactable = false;
        }

        BattleSystem.currentPlayerMove = BattleSystem.playerUnit.UnitMoves;

        LeanTween.moveLocal(BattleSystem.attackChoicePanel,BattleSystem.attackChoicePanelStartPos, 0.5f).setEaseOutBack();
        LeanTween.moveLocal(BattleSystem.battlePanel, new Vector2(0, 400), 0.5f).setEaseOutBack();
        yield return new WaitForSeconds(0.5f);
        //Re-enables the battle panel buttons.
        BattleSystem.battlePanelAttackButton.interactable = true;
        BattleSystem.battlePanelHealButton.interactable = true;
    }

    public override IEnumerator Attack()
    {
        // Stop all current text and refreshes it into a empty string.
        GameEvents.current.StopText();
        Animator playerAnim = BattleSystem.playerGO.GetComponent<Animator>();
        
        // Removes panel from scene.
        BattleSystem.battlePanelAttackButton.interactable = false;
        BattleSystem.battlePanelHealButton.interactable = false;

        // Animates choice panel out of the screen.
        LeanTween.moveLocal(BattleSystem.attackChoicePanel, BattleSystem.attackChoicePanelStartPos, 0.2f);
        yield return new WaitForSeconds(0.2f);

        // When the attack used is physical.
        if (BattleSystem.currentAttackMove.isPhysical == true && BattleSystem.currentAttackMove.isMagical == false)
        {
            // Player moves to enemy.
            LeanTween.move(BattleSystem.playerGO, new Vector2(BattleSystem.enemyBattleStation.transform.position.x + 1, BattleSystem.enemyBattleStation.transform.position.y), 1f).setEaseInBack();
            // IsWalkingLeft starts the animation for walking left
            playerAnim.SetBool("IsWalkingLeft", true);
            yield return new WaitForSeconds(1f);
            playerAnim.SetBool("IsWalkingLeft", false);
            // IsPhysicalAttacking starts the animation for physical attacks. 
            playerAnim.SetBool("IsPhysicalAttacking", true);
            // This initializes the previous health.
            BattleSystem.enemyUnit.PreviousHealth = BattleSystem.enemyUnit.CurrentHealth;
            BattleSystem.playerUnit.PreviousActionPoints = BattleSystem.playerUnit.CurrentActionPoints;
            // Checks for the damage done to the enemy, returns a bool if the damage is sufficent for killing the enemy. 
            isDead = BattleSystem.enemyUnit.takeDamageFromMoveByUnit(BattleSystem.currentAttackMove, BattleSystem.playerUnit);
            yield return new WaitForSeconds(BattleSystem.currentAttackMove.EffectLength());
            BattleSystem.enemyHUD.SetHP(BattleSystem.enemyUnit);
            BattleSystem.playerHUD.SetAP(BattleSystem.playerUnit);
            playerAnim.SetBool("IsPhysicalAttacking", false);
            if (!isDead)
            {
                BattleSystem.AddDialogue(
                    "Attack connected, damn!",
                    "Nice going Puppet!");
            }

            // Player moves back to position.
            // IsWalkingLeft starts the animation for walking right.
            playerAnim.SetBool("IsWalkingRight", true);
            LeanTween.move(BattleSystem.playerGO, BattleSystem.playerBattleStation.transform.position, 1f).setEaseInBack();
            yield return new WaitForSeconds(0.8f);
            playerAnim.SetBool("IsWalkingRight", false);
        }
        // This is for non physical attacks.
        else if (BattleSystem.currentAttackMove.isMagical == true && BattleSystem.currentAttackMove.isPhysical == false)
        {
            playerAnim.SetBool("IsMagicalAttacking", true);
            BattleSystem.enemyUnit.PreviousHealth = BattleSystem.enemyUnit.CurrentHealth;
            BattleSystem.playerUnit.PreviousActionPoints = BattleSystem.playerUnit.CurrentActionPoints;
            isDead = BattleSystem.enemyUnit.takeDamageFromMoveByUnit(BattleSystem.currentAttackMove, BattleSystem.playerUnit);
            yield return new WaitForSeconds(BattleSystem.currentAttackMove.EffectLength());
            BattleSystem.enemyHUD.SetHP(BattleSystem.enemyUnit);
            BattleSystem.playerHUD.SetAP(BattleSystem.playerUnit);
            
            playerAnim.SetBool("IsMagicalAttacking", false);
            if (!isDead)
            {
                BattleSystem.AddDialogue(
                    "Nice aim!");
            }
        }

        else
        {
            BattleSystem.playerUnit.PreviousHealth = BattleSystem.playerUnit.CurrentHealth;
            isDead = BattleSystem.enemyUnit.takeDamageFromMoveByUnit(BattleSystem.currentAttackMove, BattleSystem.playerUnit);
            BattleSystem.enemyHUD.SetHP(BattleSystem.enemyUnit);
            yield return new WaitForSeconds(BattleSystem.currentAttackMove.EffectLength());
            if (!isDead)
            {
                BattleSystem.AddDialogue(
                    "ADAIUdIUGWGDAIYDASDAS!");
            }
        }

        if (isDead)
        {
            //Player Wins
            BattleSystem.SetState(new Won(BattleSystem));
        }
        else
        {
            //Goes to enemy turn. 
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }
    }

    public override IEnumerator Heal()
    {
        Animator playerAnim = BattleSystem.playerGO.GetComponent<Animator>();
        playerAnim.SetBool("IsMagicalAttacking", true);

        if(BattleSystem.playerUnit.CurrentHealth == BattleSystem.playerUnit.MaxHealth)
        {
            BattleSystem.playerUnit.healHealth(BattleSystem.playerUnit.HealMove);
            yield return new WaitForSeconds(BattleSystem.playerUnit.HealMove.EffectLength());
            BattleSystem.AddDialogue("You've got full health already dummy!");
        }

        else
        {
            BattleSystem.playerUnit.PreviousHealth = BattleSystem.playerUnit.CurrentHealth;
            BattleSystem.playerUnit.healHealth(BattleSystem.playerUnit.HealMove);
            yield return new WaitForSeconds(BattleSystem.playerUnit.HealMove.EffectLength());
            BattleSystem.playerHUD.SetHealedHP(BattleSystem.playerUnit);


            BattleSystem.AddDialogue("Looking out for yourself is very manly~");

        }

        

        playerAnim.SetBool("IsMagicalAttacking", false);
        

        BattleSystem.SetState(new EnemyTurn(BattleSystem));


    }
}