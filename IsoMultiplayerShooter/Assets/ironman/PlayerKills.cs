using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerKills : NetworkBehaviour {


	[SyncVar] public int KillsCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		SetKillsCount();
	}

	public void SetKillsCount()
	{
		if (isLocalPlayer)
		{
			GameObject killsText = GameObject.Find("KillText");
			killsText.GetComponent<Text>().text = "Kills: " + KillsCount.ToString();
		}
	}
}
