using UnityEngine;

public class CadutaMarte : MonoBehaviour
{
    public Rigidbody sfera;            // assegna qui la sfera "mercurio"
    private Vector3 posizioneIniziale; // per il reset
    private float gravitaMarte = 3.71f; // m/s²

    void Start()
    {
        // memorizza posizione di partenza
        posizioneIniziale = sfera.transform.position;

        // disabilita gravità di Unity (così la controlliamo noi)
        sfera.useGravity = false;
    }

    // chiamato dal pulsante "Start"
    public void StartCaduta()
    {
        // reset eventuale velocità
        sfera.linearVelocity = Vector3.zero;
        sfera.angularVelocity = Vector3.zero;

        // abilita la gravità personalizzata
        sfera.useGravity = false; // restiamo in controllo via script
        sfera.gameObject.AddComponent<GravitaPersonalizzata>().Inizializza(gravitaMarte);
    }

    // chiamato dal pulsante "Reset"
    public void ResetCaduta()
    {
        // resetta posizione e velocità
        sfera.linearVelocity = Vector3.zero;
        sfera.angularVelocity = Vector3.zero;
        sfera.transform.position = posizioneIniziale;

        // elimina eventuale gravità personalizzata attaccata
        GravitaPersonalizzata gravita = sfera.GetComponent<GravitaPersonalizzata>();
        if (gravita != null) Destroy(gravita);
    }
}
