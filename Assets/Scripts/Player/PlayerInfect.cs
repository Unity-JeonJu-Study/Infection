using System.Collections.Generic;
using UnityEngine;

public class PlayerInfect : MonoBehaviour
{
    [Header("Infection Information")] 
    public float interactRayDistance;
    public GameObject currentAnimal;
    public GameObject infectAnimal;

    private Transform parentTransform;
    private Vector3 currentPosition;
    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
    private Vector3 rayOriginPosition;
    private SkinnedMeshRenderer meshRenderer;
    private Dictionary<string, GameObject> animals;

    public Dictionary<string, GameObject> Animals
    {
        get => animals;
        set => animals = value;
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        animals = new Dictionary<string, GameObject>();
        InitAnimals();
        currentAnimal = animals["Slime"];
        meshRenderer = currentAnimal.GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void InitAnimals()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject animal = transform.GetChild(i).gameObject;
            animals.Add(animal.name, animal);
        }
    }

    private void OnDrawGizmos()
    {
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        Debug.Log(meshRenderer);
        rayOriginPosition =  meshRenderer.bounds.center;
        if (Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward * 0.8f, out RaycastHit hit,
            transform.rotation, interactRayDistance))
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(rayOriginPosition, transform.forward );

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(rayOriginPosition + transform.forward , transform.lossyScale / 2.0f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayOriginPosition, transform.forward);

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(rayOriginPosition + transform.forward, transform.lossyScale / 2.0f);
        }
    }

    private void Update()
    {
        Infect();
        Cure();
    }

    private void Infect()
    {
        if (!playerInput.InfectKeyPressed)
            return;
        
        if (Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward * 0.8f, out RaycastHit hit,
            transform.rotation, interactRayDistance, LayerMask.GetMask("Animal")))
        {
            currentAnimal.SetActive(false);
            
            infectAnimal = hit.collider.gameObject;
            infectAnimal.SetActive(false);

            ChangeToInfectAnimal();
        }
    }

    private void Cure()
    {
        if (!playerInput.CureKeyPressed || currentAnimal.name.Equals("Slime"))
            return;
        infectAnimal.transform.position = currentAnimal.transform.position;
        currentAnimal.SetActive(false);
        
        ChangeToSlime();
    
        infectAnimal.SetActive(true);
    }

    private void ChangeToSlime()
    {
        parentTransform = currentAnimal.transform.parent;
        
        currentAnimal = animals["Slime"];
        currentAnimal.SetActive(true);
        currentPosition = parentTransform.position;
        parentTransform.position = new Vector3(currentPosition.x, currentPosition.y + 5f, currentPosition.z);
        
        playerMovement.ChangeStatus(currentAnimal.name);
    }

    private void ChangeToInfectAnimal()
    {
        currentAnimal = animals[infectAnimal.name.Split(' ')[0]];
        currentAnimal.SetActive(true);
        currentAnimal.transform.parent.position = infectAnimal.transform.position;
            
        playerMovement.ChangeStatus(currentAnimal.name);
        meshRenderer = currentAnimal.GetComponentInChildren<SkinnedMeshRenderer>();
    }
}
