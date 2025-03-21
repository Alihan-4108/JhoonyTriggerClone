using UnityEngine;
using UnityEngine.Splines;

public class Warzone : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private SplineContainer playerSplines;
	[SerializeField] private Transform ikTarget;
	[SerializeField] private SplineAnimate ikSplineAnimate;

	[Header("Settings")]
	[SerializeField] private float duration;
	[SerializeField] private float animatorSpeed;
	[SerializeField] private string animationToPlay;

	private void Start()
	{
		ikSplineAnimate.Duration = duration;
	}

	public void StartAnimatingIKTarget()
	{
		ikSplineAnimate.Play();
	}

	public Spline GetPlayerSpline()
	{
		return playerSplines.Spline;
	}

	public float GetDuration()
	{
		return duration;
	}

	public float GetAnimatorSpeed()
	{
		return animatorSpeed;
	}

	public string GetAnimationToPlay()
	{
		return animationToPlay;
	}

	public Transform GetIKTarget()
	{
		return ikTarget;
	}
}