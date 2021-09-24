using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private int compHealth = 3;

    public void TakeDamage(int damage)
    {
        Debug.Log(gameObject.name + " has been hit.");

        compHealth -= damage;

        if(compHealth < 1)
        {
            transform.parent = null;
            gameObject.AddComponent<Rigidbody>();
        }
    }
}
