using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private Animator animator;

	public void PlayRunAnimation()
	{
		Play("Run");
	}

	public void PlayIdleAnimation()
	{
		Play("Idle");
	}

	public void Play(string animationName)
	{
		animator.Play(animationName);
	}

	public void Play(string animationName, float animatorSpeed)
	{
		animator.speed = animatorSpeed;
		Play(animationName);
	}
}