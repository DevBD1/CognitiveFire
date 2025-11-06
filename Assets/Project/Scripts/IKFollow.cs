
using UnityEngine;

public class IKFollow : MonoBehaviour
{
    [Tooltip("The transform that the script will follow.")]
    [SerializeField] private Transform targetToFollow;

    [Tooltip("The transform that will be moved to follow the target.")]
    [SerializeField] private Transform follower;

    // Using LateUpdate to ensure this runs after the animation has been processed for the frame.
    void LateUpdate()
    {
        if (targetToFollow != null && follower != null)
        {
            follower.position = targetToFollow.position;
            follower.rotation = targetToFollow.rotation;
        }
    }
}
