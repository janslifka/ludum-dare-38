using UnityEngine;

public abstract class ActiveObject : MonoBehaviour
{
	public abstract void Highlight();
	public abstract void Unhighlight();
	public abstract void Use();
}
