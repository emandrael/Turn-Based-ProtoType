using System.Collections;
using UnityEngine;

internal class Won : State
{
    public Won(BattleSystem battleSystem) : base(battleSystem)
    {
    }
    public override IEnumerator Start()
    {

        LeanTween.move(BattleSystem.enemyGO, new Vector2(-3000, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        BattleSystem.Destroy(BattleSystem.enemyGO);
        GameEvents.current.StopText();
        yield return new WaitForSeconds(0.15f);
        GameEvents.current.FinishingUp();

        BattleSystem.AddDialogue("Darn, it ran.", "Well it doesn't matter. Good work Puppet, you don't seem entirely useless~");
    }
}