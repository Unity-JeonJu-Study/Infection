using UnityEngine;

public class Sensor : MonoBehaviour
{
    public bool cast;
    public float rayRadius;
    public float groundNormal;
    public float groundDistance;
    public float groundRayDistance;
    public float interactRayDistance;
    public Vector3 rayOriginOffset;
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
        extents = meshRenderer.bounds.extents;
        rayRadius = extents.y / 2.0f;
    }
    
    private void OnDrawGizmos()
    {
        rayOriginPosition = meshRenderer.bounds.center;
        if (CheckForward())
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(rayOriginPosition, transform.forward * hitForward.distance );

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(rayOriginPosition + transform.forward * hitForward.distance , extents);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayOriginPosition, transform.forward);

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(rayOriginPosition + transform.forward, extents);
        }

        if (playerMovement.isGround)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position - rayOriginOffset +  Vector3.down * hitGround.distance, rayRadius);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position - rayOriginOffset + Vector3.down, rayRadius);
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
        cast = Physics.SphereCast(transform.position - rayOriginOffset,
                                    extents.y / 2.0f,
                                    Vector3.down,
                                    out hitGround,
                                    groundRayDistance);
        playerMovement.isGround = cast;
    }
}
