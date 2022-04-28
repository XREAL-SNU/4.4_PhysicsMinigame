using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamManager : MonoBehaviour
{
	private GameObject player;
	private GameObject cameraAim;
	private CinemachineFreeLook thirdPersonCam;
	private void Start()
	{
		player = GameObject.FindWithTag("Player").gameObject;
		cameraAim = GameObject.Find("CameraAim").gameObject;
		
		thirdPersonCam = GameObject.Find("ThirdPersonCam").GetComponent<CinemachineFreeLook>();
		thirdPersonCam.Follow = player.transform;
		thirdPersonCam.LookAt = cameraAim.transform;
	}
}