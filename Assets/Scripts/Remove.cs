using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Remove : MonoBehaviour
{
    public Sprite[] sprites;
    public Button remove1Button;
    public Button remove2Button;

    private GemTypes remove1 = GemTypes.Yellow;
    private GemTypes remove2 = GemTypes.Unknown;
    private Board board;
    // Start is called before the first frame update
    void Start() {
        board = FindObjectOfType<Board>();
    }

    public void RemoveBlue1() {
        remove1Button.GetComponent<Image>().sprite = sprites[0];
        remove1 = GemTypes.Blue;
    }
    public void RemoveBrown1() {
        remove1Button.GetComponent<Image>().sprite = sprites[1];
        remove1 = GemTypes.Brown;
    }
    public void RemoveElemental1() {
        remove1Button.GetComponent<Image>().sprite = sprites[2];
        remove1 = GemTypes.ElementalStar;

    }
    public void RemoveWildcard1() {
        remove1Button.GetComponent<Image>().sprite = sprites[3];
        remove1 = GemTypes.WildCard;

    }
    public void RemoveGreen1() {
        remove1Button.GetComponent<Image>().sprite = sprites[4];
        remove1 = GemTypes.Green;
    }
    public void RemovePurple1() {
        remove1Button.GetComponent<Image>().sprite = sprites[5];
        remove1 = GemTypes.Purple;
    }
    public void RemoveRed1() {
        remove1Button.GetComponent<Image>().sprite = sprites[6];
        remove1 = GemTypes.Red;
    }
    public void RemoveSkull1() {
        remove1Button.GetComponent<Image>().sprite = sprites[7];
        remove1 = GemTypes.Skull;
    }
    public void RemoveSkull51() {
        remove1Button.GetComponent<Image>().sprite = sprites[8];
        remove1 = GemTypes.Skull5;
    }
    public void RemoveUmbral1() {
        remove1Button.GetComponent<Image>().sprite = sprites[9];
        remove1 = GemTypes.UmbralStar;
    }
    public void RemoveYellow1() {
        remove1Button.GetComponent<Image>().sprite = sprites[10];
        remove1 = GemTypes.Yellow;
    }

    public void RemoveUnknown1() {
        remove1Button.GetComponent<Image>().sprite = sprites[17];
        remove1 = GemTypes.Unknown;
    }
    public void RemoveUnknown2() {
        remove2Button.GetComponent<Image>().sprite = sprites[17];
        remove2 = GemTypes.Unknown;
    }

    public void RemoveBlue2() {
        remove2Button.GetComponent<Image>().sprite = sprites[0];
        remove2 = GemTypes.Blue;
    }
    public void RemoveBrown2() {
        remove2Button.GetComponent<Image>().sprite = sprites[1];
        remove2 = GemTypes.Brown;
    }
    public void RemoveElemental2() {
        remove2Button.GetComponent<Image>().sprite = sprites[2];
        remove2 = GemTypes.ElementalStar;

    }
    public void RemoveWildcard2() {
        remove2Button.GetComponent<Image>().sprite = sprites[3];
        remove2 = GemTypes.WildCard;

    }
    public void RemoveGreen2() {
        remove2Button.GetComponent<Image>().sprite = sprites[4];
        remove2 = GemTypes.Green;
    }
    public void RemovePurple2() {
        remove2Button.GetComponent<Image>().sprite = sprites[5];
        remove2 = GemTypes.Purple;
    }
    public void RemoveRed2() {
        remove2Button.GetComponent<Image>().sprite = sprites[6];
        remove2 = GemTypes.Red;
    }
    public void RemoveSkull2() {
        remove2Button.GetComponent<Image>().sprite = sprites[7];
        remove2 = GemTypes.Skull;
    }
    public void RemoveUmbral2() {
        remove2Button.GetComponent<Image>().sprite = sprites[9];
        remove2 = GemTypes.UmbralStar;
    }
    public void RemoveYellow2() {
        remove2Button.GetComponent<Image>().sprite = sprites[10];
        remove2 = GemTypes.Yellow;
    }

    public void RemoveGems() {
        board.Remove(remove1, remove2);
    }
}
