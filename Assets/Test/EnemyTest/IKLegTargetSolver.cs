using UnityEngine;

public class IKLegTargetSolver : MonoBehaviour
{

    [SerializeField]
    private LayerMask terrainLayer = default;
    [SerializeField]
    private Transform body = null;
    [SerializeField]
    private IKLegTargetSolver otherFoot = null;
    [SerializeField]
    private float speed = 3.0f, stepDistance = .3f, stepLength = .3f, stepHeight = .3f;
    [SerializeField]
    private Vector3 footPosOffset, footRotOffset;

    private float footSpacing, lerp;
    private Vector3 oldPos, currentPos, newPos;
    private Vector3 oldNorm, currentNorm, newNorm;

    [SerializeField]
    private float debugDistance = .0f;


    // Start is called before the first frame update
    void Start()
    {
        footSpacing = transform.localPosition.x;
        currentPos = newPos = oldPos = transform.position;
        currentNorm = newNorm = oldNorm = transform.up;
        lerp = 1;
    }

    // Update is called once per frame
    void Update()
    {
        #region Draw Test Ray
        DrawTestRay();
        #endregion

        transform.position = currentPos + footPosOffset;
        transform.rotation = Quaternion.LookRotation(currentNorm) * Quaternion.Euler(footRotOffset);

        Ray ray = new Ray(body.position + (body.right * footSpacing) + (Vector3.up * 2), Vector3.down);

        if(Physics.Raycast(ray, out RaycastHit hit, 10, terrainLayer.value))
        {
            debugDistance = Vector3.Distance(newPos, hit.point);
            if (Vector3.Distance(newPos, hit.point) > stepDistance && !IsMoving() && !otherFoot.IsMoving())
            {
                lerp = 0;
                int direction = 1;
                //int direction = body.InverseTransformPoint(hit.point).z > body.InverseTransformPoint(newPos).z ? 1 : -1;
                //According to Body Direction, coding has to be fixed.
                newPos = hit.point + (body.forward * (direction * stepLength)) + footPosOffset;
                newNorm = hit.normal + footRotOffset;
            }
        }

        if (lerp < 1)
        {
            Vector3 tempPos = Vector3.Lerp(oldPos, newPos, lerp);
            tempPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPos = tempPos;
            currentNorm = Vector3.Lerp(oldNorm, newNorm, lerp);
            lerp += Time.deltaTime * speed;
        }
        else
        {
            oldPos = newPos;
            oldNorm = newNorm;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(newPos, 0.1f);
    }

    public bool IsMoving()
    {
        return lerp < 1;
    }

    private void DrawTestRay()
    {
        //Debug.DrawRay(body.position + (body.right * footSpacing) + (Vector3.up * 2), Vector3.down, Color.cyan);
        //Debug.Log("Ray origin : " + body.position + (body.right * footSpacing));
    }
}
