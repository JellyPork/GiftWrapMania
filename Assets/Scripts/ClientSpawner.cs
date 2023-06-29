using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    public GameObject clientToSpawn;
    public GameObject wayPoint;
    private Test_script clientMove;
    private GameObject spawnedObject;


    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (spawnedObject == null) spawnedObject = Instantiate(clientToSpawn, transform.position, transform.rotation);
        clientMove = spawnedObject.GetComponent<Test_script>();
        clientMove.setWaypoint(wayPoint);
    }
}