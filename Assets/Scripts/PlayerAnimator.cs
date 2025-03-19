using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private Animator animator;


	public void PlayRunAnimation()
	{
		animator.Play("Run");
	}
}