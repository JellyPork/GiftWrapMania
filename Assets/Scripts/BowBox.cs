using UnityEngine;

public class BowBox : MonoBehaviour
{
    public GameObject GiftBoxAllowedForBow;

    public GameObject newBox; // nuevo objeto que tomara el lugar del otro

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        var parentObjectOfOther = other.gameObject.transform.root.gameObject; //saca el objeto padre   
        if (other.gameObject.CompareTag("Bowable") &&
            parentObjectOfOther.gameObject.CompareTag(GiftBoxAllowedForBow
                .tag)) //&& Object.ReferenceEquals(parentObjectOfOther,giftBoxAllowedForBow))
        {
            //si tiene el tag bowable el objeto y el objeto con el que contacto es un tipo de regalo que puede tener el moño.

            //crea el objeto dado en newBox en la ubicacion del objeto padre
            var instantiatedBox = Instantiate(newBox, Vector3.up + parentObjectOfOther.transform.position,
                parentObjectOfOther.transform.rotation);
            var newBoxItemScript = instantiatedBox.GetComponent<ItemInBox>();
            var oldBoxItemScript = parentObjectOfOther.GetComponent<ItemInBox>(); //el objeto guardado en un script
            //pasarselo a el otro.

            if (newBoxItemScript != null)
                newBoxItemScript.setData(oldBoxItemScript.getData()); //pasar el objeto guardado de uno al otro
            Destroy(gameObject);
            Destroy(parentObjectOfOther); //destruye los objetos
        }
    }
}