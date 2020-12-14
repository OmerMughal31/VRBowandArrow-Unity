using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class Bow : XRGrabInteractable
{
    private Animator _animator = null;
    private Puller _puller = null;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        _puller = GetComponentInChildren<Puller>();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                AnimateBow(_puller.pullAmount);
            }
        }
    }

    private void AnimateBow(float value)
    {
        _animator.SetFloat("Blend", value);
    }
}
