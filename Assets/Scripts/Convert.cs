using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Convert : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite playSprite;
    public Sprite stopSprite;
    public Button convertButton;
    public Button convertToButton;
    public Button convertButton2;
    public Button convertToButton2;
    public GameObject panel;
    public Button PlayStopButton;
    private GemTypes convert =GemTypes.Yellow;
    private GemTypes convertTo = GemTypes.Purple;
    private GemTypes convert2 = GemTypes.Unknown;
    private GemTypes convertTo2 = GemTypes.Unknown;
    private Board board;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        PlayStopButton.GetComponent<Image>().color = new Color32(239, 0, 0, 225);
        board.play = true;
    }
    public void PlayStop() {
        if (board.play) {
            PlayStopButton.GetComponent<Image>().sprite = playSprite;
            PlayStopButton.GetComponent<Image>().color = new Color32(225, 225, 225, 225);
            board.play = false;
            panel.SetActive(true);
           
        } else  {
            PlayStopButton.GetComponent<Image>().sprite = stopSprite;
            board.play = true;
            panel.SetActive(false);
        }
    }
    public void ConvertBlue() {
        convertButton.GetComponent<Image>().sprite = sprites[0];
        convert = GemTypes.Blue;
    }
    public void ConvertUnknown2() {
        convertButton2.GetComponent<Image>().sprite = sprites[17];
        convert2 = GemTypes.Unknown;
    }
    public void ConvertToUnknown2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[17];
        convertTo2 = GemTypes.Unknown;
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
    /// 2nd convert buttons
    public void ConvertBlue2() {
        convertButton2.GetComponent<Image>().sprite = sprites[0];
        convert2 = GemTypes.Blue;
    }
    public void ConvertBrown2() {
        convertButton2.GetComponent<Image>().sprite = sprites[1];
        convert2 = GemTypes.Brown;
    }
    public void ConvertElemental2() {
        convertButton2.GetComponent<Image>().sprite = sprites[2];
        convert2 = GemTypes.ElementalStar;

    }
    public void ConvertWildcard2() {
        convertButton2.GetComponent<Image>().sprite = sprites[3];
        convert2 = GemTypes.WildCard;

    }
    public void ConvertGreen2() {
        convertButton2.GetComponent<Image>().sprite = sprites[4];
        convert2 = GemTypes.Green;
    }
    public void ConvertPurple2() {
        convertButton2.GetComponent<Image>().sprite = sprites[5];
        convert2 = GemTypes.Purple;
    }
    public void ConvertRed2() {
        convertButton2.GetComponent<Image>().sprite = sprites[6];
        convert2 = GemTypes.Red;
    }
    public void ConvertSkull2() {
        convertButton2.GetComponent<Image>().sprite = sprites[7];
        convert2 = GemTypes.Skull;
    }
    public void ConvertSkull5_2() {
        convertButton2.GetComponent<Image>().sprite = sprites[8];
        convert2 = GemTypes.Skull5;
    }
    public void ConvertUmbral2() {
        convertButton2.GetComponent<Image>().sprite = sprites[9];
        convert2 = GemTypes.UmbralStar;
    }
    public void ConvertYellow2() {
        convertButton2.GetComponent<Image>().sprite = sprites[10];
        convert2 = GemTypes.Yellow;
    }

    //
    public void ConvertToBlue2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[0];
        convertTo2 = GemTypes.Blue;
    }
    public void ConvertToBrown2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[1];
        convertTo2 = GemTypes.Brown;
    }
    public void ConvertToElemental2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[2];
        convertTo2 = GemTypes.ElementalStar;

    }
    public void ConvertToWildcard2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[3];
        convertTo2 = GemTypes.WildCard;

    }
    public void ConvertToGreen2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[4];
        convertTo2 = GemTypes.Green;
    }
    public void ConvertToPurple2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[5];
        convertTo2 = GemTypes.Purple;
    }
    public void ConvertToRed2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[6];
        convertTo2 = GemTypes.Red;
    }
    public void ConvertToSkull2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[7];
        convertTo2 = GemTypes.Skull;
    }
    public void ConvertToSkull5_2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[8];
        convertTo2 = GemTypes.Skull5;
    }
    public void ConvertToUmbral2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[9];
        convertTo2 = GemTypes.UmbralStar;
    }
    public void ConvertToYellow2() {
        convertToButton2.GetComponent<Image>().sprite = sprites[10];
        convertTo2 = GemTypes.Yellow;
    }

    public void StartConvert2() {
        board.StartConvert(convert,convertTo, convert2, convertTo2);
    }
}
