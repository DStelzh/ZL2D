using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRooms
{
	public Door[] doors;

	public void CheckEnemies()
	{
		for (int i = 0; i < enemies.Length; i++)
		{
			if (enemies[i].gameObject.activeInHierarchy && i < enemies.Length - 1) // enemies.Lenth-1 is substantial for the last enemies, as the check works until intended for the every enemy 2nd to lst
			{
				return;
			}
		}
		OpenDoors();
	}
	public override void OnTriggerEnter2D(Collider2D other)
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
			CloseDoors();
			virtualCamera.SetActive(true);
		}
	}
	public override void OnTriggerExit2D(Collider2D other)
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
			virtualCamera.SetActive(false);
		}
	}
	public void CloseDoors()
	{
		for (int i = 0; i < doors.Length; i++)
		{
			doors[i].Close();
		}
	}
	public void OpenDoors()
	{
		for (int i = 0; i < doors.Length; i++)
		{
			doors[i].Open();
		}
	}
}
