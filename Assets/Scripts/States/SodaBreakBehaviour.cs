using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaBreakBehaviour : PatrolToPositionBehaviour
{
    /// <summary>
    /// At the end of the waiting time, reset the thirst then proceed to the patrol office state.
    /// </summary>
    protected override void OnEndWatchingTime()
    {
        controller.AgentThirst.ResetThirst();
        controller.ForceState(AIState.PatrolOffice); 
    }
}
