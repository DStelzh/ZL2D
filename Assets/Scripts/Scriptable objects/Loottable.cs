using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    //so in here we want this class to hold 2 things
	// - the thing that we want to loot (which is not an item but a PowerUp in our case)
	// item is an item in inv
	public PowerUpObj thisLoot; //pls check if we called it powerups in our UNITY proj.
    public int lootChance; // can be decimal as well with more items etc.
}



[CreateAssetMenu]
public class Loottable : ScriptableObject
{
	public Loot[] loots;

	public PowerUpObj LootPowerup()
	{
		//what we wanna do here is accumulutive probability
		// rng number 1-100, if between 0-25 => 25% item, 0-50 => 50% item, >50 => nothing
		int cumProb = 0;
		int currentProb = Random.Range(0, 100);
		for (int i = 0; i < loots.Length; i++)
		{
			cumProb += loots[i].lootChance;
			if (currentProb <= cumProb)
			{
				return loots[i].thisLoot;
			}
		}
		return null; //in case the enemy drops nothing at all



	}
}
