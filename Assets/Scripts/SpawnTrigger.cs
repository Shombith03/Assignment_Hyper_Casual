using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Spikes,
    NoGravity,
    Trampoline,
    TeleportBack
}


public class SpawnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _spikes;

    private Rigidbody _rb;
    private Animator _animator;
    private bool isObstacle = true;
    private BoxCollider _obstacleCollider;

    private void Start()
    {
        _rb = transform.parent.GetComponent<Rigidbody>();
        _animator = transform.parent.GetComponent<Animator>();
        _obstacleCollider = GetComponentInChildren<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObstacleCheck(other.gameObject);
        }
    }

    void ObstacleCheck(GameObject player)
    {
        if (isObstacle)
        {
            int obstacleCount = System.Enum.GetValues(typeof(ObstacleType)).Length;
            int obstacleChances = Random.Range(0, obstacleCount);

            ActivateObstacle((ObstacleType)obstacleChances, player);
        }
    }

    private void ActivateObstacle(ObstacleType obstacleType, GameObject player = null)
    {
        switch (obstacleType)
        {
            case ObstacleType.Spikes:

                GameManager.Instance.ShowTrapName("Spikes");
                _spikes.SetActive(true);
                _obstacleCollider.enabled = false;
                break;

            case ObstacleType.NoGravity:

                Debug.Log("Activating No Gravity");
                GameManager.Instance.ShowTrapName("NoGravity");

                _obstacleCollider.enabled = false;
                _rb.isKinematic = false;
                break;

            case ObstacleType.Trampoline:
                GameManager.Instance.ShowTrapName("Trampoline");
                Debug.Log("Trampoline");
                _obstacleCollider.enabled = false;
                ApplyTrampolineForce(player.GetComponent<Rigidbody>());
                break;
            case ObstacleType.TeleportBack:
                GameManager.Instance.ShowTrapName("TeleportBack");
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 10f);
                _obstacleCollider.enabled = false;
                break;

            default:
                return;
        }
    }

    private void ApplyTrampolineForce(Rigidbody playerRB)
    {
        playerRB.AddForce(Vector3.up * 100f, ForceMode.Impulse);
    }

}