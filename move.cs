using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	public float speed = 4f;
	private Rigidbody2D rigid;

	public bool grabbed=false;
	RaycastHit2D hit;
	public float distance=0.15f;
	public Transform holdpoint;
	public float throwforce;

	public LayerMask notgrabbed;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		float move = Input.GetAxis ("Horizontal");
		float moveup = Input.GetAxis ("Vertical");
		if (move != 0)
			rigid.velocity = new Vector2 (move * speed, rigid.velocity.y);
		if (moveup != 0)
			rigid.velocity = new Vector2 (rigid.velocity.x, moveup * speed);
		if (move==0 && moveup==0)
			rigid.velocity = new Vector2 (0, 0);
		if(Input.GetKeyDown(KeyCode.B))
		{
			if(!grabbed)
			{
				Vector3 vec2 = new Vector3 (-0.15f, -0.15f, 0f);
				Vector3 vec3 = new Vector3 (1f, 0.8f, 0f);
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
		Vector3 vec3 = new Vector3 (1f, 0.8f, 0f);
		Vector3 vec4 = new Vector3 (0.15f, 0.15f, 0f);
		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position-vec3,transform.position-vec3-vec4);
	}
}
