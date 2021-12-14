using System.Collections.Generic;
using UnityEngine;

public class PlayerInfect : MonoBehaviour
{
    [Header("Infection Information")] 
    public GameObject currentAnimal;
    public GameObject infectAnimal;

    private Sensor sensor;
    private Transform parentTransform;
    private Vector3 currentPosition;
    private PlayerMovement playerMovement;
    private PlayerInput playerInput;
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
        sensor = GetComponent<Sensor>();
    }

    private void InitAnimals()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject animal = transform.GetChild(i).gameObject;
            animals.Add(animal.name, animal);
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
        
        if (sensor.CheckForward() && sensor.hitForward.collider.gameObject.layer == LayerMask.NameToLayer("Animal"))
        {
            currentAnimal.SetActive(false);
            
            infectAnimal = sensor.hitForward.collider.gameObject;
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
        currentPosition = parentTransform.position;
        parentTransform.position = new Vector3(currentPosition.x, sensor.MeshRenderer.bounds.extents.y + currentPosition.y, currentPosition.z);
        
        currentAnimal = animals["Slime"];
        currentAnimal.SetActive(true);

        ChangeInitSensor();

        playerMovement.ChangeStatus(currentAnimal.name);
    }

    private void ChangeToInfectAnimal()
    {
        currentAnimal = animals[infectAnimal.name.Split(' ')[0]];
        currentAnimal.SetActive(true);
        currentAnimal.transform.parent.position = infectAnimal.transform.position;
        ChangeInitSensor();
        playerMovement.ChangeStatus(currentAnimal.name);
        
    }

    private void ChangeInitSensor()
    {
        sensor.MeshRenderer = currentAnimal.GetComponentInChildren<SkinnedMeshRenderer>();
        sensor.extents = sensor.MeshRenderer.bounds.extents / 2;
        sensor.rayRadius = sensor.extents.y / 2.0f;
    }
}
