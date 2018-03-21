using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : Unit
 {
    #region Globals

    private NavMeshAgent agent;
    private PlayerController player;
    private IEnumerator currentState;
    public float playerDetectionRadius = 2f;
    private int _PlayerLayer = -1;

    private float ShakeAmount = 0.1f;
    private float TimeShaking = 10f;
    private float ShakeDuration = 0.5f;
    private float positionX;
    private float positionY;
    private float positionZ;


    #endregion

    private void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }


    // Use this for initialization
    void Start () {
        //var i = anim.SetFloat("moveHorizontal", 1);
        player = GameObject.FindObjectOfType<PlayerController>();
        _PlayerLayer = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetFloat("moveHorizontal", agent.velocity.magnitude);
        SetState(OnMovingToPlayer()); //Set my default state

        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;

        //Shake enemy when taken damage (0.5 seconds(ShakeDuration))
        if (TimeShaking <= ShakeDuration)
        {
            transform.position = new Vector3((Random.Range(positionX - ShakeAmount, positionX + ShakeAmount)), (Random.Range((positionY - ShakeAmount) / 3, (positionY + ShakeAmount) / 3)), positionZ);
            TimeShaking += Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(positionX, positionY, positionZ);
        }
    }

    private void SetState (IEnumerator newState)
    {
        if (currentState != null)
            StopCoroutine(currentState);

        currentState = newState;
        //StartCoroutine(newState);
    }

    
    //Enemy moves to player and starts attack when close
    IEnumerator OnMovingToPlayer()
    {
        agent.SetDestination(player.transform.position);

        Collider[] detectSphere = Physics.OverlapSphere(transform.position, playerDetectionRadius, _PlayerLayer);
        foreach (var c in detectSphere)
        {
            PlayerController player = c.GetComponent<PlayerController>();
            if( player != null)
            {
                SetState(OnAttackingPlayer());
            }
            //print("OLS: "+ c.name);
        }
        return null;
    }

    
    IEnumerator OnAttackingPlayer()
    {
        anim.SetTrigger("Attack");

        player.OnHitPlayer(10);
        //player.UpdateHealthBar();
        //print(player.health);
        return null;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

    private void OnTriggerEnter()
    {
        OnHit(25); //25 Damage to enemy
        //print(health);
        TimeShaking = 0; //Reset enemy shake timer
        GameObject metalEffect = Instantiate(damageMetalEffectPrefab, (transform.position + new Vector3(0,1,0)), transform.rotation);
    }


}
