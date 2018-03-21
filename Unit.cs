using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Unit : MonoBehaviour {
    
    //Base class Unit variables
    protected Animator anim;
    public float maxHealth = 100;
    public float health = 100;
    public int attackPower = 25;
    public GameObject explodePrefab;
    public GameObject damageMetalEffectPrefab;
    private int destroyExplode = 3;
    public bool isDie = true;
    internal float nextAttack = 2;

    protected void Awake ()
    {
        anim = GetComponentInChildren<Animator>();
	}

    //Decrease health on hit
    //Run Die() function when health is 0
    public void OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
            isDie = false;
            //anim.SetBool("isDead", true); //change isDead bool to true, so dead animation plays
            Destroy(gameObject); 
            GameObject clone = Instantiate(explodePrefab, transform.position, transform.rotation); //Spawn Explotion effect
            Destroy(clone, destroyExplode);
    }

    //Hit function for player
    public void OnHitPlayer(int damage)
    {
        nextAttack = nextAttack - Time.deltaTime < 0 ? 0 : nextAttack - Time.deltaTime; //countdown from 2 secons

        //apply damage to health every 2 seconds
        if (nextAttack == 0)
        {
            health -= damage;
            nextAttack = 2;
        }
        
        //Run Die() function when health is 0
        if (health <= 0)
        {
            Die();
        }
    }


}

    

