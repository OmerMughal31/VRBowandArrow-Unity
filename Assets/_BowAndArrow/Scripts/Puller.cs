using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Puller : XRBaseInteractable
{
    // How much the string of bow has been pulled?
    // for this a variable with public access to get the pull distance and 
    // private set to set the pull amount is done as
    public float pullAmount { get; private set; } = 0.0f;

    // The start position which is a linear transform from initial position of bow string so,
    public Transform start;
    // similarly end point
    public Transform end;

    // Now an Interactor has be declared so that the translation could be known upon the interaction of arrow and bow and pulling action
    private XRBaseInteractor _pullingInteractor = null;

    // Inputs:
       //   Takes the interactor
    // Outputs:
        //  After getting the iteraction values give the interactor
    
    protected override void OnSelectEnter(XRBaseInteractor interactor)
    {
        base.OnSelectEnter(interactor);
        _pullingInteractor = interactor;
    }

    // Inputs:
        //  Takes the interactor
    // Outputs:
        // On leaving the iteraction between bow and arrow upon releasing remove the interactor
        // set pull amount to zero again
    
    protected override void OnSelectExit(XRBaseInteractor interactor)
    {
        base.OnSelectExit(interactor);
        _pullingInteractor = null;
        pullAmount = 0.0f;
    }

    // Inputs: Upon pulling the string, the value of interactor is updated
        // Onserve the update in interactor value
    // Outputs:
        //  Gives the pull amount which is the translation of axis
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = _pullingInteractor.transform.position;
                pullAmount = CalculatePull(pullPosition);
            }
        }
    }

    // Inputs:
        //  Distance between our hands = pull position - starting position
        //  Target direction
    // Outputs:
        //  Output distance for pulling
    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;

        return Mathf.Clamp(pullValue, 0, 1);
    }
}
