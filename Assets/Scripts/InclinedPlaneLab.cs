using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InclinedPlaneLab : MonoBehaviour
{
    [Header("Finish Line")]
    public string sphereTag = "Sphere"; 

    [Header("Physics Objects")]
    public Rigidbody sphereRigidbody;
    public PhysicsMaterial planeMaterial;
    public Transform startPosition;

    [Header("UI Sliders")]
    public Slider massSlider;
    public Slider frictionSlider;

    [Header("UI Outputs (TMP)")]
    public TMP_Text massOutput;
    public TMP_Text frictionOutput;
    public TMP_Text timerOutput;

    [Header("UI Buttons")]
    public Button startButton;
    public Button resetButton;

    private float timer = 0f;
    private bool isRunning = false;

    void Start()
    {
        FreezeSphere();
        ResetSpherePosition();

        UpdateMass(massSlider.value);
        UpdateFriction(frictionSlider.value);
        UpdateTimerText();

        massSlider.onValueChanged.AddListener(UpdateMass);
        frictionSlider.onValueChanged.AddListener(UpdateFriction);

        startButton.onClick.AddListener(StartExperiment);
        resetButton.onClick.AddListener(ResetExperiment);
    }

    void Update()
    {
        if (isRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }

    // --------------------
    // UI → FISICA
    // --------------------

    void UpdateMass(float value)
    {
        sphereRigidbody.mass = value;
        massOutput.text = value.ToString("0.00");
    }

    void UpdateFriction(float value)
    {
        planeMaterial.staticFriction = value;
        planeMaterial.dynamicFriction = value;
        frictionOutput.text = value.ToString("0.00");
    }

    // --------------------
    // CONTROLLO ESPERIMENTO
    // --------------------

    void StartExperiment()
    {
        if (isRunning) return;

        isRunning = true;
        UnfreezeSphere();
    }

    void ResetExperiment()
    {
        isRunning = false;
        timer = 0f;

        FreezeSphere();
        ResetSpherePosition();
        UpdateTimerText();
    }

    // --------------------
    // UTILITÀ FISICHE
    // --------------------

    void FreezeSphere()
    {
        sphereRigidbody.linearVelocity = Vector3.zero;
        sphereRigidbody.angularVelocity = Vector3.zero;
        sphereRigidbody.isKinematic = true;
    }

    void UnfreezeSphere()
    {
        sphereRigidbody.isKinematic = false;
    }

    void ResetSpherePosition()
    {
        sphereRigidbody.transform.position = startPosition.position;
        sphereRigidbody.transform.rotation = startPosition.rotation;
    }

    void UpdateTimerText()
    {
        timerOutput.text = timer.ToString("0.00") + " s";
    }
private void OnTriggerEnter(Collider other)
{
    if (!isRunning) return;

    if (other.CompareTag(sphereTag))
    {
        isRunning = false;
        FreezeSphere();
    }
}
}


