using System;
using UnityEngine;

public class VenueOwnerController : MonoBehaviour
{
    //---References---//
    private Animator animator;
    private string animatorBool = "isConfused";

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
                animator.SetBool(animatorBool, Convert.ToBoolean(value));
                break;
            default:
                animator.SetBool(animatorBool, false);
                break;

        }
    }
}
