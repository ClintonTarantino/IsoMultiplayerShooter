using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerID : NetworkBehaviour {

	[SyncVar] public string playerUniqueName;

	private NetworkInstanceId playerNetID;
	public override void OnStartLocalPlayer()

	{
		base.OnStartLocalPlayer();
		{
			//Todo: Get network ID
			//Todo: set the ID of the player
			GetNetIdentity();
			SetIdentity();
			SetCameraTarget();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.name == "" || transform.name == "ironmanPrefab(Clone)")
		{
			SetIdentity();
		}
	}

	[Client]
	void GetNetIdentity()
	{
		playerNetID = GetComponent<NetworkIdentity>().netId;

		//todo: tell all the clients about the players ID
		CmdTellSeverMyIdentity(MakeUniqueIdentity());
	}
	
	void SetIdentity()
	{
		if (!isLocalPlayer)
		{
			this.transform.name = playerUniqueName;
		}
		else
		{
			//When its local client player, create ID
			transform.name = MakeUniqueIdentity();
		}
	}

	string MakeUniqueIdentity()
	{
		return "Player " + playerNetID.ToString();

	}

	[Command]
	void CmdTellSeverMyIdentity(string name)
	{
		//Todo:
		playerUniqueName = name;
	}

	public void SetCameraTarget()
	{
		if (isLocalPlayer)
		{
			Camera.main.GetComponent<CameraFollow>().Target = this.gameObject;
			Camera.main.GetComponent<CameraFollow>().SetCameraOffSet(this.gameObject.transform.position);
		}
	}

}
