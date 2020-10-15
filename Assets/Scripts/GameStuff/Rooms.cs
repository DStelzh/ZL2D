using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooms : MonoBehaviour
{
    public Enemies[] enemies;
    public Pot[] pots;
<<<<<<< HEAD
=======
	public GameObject virtualCamera;
>>>>>>> Cinemachine-Implement

	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			// Activate all enemies and pots
			for (int i = 0; i < enemies.Length; i++)
			{
				ChangeActivation(enemies[i], true);
			}
			for (int i = 0; i < pots.Length; i++)
			{
				ChangeActivation(pots[i], true);
			}
<<<<<<< HEAD
=======
			virtualCamera.SetActive(true);
>>>>>>> Cinemachine-Implement
		}
	}
	public virtual void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !other.isTrigger)
		{
			// Activate all enemies and pots
			for (int i = 0; i < enemies.Length; i++)
			{
				ChangeActivation(enemies[i], false);
			}
			for (int i = 0; i < pots.Length; i++)
			{
				ChangeActivation(pots[i], false);
			}
<<<<<<< HEAD
		}
	}

	void ChangeActivation(Component component, bool activation)
=======
			virtualCamera.SetActive(false);
		}
	}

	public void ChangeActivation(Component component, bool activation)
>>>>>>> Cinemachine-Implement
	{
		component.gameObject.SetActive(activation);
	}
}
