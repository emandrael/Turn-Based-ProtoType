using System.Collections;
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
            yield return new WaitForSeconds(1f);

            // Checks for the damage done to the enemy, returns a bool if the damage is sufficent for killing the enemy. 
            isDead = BattleSystem.enemyUnit.takeDamageFromMoveByUnit(BattleSystem.currentAttackMove, BattleSystem.playerUnit, BattleSystem.enemyUnit);
            BattleSystem.enemyHUD.SetHP(BattleSystem.enemyUnit);

            if (!isDead)
            {
                BattleSystem.AddDialogue(
                    "Attack connected, damn!",
                    "Nice going Puppet!");
            }

            // Player moves back to position.
            LeanTween.move(BattleSystem.playerGO, BattleSystem.playerBattleStation.transform.position, 1f).setEaseInBack();
            yield return new WaitForSeconds(1f);
        }
        
        // This is for non physical attacks.
        else if (BattleSystem.currentAttackMove.isMagical == true && BattleSystem.currentAttackMove.isPhysical == false)
        {
            isDead = BattleSystem.enemyUnit.takeDamageFromMoveByUnit(BattleSystem.currentAttackMove, BattleSystem.playerUnit, BattleSystem.enemyUnit);
            BattleSystem.enemyHUD.SetHP(BattleSystem.enemyUnit);
            yield return new WaitForSeconds(BattleSystem.currentAttackMove.EffectLength());
            if (!isDead)
            {
                BattleSystem.AddDialogue(
                    "Nice aim!");
            }
        }

        else
        {
            isDead = BattleSystem.enemyUnit.takeDamageFromMoveByUnit(BattleSystem.currentAttackMove, BattleSystem.playerUnit, BattleSystem.enemyUnit);
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
            BattleSystem.SetState(new EnemyTurn(BattleSystem));
        }
    }
}