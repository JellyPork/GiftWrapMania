using UnityEngine;

public class ItemInBox : MonoBehaviour
{
    [SerializeField] public string savedItem;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void setData(string data)
    {
        savedItem = data;
    }

    public string getData()
    {
        return savedItem;
    }
}