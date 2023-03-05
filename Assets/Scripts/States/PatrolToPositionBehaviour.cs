using UnityEngine;

/// <summary>
/// This AI State Machine Behaviour will handle the behaviour to go to a position and wait a few seconds before calling the OnEndWatchingTime Method.
/// </summary>
public class PatrolToPositionBehaviour : AIStateMachineBehaviour
{
    [SerializeField] protected Vector2 officePosition;
    [SerializeField] private float watchingTime = 10.0f;
    private float timer = 0f;

    /// <summary>
    /// Reset the Timer value and set the Movable Agent Destination
    /// </summary>
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timer = 0f; 
        controller.MovableAgent.SetDestination(officePosition); 
    }

    /// <summary>
    /// When the agent has reached its destination (aka is not moving anymore) update the timer value.
    /// When the timer has reached its limit, call the <see cref="OnEndWatchingTime"/> method.
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="stateInfo"></param>
    /// <param name="layerIndex"></param>
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!controller.MovableAgent.IsMoving)
            timer += Time.deltaTime;

        if (timer > watchingTime)
            OnEndWatchingTime();
    }

    /// <summary>
    /// This Method will be called when the agent has reached its destination and waited for <see cref="watchingTime"/> seconds
    /// </summary>
    protected virtual void OnEndWatchingTime()
    {
        controller.UpdateState(AIState.PatrolOffice); 
    }    
}
