using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public GameObject[] cubes; // Assign RedCube, GreenCube, BlueCube in Inspector
    public GameObject successText; // Assign SuccessText in Inspector
    public Button restartButton; // Assign RestartButton in Inspector
    public AudioClip successClip; // Assign in Inspector
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void CheckPuzzleCompletion()
    {
        bool allPlaced = true;
        foreach (GameObject cube in cubes)
        {
            CubeBehavior cubeBehavior = cube.GetComponent<CubeBehavior>();
            if (!cubeBehavior.IsPlaced())
            {
                allPlaced = false;
                break;
            }
        }

        if (allPlaced)
        {
            successText.GetComponent<TextMeshProUGUI>().enabled = true;
            restartButton.gameObject.SetActive(true);
            audioSource.PlayOneShot(successClip);
        }
    }

    public void RestartPuzzle()
    {
        foreach (GameObject cube in cubes)
        {
            CubeBehavior cubeBehavior = cube.GetComponent<CubeBehavior>();
            cubeBehavior.GetComponent<XRGrabInteractable>().enabled = true;
            cubeBehavior.GetComponent<Rigidbody>().isKinematic = false;
            cube.transform.position = cube.GetComponent<CubeBehavior>().originalPosition;
            cube.transform.rotation = cube.GetComponent<CubeBehavior>().originalRotation;
        }
        successText.GetComponent<TextMeshProUGUI>().enabled = false;
        restartButton.gameObject.SetActive(false);
    }
}
