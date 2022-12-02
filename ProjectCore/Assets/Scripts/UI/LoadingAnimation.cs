using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private float animationSpeed = 5f;
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.back * animationSpeed * Time.fixedDeltaTime, Space.World);
    }
}
