using System.Collections;
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
        int randomMoveNumber = Random.Range(0, BattleSystem.enemyUnit.UnitMoves.Length);
        Move randomlySelectedMove = BattleSystem.enemyUnit.UnitMoves[randomMoveNumber];
        
        if (randomlySelectedMove.isPhysical)
        {
            LeanTween.move(BattleSystem.enemyGO, new Vector2(BattleSystem.playerBattleStation.transform.position.x - 1, BattleSystem.enemyBattleStation.transform.position.y), 1f).setEaseInBack();
            yield return new WaitForSeconds(1f);
            isDead = BattleSystem.playerUnit.takeDamageFromMoveByUnit(randomlySelectedMove, BattleSystem.enemyUnit, BattleSystem.playerUnit);
            BattleSystem.playerHUD.SetHP(BattleSystem.playerUnit);
            if(!isDead)
                BattleSystem.AddDialogue($"Ouch, that looked like a {randomlySelectedMove.moveName}, you okay?");
        }
        if(randomlySelectedMove.isMagical)
        {
            isDead = BattleSystem.playerUnit.takeDamageFromMoveByUnit(randomlySelectedMove, BattleSystem.enemyUnit, BattleSystem.playerUnit);
            yield return new WaitForSeconds(BattleSystem.currentAttackMove.EffectLength());
            BattleSystem.playerHUD.SetHP(BattleSystem.playerUnit);
            if (!isDead)
                BattleSystem.AddDialogue($"That was a {randomlySelectedMove.moveName}. Eesh, that thing can aim haha.");
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