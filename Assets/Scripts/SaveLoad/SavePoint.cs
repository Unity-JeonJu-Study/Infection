using UnityEngine;

// this script is temporary, after the core implementation is done, this script would be altered based on it
// things to change: respawn point

public class SavePoint : MonoBehaviour
{
    public string stageName;
    [ReadOnly] public Vector3 respawnPoint;

    private void Awake() {
        respawnPoint = transform.position + new Vector3(2f, 5f, -2f);
    }
}
