using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VenueOwnerController : MonoBehaviour
{
    //---References---//
    private Animator animator;
    private readonly string animatorConfused = "isConfused";
    private readonly string animatorHappy = "isHappy";

    //---Events---//
    private void OnEnable() => DialogueManager.OnDialogueTagEncountered += HandleDialogueTags;
    private void OnDisable() => DialogueManager.OnDialogueTagEncountered -= HandleDialogueTags;

    //---Methods---//
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void HandleDialogueTags(string tag, string value)
    {
        switch (tag)
        {
            case "Confused":
                animator.SetBool(animatorConfused, Convert.ToBoolean(value));
                break;
            case "Happy":
                animator.SetBool(animatorHappy, Convert.ToBoolean(value));
                break;
            default:
                ResetAllAnimatorParameters();
                break;
        }
    }

    private void ResetAllAnimatorParameters()
    {
        // Guard clause in case the animator component isn't set yet
        if (animator == null) return;

        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            switch (param.type)
            {
                case AnimatorControllerParameterType.Bool:
                    // Set bools to false
                    animator.SetBool(param.name, false);
                    break;

                case AnimatorControllerParameterType.Int:
                    // Set ints to 0
                    animator.SetInteger(param.name, 0);
                    break;

                case AnimatorControllerParameterType.Float:
                    // Set floats to 0
                    animator.SetFloat(param.name, 0f);
                    break;

                case AnimatorControllerParameterType.Trigger:
                    // Triggers are special; you "Reset" them to clear any pending activations
                    animator.ResetTrigger(param.name);
                    break;
            }
        }
    }
}
