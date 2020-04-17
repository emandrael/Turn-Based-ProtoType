using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    public override IEnumerator Start()
    {
        //Instantiates the player and enemy. 
        playerGO = Instantiate(playerCharacters, playerBattleStation);
        enemyGO = Instantiate(enemyCharacters, enemyBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        enemyUnit = enemyGO.GetComponent<Unit>();

        //This is just some normal dialogue. 

        AddDialogue("Welcome to the battle, Puppet.", "That's a wild one, if you're not careful. Well, I think you know what can happen~", "Have fun! <3");

        //Hooks up the units to their respective HUDs.
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        // Ending with giving the state to the
        state = BattleState.PLAYERTURN;
        StartCoroutine(PlayerTurn());
    }
}
