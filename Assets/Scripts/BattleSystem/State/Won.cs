using System.Collections;
using UnityEngine;

internal class Won : State
{
    public Won(BattleSystem battleSystem) : base(battleSystem)
    {
    }
    public override IEnumerator Start()
    {

        LeanTween.moveLocal(BattleSystem.enemyGO, new Vector3(-20, 0,BattleSystem.enemyGO.transform.position.z), 2f);
        yield return new WaitForSeconds(0.5f);
        BattleSystem.Destroy(BattleSystem.enemyGO);
        GameEvents.current.StopText();
        yield return new WaitForSeconds(0.15f);
        GameEvents.current.FinishingUp();

        BattleSystem.AddDialogue("Darn, it ran.", "Well it doesn't matter. Good work Puppet, you don't seem entirely useless", "Thanks for playing~", "The game should close on it's own.", "Lmao it's still on", "Any second now", 
            "Hitler did nothing wrong"
            ,"What"
            ,"A man can have a view"
            ,"Don't judge me"
            ,"You probably think this is a bit much",
            "But whatever it's my game not yours <3"
            , "Anyways why are you sill here.",
            "You must be really trying for this text",
            "Maybe hoping to find a secret message.",
            "Well you're not getting one",
            "Or are you?",
            "Maybe if you check the first letters of every sentence in this screen you can find out <3",
            "                                                                                         "
            ,
            "                                                                                         "
            ,
            "                                                                                         "
            ,
            "                                                                                         ",
            "Damn did you actually do that?",
            "Wow you're stupid you think I could be asked with that?",
            "Listen, I ain't that much of a loser to take my time to type a long ass puzzle for you",
            "I have much better uses of my time :)",
            "If you came this far. I'd just like to say thank you!");

        yield return new WaitForSeconds(15f);
        Application.Quit();
    }
}