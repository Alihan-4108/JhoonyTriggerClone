using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private SpriteRenderer gradient;


	public void Interact()
	{
		gradient.color = Color.green;
	}
}