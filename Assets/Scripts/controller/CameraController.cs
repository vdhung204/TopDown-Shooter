using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    private void Update()
    {
        FollowPlayer();
    }
    public void FollowPlayer()
    {
        vcam.Follow = PlayerController.instance.transform;
    }

}
