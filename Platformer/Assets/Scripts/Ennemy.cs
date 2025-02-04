using System;
using System.Collections;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class Ennemy : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefabs;
    [SerializeField] private int numberOfProjectile;
    [SerializeField] private EnnemyState currentEnnemyState;
    [SerializeField] private float radius;
    [SerializeField] private bool shooting;
    [SerializeField] private float spawnRate;
    [SerializeField] private Animator animatorEnnemy;
    [SerializeField] private Ennemy scriptToCancel;
    
    private enum EnnemyState
    {
        Idle,
        Attack,
        Dead
    }

    private void Start()
    {
        scriptToCancel = this;
        animatorEnnemy = GetComponent<Animator>();
        currentEnnemyState = EnnemyState.Idle;
    }

    private IEnumerator PrepareSpell()
    {
        currentEnnemyState = EnnemyState.Attack; 
        animatorEnnemy.SetTrigger("AttackSpell");

        for (var i = 0; i < numberOfProjectile; i++)
        {
            var angle = i * Mathf.PI * 2 / numberOfProjectile;
            float x = Mathf.Cos(angle);
            float y = Mathf.Sin(angle);
            Vector2 dirValue = new Vector2(x, y);
            Vector3 position = new Vector3(transform.position.x + dirValue.X * radius,transform.position.y + dirValue.Y * radius);
            Instantiate(projectilePrefabs, position, Quaternion.Euler(0,0,360/numberOfProjectile*i));
            yield return new WaitForSeconds(spawnRate); 
        }

        yield return new WaitForSeconds(10); 
        currentEnnemyState = EnnemyState.Idle; 
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (currentEnnemyState != EnnemyState.Dead && !other.gameObject.CompareTag("Rock")) return;
        currentEnnemyState = EnnemyState.Dead;
        animatorEnnemy.Play("Death");
        Destroy(other.gameObject);
        Die();
    }

    private void Die()
    {
        this.enabled = false;
        Destroy(gameObject,10);
    }   
    
    private void Update()
    {
        if (currentEnnemyState == EnnemyState.Idle)
        {
            StartCoroutine(PrepareSpell()); 
        }

        CheckForAnimationAttack();
    }

    private void CheckForAnimationAttack()
    {
        
    }
}