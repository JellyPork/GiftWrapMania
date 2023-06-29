using UnityEngine;

public class ClientBehavior : MonoBehaviour
{
    [Header("Could Want")] [SerializeField]
    private GameObject[] gifts; //regalos que podria desear

    [SerializeField] private string[] giftColor; //color de regalo que podria desear

    public GameObject iWantGift; //por el momento seran public (para probar, cambiar despues)

    public string iWantColor;

    //public String
    // Start is called before the first frame update
    private void Start()
    {
        var decider = Random.Range(0, gifts.Length);
        iWantGift = gifts[decider];
        decider = Random.Range(0, giftColor.Length); //decidir de manera aleatoria lo que quiere
        iWantColor = giftColor[decider];
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public GameObject GetDesiredGift() //para obtener el regalo que quiere 
    {
        return iWantGift;
    }

    public string GetDesiredColor() //para obtener el color del regalo que quiere
    {
        return iWantColor;
    }
}