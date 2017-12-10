using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerRespawn : NetworkBehaviour {

	bool respawning = false;

	public int countDownStartingValue = 9;
	private int countDownCurrentValue;
	// Use this for initialization
	void Start () {
		respawning = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(GetComponent<PlayerHealth>().currentHealth <= 0 && !respawning)
		{
			respawning = true;
			//Logic for respawning
			Invoke("RpcRespawn", countDownStartingValue);
			if (isLocalPlayer)
			{ 
				GameObject TextRespawnObject = GameObject.Find("TextRespawn");

			Text TextRespawn = TextRespawnObject.GetComponent<Text>();
				countDownCurrentValue = countDownStartingValue;
				InvokeRepeating("UpdateRespawnText", 1.0f, 1.0f);

			TextRespawn.text = countDownCurrentValue.ToString();
			}
		}
	}


	[ClientRpc]
	public void RpcRespawn()
	{
		//We need retrive spawnpoint location from the start location (via network)
		Transform spawn = NetworkManager.singleton.GetStartPosition();
		transform.position = spawn.position;
		//Reset Health from 0 to starting health
		GetComponent<PlayerHealth>().currentHealth = GetComponent<PlayerHealth>().startingHealth;
		GetComponent<PlayerHealth>().isDead = false;
		//We put the player in idle animation
		GetComponent<Animator>().Play("IdleWalk");


		GetComponent<PlayerHealth>().isDead = false;
		GetComponent<PlayerShoot>().isShooting = false;
		GetComponent<PlayerShoot>().isEnabled = true;
		respawning = false;


	}

	public void UpdateRespawnText()
	{
		GameObject TextRespawnObject = GameObject.Find("TextRespawn");
		Text TextRespawn = TextRespawnObject.GetComponent<Text>();
		countDownCurrentValue--;
		TextRespawn.text = countDownCurrentValue.ToString();
		if (countDownCurrentValue <= 0)
		{
			CancelInvoke("UpdateRespawnText");
			TextRespawn.text = "";
		}
	}
}
