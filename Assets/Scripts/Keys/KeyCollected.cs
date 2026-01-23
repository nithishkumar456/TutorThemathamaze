using UnityEngine;

public class KeyCollected : MonoBehaviour
{
    [SerializeField] private GameObject puzzleManager;

    void OnTriggerEnter(Collider other)
    {
        puzzleManager.GetComponent<PuzzleManager>().CollectKey();
        Destroy(gameObject);
    }
}
