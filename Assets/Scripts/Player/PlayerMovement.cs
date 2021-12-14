
using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Information")]
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpPower;
    public bool isGround;
    public bool canJump;
    public bool isInWater;
    public Movement movement;
    public CinemachineVirtualCamera _virtualCamera;
    [HideInInspector] public AnimalData animalData;
    [HideInInspector] public Animator _animator;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Rigidbody _rigidbody;
    [HideInInspector]public Sensor sensor;
    [HideInInspector]public ConstantForce _constantForce;
    
    private GameObject currentAnimal;
    private PlayerInfect playerInfect;

    private void Start()
    {
        _constantForce = GetComponent<ConstantForce>();
        sensor = GetComponent<Sensor>();
        playerInfect = GetComponent<PlayerInfect>();
        playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        currentAnimal = playerInfect.currentAnimal;
        _animator = currentAnimal.GetComponent<Animator>();
        canJump = true;
        ChangeStatus("Slime");
        movement = new SlimeMovement(this);
    }

    private void FixedUpdate()
    {
        movement.Execute();
    }

    
    public void InitMoveInformation(string animalName)
    {
        animalData = Resources.Load<AnimalData>("Data/Animal/"+animalName);
        movementSpeed = animalData.movementSpeed;
        rotationSpeed = animalData.rotationSpeed;
        jumpPower = animalData.jumpPower;
        sensor.groundRayDistance = animalData.groundRayDistance;
        sensor.interactRayDistance = animalData.interactRayDistance;
        sensor.rayOriginOffset = animalData.rayOriginOffset;
        canJump = true;
    }
    
    public void ChangeStatus(string animalName)
    {
        InitMoveInformation(animalName);
        _animator = playerInfect.Animals[animalName].GetComponent<Animator>();
        animalData = Resources.Load<AnimalData>("Data/Animal/" + animalName);
        ChangeCamera(animalData.fov, animalData.cameraRotation);
        _constantForce.force = Vector3.zero;
        switch (animalName)
        {
            case "Slime":
                movement = new SlimeMovement(this);
                break;
            case "Chick":
                movement = new ChickMovement(this);
                break;
            case "Fish":
                movement = new FishMovement(this);
                break;
        }
    }

    public void ChangeCamera(float fov, Vector3 cameraRotation)
    {
        _virtualCamera.m_Lens.FieldOfView = fov;
        _virtualCamera.transform.rotation = Quaternion.Euler(cameraRotation);
    }
}
