using UnityEngine;

public class CloseBox : MonoBehaviour
{
    public GameObject newBox; // nuevo objeto que tomara el lugar del otro
    // Start is called before the first frame update

    private string itemToSave;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Closeable")) //si el objeto con el que interactua puede ser cerrado
        {
            itemToSave = gameObject.name;
            var parentObjectOfOther = other.gameObject.transform.root.gameObject; //saca el objeto padre 
            Destroy(gameObject);
            Destroy(parentObjectOfOther); //destruye los objetos
            var instantiatedBox = Instantiate(newBox, parentObjectOfOther.transform.position,
                parentObjectOfOther.transform.rotation);
            var newBoxScript = instantiatedBox.GetComponent<ItemInBox>();

            if (newBoxScript != null) newBoxScript.setData(itemToSave);
        }
    }
}