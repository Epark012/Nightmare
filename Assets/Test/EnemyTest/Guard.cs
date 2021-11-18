using UnityEngine;

public class Guard : Enemy, IDamageable
{
    #region Guard Property
    [Header("Guard Property")]
    

    public float distance = 0f;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle:
                //
                break;
            case EnemyState.Wandering:
                Wander();
                break;
            case EnemyState.Working:
                PatrollingState();
                break;
            //ScanningByRay(); 
            default:
                //
                break;
        }
    }

   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(targetWorld, 0.1f);
    }
    public void TakeDamage(float damage)
    {

    }
}
