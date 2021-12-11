using UnityEngine;

public class Sensor : MonoBehaviour
{
    public float rayRadius;
    public float groundNormal;
    public float groundDistance;
    public float groundRayDistance;
    public float interactRayDistance;
    public Vector3 rayOriginOffset;
    private Vector3 rayOriginPosition;
    private Vector3 extents;
    public RaycastHit hit;
    
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
        rayOriginPosition =  meshRenderer.bounds.center;
        extents = meshRenderer.bounds.extents;
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    
    private void OnDrawGizmos()
    {
        if (CheckForward())
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(rayOriginPosition, transform.forward * hit.distance );

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(rayOriginPosition + transform.forward * hit.distance , extents * 2);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayOriginPosition, transform.forward * hit.distance );

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(rayOriginPosition + transform.forward * hit.distance , extents * 2);
        }
    }

    public bool CheckForward()
    {
        bool cast = Physics.BoxCast(rayOriginPosition, extents, transform.forward,
                                    out hit,
                                    Quaternion.identity, interactRayDistance);

        return cast;
    }

    public void CheckGround()
    {
        if (Physics.SphereCast(rayOriginPosition, 
            rayRadius,
            Vector3.down,
            out hit,
            groundRayDistance))
        {
            playerMovement.isGround = true;
        }
        else
        {
            playerMovement.isGround = false;
        }
    }
}
