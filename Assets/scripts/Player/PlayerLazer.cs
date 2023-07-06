using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLazer : MonoBehaviour
{
    public GameObject witness;
    private LineRenderer Line;
    public LayerMask IgnoreThis;

    // Start is called before the first frame update
    void Start()
    {
        Line = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
       Line.SetPosition(1, witness.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(witness.transform.position, witness.transform.up,1000f,IgnoreThis);
       Line.SetPosition(0, hit.point);
    }
}
