using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour
{
	public Camera mainCamera;
	public float zoomedFOV = 30f;
	public float zoomSpeed = 5f;

	private float defaultFOV;
	private bool isZoomed = false;

	private void Start()
	{
		defaultFOV = mainCamera.fieldOfView;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire2"))
		{
			isZoomed = !isZoomed;
			if (isZoomed)
			{
				StartCoroutine(ZoomIn());
			}
			else
			{
				StartCoroutine(ZoomOut());
			}
		}
	}

	IEnumerator ZoomIn()
	{
		while (mainCamera.fieldOfView > zoomedFOV)
		{
			mainCamera.fieldOfView -= zoomSpeed * Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator ZoomOut()
	{
		while (mainCamera.fieldOfView < defaultFOV)
		{
			mainCamera.fieldOfView += zoomSpeed * Time.deltaTime;
			yield return null;
		}
	}
}
