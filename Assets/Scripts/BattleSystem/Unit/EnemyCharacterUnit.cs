using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacterUnit : Unit
{
    [SerializeField] private Move[] supportMoves;

    public Move[] SupportMoves { get => supportMoves; set => supportMoves = value; }
}
