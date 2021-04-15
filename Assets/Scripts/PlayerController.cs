using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	public Rigidbody rb;
	public TextMeshProUGUI startTap;
	private bool isStopped = false;
	public bool isRotating = false;
	private bool isGrounded = true;
	public bool isEnded = false;
	private bool isJumped = false;

	private float startPos;
	private float startPosY;
	private float endPos;
	private float endPosY;
	private float swipeDelta;
	private float swipeDeltaY;

	public float jumpForce;
    public float speed;

	private Vector3 left = new Vector3(0, -90, 0);
	private Vector3 right = new Vector3(0, 0, 0);

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		isStopped = true;
		isRotating = true;
		isEnded = false;
	}

	private void Update()
	{
		ChangeDirection();
		Move();
	}

	private void ChangeDirection()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isStopped = false;
			startTap.transform.DOScale(0, 1.5f);
			startTap.transform.DOMoveY(-800,4f);
			Invoke("StartTapActive", 1.1f);
			startPos = Input.mousePosition.x;
			startPosY = Input.mousePosition.y;
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			endPos = Input.mousePosition.x;
			endPosY = Input.mousePosition.y;
			swipeDelta = endPos - startPos;
			swipeDeltaY = endPosY - startPosY;

			if (isRotating == true)
			{
				if (Mathf.Abs(swipeDelta) > Screen.width * 0.05f && isJumped == false) // Dead Zone
				{
					if (swipeDelta < 0) //left
					{
						gameObject.transform.DORotate(left, 0.3f);
					}

					else //right
					{
						gameObject.transform.DORotate(right, 0.3f);
					}
				}

				if (Mathf.Abs(swipeDeltaY) > Screen.height * 0.05f) // Dead Zone
				{
					if (swipeDeltaY > 0 && isGrounded == true) //Jump
					{
						isJumped = true;
						rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
						isGrounded = false;
						Invoke("GroundedTrue", 1.2f);
					}
					else
					{
						return;
					}
				}
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{	
		if (other.tag == "Respawn") 
		{
			isStopped = true;
			isRotating = false;
			SceneManager.LoadScene(0);
		}
	}

	private void Move() // Küpe hareket kazandırır
	{
		if (isEnded == false)
		{
			if (isStopped == false)
			{
				transform.Translate(Vector3.forward * speed * Time.deltaTime);
				speed += 0.15f * Time.deltaTime;
			}
		}		
	}

	private void GroundedTrue()
	{
		isGrounded = true;
		isJumped = false;
	}

	private void StartTapActive()
	{
		startTap.gameObject.SetActive(false);
	}
}
 