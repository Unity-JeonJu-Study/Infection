using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PlayerMovement>().isInWater = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PlayerMovement>().isInWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<PlayerMovement>().isInWater = false;
        }
    }
}
