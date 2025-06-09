using UnityEngine;

public class PlaceholderDetector : MonoBehaviour
{
    public string acceptedColor;
    public Transform snapPoint;

    private void OnTriggerEnter(Collider other)
    {
        var cube = other.GetComponent<ColorCube>();
        if (cube != null)
        {
            if (cube.colorID == acceptedColor && !cube.isPlaced)
            {
                cube.transform.position = snapPoint.position;
                cube.GetComponent<Rigidbody>().isKinematic = true;
                cube.isPlaced = true;
                PuzzleManager.Instance.CheckCompletion();
            }
            else if (!cube.isPlaced)
            {
                cube.ResetPosition(); // Handles buzz + reset
            }
        }
    }
}