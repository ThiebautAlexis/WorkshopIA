using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    /// <summary>This field is used to store the Parameter Name in the Animator as an int</summary>
    private static readonly int iaState_Hash = Animator.StringToHash("IAState");

    [SerializeField] private Animator animatorController = null;
    [SerializeField] private MovableAgent movableAgent = null;
    [SerializeField] private AgentThirst agentThirst = null;

    public MovableAgent MovableAgent => movableAgent; 
    public AgentThirst AgentThirst => agentThirst; 

    /// <summary>
    /// Update the state in most of the situations.
    /// If you want to override this method to set a state manually, call <see cref="ForceState(AIState)"/> instead.
    /// </summary>
    /// <param name="_currentState">The current state of the agent.</param>
    public void UpdateState(AIState _currentState)
    {
        AIState _state = AIState.PatrolOffice;

        if (agentThirst.CheckThirst())
            _state = AIState.SodaBreak;
        else
            _state = _currentState == AIState.PatrolOffice ?  AIState.InspectDelivery : AIState.PatrolOffice; 

        animatorController.SetInteger(iaState_Hash, (int)_state); 
    }

    /// <summary>
    /// Force the state of the Agent to a value
    /// </summary>
    /// <param name="_state">The targeted state</param>
    public void ForceState(AIState _state) => animatorController.SetInteger(iaState_Hash, (int)_state);

    /// <summary>
    /// Call this method to grab a delivery (if there is one)
    /// </summary>
    /// <param name="_position">The position where the delivery should be</param>
    /// <returns>Return true if there is a delivery and it has been grabbed, otherwise return false</returns>
    public bool GrabDelivery(Vector2 _position)
    {
        // Est-ce que je peux grab qqlque chose? 
        // SI oui je le grab ici et je le parente à moi

        return true; 
    }

}

public enum AIState
{
    PatrolOffice = 0, 
    SodaBreak, 
    InspectDelivery, 
    StoreDelivery
}

[System.Flags]
public enum AIDetection
{
    Thirsty = 1 << 1, 
    DeliveryFound = 1<<2, 
    SuspiciousSound = 1 <<3, 
    Tired = 1 << 4 
}