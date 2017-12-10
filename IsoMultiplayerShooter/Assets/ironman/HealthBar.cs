using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public GameObject Player;
	private int maxHealth;

	public Color minColor = Color.red;
	public Color maxColor = Color.blue;

	private SpriteRenderer rend;

	public float initalLength = 0.2f;
	public float currentLength = 0.2f;
	
	// Use this for initialization

	void Start () {
		maxHealth = Player.GetComponent<PlayerHealth>().startingHealth;
		rend = GetComponent<SpriteRenderer>();


	}
	
	// Update is called once per frame
	void Update () {
		float fraction = (float) Player.GetComponent<PlayerHealth>().currentHealth / maxHealth;

		//Handle color of the healthbar
		rend.color = Color.Lerp(minColor, maxColor, Mathf.Lerp(0, 1, 
			Player.GetComponent<PlayerHealth>().currentHealth / maxHealth));

		// handle the size (length)
		transform.localScale = new Vector3(initalLength * fraction,
			transform.localScale.y,
			transform.localScale.z);

		//Facing Camera, Always
		transform.LookAt(Camera.main.transform);
	}
}
