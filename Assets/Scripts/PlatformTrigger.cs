using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _platformSets;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        Vector3 newPos = new Vector3(0f, 2f, transform.parent.position.z + 100f);

        GameObject platformPrefab = _platformSets[Random.Range(0, _platformSets.Length)];

        GameObject platform = Instantiate(platformPrefab, newPos, Quaternion.identity);
        DeletePlatform();
    }

    private void DeletePlatform()
    {
        Destroy(transform.gameObject, 5f);
    }
}
