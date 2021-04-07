using System.Collections;
using UnityEngine;

public class InteractWithItem : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private Animator animator = null;
    [SerializeField] private ItemTypes itemType = default;
    [SerializeField] private GameObject glow = null;
    [SerializeField, Tooltip("Zero means unlimited")] int interactionCountLimit = 0;

    [Header("Ignore default controller and use the following controllers in order:")]
    [SerializeField] private RuntimeAnimatorController[] multipleAnimations = default;

    private const float AnimationDelay = 0.3f;
    private int animationCoroutinesRunning = 0;

    private int interactionCount = 0;

    private bool NoAnimationsLeft => interactionCount >= multipleAnimations.Length;
    private bool HasMultipleAnimations => multipleAnimations.Length > 0;

    public ItemTypes ItemType => itemType;

    private void OnValidate()
    {
        if (multipleAnimations.Length > 0)
            interactionCountLimit = multipleAnimations.Length;
    }

    public void Interact()
    {
        if (!PlayerCanInteract())
            return;

        if (HasMultipleAnimations)
        {
            MultiAnimationInteract();
        }
        else
        {
            StartCoroutine(DelayedAnimation());
        }

        HideInteractionCue();
        interactionCount++;
    }

    private void MultiAnimationInteract()
    {
        if (NoAnimationsLeft)
            return;

        animator.runtimeAnimatorController = multipleAnimations[interactionCount];
        StartCoroutine(DelayedAnimation());
    }

    private IEnumerator DelayedAnimation()
    {
        animator.enabled = false;
        animator.Rebind();
        yield return new WaitUntil(() => animationCoroutinesRunning == 0);
        ++animationCoroutinesRunning;
        yield return new WaitForSeconds(AnimationDelay);
        animator.enabled = true;
        --animationCoroutinesRunning;
    }

    public bool PlayerCanInteract()
    {
        if (interactionCountLimit != 0 && interactionCount >= interactionCountLimit)
            return false;

        if (HasMultipleAnimations && NoAnimationsLeft)
            return false;

        return true;
    }

    public void ShowInteractionCue()
    {
        if (glow == null)
            return;

        if (!PlayerCanInteract())
            return;

        glow.SetActive(true);
    }

    private void HideInteractionCue()
    {
        if (glow == null)
            return;

        glow.SetActive(false);
    }

    private void OnMouseExit()
    {
        HideInteractionCue();
    }
}