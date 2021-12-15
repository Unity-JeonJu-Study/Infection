using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalData", menuName = "Data/Animal"), InlineEditor]
[ShowOdinSerializedPropertiesInInspector]
public class AnimalData : ScriptableObject
{

    [BoxGroup("Animal Info"),LabelWidth(100)]
    public string animalName;
    [BoxGroup("Animal Info"), LabelWidth(100), TextArea, GUIColor(0.53f, 1f, 0.8f)]
    public string description;

    [VerticalGroup("Data", 75), PreviewField(80), HideLabel]
    public GameObject animalModel;
    [VerticalGroup("Data/Stats"), LabelWidth(150), Range(0f, 100f)][GUIColor(0.3f,0.5f,1f)]
    public float movementSpeed;
    [VerticalGroup("Data/Stats"), LabelWidth(150), Range(0f, 100f)][GUIColor(0.3f,0.5f,1f)]
    public float rotationSpeed;
    [VerticalGroup("Data/Stats"), LabelWidth(150), Range(0f, 200f)][GUIColor(0.5f,1f,0.5f)]
    public float jumpPower;
    [VerticalGroup("Data/Stats"), LabelWidth(150), Range(0f, 100f)][GUIColor(0.5f,0.8f,0.5f)]
    public float groundRayDistance;
    [VerticalGroup("Data/Stats"), LabelWidth(150), Range(0f, 100f)][GUIColor(0.5f,1f,1f)]
    public float interactRayDistance;
    [VerticalGroup("Data/Stats"), LabelWidth(150), Range(0f, 200f)][GUIColor(1f,1f,0.5f)]
    public float fov;
    [VerticalGroup("Data/Stats"), LabelWidth(150)] [GUIColor(0.8f, 1f, 1f)]
    public Vector3 cameraRotation;
    [VerticalGroup("Data/Stats"), LabelWidth(150), Range(0f, 200f)][GUIColor(0.7f,1f,0.5f)]
    public float power;
    [VerticalGroup("Data/Stats"), LabelWidth(150), Range(0f, 200f)][GUIColor(0.2f,1f,0.5f)]
    public float cameraDistance;

}

