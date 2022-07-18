using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public float movementSpeed;
    public float followDistance;
    public LayerMask layerMask;

    private NavMeshAgent zombieNav;
    private GameObject zombie;
    private GameObject player;
    private float sightDistance;

    // Start is called before the first frame update
    void Start()
    {
        //Assigns the player.
        player = GameObject.FindGameObjectWithTag("Player");

        //Assigns the zombie.
        zombieNav = GetComponent<NavMeshAgent>();
        zombie = gameObject;

        //Sets a destination for the zombie to go to, in this case, a  player.
        zombieNav.SetDestination(player.transform.position);

        //Rotates to face the player.
        zombie.transform.LookAt(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Detects if the player is within the followDistance, if not, it won't follow the player.
        sightDistance = Vector3.Distance(transform.position, player.transform.position);
        if (followDistance >= sightDistance && CanSeePlayer())
        {
            zombieNav.speed = movementSpeed;
            zombieNav.SetDestination(player.transform.position);
            zombie.transform.LookAt(player.transform.position);
        }
        else
        {
            zombieNav.speed = 0;
        }
    }

    bool CanSeePlayer()
    {
        bool hit = Physics.Linecast(zombie.transform.position, player.transform.position, 1 << 7);

        if(!hit)
        {
            Debug.Log("hit player");
            return true;
        }
        else
        {
            Debug.Log("not hit player");
            return false;
        }
    }
}
