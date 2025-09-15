using UnityEngine;

public class GravitaPersonalizzata : MonoBehaviour
{
    private float g;          // valore della gravità
    private Rigidbody rb;

    public void Inizializza(float gravita)
    {
        g = gravita;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Applica accelerazione verso il basso (asse Y negativo)
        rb.AddForce(Vector3.down * g * rb.mass, ForceMode.Force);
    }
}
