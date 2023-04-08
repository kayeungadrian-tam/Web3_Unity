using UnityEngine;

public class PlayerGhost : MonoBehaviour
{
    public Transform target;    // the object to follow
    public float followDelay;   // the delay in seconds


    public float followSpeed = 2f;
    private Vector3 targetPos;  // the current position of the target object
    private Quaternion targetRot; 
    

    private void Update()
    {
        // calculate the new position to move towards
        targetPos = target.position; // - (target.forward * followDelay);
        targetRot = Quaternion.LookRotation(-target.forward, Vector3.up); // rotate to face the target's back

        // smoothly move towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation,   targetRot, followSpeed * Time.deltaTime);
    }
}