using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpawnEnemyScript : NetworkBehaviour {

	public GameObject objectToSpawn;
	public float timeToWaitBetweenSpawns = 2.0f;
	public float timer = 0;

	GameObject[] players;

	bool gameStarted = false;

	
	// Update is called once per frame
	void Update () {
		if (!gameStarted)
			return;
		
		timer += Time.deltaTime;
		if (timer > timeToWaitBetweenSpawns) {
			CmdSpawnEnemy();
			timer = 0;
		}
	}

	[Command]
	void CmdSpawnEnemy()
	{

		GameObject enemy = (GameObject) Instantiate(objectToSpawn, transform.position, transform.rotation);
		players = GameObject.FindGameObjectsWithTag("Player");
		int index = Random.Range(0, 1000) % players.Length;
		GameObject playersToAttack = players[index];
		enemy.GetComponent<EnemyMovement>().player = playersToAttack;
		enemy.GetComponent<EnemyAttack>().player = playersToAttack;
		NetworkServer.Spawn(enemy);

	}
	public override void OnStartServer()
	{
		timer = 0;
		gameStarted = true;
	}
}
