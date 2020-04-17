using System.Collections;
using UnityEngine;

internal class Lost : State
{
    public Lost(BattleSystem battleSystem) : base(battleSystem)
    {
    }

   
    public override IEnumerator Start()
    {

        LeanTween.scale(BattleSystem.playerGO, new Vector2(0, 0), 0.5f);
        yield return new WaitForSeconds(0.5f);
        GameEvents.current.FinishingUp();
        BattleSystem.Destroy(BattleSystem.playerGO);
        BattleSystem.AddDialogue("Ugh, looks like you weren't enough.");
    }

}