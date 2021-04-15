using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Invoke("DelayFall", 1f);
		}
	}

	private void DelayFall()
	{
		transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
	}
}
