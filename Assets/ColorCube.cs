using UnityEngine;

public class ColorCube : MonoBehaviour
{
    public string colorID;
    public bool isPlaced = false;
    private Vector3 startPosition;
    private AudioSource buzzAudio;

    void Start()
    {
        startPosition = transform.position;
        buzzAudio = GetComponent<AudioSource>();
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        buzzAudio.Play();
    }
}