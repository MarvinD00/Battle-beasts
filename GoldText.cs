using UnityEngine;
using UnityEngine.UI;
public class GoldText : MonoBehaviour
{
    public Text goldText;
    public Image goldImage;
    
    void Update() {
        goldText.text = GameManager.player.gold + "G";
    }
}
