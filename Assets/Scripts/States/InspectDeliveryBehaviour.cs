using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectDeliveryBehaviour : PatrolToPositionBehaviour
{
    /// <summary>
    /// At the end of the waiting time, check if the agent can grab a delivery and proceed to the according state.
    /// </summary>
    protected override void OnEndWatchingTime()
    {
        if (controller.GrabDelivery(officePosition))
            controller.ForceState(AIState.StoreDelivery);
        else controller.UpdateState(AIState.InspectDelivery); 
    }
}
