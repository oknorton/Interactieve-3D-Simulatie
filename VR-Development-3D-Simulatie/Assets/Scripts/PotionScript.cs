using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionScript : MonoBehaviour
{
    public XRBaseInteractor socketInteractor;
    public Liquid liquid;
    public ParticleSystem ps;
    public AudioSource audioSource;
    public AudioClip uncorkSound;

    public bool isPlugged { get; set; } = false;
    public bool attachedToGun { get; set; } = false;

    void Start()
    {
        socketInteractor.selectEntered.AddListener(plugBottle);
        socketInteractor.selectExited.AddListener(unplugBottle);
    }

    public void unplugBottle(SelectExitEventArgs arg0)
    {
        Debug.Log("Bottle unplugged");
        audioSource.PlayOneShot(uncorkSound);
        isPlugged = false;
    }

    public void plugBottle(SelectEnterEventArgs arg0)
    {
        Debug.Log("Bottle plugged");
        isPlugged = true;
    }

    void Update()
    {
        if (!isPlugged && IsUpsideDown() && !attachedToGun)
        {
            if (liquid.fillAmount < 0.6)
            {
                ps.Play();
            } else
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