
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
    public CameraFollow cameraFollow;
    [HideInInspector] public Animator _animator;
    [HideInInspector] public PlayerInput playerInput;
    [HideInInspector] public Rigidbody _rigidbody;
    [HideInInspector] public Sensor sensor;
    [HideInInspector] public ConstantForce _constantForce;

    private Player player;
    private GameObject currentAnimal;
    private PlayerInfect playerInfect;
    
    private void Start()
    {
        cameraFollow = FindObjectOfType<CameraFollow>().GetComponent<CameraFollow>();
        player = GetComponent<Player>();
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
    
    
    public void ChangeStatus(string animalName)
    {
        player.InitMoveInformation(animalName);
        _animator = playerInfect.Animals[animalName].GetComponent<Animator>();
        player.animalData = Resources.Load<AnimalData>("Data/Animal/" + animalName);
        ChangeCamera(player.animalData.cameraHeight, player.animalData.cameraDistance);
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

    public void ChangeCamera(float height, float distance)
    {
        cameraFollow.Offset = new Vector3(0, height, -distance);
    }
}
