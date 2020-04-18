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
        // This selects a move from the list of moves that the enemy can get.
        int randomMoveNumber = Random.Range(0, BattleSystem.enemyUnit.UnitMoves.Length);
        Move randomlySelectedMove = BattleSystem.enemyUnit.UnitMoves[randomMoveNumber];
        
        if (randomlySelectedMove.isPhysical)
        {
            LeanTween.move(BattleSystem.enemyGO, new Vector2(BattleSystem.playerBattleStation.transform.position.x - 1, BattleSystem.enemyBattleStation.transform.position.y), 1f).setEaseInBack();
            yield return new WaitForSeconds(1f);
            isDead = BattleSystem.playerUnit.takeDamageFromMoveByUnit(randomlySelectedMove, BattleSystem.enemyUnit);
            BattleSystem.playerHUD.SetHP(BattleSystem.playerUnit);
            if(!isDead)
                BattleSystem.AddDialogue($"Ouch, that looked like a {randomlySelectedMove.moveName}, you okay?");
        }
        if(randomlySelectedMove.isMagical)
        {
            isDead = BattleSystem.playerUnit.takeDamageFromMoveByUnit(randomlySelectedMove, BattleSystem.enemyUnit);
            yield return new WaitForSeconds(randomlySelectedMove.EffectLength());
            BattleSystem.playerHUD.SetHP(BattleSystem.playerUnit);
            if (!isDead)
                BattleSystem.AddDialogue($"That was a {randomlySelectedMove.moveName}. Eesh, that thing can aim haha.");
        }

        // Player moves back to position.
        LeanTween.move(BattleSystem.enemyGO, BattleSystem.enemyBattleStation.transform.position, 1f).setEaseInBack();
        yield return new WaitForSeconds(2f);

        GameEvents.current.StopText();

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