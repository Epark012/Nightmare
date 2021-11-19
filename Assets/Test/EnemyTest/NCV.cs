using UnityEngine;

public class NCV : Enemy, IDamageable
{

    [SerializeField]
    private IKChainTargetSolver[] hands;

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
            case EnemyState.Working:
                //
                WorkingOnMineral();
                break;
        }

    }

    private void WorkingOnMineral()
    {
        Mineral target = cortex.GetTargetMineral();
        if(mState == MovementState.IsReady)
        {
            mState = MovementState.Moving;
            //Approach target mineral
            agent.SetDestination(target.transform.position);
        }

        else if (mState == MovementState.Moving)
        {
            if (Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), 
                                new Vector3(target.transform.position.x, 0f, target.transform.position.z)) 
                                <= agent.stoppingDistance)
            {
                mState = MovementState.Operating;
                //Check it is mineral or fake
                if(!target.IsMineralGrenades)
                {
                    //Stop navMesh
                    agent.SetDestination(transform.position);
                    //if minerall, working on that.
                    SetHandsTarget(target);
                }
            }        
        }
        //mState == Operating
        else
        {
            //Operating Events
            //Effect
            //Sound
            //Operating Duration
        }
    }

    private void SetHandsTarget(Mineral mineral)
    {
        for(int i = 0; i < hands.Length; i++)
        {
            hands[i].transform.position = mineral.transform.position;
            hands[i].ToggleIKWeight(true);
        }
    }

    public void TakeDamage(float damage)
    {
        enemyHP -= damage;

        if (enemyHP < 1)
        {
            base.Destroyed();
        }
    }
}
