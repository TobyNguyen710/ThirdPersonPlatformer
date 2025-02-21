using UnityEngine;
using UnityEngine.Events;

public class CoinTrigger : MonoBehaviour
{
    public UnityEvent OnCoinCollected = new UnityEvent();
    [SerializeField] private float rotationSpeed = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the coin around the Y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.Self);  //Make the coin rotate around the Y-axis;
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
