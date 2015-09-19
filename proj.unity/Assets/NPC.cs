using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour 
{
	public float health;
	public Vector2 movementSpeed;
	public string pncName;

	[SerializeField]
	public GameObject m_Head;
	public Transform m_GunPivot;
}
