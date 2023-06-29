using UnityEngine;
using UnityEngine.UI;

public class ClientSpeech : MonoBehaviour
{
    public Image desireGift;
    public Image desireColor;
    public ClientBehavior client;

    private void Start()
    {
        desireGift.color = client.GetDesiredGift().GetComponent<Renderer>().sharedMaterial.color;
        desireColor.sprite = client.GetDesiredColor() switch
        {
            "YellowGift" => Resources.Load<Sprite>("Sprites/SantaGoldColor"),
            "SantaRedGift" => Resources.Load<Sprite>("Sprites/SantaRedColor"),
            "BlueGift" => Resources.Load<Sprite>("Sprites/SnowflakeBlueColor"),
            "SilverGift" => Resources.Load<Sprite>("Sprites/TreeSilverColor"),
            "GreenGift" => Resources.Load<Sprite>("Sprites/TreeGreenColor"),
            "SnowflakeRedGift" => Resources.Load<Sprite>("Sprites/SnowflakeRedColor"),
            _ => Resources.Load<Sprite>("Sprites/TreeGreenColor")
        };
        print(
            client.GetDesiredColor() + " -> " +
            desireColor.sprite);
    }
}