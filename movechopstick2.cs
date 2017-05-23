using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movechopstick2 : MonoBehaviour {
	public float speed = 4f;
	private Rigidbody2D rigid2;
	// Use this for initialization
	void Start () {
		rigid2 = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis ("Horizontal");
		float moveup = Input.GetAxis ("Vertical");
		if (move != 0)
			rigid2.velocity = new Vector2 (move * speed, rigid2.velocity.y);
		if (moveup != 0)
			rigid2.velocity = new Vector2 (rigid2.velocity.x, moveup * speed);
		if (move==0 && moveup==0)
			rigid2.velocity = new Vector2 (0, 0);
		if (Input.GetKeyDown(KeyCode.Space))
			transform.rotation = Quaternion.Euler(-20, 20, 0);
		else if (Input.GetKeyUp(KeyCode.Space))
			transform.rotation = Quaternion.identity;
	}
}
