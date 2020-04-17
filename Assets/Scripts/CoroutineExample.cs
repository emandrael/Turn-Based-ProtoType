using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    [SerializeField] private float duration = 2f;
    [SerializeField] private GameObject leenMeanTweeenGreen;
    private bool isLogging = false;

    private bool trueFalsePositon = true;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isLogging)
        {
            StartCoroutine(DoSomething());
        }
    }

    private IEnumerator DoSomething()
    {
        Debug.Log("Hi");

        
        
        if (trueFalsePositon == true)
        {
            LeanTween.moveLocal(leenMeanTweeenGreen, new Vector3(0.0f, 0.4f,0), 0.5f);
        }
        else
        {
            LeanTween.moveLocal(leenMeanTweeenGreen, new Vector3(0.0f, 2.0f,0), 0.5f);
        }


        yield return new WaitForSeconds(1f);

        trueFalsePositon = !trueFalsePositon;

        isLogging = false;
    }
}
