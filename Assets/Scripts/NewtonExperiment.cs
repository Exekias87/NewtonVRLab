using UnityEngine;
using TMPro;

public class NewtonExperiment : MonoBehaviour
{
    public Rigidbody oggetto;         
    public float forza = 10f;         
    public float massa = 1f;          
    public Vector3 direzione = new Vector3(0, 0, 1); 
    public TextMeshProUGUI outputAccelerazione;
    public TextMeshProUGUI indicatoreMassa;
    public TextMeshProUGUI indicatoreForza;
    public LineRenderer frecciaForza;
    public float scalaVisualizzazione = 5f; 
    private Vector3 posizioneIniziale;



    public Transform tavolo; // assegna qui il riferimento al tavolo

    private bool forzaApplicata = false;

    void Start()
    {
        oggetto.mass = massa;
        posizioneIniziale = oggetto.transform.position;
        forzaApplicata = false;
    }

   // Imposta la forza dallo slider
    public void ImpostaForza(float nuovoValore)
    {
    forza = nuovoValore;
    if (indicatoreForza != null)
        indicatoreForza.text = $"{forza:F1} N";  // un decimale
    }

   // Imposta la massa dallo slider
   public void ImpostaMassa(float valoreSlider)
   {
    massa = valoreSlider;
    oggetto.mass = massa;

    if (indicatoreMassa != null)
        indicatoreMassa.text = $"{massa:F1} Kg"; // un decimale
   }

    // Applica la forza e visualizza accelerazione + freccia
    public void ApplicaForza()
    {
        if (!forzaApplicata)
        {
            oggetto.linearVelocity = Vector3.zero;       // Reset velocità
            oggetto.angularVelocity = Vector3.zero;

            // usa la direzione "forward" del tavolo
            if (tavolo != null)
                oggetto.AddForce(tavolo.transform.up * forza, ForceMode.Force);
            else
                oggetto.AddForce(direzione.normalized * forza, ForceMode.Force);

            // Calcola il punto finale della freccia
            Vector3 start = oggetto.transform.position;
            Vector3 end = start + (direzione.normalized * forza / scalaVisualizzazione);

            // Imposta LineRenderer
            if (frecciaForza != null)
            {
                frecciaForza.SetPosition(0, start);
                frecciaForza.SetPosition(1, end);
                frecciaForza.enabled = true;
            }

            // Calcola accelerazione
            float accelerazione = forza / massa;
            if (outputAccelerazione != null)
                outputAccelerazione.text = $"a = {accelerazione:F2} m/s²";

            forzaApplicata = true;
        }
    }

    // Reset della simulazione
    public void ResetEsperimento()
    {
        oggetto.linearVelocity = Vector3.zero;
        oggetto.angularVelocity = Vector3.zero;
        oggetto.transform.position = posizioneIniziale;
        forzaApplicata = false;

        if (outputAccelerazione != null)
            outputAccelerazione.text = "a = 0.00 m/s²";

        if (frecciaForza != null)
            frecciaForza.enabled = false;
    }
}
