using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target; // Assign your player here
    
    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0, 10, -8);
    public float followSpeed = 2f;
    public float rotationSpeed = 2f;
    
    [Header("Look Settings")]
    public bool lookAtTarget = true;
    public Vector3 lookOffset = Vector3.zero;
    
    void Start()
    {
        // If no target assigned, try to find player
        if (target == null)
        {
            PlayerController player = FindAnyObjectByType<PlayerController>();
            if (player != null)
                target = player.transform;
        }
        
        // Set initial position
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
    
    void LateUpdate()
    {
        if (target == null) return;
        
        // Follow target position
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        
        // Look at target
        if (lookAtTarget)
        {
            Vector3 lookAtPosition = target.position + lookOffset;
            Vector3 direction = lookAtPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    // Optional: Draw gizmos to visualize camera setup
    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawWireSphere(target.position + lookOffset, 0.5f);
        }
    }
}