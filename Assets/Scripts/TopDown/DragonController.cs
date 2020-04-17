using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField] private GameObject dragon;
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onTriggerEnter += OnEnter;
    }

    // Update is called once per frame
    private void OnEnter()
    {
        dragon.SetActive(true);
    }

    private void OnDestroy()
    {
        GameEvents.current.onTriggerEnter -= OnEnter;
    }
}
