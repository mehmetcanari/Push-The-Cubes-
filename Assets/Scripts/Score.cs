using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
	PlayerController playerController;
	public TextMeshProUGUI scoreUI;
	public Text levelText;
	public GameObject scoreObject;
	public ParticleSystem portalEntry;
	public ParticleSystem destroyParticle;
	public Image levelCompleted;
	
	private float score;
	private bool isClicked = false;

	public ParticleSystem endParticle1;
	public ParticleSystem endParticle2;
	public ParticleSystem startParticle1;
	public ParticleSystem startParticle2;
	private Vector3 scoreScale = new Vector3(2.5f, 2.5f, 2.5f);

	private void Start()
	{
		playerController = GetComponent<PlayerController>();
		portalEntry.Stop();
	}

	private void Update()
	{
		if (isClicked == false)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Invoke("StartParticleInvoker", 0);
				isClicked = true;
				scoreObject.transform.DOScale(scoreScale, 1f);
				levelText.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Collectible")
		{
			other.gameObject.SetActive(false);
			Instantiate(destroyParticle, other.transform.position, Quaternion.identity);
			score++;
			scoreUI.text = "" + score;
		}

		if (other.tag == "Finish")
		{
			playerController.isRotating = false;
			playerController.isEnded = true;
			levelCompleted.transform.DOScale(5f, 1f);
			InvokeRepeating("EndParticleInvoker", 0, 1f);
			Invoke("NextLevel", 2f);
			portalEntry.Play();
			Invoke("EntryPortal", 1f);
			
		}

		if (other.tag == "Finish2")
		{
			playerController.isRotating = false;
			playerController.isEnded = true;
			levelCompleted.transform.DOScale(5f, 1f);
			InvokeRepeating("EndParticleInvoker", 0, 1f);
			Invoke("PreviousLevel", 2f);
			
		}
	}

	#region Methods
	private void EndParticleInvoker()
	{
		endParticle1.Play();
		endParticle2.Play();
	}

	private void StartParticleInvoker()
	{
		startParticle1.Play();
		startParticle2.Play();
	}

	private void NextLevel()
	{
		SceneManager.LoadScene(1);
	}

	private void PreviousLevel()
	{
		SceneManager.LoadScene(0);
	}

	private void EntryPortal()
	{
		gameObject.transform.DOMoveZ(220, 1f);
	}
	#endregion
}
