using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TransitionEvent : MonoBehaviour
{
    private static TransitionCore core;
    public static Queue<Action> ActionQueue =
        new Queue<Action>();

    private void Awake()
    {
        core = GetComponentInParent<TransitionCore>();
    }

    public static void MakeTransitionEvent(Action act)
    {

        core?.FadeIn();
        ActionQueue.Enqueue(act);
    }

    public void PlayCurrentAction()
    {
        while (ActionQueue.Count > 0)
        {
            ActionQueue.Dequeue()?.Invoke();
        }
        core?.FadeOut();
    }
}
