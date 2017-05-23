using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movechopstick1 : MonoBehaviour {
	public float speed = 4f;
	private Rigidbody2D rigid1;
	public bool grabbed=false;
	RaycastHit2D hit;
	public float distance=0.45f;
	public Transform holdpoint;
	public float throwforce;

	public LayerMask notgrabbed;
	// Use this for initialization
	void Start () {
		rigid1 = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis ("Horizontal");
		float moveup = Input.GetAxis ("Vertical");
		if (move != 0)
			rigid1.velocity = new Vector2 (move * speed, rigid1.velocity.y);
		if (moveup != 0)
			rigid1.velocity = new Vector2 (rigid1.velocity.x, moveup * speed);
		if (move==0 && moveup==0)
			rigid1.velocity = new Vector2 (0, 0);
		if (Input.GetKeyDown (KeyCode.Space)) {
			transform.rotation = Quaternion.Euler (20, 20, 0);
			Vector3 vec2 = new Vector3 (-2.45f, -2.25f, 0f);
			Vector3 vec3 = new Vector3 (2f, 1.8f, 0f);
			Physics2D.queriesStartInColliders = false;

			hit =	Physics2D.Raycast (transform.position - vec3, vec2, distance);

			if (hit.collider != null && hit.collider.tag == "grabbable") {
				Debug.Log ("hit");
				grabbed = true;

			}
		} else if (Input.GetKeyUp (KeyCode.Space)) {
			transform.rotation = Quaternion.identity;
			if(!Physics2D.OverlapPoint(holdpoint.position,notgrabbed))
			{
				grabbed=false;

				if(hit.collider.gameObject.GetComponent<Rigidbody2D>()!=null)
				{

					hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity= new Vector2(transform.localScale.x,1)*throwforce;
				}


				//throw
			}
		}
		if(Input.GetKeyDown(KeyCode.B))
		{
			if(!grabbed)
			{
				Vector3 vec2 = new Vector3 (-2.45f, -2.25f, 0f);
				Vector3 vec3 = new Vector3 (2f, 1.8f, 0f);
				Physics2D.queriesStartInColliders=false;

				hit =	Physics2D.Raycast(transform.position-vec3,vec2,distance);

				if(hit.collider!=null && hit.collider.tag=="grabbable")
				{
					Debug.Log ("hit");
					grabbed=true;

				}


				//grab
			}else if(!Physics2D.OverlapPoint(holdpoint.position,notgrabbed))
			{
				grabbed=false;

				if(hit.collider.gameObject.GetComponent<Rigidbody2D>()!=null)
				{

					hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity= new Vector2(transform.localScale.x,1)*throwforce;
				}


				//throw
			}


		}

		if (grabbed)
			hit.collider.gameObject.transform.position = holdpoint.position;
		
	}
	void OnDrawGizmos()
	{
		//Vector3 vec2 = new Vector3 (1f, 0f, 0f);
		Vector3 vec3 = new Vector3 (2f, 1.8f, 0f);
		Vector3 vec4 = new Vector3 (0.45f, 0.45f, 0f);
		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position-vec3,transform.position-vec3-vec4);
	}
}
