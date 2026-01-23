using System.Numerics;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject puzzle1;
    private PlayerMovement playerMovement;

    void Start()
    {
        puzzle1.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        puzzle1.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        puzzle1.SetActive(false);
    }
}
