using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public float movementSpeed;
    public float followDistance;
    public LayerMask obstacleLayerMask;

    private NavMeshAgent zombieNav;
    private GameObject zombie;
    private GameObject player;
    private float sightDistance;
    private Rigidbody rb;

    [Tooltip("0 = idle, 1 = run, 2 = jump")]
    public int state;

    public Animator anim;

    private bool isRunning;
    private bool isIdle;
    private bool isJumping;

    private Vector3 wanderTarget;

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

        rb = GetComponent<Rigidbody>();

        //Rotates to face the player.
        zombie.transform.LookAt(player.transform.position);

        isIdle = false;
        isRunning = false;
        isJumping = false;

        wanderTarget = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 2:
                print("jump");

                isIdle = false;
                isRunning = false;
                isJumping = true;

                anim.SetBool("isJumping", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);

                state = 2;

                break;
            case 1:
                print("run");

                isIdle = false;
                isRunning = true;
                isJumping = false;

                anim.SetBool("isJumping", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", true);

                state = 1;

                break;
            case 0:
                print("idle");

                isIdle = true;
                isRunning = false;
                isJumping = false;

                anim.SetBool("isJumping", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);

                state = 0;

                break;
            default:
                print("error");

                isIdle = true;
                isRunning = false;
                isJumping = false;

                anim.SetBool("isJumping", false);
                anim.SetBool("isIdle", true);
                anim.SetBool("isRunning", false);

                state = 0;

                break;
        }

        //Detects if the player is within the followDistance, if not, it won't follow the player.
        sightDistance = Vector3.Distance(transform.position, player.transform.position);
        if (followDistance >= sightDistance && CanSeePlayer())
        {
            //Debug.Log("moving towards player");
            zombieNav.speed = movementSpeed;
            zombieNav.SetDestination(player.transform.position);
            
            if(rb.velocity.y == 0)
			{
                zombie.transform.LookAt(player.transform.position);
            }

            state = 1;
        }
        else
        {
            Wander();
        }
    }

    bool CanSeePlayer()
    {
        bool hit = Physics.Linecast(zombie.transform.position, player.transform.position, 1 << 7);

        if(!hit)
        {
            //Debug.Log("hit player");
            return true;
        }
        else
        {
            //Debug.Log("not hit player");
            return false;
        }
    }

    public void Wander()
	{
        zombieNav.SetDestination(wanderTarget);

        zombieNav.speed = movementSpeed * 0.8f;
        state = 1;

        //print("Moving to " + wanderTarget);

        if (Vector3.Distance(transform.position, wanderTarget) < 2.1f || zombieNav.speed == 0)
		{
            zombieNav.speed = 0;
            state = 0;

            StartCoroutine(RandomWanderPosition());
            //print("At wander target.");
		}
	}

    public IEnumerator RandomWanderPosition()
	{
        //print("Set new wander target at " + wanderTarget);

        yield return new WaitForSeconds(Random.Range(1, 3));

        wanderTarget = RandomNavSphere(transform.position, 15);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.onUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, -1);

        return navHit.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hole"))
        {
            Destroy(gameObject);
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Hole"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Score"))
        {
            player.GetComponent<PlayerController>().score++;
        }
    }
}
