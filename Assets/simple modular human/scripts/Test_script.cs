using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test_script : MonoBehaviour
{
    public Animator ani;
    public GameObject aim_point;

    public bool execute_walking;
    public bool execute_sitting;
    public bool execute_stealing;
    public bool execute_picking_up;

    public bool execute_running;

    public float walk_speed;
    public float run_speed;


    public bool walk;
    public bool run;
    public bool sit;
    public bool steal;
    public bool pick_up;

    public bool destermine_new_aim;

    public GameObject crowbar;

    public bool ready;


    public List<GameObject> way_points = new();
    public List<GameObject> Sitting_points = new();
    public List<GameObject> Stealing_points = new();
    public List<GameObject> pick_up_points = new();


    private NavMeshAgent agent;
    private bool in_pickup;

    private bool in_sitting;
    private bool in_stealing;
    private Coroutine pickup_start;

    private Vector3 sitting_position;
    private Vector3 sitting_Rotation;

    private Coroutine sitting_start;

    private Vector3 stealing_position;
    private Vector3 stealing_Rotation;
    private Coroutine stealing_start;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        sitting_position = new Vector3(0, 0, 0);
        sitting_Rotation = new Vector3(180, -90, -90);

        stealing_position = new Vector3(0, 0.04185915f, -0.07200003f);
        stealing_Rotation = new Vector3(0, 180, 0);

        way_points.Clear();
        Sitting_points.Clear();
        Stealing_points.Clear();
        pick_up_points.Clear();

        var waypointsFind = GameObject.FindGameObjectsWithTag("waypoint");
        var SittingpointsFind = GameObject.FindGameObjectsWithTag("sittingpoint");
        var stealingpointsFind = GameObject.FindGameObjectsWithTag("stealingpoint");
        var pick_up_pointsFind = GameObject.FindGameObjectsWithTag("pickuppoint");

        foreach (var g in waypointsFind) way_points.Add(g);
        foreach (var g in SittingpointsFind) Sitting_points.Add(g);
        foreach (var g in stealingpointsFind) Stealing_points.Add(g);
        foreach (var g in pick_up_pointsFind) pick_up_points.Add(g);
    }

    private void Update()
    {
        if (!ready) return;


        if (!destermine_new_aim)
        {
            var what_to_choose = 0;


            walk = false;
            run = false;
            sit = false;
            steal = false;
            pick_up = false;


            if (what_to_choose == 0)
            {
                walk = true;

                var Which_point = Random.Range(0, way_points.Count);
                aim_point = way_points[Which_point].gameObject;
                destermine_new_aim = true;
            }

            if (what_to_choose == 1)
            {
                run = true;

                var Which_point = Random.Range(0, way_points.Count);
                aim_point = way_points[Which_point].gameObject;
                destermine_new_aim = true;
            }

            if (what_to_choose == 2)
            {
                sit = true;

                var Which_point = Random.Range(0, Sitting_points.Count);
                aim_point = Sitting_points[Which_point].gameObject;
                destermine_new_aim = true;
            }

            if (what_to_choose == 3)
            {
                steal = true;

                var Which_point = Random.Range(0, Stealing_points.Count);
                aim_point = Stealing_points[Which_point].gameObject;
                destermine_new_aim = true;
            }

            if (what_to_choose == 4)
            {
                pick_up = true;

                var Which_point = Random.Range(0, pick_up_points.Count);
                aim_point = pick_up_points[Which_point].gameObject;
                destermine_new_aim = true;
            }
        }

        if (destermine_new_aim)
        {
            if (walk)
            {
                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false;
                }
            }

            if (run)
            {
                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    Debug.Log("going to run");
                    agent.speed = run_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 2);
                    ani.SetInteger("legs", 2);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;

                    ani.SetInteger("arms", 5);
                    ani.SetInteger("legs", 5);

                    destermine_new_aim = false;
                }
            }

            if (sit && !in_sitting)
            {
                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;


                    if (!in_sitting)
                    {
                        in_sitting = true;

                        sitting_start = StartCoroutine(sitting_down());
                    }
                }
            }

            if (steal && !in_stealing)
            {
                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;


                    if (!in_stealing)
                    {
                        in_stealing = true;

                        stealing_start = StartCoroutine(stealing_execute());
                    }
                }
            }

            if (pick_up && !in_pickup)
            {
                if (Vector3.Distance(transform.position, aim_point.transform.position) > 0.25f)
                {
                    agent.speed = walk_speed;
                    agent.SetDestination(aim_point.transform.position);
                    ani.SetInteger("arms", 1);
                    ani.SetInteger("legs", 1);
                }

                if (Vector3.Distance(transform.position, aim_point.transform.position) < 0.25f)
                {
                    agent.speed = 0;


                    if (!in_pickup)
                    {
                        in_pickup = true;

                        pickup_start = StartCoroutine(pickup_execute());
                    }
                }
            }
        }
    }

    private IEnumerator sitting_down()
    {
        yield return new WaitForSeconds(0);

        transform.parent = aim_point.transform;


        Destroy(agent);

        ani.SetInteger("legs", 3);
        ani.SetInteger("arms", 3);

        transform.localPosition = sitting_position;
        transform.localEulerAngles = sitting_Rotation;


        yield return new WaitForSeconds(5);

        agent = gameObject.AddComponent<NavMeshAgent>();


        in_sitting = false;
        destermine_new_aim = false;
        transform.parent = null;

        StopCoroutine(sitting_start);
    }


    private IEnumerator stealing_execute()
    {
        yield return new WaitForSeconds(0);
        crowbar.SetActive(true);
        transform.parent = aim_point.transform;
        transform.localPosition = stealing_position;
        transform.localEulerAngles = stealing_Rotation;

        ani.SetInteger("legs", 5);
        ani.SetInteger("arms", 22);

        yield return new WaitForSeconds(5);
        crowbar.SetActive(false);
        in_stealing = false;
        destermine_new_aim = false;
        transform.parent = null;

        StopCoroutine(stealing_start);
    }


    private IEnumerator pickup_execute()
    {
        yield return new WaitForSeconds(0);


        ani.SetInteger("legs", 32);
        ani.SetInteger("arms", 32);


        yield return new WaitForSeconds(2);

        in_pickup = false;
        destermine_new_aim = false;


        StopCoroutine(pickup_start);
    }

    public void setWaypoint(GameObject waypoint)
    {
        way_points.Add(waypoint);
    }
}