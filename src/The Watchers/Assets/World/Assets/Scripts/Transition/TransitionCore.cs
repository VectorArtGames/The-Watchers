using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class TransitionCore : MonoBehaviour
{
    #region Singleton
    public static TransitionCore Instance { get; set; }
    private void Awake()
    {
        Instance = this;

        animator = GetComponentInChildren<Animator>();
    }

    #endregion

    public Animator animator;

    public void FadeIn() => animator?.SetTrigger("in");

    public void FadeOut() => animator?.SetTrigger("out");
}
