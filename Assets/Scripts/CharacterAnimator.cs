using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    public AnimationClip ReplaceableAttackAnim;
    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float blendTime = .1f;
    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;

	// Use this for initialization
	protected virtual void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, blendTime, Time.deltaTime);

        animator.SetBool("inCombat", combat.InCombat);
        combat.OnAttack += OnAttack;
	}

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);
        overrideController[ReplaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
