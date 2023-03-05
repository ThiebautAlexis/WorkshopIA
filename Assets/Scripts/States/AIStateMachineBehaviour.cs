using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will handle the basic behaviour of the AI to get the AIController script.
/// Inherit from this class to create a basic AI Behaviour.
/// </summary>
public abstract class AIStateMachineBehaviour : StateMachineBehaviour
{
    protected AIController controller = null;
    private bool isInitialized = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!isInitialized)
        {
            isInitialized = animator.TryGetComponent(out controller); 
        }
    }
}
