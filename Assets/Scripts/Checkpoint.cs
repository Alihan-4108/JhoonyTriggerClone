using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SpriteRenderer gradient;

    [Header("Actions")]
    public static Action<Checkpoint> onInteracted;

    public void Interact()
    {
        gradient.color = Color.green;

        onInteracted?.Invoke(this);
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}