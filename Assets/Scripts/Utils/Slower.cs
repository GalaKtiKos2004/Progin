using UnityEngine;

public class Slower : MonoBehaviour
{
    [SerializeField] private float _speedMultiplier = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.SetSpeed(1 / _speedMultiplier);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMover playerMover))
        {
            playerMover.SetSpeed(_speedMultiplier);
        }
    }
}
