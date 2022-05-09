public class TargetDiePosition : Transition
{
    private void Update()
    {
        if (Target.IsDead)
            NeedTransit = true;
    }
}
