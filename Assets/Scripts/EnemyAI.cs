using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private float timer = 0f;

    [SerializeField] private float updateWhileInRange = 1f; // Snelheid van updaten
    [SerializeField] private float distance;
    [SerializeField] private float distanceLastFrame = 0f;
    [SerializeField] private float rangeDistance = 35f;

    public Transform Player; // Wordt automatisch gevonden
    public NavMeshAgent navMeshAgent;

    private NavMeshPath path;

    void Start()
    {
        // Zoek automatisch naar GameObject met naam "FirstPersonPlayer"
        GameObject playerObj = GameObject.Find("FirstPersonPlayer");

        if (playerObj != null)
        {
            Player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("EnemyAI kon geen GameObject met naam 'FirstPersonPlayer' vinden!");
        }

        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        if (Player == null) return;

        distance = Vector3.Distance(this.transform.position, Player.position);

        if (distance < rangeDistance)
        {
            navMeshAgent.destination = Player.position;

            if (distanceLastFrame > rangeDistance)
            {
                Debug.Log("Enemy is within range: " + distance);
            }
        }

        distanceLastFrame = distance;
    }
}
