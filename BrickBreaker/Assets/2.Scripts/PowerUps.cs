using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("BotCol"))
        {
            Destroy(this.gameObject);
        }
    }
}
