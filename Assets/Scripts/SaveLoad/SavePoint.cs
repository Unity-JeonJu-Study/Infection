using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public string stageName;
    [ReadOnly] public Vector3 respawnPoint;

    private void Awake() {
        respawnPoint = transform.position + new Vector3(2f, 5f, -2f);
    }
}
