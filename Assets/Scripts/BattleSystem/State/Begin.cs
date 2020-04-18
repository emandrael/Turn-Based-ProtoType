using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Begin : State
{
    public Begin(BattleSystem battleSystem) : base(battleSystem)
    {
    }
    public override IEnumerator Start()
    {
        // Instantiates the two characters in the scene, and sets up the components for both of them.
        BattleSystem.playerGO = BattleSystem.Instantiate(BattleSystem.playerCharacters, BattleSystem.playerBattleStation);
        BattleSystem.enemyGO = BattleSystem.Instantiate(BattleSystem.enemyCharacters, BattleSystem.enemyBattleStation);
        BattleSystem.playerUnit = BattleSystem.playerGO.GetComponent<PlayerCharacterUnit>();
        BattleSystem.enemyUnit = BattleSystem.enemyGO.GetComponent<Unit>();

        BattleSystem.currentPlayerMove = BattleSystem.playerUnit.UnitMoves;

        Animator playerAnim = BattleSystem.playerGO.GetComponent<Animator>();
        playerAnim.SetBool("IsWalkingLeft", true);


        for (int i = 0; i < BattleSystem.attackChoiceButtons.Length; i++)
        {
            if (BattleSystem.currentPlayerMove[i] != null)
            {
                BattleSystem.attackChoiceButtons[i].GetComponentInChildren<Text>().text = BattleSystem.currentPlayerMove[i].moveName;
            }
            else
            {
               BattleSystem.attackChoiceButtons[i].SetActive(false);
            }  
        }

        BattleSystem.AddDialogue(
            "Welcome to the battle, Puppet. You can continue the text by clicking on the text box~", 
            "That's a wild one, if you're not careful. Well, I think you know what can happen~", 
            "Have fun! <3");

        //Hooks up the units to their respective HUDs.
        BattleSystem.playerHUD.SetHUD(BattleSystem.playerUnit);
        BattleSystem.enemyHUD.SetHUD(BattleSystem.enemyUnit);

        yield return new WaitForSeconds(2f);

        playerAnim.SetBool("IsWalkingLeft", false);
        BattleSystem.SetState(new PlayerTurn(BattleSystem));
    }
}