using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public ColorCube[] cubes;
    public GameObject successUI;
    public Button restartButton;
    public AudioSource successSound;

    void Awake() => Instance = this;

    public void CheckCompletion()
    {
        foreach (var cube in cubes)
            if (!cube.isPlaced) return;

        successUI.SetActive(true);
        successSound.Play();
    }

    public void RestartPuzzle()
    {
        foreach (var cube in cubes)
        {
            cube.isPlaced = false;
            cube.GetComponent<Rigidbody>().isKinematic = false;
            cube.ResetPosition();
        }

        successUI.SetActive(false);
    }
}