using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public float movementSpeed;

    private NavMeshAgent zombieNav;
    private GameObject zombie;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Assigns the player.
        player = GameObject.FindGameObjectWithTag("Player");

        //Assigns the zombie.
        zombieNav = GetComponent<NavMeshAgent>();
        zombie = gameObject;

        //Changes the zombie speed based on a variable.
        zombieNav.speed = movementSpeed;

        //Sets a destination for the zombie to go to,in this case, a  player.
        zombieNav.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
