
using UnityEngine;

public class SlimeResueTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InGameUIManager.instance.AddOneRescuedSlimeSlot();
            Destroy(gameObject);
            SoundManager.Instance.PlaySound("ItemGet");
        }
    }
}
