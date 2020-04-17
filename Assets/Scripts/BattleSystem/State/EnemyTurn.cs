using System.Collections;
using UnityEngine;

internal class EnemyTurn : State
{
    public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {

        yield return new WaitForSeconds(1f);
        // Enemy moves to player.
        LeanTween.move(BattleSystem.enemyGO, BattleSystem.playerBattleStation.transform.position, 1f).setEaseInBack();
        yield return new WaitForSeconds(1f);

        bool isDead = BattleSystem.playerUnit.takeDamage(BattleSystem.enemyUnit.Strength);
        BattleSystem.playerHUD.SetHP(BattleSystem.playerUnit);

        BattleSystem.AddDialogue("Ouch, that looked like it stung.");

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