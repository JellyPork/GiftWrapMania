using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform camPos;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = camPos.position;
    }
}