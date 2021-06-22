using System.Collections;
using UnityEngine;

/// <summary>
/// Script for objects that can be interacted with only if player is holding
/// the right item.
/// </summary>
public class InteractWithItem : MonoBehaviour
{
    /// <summary>
    /// Reference to the main camera - required for interaction cue to hide
    /// </summary>
    [SerializeField] private Camera mainCamera = null;
    /// <summary>
    /// Reference to the animator component (required)
    /// </summary>
    [Header("General settings")]
    [SerializeField] private Animator animator = null;

    /// <summary>
    /// What item type should this object interact with?
    /// </summary>
    [SerializeField] private ItemTypes itemType = default;

    /// <summary>
    /// Reference to a GameObject with glow effect that appears when mousing
    /// over.
    /// </summary>
    [SerializeField] private GameObject glow = null;

    /// <summary>
    /// (ignored when the multipleAnimations array below is not empty)
    /// 
    /// Should the player be able to interact with the object a limited number
    /// of times? Zero means unlimited.
    /// 
    /// After reaching this limit, object stops being intaractable.
    /// </summary>
    [SerializeField, Tooltip("Zero means unlimited")] int interactionCountLimit = 0;

    /// <summary>
    /// Should a Puzzle be activated upon interaction?
    /// </summary>
    [SerializeField] Puzzle puzzleToActivate = null;

    /// <summary>
    /// Use this array if object has multiple animations and different ones
    /// should be used depending on how many times player has interacted with
    /// the object.
    /// 
    /// After the last animation is played, object stops being interactable.
    /// </summary>
    [Header("Ignore default controller and use the following controllers in order:")]
    [SerializeField] private RuntimeAnimatorController[] multipleAnimations = default;

    /// <summary>
    /// Animation delay after clicking the object (time in seconds)
    /// </summary>
    private const float AnimationDelay = 0.3f;

    /// <summary>
    /// Counter for how many DelayedAnimation() coroutines are running. Used to
    /// make sure we wait for previous coroutine to finish before new one is
    /// started.
    /// </summary>
    private int animationCoroutinesRunning = 0;

    /// <summary>
    /// How many times has player interacted with this object?
    /// </summary>
    private int interactionCount = 0;

    /// <summary>
    /// Have we played all the animations?
    /// Note: this is used only if multipleAnimations array is not empty.
    /// </summary>
    private bool NoAnimationsLeft => interactionCount >= multipleAnimations.Length;

    /// <summary>
    /// Is multipleAnimations array not empty?
    /// </summary>
    private bool HasMultipleAnimations => multipleAnimations.Length > 0;

    /// <summary>
    /// What item type should this object interact with?
    /// </summary>
    public ItemTypes ItemType => itemType;

    /// <summary>
    /// Is the interaction cue visible?
    /// </summary>
    private bool interactionCueVisible = false;

    /// <summary>
    /// Max raycast distance
    /// </summary>
    private const float MaxRayDistance = 100f;

    /// <summary>
    /// This is a Unity-provided function that runs when changing something
    /// in the inspector. 
    /// 
    /// If multipleAnimations array is not empty, it sets interactionCountLimit
    /// to its length in order to avoid confusion.
    /// </summary>
    private void OnValidate()
    {
        if (multipleAnimations.Length > 0)
            interactionCountLimit = multipleAnimations.Length;
    }

    /// <summary>
    /// Begin interaction with this object.
    /// </summary>
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

        ActivatePuzzle();
        HideInteractionCue();
        interactionCount++;
    }

    private void ActivatePuzzle()
    {
        if (puzzleToActivate == null)
            return;

        if (puzzleToActivate.Activated)
            return;

        puzzleToActivate.Activate();
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
        if (animator == null)
            yield break;

        animator.enabled = false;
        animator.Rebind();
        yield return new WaitUntil(() => animationCoroutinesRunning == 0);
        ++animationCoroutinesRunning;
        yield return new WaitForSeconds(AnimationDelay);
        animator.enabled = true;
        --animationCoroutinesRunning;
    }

    /// <summary>
    /// Is this object interactable?
    /// </summary>
    public bool PlayerCanInteract()
    {
        if (interactionCountLimit != 0 && interactionCount >= interactionCountLimit)
            return false;

        if (HasMultipleAnimations && NoAnimationsLeft)
            return false;

        return true;
    }

    /// <summary>
    /// Show a visual cue that this object can be interacted with (usually
    /// a glow effect)
    /// </summary>
    public void ShowInteractionCue()
    {
        if (glow == null)
            return;

        if (!PlayerCanInteract())
            return;

        glow.SetActive(true);
        interactionCueVisible = true;
    }

    private void HideInteractionCue()
    {
        if (glow == null)
            return;

        glow.SetActive(false);
        interactionCueVisible = false;
    }

    private void Update()
    {
        if (!interactionCueVisible)
            return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, MaxRayDistance, layerMask: 1 << 0, QueryTriggerInteraction.Ignore))
        {
            HideInteractionCue();
            return;
        }

        if (hit.collider.gameObject == gameObject)
            return;

        HideInteractionCue();
    }
}