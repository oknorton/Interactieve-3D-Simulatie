using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionScript : MonoBehaviour
{
    public XRBaseInteractor socketInteractor;
    public Liquid liquid;
    public ParticleSystem ps;
    public ParticleSystem.MinMaxGradient startColor;
    public AudioSource audioSource;
    public AudioClip uncorkSound;

    public readonly string potionPlugged = "PotionPlugged";
    public readonly string potion = "Potion";
    public bool attachedToGun { get; set; } = false;


    void Start()
    {
        ParticleSystem.MainModule mainModule = ps.main;
        mainModule.startColor = startColor;
        socketInteractor.selectEntered.AddListener(plugBottle);
        socketInteractor.selectExited.AddListener(unplugBottle);
        InitializeLiquid();
    }

    void InitializeLiquid()
    {
        // liquid = gameObject.AddComponent<Liquid>();
    }


    public void unplugBottle(SelectExitEventArgs arg0)
    {
        Debug.Log("Bottle unplugged");
        audioSource.PlayOneShot(uncorkSound);
        gameObject.tag = potion;
    }

    public void plugBottle(SelectEnterEventArgs arg0)
    {
        Debug.Log("Bottle plugged");
        gameObject.tag = potionPlugged;
    }

    void Update()
    {
        if (IsUpsideDown() && !attachedToGun && CompareTag(potion))
        {
            if (liquid.fillAmount < 0.6)
            {
                ps.Play();
            }
            else
            {
                ps.Pause();
                ps.Clear();
            }

            liquid.DecreaseFillAmount();
        }
        else
        {
            ps.Pause();
            ps.Clear();
        }
    }

    bool IsUpsideDown()
    {
        return Vector3.Dot(transform.up, Vector3.up) < -0.5f;
    }
}