using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraBounds : MonoBehaviour
{
    // Boolean Values on whether to apply bounds to camera
    [Header("Use Box Bounds")]
    [Space(5)]
    [SerializeField]
    private bool upperBound;
    [SerializeField]
    private bool lowerBound;
    [SerializeField]
    private bool leftBound;
    [SerializeField]
    private bool rightBound;

    // Points outside of box to use as Camera Bound
    [Header("Point Bounds")]
    [SerializeField]
    private float upperPBound;
    [SerializeField]
    private float lowerPBound, leftPBound, rightPBound;

    // Values that holds information on BoxCollider2D
    [Header("Box Values")]
    [SerializeField]
    private Vector2 sizeHalf;
    [SerializeField]
    private Vector2 boxCenter; // Get it from Transform and convert it to Vector2
    
    // Start is called before the first frame update
    void Start()
    {
        // Getting BoxCollider2D height and width halves
        // sizeHalf = SizeBoxCollider * Vector2(gameObject.Scale) / 2; The scale is from transform and sets size of box collider.
        sizeHalf = gameObject.GetComponent<BoxCollider2D>().size * gameObject.GetComponent<BoxCollider2D>().transform.localScale / 2;

        // Getting BoxCollider2D Center
        // Offset use transform as relative point.
        // The Offset goes first then the size comes later when scale is not 1;
        // Then add in the transform of this gameObject to get center of Box.
        // BoxCenter = transform + Box.Offset * transform.scale; to get borders of boxcollider on the scene.
        Vector2 boxOffset = gameObject.GetComponent<BoxCollider2D>().offset * transform.localScale;
        boxCenter = new Vector2(transform.position.x + boxOffset.x, transform.position.y + boxOffset.y);

        Transform[] points = gameObject.GetComponentsInChildren<Transform>();
        if (points != null)
        {
            foreach (Transform bound in points)
            {
                // The Bounds already have transform calculated by distance from object.transform and then multiplied by scale
                // BoundPoint = (ChildTransform + ParentTransform) * scale; already done by Unity
                if (bound.CompareTag("LeftPB"))
                {
                    leftPBound = bound.GetComponent<Transform>().position.x;
                }
                else if (bound.CompareTag("RightPB"))
                {
                    rightPBound = bound.GetComponent<Transform>().position.x;
                }
                else if (bound.CompareTag("UpperPB"))
                {
                    upperPBound = bound.GetComponent<Transform>().position.y;
                }
                else if (bound.CompareTag("LowerPB"))
                {
                    lowerPBound = bound.GetComponent<Transform>().position.y;
                }
            }
        }
    }

    // When player enters the box collider it will set the bounds for the camera based
    // on BoxCollider2D
    // What it inputs to the CameraFollowTarget is the box collider borders
    // If it is false then use the points from PointBound i.e. upperPBound, etc...
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (upperBound) // Upper Bound
            {
                CameraFollowTarget.Instance.SetUpperBound(boxCenter.y + sizeHalf.y);
            }
            else
            {
                CameraFollowTarget.Instance.SetUpperBound(upperPBound);
            }

            if (lowerBound) // Lower Bound
            {
                CameraFollowTarget.Instance.SetLowerBound(boxCenter.y - sizeHalf.y);
            }
            else
            {
                CameraFollowTarget.Instance.SetLowerBound(lowerPBound);
            }

            if (leftBound) // Left Bound
            {
                CameraFollowTarget.Instance.SetLeftBound(boxCenter.x - sizeHalf.x);
            }
            else
            {
                CameraFollowTarget.Instance.SetLeftBound(leftPBound);
            }

            if (rightBound) // Right Bound
            {
                CameraFollowTarget.Instance.SetRightBound(boxCenter.x + sizeHalf.x);
            }
            else
            {
                CameraFollowTarget.Instance.SetRightBound(rightPBound);
            }
        }
    }
}
