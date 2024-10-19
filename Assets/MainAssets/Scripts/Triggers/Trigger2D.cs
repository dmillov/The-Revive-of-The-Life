using UnityEngine;
using UnityEngine.Events;

public class Trigger2D : MonoBehaviour
{
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameConst.PLAYER_TAG))
        {
            onTriggerEnter.Invoke();
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(GameConst.PLAYER_TAG))
        {
            onTriggerExit.Invoke();
        }
    }
}