using UnityEngine;
using UnityEngine.Events;

public class CoinTrigger : MonoBehaviour
{
    public UnityEvent OnCoinCollected = new UnityEvent();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnCoinCollected?.Invoke();
            Destroy(gameObject);
            // Debug.Log("Coin Collected");
        }
    }
}
