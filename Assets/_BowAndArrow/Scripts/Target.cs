using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    [Serializable] public class HitEvent : UnityEvent<int> { }
    public HitEvent OnHit = new HitEvent();

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
            FigureOutScore(collision.transform.position);
    }

    private void FigureOutScore(Vector3 hitPosition)
    {
        float distanceFromCenter = Vector3.Distance(transform.position, hitPosition);

        int score = 0;

        if (distanceFromCenter <= 0.15f)
            score = 25;
        else if (distanceFromCenter > 0.15f && distanceFromCenter < 0.4f)
            score = 20;
        else if (distanceFromCenter > 0.4f && distanceFromCenter < 0.6f)
            score = 15;
        else if (distanceFromCenter > 0.6f && distanceFromCenter < 0.8f)
            score = 10;
        else if (distanceFromCenter > 0.8f && distanceFromCenter < 1.0f)
            score = 5;
        else if (distanceFromCenter > 1.0f && distanceFromCenter < 1.18f)
            score = 1;
        else
            score = 0;

        OnHit.Invoke(score);
    }
}
