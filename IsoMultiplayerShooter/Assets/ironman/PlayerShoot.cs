﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using DigitalRuby.PyroParticles;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {


	RaycastHit shootHit;
	Ray shootRay;
	int shootableMask;
	public bool isShooting = false;
	public int damagePoints = 10;

	public bool isEnabled = true;

	public GameObject[] projectilePrefabs;

	private GameObject selectedProjecticlePrefab;
	private GameObject currentPrefabObject;
	FireBaseScript currentPrefabScript;
	public GameObject projectileSpawnPoint;

	public PlayerHealth healthScript;

	// Use this for initialization
	void Start () {

		healthScript = GetComponent<PlayerHealth>();

		InitializeProjectile();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer)
			return;
		#if !MOBILE_INPUT
		if (Input.GetButtonDown ("Fire1") && isShooting == false && isEnabled == true) {
			CmdShoot ();
			
		} 
		#else
		if(CrossPlatformInputManager.GetAxisRaw("Mouse X") != 0 || CrossPlatformInputManager.GetAxisRaw("Mouse Y") != 0){
			CmdShoot();
		}
		#endif
	
	}

	[Command]
	public void CmdShoot(){

		if(healthScript.currentHealth <= 0)
		{
			return;
		}
		isShooting = true;
		SpawnProjectile();

		Invoke ("StopShooting", 0.15f);
	}

	void StopShooting(){
		isShooting = false;
	}

	public void DisableShooting(){
		isEnabled = false;
	}

	public void InitializeProjectile()
	{
		//Getting Index, Adding random, only letting it be between 0 and the max projectiles (always be in array length)
		int selected = Random.Range(1, 1000) % projectilePrefabs.Length;
		selectedProjecticlePrefab = projectilePrefabs[selected];
	}


	public void SpawnProjectile()
	{
		currentPrefabObject = GameObject.Instantiate(selectedProjecticlePrefab);
		currentPrefabObject.transform.position = projectileSpawnPoint.transform.position;
		currentPrefabObject.transform.rotation = transform.rotation;
		NetworkServer.Spawn(currentPrefabObject);

		currentPrefabObject.GetComponent<FireProjectileScript>().ownerName = transform.name; 
	}
}
