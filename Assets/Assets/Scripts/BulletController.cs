using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public int bulletDamage;

	void OnCollisionEnter(Collision other)
	{	
		if(other.gameObject.CompareTag("Player")) {
			var player = other.gameObject;
			var health = player.GetComponentInParent<HealthController>();
			if(health != null){
				health.TakeDamage(bulletDamage);
			}
			Destroy (gameObject);
		}	
	}
}