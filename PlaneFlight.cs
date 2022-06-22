using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFlight : MonoBehaviour
{

    public float moveSpeed = 0.5f;

    public GameObject[] flightTargs;

    private Vector2 targ;

    private bool landed;

    // Start is called before the first frame update
    void Start()
    {
        targ = flightTargs[Random.Range(0, flightTargs.Length - 1)].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float flight = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targ, flight);

        var dir = targ - new Vector2(transform.position.x, transform.position.y);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.y), targ) < 0.25f && !landed)
        {
            landed = true;
            StartCoroutine(Landed());
        }
    }

    IEnumerator Landed()
    {

        targ = transform.position;
        yield return new WaitForSecondsRealtime(2.5f);
        targ = flightTargs[Random.Range(0, flightTargs.Length - 1)].transform.position;
        landed = false;
    }
}
