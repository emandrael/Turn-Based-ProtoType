using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private int level;

    [SerializeField] private int currentHealth;

    [SerializeField] private int maxHealth;

    [SerializeField] private int currentActionPoints;
    [SerializeField] protected int maxActionPoints;

    [SerializeField] private int strength;
    [SerializeField] private int dexterity;
    [SerializeField] private int intelligence;

    [SerializeField] private Move[] unitMoves;

    [SerializeField] private bool isEnemy;

    private int previousHealth;

    public int PreviousHealth { get => level; set => level = value; }
    public int Level { get => level; set => level = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int CurrentActionPoints { get => currentActionPoints; set => currentActionPoints = value; }
    public int Strength { get => strength; set => strength = value; }
    public int Dexterity { get => dexterity; set => dexterity = value; }
    public int Intelligence { get => intelligence; set => intelligence = value; }
    public Move[] UnitMoves { get => unitMoves; set => unitMoves = value; }

    public virtual bool takeDamage(int damageTaken)
    {
        PreviousHealth = CurrentHealth;
        CurrentHealth -= damageTaken;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            return true;
      
        }
        else return false;
    }

    public virtual bool takeDamageFromMoveByUnit(Move moveUsed, Unit attackingUnit, Unit defendingUnit)
    {
        int damageTaken;
        
        if (moveUsed.isPhysical == true && moveUsed.isMagical == false) damageTaken = attackingUnit.Strength + moveUsed.moveAttackValue;
        else if (moveUsed.isMagical == true && moveUsed.isPhysical == false) damageTaken = attackingUnit.intelligence + moveUsed.moveAttackValue;
        else damageTaken = moveUsed.moveAttackValue;
        
        if (moveUsed.moveEffect != null)
            Instantiate(moveUsed.moveEffect, defendingUnit.transform);
        PreviousHealth = CurrentHealth;
        CurrentHealth -= damageTaken;
        
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            return true;

        }
        else return false;

    }

    public virtual void healHealth(int healthHealded)
    {
        CurrentHealth += healthHealded;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
    }


}
