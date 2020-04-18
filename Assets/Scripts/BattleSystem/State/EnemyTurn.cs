using System.Collections;
using System;
using UnityEngine;


internal class EnemyTurn : State
{
    bool isDead;
    public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        System.Random rnd = new System.Random();
        int randomMoveNumber = rnd.Next(0, BattleSystem.enemyUnit.UnitMoves.Length);
        Move randomlySelectedMove = BattleSystem.enemyUnit.UnitMoves[randomMoveNumber];
        
        if (randomlySelectedMove.isPhysical)
        {
            LeanTween.move(BattleSystem.enemyGO, new Vector2(BattleSystem.playerBattleStation.transform.position.x - 1, BattleSystem.enemyBattleStation.transform.position.y), 1f).setEaseInBack();
            yield return new WaitForSeconds(1f);
            isDead = BattleSystem.playerUnit.takeDamageFromMoveByUnit(randomlySelectedMove, BattleSystem.enemyUnit, BattleSystem.playerUnit);
            yield return new WaitForSeconds(1f);
            BattleSystem.playerHUD.SetHP(BattleSystem.playerUnit);
            if(!isDead)
                BattleSystem.AddDialogue("Ouch, that looked like it stung.");
        }
        if(randomlySelectedMove.isMagical)
        {
            isDead = BattleSystem.playerUnit.takeDamageFromMoveByUnit(randomlySelectedMove, BattleSystem.enemyUnit, BattleSystem.playerUnit);
            BattleSystem.playerHUD.SetHP(BattleSystem.playerUnit);
            yield return new WaitForSeconds(BattleSystem.currentAttackMove.EffectLength());
            if(!isDead)
                BattleSystem.AddDialogue("Eesh, that thing can aim haha.");
        }
        

        // Player moves back to position.
        LeanTween.move(BattleSystem.enemyGO, BattleSystem.enemyBattleStation.transform.position, 1f).setEaseInBack();
        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            BattleSystem.SetState(new Lost(BattleSystem));
        }
        else
        {
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
    }
}