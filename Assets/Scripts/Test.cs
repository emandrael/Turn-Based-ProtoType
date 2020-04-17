using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.move(this.gameObject, new Vector3(1, 1, 1), 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
