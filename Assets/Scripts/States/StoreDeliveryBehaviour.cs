using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreDeliveryBehaviour : PatrolToPositionBehaviour
{
    /// <summary>
    /// At the end of this behaviour; go back to the patrol office State.
    /// </summary>
    protected override void OnEndWatchingTime()
    {
        // Leave Delivery here
        controller.ForceState(AIState.PatrolOffice); 
    }
}
