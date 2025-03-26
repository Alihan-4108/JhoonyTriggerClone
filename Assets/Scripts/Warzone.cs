using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Splines;

public class Warzone : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SplineContainer playerSplines;
    [SerializeField] private Transform ikTarget;
    [SerializeField] private SplineAnimate ikSplineAnimate;
    [SerializeField] private SplineComputer newPlayerSpline;

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

    public SplineComputer GetPlayerSpline()
    {
        return newPlayerSpline;
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