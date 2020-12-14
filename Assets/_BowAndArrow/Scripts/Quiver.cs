using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Quiver : XRSocketInteractor
{
    // to deal with arrows in our scene to appear and other functionality I have created a gameobject for arrowprefab
    public GameObject arrowPrefab = null;

    // to deal with the spawn of arrow, the arrow offset is requires
    // the attachement offset is required to position the arrow correctly in our scene

    private Vector3 _attachOffset = Vector3.zero;
    protected override void Awake()
    {
        base.Awake();
        // create arrows as the game starts
        CreateAndSelectArrow();
        // initiate the offsets for arrows to know where to grab the arrow
        SetAttachOffset();
    }


    protected override void OnSelectExit(XRBaseInteractable interactable)
    {
        base.OnSelectExit(interactable);
        // Whenever the arrow is created and pulled back in the bow socket the new one has to created also
        CreateAndSelectArrow();
    }

    private void CreateAndSelectArrow()
    {
        Arrow arrow = CreateArrow();
        SelectArrow(arrow);
    }

    private Arrow CreateArrow()
    {
        GameObject arrowObject = Instantiate(arrowPrefab, transform.position - _attachOffset, transform.rotation);
        return arrowObject.GetComponent<Arrow>();
    }

    private void SelectArrow(Arrow arrow)
    {
        OnSelectEnter(arrow);
        arrow.OnSelectEnter(this);
    }

    private void SetAttachOffset()
    {
        if (selectTarget is XRGrabInteractable interactable)
        {
            _attachOffset = interactable.attachTransform.localPosition;
        }
    }
}
