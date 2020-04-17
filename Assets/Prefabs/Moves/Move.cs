using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Moves/New Move")]
public class Move : ScriptableObject
{
    public string moveName;
   
    public int moveAttackValue;
    public int moveAPCost;
  
    public bool isPhysical;
    public bool isMagical;
    
    public Effect moveEffect;

    public int moveAccuracy = 0;


    public float EffectLength()
    {
        if (moveEffect != null)
            return moveEffect.effectLength;
        else
            return 1f;
    }
        
    
    
}