using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextTypeWriter : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    List<string> story = new List<string>();
    float timeBetweenTypes = 0.025f;    
    
    private bool skipper;
    public void SkipText() => skipper = true;
    
    private bool goNextLine = false;

    public void GoNext() => goNextLine = true;
    

    public void Start()
    {
        GameEvents.current.onSkippableTextAdded += StartTypingSkippableText;
        GameEvents.current.onStopText += StopTextTyping;
    }


    public void StartTypingSkippableText(List<string> stories)
    {
        GameEvents.current.onSkipText += SkipText;
        text = GetComponent<Text>();

        StartCoroutine(PlaySkippableText(stories));

    }

    public void StopTextTyping()
    {
        
        StopAllCoroutines();
        text.text = "";
    }


    public IEnumerator PlaySkippableText(List<string> dialogues)
    {
        foreach (string line in dialogues)
        {
            text.text = "";
            foreach (char c in line)
            {

                if (skipper)
                {
                    text.text = line;
                    GameEvents.current.onSkipText -= SkipText;
                    GameEvents.current.onGoingNextLine += GoNext;
                    while(!goNextLine)
                    {
                        yield return null;
                    }
                    goNextLine = false;
                    GameEvents.current.onSkipText += SkipText;
                    GameEvents.current.onGoingNextLine -= GoNext;
                    skipper = false;
                    break;
                    
                }
                else
                {
                    text.text += c;
                    yield return new WaitForSeconds(timeBetweenTypes);
                }
            }
            GameEvents.current.onSkipText -= SkipText;
            GameEvents.current.onGoingNextLine += GoNext;
            while (!goNextLine)
            {
                yield return null;
            }
            goNextLine = false;
            GameEvents.current.onSkipText += SkipText;
            GameEvents.current.onGoingNextLine -= GoNext;
        }
        GameEvents.current.onSkipText -= SkipText;
        GameEvents.current.onGoingNextLine -= GoNext;
    }
}
