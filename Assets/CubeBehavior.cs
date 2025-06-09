using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CubeBehavior : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isPlaced = false;
    private AudioSource audioSource;
    public AudioClip wrongBuzzClip; // Assign in Inspector
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        audioSource = gameObject.AddComponent<AudioSource>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isPlaced) return;

        // Check if the collider is a placeholder
        if (other.CompareTag(gameObject.tag.Replace("Cube", "Placeholder")))
        {
            // Correct placeholder
            transform.position = other.transform.position + Vector3.up * 0.25f; // Snap above placeholder
            transform.rotation = other.transform.rotation;
            isPlaced = true;
            grabInteractable.enabled = false; // Lock cube
            GetComponent<Rigidbody>().isKinematic = true;
            PuzzleManager.Instance.CheckPuzzleCompletion();
        }
        else if (other.tag.Contains("Placeholder"))
        {
            // Wrong placeholder
            audioSource.PlayOneShot(wrongBuzzClip);
            transform.position = originalPosition;
            transform.rotation = originalRotation;
        }
    }

    public bool IsPlaced()
    {
        return isPlaced;
    }
}

