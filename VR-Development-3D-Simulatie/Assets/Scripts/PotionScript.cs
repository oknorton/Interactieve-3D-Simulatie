using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PotionScript : MonoBehaviour
{
    public XRBaseInteractor socketInteractor;
    public Liquid liquid;

    private bool isPlugged = true;

    void Start()
    {
        socketInteractor.selectEntered.AddListener(plugBottle);
        socketInteractor.selectExited.AddListener(unplugBottle);
    }

    private void unplugBottle(SelectExitEventArgs arg0)
    {
        Debug.Log("Bottle unplugged");
        isPlugged = false;
    }

    private void plugBottle(SelectEnterEventArgs arg0)
    {
        Debug.Log("Bottle plugged");
        isPlugged = true;
    }

    void Update()
    {
        if (!isPlugged && IsUpsideDown())
        {
            liquid.DecreaseFillAmount();
        }
    }

    bool IsUpsideDown()
    {
        return Vector3.Dot(transform.up, Vector3.up) < -0.5f;
    }
}