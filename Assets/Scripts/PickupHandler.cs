using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform targetObjectTransform;
    private bool isFollowing = false;


    private void LateUpdate()
    {
        if (isFollowing)
        {
            targetObjectTransform.position = handTransform.position;
        }
    }

    public void toggleFollowing()
    {
        isFollowing = !isFollowing;
    }
}
