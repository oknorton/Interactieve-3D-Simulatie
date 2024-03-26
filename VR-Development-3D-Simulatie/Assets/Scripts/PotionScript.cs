using DefaultNamespace;
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

    public PotionType potionType;
    
    
    public float drainRate;

    public readonly string potionPlugged = "PotionPlugged";
    public readonly string potion = "Potion";
    public bool attachedToGun { get; set; } = false;
    public float empty = 0.61f;


    void Start()
    {
        ParticleSystem.MainModule mainModule = ps.main;
        mainModule.startColor = startColor;
        socketInteractor.selectEntered.AddListener(plugBottle);
        socketInteractor.selectExited.AddListener(unplugBottle);
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

            DrainLiquidOnEmpty(0.11f);
        }
        else
        {
            ps.Pause();
            ps.Clear();
        }
    }

    public void DrainLiquidOnEmpty(float drainRate)
    {
        liquid.DecreaseFillAmount(drainRate);
    }

    public void DrainLiquidOnFire()
    {
        liquid.DecreaseFillAmount(potionType.fireDrainRate);
    }

    bool IsUpsideDown()
    {
        return Vector3.Dot(transform.up, Vector3.up) < -0.5f;
    }
}

