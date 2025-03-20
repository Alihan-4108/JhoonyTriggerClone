using UnityEngine;
using UnityEngine.Splines;

public class Warzone : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private SplineContainer playerSplines;

	[Header("Settings")]
	[SerializeField] private float duration;
	[SerializeField] private float animatorSpeed;
	[SerializeField] private string animationToPlay;

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
}