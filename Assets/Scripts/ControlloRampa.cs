using UnityEngine;

public class ControlloRampa : MonoBehaviour
{
    [Header("Riferimenti")]
    public Transform baseRampa; // per rotazione orizzontale
    public Transform rampa;     // per rotazione verticale

    [Header("Angoli massimi")]
    public float maxOrizzontale = 45f;
    public float maxVerticale = 60f;

    private float orizzontaleAttuale = 0f;
    private float verticaleAttuale = 0f;

    // --- Chiamati dagli slider (OnValueChanged) ---
    public void SetOrizzontale(float valore)
    {
        orizzontaleAttuale = Mathf.Lerp(-maxOrizzontale, maxOrizzontale, valore);
        baseRampa.localRotation = Quaternion.Euler(0f, orizzontaleAttuale, 0f);
    }

    public void SetVerticale(float valore)
    {
        verticaleAttuale = Mathf.Lerp(0f, maxVerticale, valore);
        rampa.localRotation = Quaternion.Euler(-verticaleAttuale, 0f, 0f);
    }
}
