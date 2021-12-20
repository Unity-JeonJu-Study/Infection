
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
    public Transform cameraTransform;
    public CinemachineVirtualCamera _virtualCamera;
    [HideInInspector] public PlayerKinematics playerKinematics;
    [HideInInspector] public AnimalData animalData;
    [HideInInspector] public Animator _animator;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Rigidbody _rigidbody;
    [HideInInspector] public Sensor sensor;
    [HideInInspector] public ConstantForce _constantForce;
    
    private GameObject currentAnimal;
    private PlayerInfect playerInfect;
    // private CinemachineFramingTransposer framingTransposer;
    
    private void Start()
    {
        _constantForce = GetComponent<ConstantForce>();
        sensor = GetComponent<Sensor>();
        playerKinematics = GetComponent<PlayerKinematics>();
        playerInfect = GetComponent<PlayerInfect>();
        playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        currentAnimal = playerInfect.currentAnimal;
        _animator = currentAnimal.GetComponent<Animator>();
        // framingTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
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
        playerKinematics.power = animalData.power;
        
        canJump = true;
    }
    
    public void ChangeStatus(string animalName)
    {
        InitMoveInformation(animalName);
        _animator = playerInfect.Animals[animalName].GetComponent<Animator>();
        animalData = Resources.Load<AnimalData>("Data/Animal/" + animalName);
        ChangeCamera(animalData.fov, animalData.cameraRotation, animalData.cameraDistance);
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
            case "Bear":
                movement = new BearMovement(this);
                break;
        }
    }

    public void ChangeCamera(float fov, Vector3 cameraRotation, float cameraDistance)
    {
        _virtualCamera.m_Lens.FieldOfView = fov;
        _virtualCamera.transform.rotation = Quaternion.Euler(cameraRotation);
        // framingTransposer.m_CameraDistance = cameraDistance;

    }
}
