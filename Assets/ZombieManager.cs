using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public GameObject player;
    public float movementSpeed;

    private NavMeshAgent zombie;

    // Start is called before the first frame update
    void Start()
    {
        //Assigns the zombie.
        zombie = GetComponent<NavMeshAgent>();

        //Changes the zombie speed based on a variable.
        zombie.speed = movementSpeed;

        //Sets a destination for the zombie to go to,in this case, a  player.
        zombie.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
