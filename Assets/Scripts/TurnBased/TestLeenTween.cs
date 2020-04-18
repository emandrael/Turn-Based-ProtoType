﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLeenTween : MonoBehaviour
{
    [SerializeField] private Vector2 location;
    [SerializeField] private Vector2 location2;
    [SerializeField] private bool isEnemy;
    [SerializeField] private bool isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onButtonPressed += MoveToStartingPositions;
        GameEvents.current.ButtonPressed();
        GameEvents.current.onButtonPressed -= MoveToStartingPositions;
        if (isEnemy)
        {
            GameEvents.current.onFinishingUp += GetOutOfScreen;
        }      
    }

    // Update is called once per frame
    
    void MoveToStartingPositions()
    {
        if(isPlayer)
            gameObject.GetComponent<Animation>().Play("WalkingIn");
        LeanTween.moveLocal(gameObject, new Vector3(location.x, location.y, gameObject.transform.position.z), 2).setOnComplete(StopAnim);

    }

    void GetOutOfScreen()
    {
        LeanTween.moveLocal(gameObject, new Vector3(location2.x, location2.y, gameObject.transform.position.z), 2);
    }

    void StopAnim()
    {
        if (!isEnemy)
        {
            gameObject.GetComponent<Animation>().Stop("WalkingIn");
        }
    }


}
