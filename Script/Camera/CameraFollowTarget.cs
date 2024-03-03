using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : Singleton<CameraFollowTarget>
{
    // Camera pointing at target
    [SerializeField]
    private Transform target; // What camera is following
    [SerializeField]
    private Vector3 targetCamPos; // Position of the camera following target
    [SerializeField]
    private float smoothing; // smooths camera following target

    // Bounds where the camera should not show.
    // These are manually set.
    // Operations using it happens only once.
    // With the use of BoundBoxCamera, these are useless.
    [Header("Manually Set Bounds")]
    [SerializeField]
    private float lowerBound;
    [SerializeField]
    private float leftBound;
    [SerializeField]
    private float rightBound;
    [SerializeField]
    private float upperBound;

    //Leaves buffer zone where camera does not move to follow target.
    [Header("Buffer Zone Values")]
    [SerializeField] private float bufferY;
    [SerializeField] private float bufferX;

    // Holds half of height and width of camera.
    //[SerializeField]
    private float camWidthHalf, camHeightHalf;
    /*
     * These are bounds for center of camera.
     * So camera does not stop at the bounds of map
     * but rather at the edges of the camera.
     */
    //[SerializeField]
    private float camXLB, camXRB, camYUB, camYLB;
    
    // Start is called before the first frame update
    void Start()
    {
        targetCamPos = transform.position; // Need it because the Z value is assigned.
        bufferY *= target.localScale.y;
        bufferX *= target.localScale.x;
        /*
         * Used to find edges for the camera that should
         * not show things outside of map.
         */
        // Assuming that Half of Camera Height is always equal to value 1; If not use Camera.main.orthographicsize;
        camHeightHalf = Mathf.Abs(transform.position.z) * Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2);
        // Camera aspect is width/height. Width is also half.
        camWidthHalf = Camera.main.aspect * camHeightHalf;
        camXLB = leftBound + camWidthHalf;
        camXRB = rightBound - camWidthHalf;
        camYUB = upperBound - camHeightHalf;
        camYLB = lowerBound + camHeightHalf;

        // Depending on field of view the smoothing will slow down.
        /// Probably will remove if testing it not good
        smoothing = smoothing / (Camera.main.fieldOfView / 10);
    }

    // Uses FixedUpdate because body is a physics object.
    // When not using FixedUpdate the camera would leave after images as it follows target.
    /* FixedUpdate:
     * Called just before performing any physics calculations
     * Physics code go to ensure they all work in sync
     */
    void FixedUpdate()
    {
        UpdateTargetCamPos();
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

    }

    /*********************************************/
    /*********************************************/
    // Functions

    // Updates Target Camera Position and make sure that the camera does not go outside of map.
    void UpdateTargetCamPos()
    {
        targetCamPos.x = Mathf.Clamp(TargetXFollow(), camXLB, camXRB);
        targetCamPos.y = Mathf.Clamp(TargetYFollow(), camYLB, camYUB);
    }

    /*
     * If target movings outside of buffer zone then the camera will
     * be dragged by target by adding or subtracting the buffer length.
     */
    float TargetYFollow()
    {
        // if target position is less than camera - buffer then add buffer to target position. So drag camera behind target.
        if (target.transform.position.y < transform.position.y - bufferY)
        {
            return target.transform.position.y + bufferY;
        }
        // if target position is greater than camera + buffer then subtract buffer to target position. So drag camera behind target.
        else if (target.transform.position.y > transform.position.y + bufferY)
        {
            return target.transform.position.y - bufferY;
        }

        return transform.position.y;
    }

    /*
     * If target movings outside of buffer zone then the camera will
     * be dragged by target by adding or subtracting the buffer length.
     */
    float TargetXFollow()
    {
        // if target position is less than camera - buffer then add buffer to target position. So drag camera behind target.
        if (target.transform.position.x < transform.position.x - bufferX)
        {
            return target.transform.position.x + bufferX;
        }
        // if target position is greater than camera + buffer then subtract buffer to target position. So drag camera behind target.
        else if (target.transform.position.x > transform.position.x + bufferX)
        {
            return target.transform.position.x - bufferX;
        }

        return transform.position.x;
    }

    // Set new Target to follow
    public void NewTarget(Transform newTarget)
    {
        target = newTarget;
    }
    
    // These all change the center of camera so that it doesn't go beyond border;
    // border = CenterOfCamera + CameraSideLengthFromCenter;
    public void SetLowerBound(float y)
    {
        camYLB = y + camHeightHalf;
    }

    public void SetUpperBound(float y)
    {
        camYUB = y - camHeightHalf;
    }

    public void SetLeftBound(float x)
    {
        camXLB = x + camWidthHalf; ;
    }

    public void SetRightBound(float x)
    {
        camXRB = x - camWidthHalf;
    }
}
