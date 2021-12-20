using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fovLazer : MonoBehaviour
{
    public GameObject witness;
    private LineRenderer Line;
    private Vector2 middlePoint;
    public int sightDistance = 10;
    public int sightAngle = 45;
    // Start is called before the first frame update
    void Start()
    {
        Line = GetComponent<LineRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (witness == null)
        {
            Destroy(gameObject);
        }
        else
        {
            if (witness.GetComponent<EnemyAI>() != null)
            {
                var sightParam = witness.GetComponent<EnemyAI>();
                sightDistance = sightParam.sightDistance;
                sightAngle = sightParam.sightAngle;
            }
            if (witness.GetComponent<Securitycamera>() != null)
            {
                var sightParam = witness.GetComponent<Securitycamera>();
                sightDistance = sightParam.sightDistance;
                sightAngle = sightParam.sightAngle;
            }
        
        Line.SetPosition(1, witness.transform.position);
       middlePoint = Line.GetPosition(1);
        //Vector2 noAngle = witness.transform.up;
        var right45 = (witness.transform.up + witness.transform.right).normalized;
        var left45 = (witness.transform.up - witness.transform.right).normalized;
        Quaternion spreadAngle = Quaternion.AngleAxis(-45, witness.transform.up);
        //Vector2 targetDirection = spreadAngle * noAngle;
        RaycastHit2D hit = Physics2D.Raycast(middlePoint, right45, sightDistance);
        if (hit.point != new Vector2(0,0)) {
            Line.SetPosition(0, hit.point);
        } else
        {
            Line.SetPosition(0, witness.transform.position +right45 * sightDistance);

        }

        //Vector2 targetDirection2 = spreadAngle * -noAngle;
        RaycastHit2D hit2 = Physics2D.Raycast(middlePoint, left45, sightDistance);
        if (hit2.point != new Vector2(0, 0))
        {
            Line.SetPosition(2, hit2.point);
        }
        else
        {
            Line.SetPosition(2, witness.transform.position + left45 * sightDistance);

        }
        }
    }
}
