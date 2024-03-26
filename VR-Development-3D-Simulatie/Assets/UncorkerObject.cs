using System.Collections;
using UnityEngine;

public class UncorkerObject : MonoBehaviour
{
    private const string originalTag = "Cork";
    private const string newTag = "CorkBitten";

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(originalTag))
        {
            StartCoroutine(ChangeTagCoroutine(other.gameObject));
        }
    }

    IEnumerator ChangeTagCoroutine(GameObject corkObject)
    {
        corkObject.tag = newTag;

        yield return new WaitForSeconds(1f);

        corkObject.tag = originalTag;
    }
}