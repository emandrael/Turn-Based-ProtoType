using System.Collections;
public abstract class State
{

    protected BattleSystem BattleSystem;

    public State(BattleSystem battleSystem)
    {
        BattleSystem = battleSystem;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator Attack()
    {
        yield break;
    }

    public virtual IEnumerator Heal()
    {
        yield break;
    }

    public virtual IEnumerator UseMove1()
    {
        yield break;
    }

    public virtual IEnumerator UseMove2()
    {
        yield break;
    }

    public virtual IEnumerator UseMove3()
    {
        yield break;
    }

    public virtual IEnumerator UseMove4()
    {
        yield break;
    }
}
