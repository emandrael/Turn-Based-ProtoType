using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    private event Action<string> onDialogueChange;

    public void DiaglogueChange(string toChange)
    {
        onDialogueChange?.Invoke(toChange);
    }

    public event Action onTriggerEnter;
    
    public void TriggerEnter()
    {
        onTriggerEnter?.Invoke();
    }

    public event Action onButtonPressed;

    public void ButtonPressed()
    {
        onButtonPressed?.Invoke();
    }

    public event Action onFinishingUp;

    public void FinishingUp()
    {
        onFinishingUp?.Invoke();
    }

    public event Action<List<string>> onSkippableTextAdded;

    public void SkippableTextAdded(List<string> text)
    {
        onSkippableTextAdded?.Invoke(text);
    }

    public event Action onStopText;
    
    public void StopText()
    {
        onStopText?.Invoke();
    }

    public event Action<Move> onAttackOrdered;
    
    public void AttackOrdered(Move move)
    {
        onAttackOrdered?.Invoke(move);
    }

    public event Action onSkipText;

    public void SkipText()
    {
        onSkipText?.Invoke();
    }

    public event Action onGoingNextLine;
    
    public void GoingNextLine()
    {
        onGoingNextLine?.Invoke();
    }

}
