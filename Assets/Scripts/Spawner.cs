using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    private GameObject spawnedObject;


    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (spawnedObject == null) spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
    }
}