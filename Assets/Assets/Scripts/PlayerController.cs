using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	public override void OnStartLocalPlayer()
	{	
		GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
	}

	void Update () {
		if (!isLocalPlayer)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CmdFire();
		}
		var xMovement = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		var zMovement = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;
		transform.Rotate (0, xMovement,0);
		transform.Translate (0, 0, zMovement);
	}

	[Command]
	void CmdFire() {
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate (
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 100.0f;

		NetworkServer.Spawn(bullet);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

}

