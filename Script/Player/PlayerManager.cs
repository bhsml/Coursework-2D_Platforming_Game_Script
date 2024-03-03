using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // When player starts assign stuff.
    private void Awake()
    {
        CameraFollowTarget.Instance.NewTarget(transform);
    }
}
