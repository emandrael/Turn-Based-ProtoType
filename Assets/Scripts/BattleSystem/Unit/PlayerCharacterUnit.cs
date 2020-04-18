using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterUnit : Unit
{
    [SerializeField] private Move healMove;

    public Move HealMove { get => healMove; set => healMove = value; }
}
