using System.Collections;
using UnityEngine;
using UnityEngine.UI;

internal class AttackChoice : State
{
    public AttackChoice(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        // Removes panel from scene.
        BattleSystem.battlePanelAttackButton.interactable = false;
        BattleSystem.battlePanelHealButton.interactable = false;

        LeanTween.moveLocal(BattleSystem.battlePanel, BattleSystem.battlePanelStartPos, 0.4f);

        LeanTween.moveLocal(BattleSystem.attackChoicePanel, new Vector2(0, 400), 0.4f).setEaseOutBack();
        yield return new WaitForSeconds(0.4f);

        BattleSystem.attackChoicePanelBackButton.interactable = true;
        
        foreach(GameObject button in BattleSystem.attackChoiceButtons)
        {
            button.GetComponentInChildren<Button>().interactable = true;
        }

    }

    public override IEnumerator UseMove1()
    {

        Debug.Log(BattleSystem.attackChoiceButtons[0].GetComponentInChildren<Text>().text);
        BattleSystem.currentAttackMove = BattleSystem.FindMove(BattleSystem.attackChoiceButtons[0].GetComponentInChildren<Text>().text);
        if ((BattleSystem.playerUnit.CurrentActionPoints - BattleSystem.currentAttackMove.moveAPCost) < 0)
        {
            BattleSystem.AddDialogue("Not enough Ap! Wow you suck, how did you manage that haha!");
            yield break;
        }
        BattleSystem.SetAttackState(new PlayerTurn(BattleSystem));
        yield break;
    }

    public override IEnumerator UseMove2()
    {
        BattleSystem.currentAttackMove = BattleSystem.FindMove(BattleSystem.attackChoiceButtons[1].GetComponentInChildren<Text>().text);
        if ((BattleSystem.playerUnit.CurrentActionPoints - BattleSystem.currentAttackMove.moveAPCost) < 0)
        {
            BattleSystem.AddDialogue("Not enough Ap! Wow you suck, how did you manage that haha!");
            yield break;
        }
        BattleSystem.SetAttackState(new PlayerTurn(BattleSystem));
        yield break;

    }

    public override IEnumerator UseMove3()
    {
        BattleSystem.currentAttackMove = BattleSystem.FindMove(BattleSystem.attackChoiceButtons[2].GetComponentInChildren<Text>().text);
        BattleSystem.SetAttackState(new PlayerTurn(BattleSystem));
        if ((BattleSystem.playerUnit.CurrentActionPoints - BattleSystem.currentAttackMove.moveAPCost) < 0)
        {
            BattleSystem.AddDialogue("Not enough Ap! Wow you suck, how did you manage that haha!");
            yield break;
        }
        yield break;

    }

    public override IEnumerator UseMove4()
    {
        BattleSystem.currentAttackMove = BattleSystem.FindMove(BattleSystem.attackChoiceButtons[3].GetComponentInChildren<Text>().text);
        if ((BattleSystem.playerUnit.CurrentActionPoints - BattleSystem.currentAttackMove.moveAPCost) < 0)
        {
            BattleSystem.AddDialogue("Not enough Ap! Wow you suck, how did you manage that haha!");
            yield break;
        }
        BattleSystem.SetAttackState(new PlayerTurn(BattleSystem));
        yield break;

    }
}