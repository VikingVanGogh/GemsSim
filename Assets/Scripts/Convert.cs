using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Convert : MonoBehaviour
{
    public Sprite[] sprites;
    public Button convertButton;
    public Button convertToButton;
    private GemTypes convert =GemTypes.Yellow;
    private GemTypes convertTo = GemTypes.Purple;
    private Board board;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }
    public void ConvertBlue() {
        convertButton.GetComponent<Image>().sprite = sprites[0];
        convert = GemTypes.Blue;
    }
    public void ConvertBrown() {
        convertButton.GetComponent<Image>().sprite = sprites[1];
        convert = GemTypes.Brown;
    }
    public void ConvertElemental() {
        convertButton.GetComponent<Image>().sprite = sprites[2];
        convert = GemTypes.ElementalStar;

    }
    public void ConvertWildcard() {
        convertButton.GetComponent<Image>().sprite = sprites[3];
        convert = GemTypes.WildCard;

    }
    public void ConvertGreen() {
        convertButton.GetComponent<Image>().sprite = sprites[4];
        convert = GemTypes.Green;
    }
    public void ConvertPurple() {
        convertButton.GetComponent<Image>().sprite = sprites[5];
        convert = GemTypes.Purple;
    }
    public void ConvertRed() {
        convertButton.GetComponent<Image>().sprite = sprites[6];
        convert = GemTypes.Red;
    }
    public void ConvertSkull() {
        convertButton.GetComponent<Image>().sprite = sprites[7];
        convert = GemTypes.Skull;
    }
    public void ConvertSkull5() {
        convertButton.GetComponent<Image>().sprite = sprites[8];
        convert = GemTypes.Skull5;
    }
    public void ConvertUmbral() {
        convertButton.GetComponent<Image>().sprite = sprites[9];
        convert = GemTypes.UmbralStar;
    }
    public void ConvertYellow() {
        convertButton.GetComponent<Image>().sprite = sprites[10];
        convert = GemTypes.Yellow;
    }

    //
    public void ConvertToBlue() {
        convertToButton.GetComponent<Image>().sprite = sprites[0];
        convertTo = GemTypes.Blue;
    }
    public void ConvertToBrown() {
        convertToButton.GetComponent<Image>().sprite = sprites[1];
        convertTo = GemTypes.Brown;
    }
    public void ConvertToElemental() {
        convertToButton.GetComponent<Image>().sprite = sprites[2];
        convertTo = GemTypes.ElementalStar;

    }
    public void ConvertToWildcard() {
        convertToButton.GetComponent<Image>().sprite = sprites[3];
        convertTo = GemTypes.WildCard;

    }
    public void ConvertToGreen() {
        convertToButton.GetComponent<Image>().sprite = sprites[4];
        convertTo = GemTypes.Green;
    }
    public void ConvertToPurple() {
        convertToButton.GetComponent<Image>().sprite = sprites[5];
        convertTo = GemTypes.Purple;
    }
    public void ConvertToRed() {
        convertToButton.GetComponent<Image>().sprite = sprites[6];
        convertTo = GemTypes.Red;
    }
    public void ConvertToSkull() {
        convertToButton.GetComponent<Image>().sprite = sprites[7];
        convertTo = GemTypes.Skull;
    }
    public void ConvertToSkull5() {
        convertToButton.GetComponent<Image>().sprite = sprites[8];
        convertTo = GemTypes.Skull5;
    }
    public void ConvertToUmbral() {
        convertToButton.GetComponent<Image>().sprite = sprites[9];
        convertTo = GemTypes.UmbralStar;
    }
    public void ConvertToYellow() {
        convertToButton.GetComponent<Image>().sprite = sprites[10];
        convertTo = GemTypes.Yellow;
    }
    public void StartConvert() {
        board.StartConvert(convert,convertTo);
    }
}
