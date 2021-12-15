using UnityEngine;

public class Sensor : MonoBehaviour
{
    public bool cast;
    public float rayRadius;
    public float groundRayDistance;
    public float interactRayDistance;
    public Vector3 rayOriginPosition;
    public Vector3 extents;
    public RaycastHit hitForward;
    public RaycastHit hitGround;
    
    private PlayerMovement playerMovement;
    private SkinnedMeshRenderer meshRenderer;

    public SkinnedMeshRenderer MeshRenderer
    {
        get => meshRenderer;
        set => meshRenderer = value;
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        extents = meshRenderer.bounds.extents / 2;
        rayRadius = extents.y / 2.0f;
    }
    
    private void OnDrawGizmos()
    {
        rayOriginPosition = meshRenderer.bounds.center;
        if (CheckForward())
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(rayOriginPosition, transform.forward * hitForward.distance * interactRayDistance);

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(rayOriginPosition + transform.forward * hitForward.distance * interactRayDistance, extents);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayOriginPosition, transform.forward * interactRayDistance);

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(rayOriginPosition + transform.forward * interactRayDistance, extents);
        }

        if (playerMovement.isGround)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(rayOriginPosition +  Vector3.down * hitGround.distance, rayRadius);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(rayOriginPosition + Vector3.down * hitGround.distance, rayRadius);
        }
    }

    public bool CheckForward()
    {
        rayOriginPosition = meshRenderer.bounds.center;
        
        cast = Physics.BoxCast(rayOriginPosition, extents, transform.forward,
                                    out hitForward,
                                    Quaternion.identity, interactRayDistance);

        return cast;
    }

    public void CheckGround()
    {
        rayOriginPosition = meshRenderer.bounds.center;
        cast = Physics.SphereCast(rayOriginPosition,
                                rayRadius,
                                Vector3.down,
                                out hitGround,
                                groundRayDistance);
        playerMovement.isGround = cast;
    }
    
}
