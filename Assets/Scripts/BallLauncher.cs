using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallParabolaController : MonoBehaviour
{
    [Header("Riferimenti")]
    public Rigidbody ballRb;          // Trascina qui la sfera
    public Transform rampa;           // Trascina qui l'oggetto che indica la direzione del lancio

    [Header("UI Input")]
    public Slider sliderForza;
    public Slider sliderMassa;

    [Header("UI Output")]
    public TextMeshProUGUI forzaOutputText;
    public TextMeshProUGUI massaOutputText;
    public TextMeshProUGUI tempoVoloText;
    public TextMeshProUGUI altezzaMaxText;
    public TextMeshProUGUI gittataText;

    [Header("Parametri")]
    public float forzaMassima = 50f;

    Vector3 posizioneIniziale;
    Quaternion rotazioneIniziale;
    Vector3 velocitaUltimoLancio;

    void Start()
    {
        posizioneIniziale = ballRb.transform.position;
        rotazioneIniziale = ballRb.transform.rotation;

        AggiornaOutputForza();
        AggiornaOutputMassa();
    }

    public void AggiornaOutputForza()
    {
        float forza = sliderForza.value * forzaMassima;
        forzaOutputText.text = forza.ToString("F1") + " N";
    }

    public void AggiornaOutputMassa()
    {
        float massa = sliderMassa.value;
        massaOutputText.text = massa.ToString("F2") + " kg";
        ballRb.mass = massa;
    }

    public void Lancia()
    {
        ballRb.linearVelocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;

        float forza = sliderForza.value * forzaMassima;
        ballRb.AddForce(rampa.up * forza, ForceMode.Impulse);

        velocitaUltimoLancio = rampa.up * forza / ballRb.mass;
        CalcolaTraiettoria();
    }

    void CalcolaTraiettoria()
    {
        float g = Mathf.Abs(Physics.gravity.y);
        float v = velocitaUltimoLancio.y;
        float vxz = new Vector3(velocitaUltimoLancio.x, 0, velocitaUltimoLancio.z).magnitude;

        float tempoVolo = (2 * v) / g;
        float altezzaMax = (v * v) / (2 * g);
        float gittata = vxz * tempoVolo;

        tempoVoloText.text = tempoVolo.ToString("F2") + " s";
        altezzaMaxText.text = altezzaMax.ToString("F2") + " m";
        gittataText.text = gittata.ToString("F2") + " m";
    }

    public void Reset()
    {
        ballRb.linearVelocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;

        ballRb.transform.position = posizioneIniziale;
        ballRb.transform.rotation = rotazioneIniziale;

        tempoVoloText.text = "";
        altezzaMaxText.text = "";
        gittataText.text = "";
    }
}
