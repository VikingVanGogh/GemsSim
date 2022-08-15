using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explode : MonoBehaviour
{
    public Sprite[] sprites;
    public Button ExplodeButton1;
    public Button ExplodeButton2;
    public Button ExplodeButton3;

    private GemTypes explode1 = GemTypes.Unknown;
    private GemTypes explode2 = GemTypes.Unknown;
    private GemTypes explode3 = GemTypes.Unknown;
    private Board board;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }
    public void BlueExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[0];
        explode1 = GemTypes.Blue;
    }
    public void BlueExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[0];
        explode2 = GemTypes.Blue;
    }
    public void BlueExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[0];
        explode3 = GemTypes.Blue;
    }

    public void BrownExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[1];
        explode1 = GemTypes.Brown;
    }
    public void BrownExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[1];
        explode2 = GemTypes.Brown;
    }
    public void BrownExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[1];
        explode3 = GemTypes.Brown;
    }

    public void ElementalStarExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[2];
        explode1 = GemTypes.ElementalStar;
    }
    public void ElementalStarExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[2];
        explode2 = GemTypes.ElementalStar;
    }
    public void ElementalStarExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[2];
        explode3 = GemTypes.ElementalStar;
    }

    public void WildCardExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[3];
        explode1 = GemTypes.WildCard;
    }
    public void WildCardExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[3];
        explode2 = GemTypes.WildCard;
    }
    public void WildCardExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[3];
        explode3 = GemTypes.WildCard;
    }

    public void GreenExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[4];
        explode1 = GemTypes.Green;
    }
    public void GreenExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[4];
        explode2 = GemTypes.Green;
    }
    public void GreenExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[4];
        explode3 = GemTypes.Green;
    }

    public void PurpleExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[5];
        explode1 = GemTypes.Purple;
    }
    public void PurpleExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[5];
        explode2 = GemTypes.Purple;
    }
    public void PurpleExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[5];
        explode3 = GemTypes.Purple;
    }

    public void RedExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[6];
        explode1 = GemTypes.Red;
    }
    public void RedExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[6];
        explode2 = GemTypes.Red;
    }
    public void RedExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[6];
        explode3 = GemTypes.Red;
    }

    public void SkullExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[7];
        explode1 = GemTypes.Skull;
    }
    public void SkullExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[7];
        explode2 = GemTypes.Skull;
    }
    public void SkullExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[7];
        explode3 = GemTypes.Skull;
    }

    public void Skull5Explode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[8];
        explode1 = GemTypes.Skull5;
    }
    public void Skull5Explode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[8];
        explode2 = GemTypes.Skull5;
    }
    public void Skull5Explode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[8];
        explode3 = GemTypes.Skull5;
    }

    public void UmbralStarExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[9];
        explode1 = GemTypes.UmbralStar;
    }
    public void UmbralStarExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[9];
        explode2 = GemTypes.UmbralStar;
    }
    public void UmbralStarExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[9];
        explode3 = GemTypes.UmbralStar;
    }

    public void YellowExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[10];
        explode1 = GemTypes.Yellow;
    }
    public void YellowExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[10];
        explode2 = GemTypes.Yellow;
    }
    public void YellowExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[10];
        explode3 = GemTypes.Yellow;
    }
    public void UnknownExplode1() {
        ExplodeButton1.GetComponent<Image>().sprite = sprites[11];
        explode1 = GemTypes.Unknown;
    }
    public void UnknownExplode2() {
        ExplodeButton2.GetComponent<Image>().sprite = sprites[11];
        explode2 = GemTypes.Unknown;
    }
    public void UnknownExplode3() {
        ExplodeButton3.GetComponent<Image>().sprite = sprites[11];
        explode3 = GemTypes.Unknown;
    }
    public void StartExplode() {
        board.StartExplode(explode1,explode2,explode3);

    }

}
