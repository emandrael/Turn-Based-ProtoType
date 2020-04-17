using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : StateMachine

{
    #region References
    [SerializeField] public GameObject playerCharacters;

    [SerializeField] public GameObject enemyCharacters;


    [SerializeField] public Transform playerBattleStation;

    [SerializeField] public Transform enemyBattleStation;

    [SerializeField] public Text dialogue;

    public Unit playerUnit;
    public Unit enemyUnit;

    [SerializeField] public BattleHUD playerHUD;
    [SerializeField] public BattleHUD enemyHUD;
    
    [SerializeField] public GameObject battlePanel;
    [SerializeField] public Vector3 battlePanelStartPos;
    [SerializeField] public Button battlePanelAttackButton;
    [SerializeField] public Button battlePanelHealButton;

    [SerializeField] public GameObject attackChoicePanel;
    [SerializeField] public Vector3 attackChoicePanelStartPos;

    [SerializeField] public GameObject[] attackChoiceButtons;

    [SerializeField] public Button attackChoicePanelBackButton;

    public GameObject playerGO;
    public GameObject enemyGO;

    public Move[] allMoves; 

    public Move[] currentPlayerMove;

    public Move currentAttackMove;

    #endregion


    // Sets the state to Start and finds the initial start for the battle panel.
    private void Start()
    {
        //Set the starting positions of the battle panels. So they can be used again when animating the UI movements.
        attackChoicePanelStartPos = attackChoicePanel.transform.position;
        battlePanelStartPos = battlePanel.transform.position;
        
        SetState(new Begin(this));
    }

    public void AddDialogue(params string[] dialogue)
    { 
        List<string> diallogueForBattle = new List<string>();

        foreach (string line in dialogue)
        {
            diallogueForBattle.Add(line);
        }
        GameEvents.current.SkippableTextAdded(diallogueForBattle);
    }


    public void OnAttackButton()
    {
        //StartCoroutine(State.Attack());
        SetState(new AttackChoice(this));
    }

    public void OnAttackChoiceBackButton()
    {
        SetState(new PlayerTurn(this));
    }

    public void OnMove1Button()
    {
        StartCoroutine(State.UseMove1());
    }
    public void OnMove2Button()
    {
        StartCoroutine(State.UseMove2());
    }
    public void OnMove3Button()
    {
        StartCoroutine(State.UseMove3());
    }
    public void OnMove4Button()
    {
        StartCoroutine(State.UseMove4());
    }

    public Move FindMove(string moveName)
    {
        for (int i = 0; i < allMoves.Length; i++)
        {
            if(moveName == allMoves[i].moveName)
            {
                return allMoves[i];
            }
        }
        return null;
    }

}