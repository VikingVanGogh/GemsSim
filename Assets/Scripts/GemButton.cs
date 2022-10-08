using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemButton : MonoBehaviour
{
    private Board board;
    public Button HighlightButton;
    public Button brownGemButton;
    public Button redGemButton;
    public Button greenGemButton;
    public Button yellowGemButton;
    public Button skullGemButton;
    public Button purpleGemButton;
    public Button blueGemButton;
    public Button wildCardGemButton;
    public Button skull5GemButton;
    public Button blockGemButton;
    public Button gremlinGemButton;
    public Button wishGemButton;
    public Button umbralStarGemButton;
    public Button elementalStarGemButton;

    public Button redGiantButton;
    public Button yellowGiantButton;
    public Button greenGiantButton;
    public Button blueGiantButton;
    public Button brownGiantButton;
    public Button purpleGiantButton;

    public Color highlightedColor;
    public Color nonHighlightedColor;
    private ColorBlock cb;
    private ColorBlock ncb;
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        cb = HighlightButton.colors;
        cb.normalColor = HighlightButton.colors.normalColor;

        ncb = brownGemButton.colors;
        ncb.pressedColor = brownGemButton.colors.pressedColor;
        ncb.highlightedColor = brownGemButton.colors.highlightedColor;
        ncb.normalColor = brownGemButton.colors.normalColor;

        brownGemButton.colors = ncb;
        redGemButton.colors = ncb;
        greenGemButton.colors = ncb;
        yellowGemButton.colors = ncb;
        skullGemButton.colors = ncb;
        purpleGemButton.colors = ncb;
        blueGemButton.colors = ncb;
        wildCardGemButton.colors = ncb;
        skull5GemButton.colors = ncb;
        blockGemButton.colors = ncb;
        gremlinGemButton.colors = ncb;
        //------new explodygems
        redGiantButton.colors = ncb;
        yellowGiantButton.colors = ncb;
        greenGiantButton.colors = ncb;
        blueGiantButton.colors = ncb;
        brownGiantButton.colors = ncb;
        purpleGiantButton.colors = ncb;
    }    
    public void BrownGem() {
        board.UpdateSelectedGem(GemTypes.Brown);
        ChangeButtonColor(GemTypes.Brown);
    }

    private void ChangeButtonColor(GemTypes gem) {
        
        brownGemButton.colors = ncb;
        redGemButton.colors = ncb;
        greenGemButton.colors = ncb;
        yellowGemButton.colors = ncb;
        skullGemButton.colors = ncb;
        purpleGemButton.colors = ncb;
        blueGemButton.colors = ncb;
        wildCardGemButton.colors = ncb;
        skull5GemButton.colors = ncb;
        gremlinGemButton.colors = ncb;
        blockGemButton.colors = ncb;
        wishGemButton.colors = ncb;
        elementalStarGemButton.colors = ncb;
        umbralStarGemButton.colors = ncb;

        redGiantButton.colors = ncb;
        yellowGiantButton.colors = ncb;
        greenGiantButton.colors = ncb;
        blueGiantButton.colors = ncb;
        brownGiantButton.colors = ncb;
        purpleGiantButton.colors = ncb;


        switch (gem) {
            case GemTypes.Brown:
                brownGemButton.colors = cb;
                break;
            case GemTypes.Red:
                redGemButton.colors = cb;
                break;
            case GemTypes.Green:
                greenGemButton.colors = cb;
                break;
            case GemTypes.Yellow:
                yellowGemButton.colors = cb;
                break;
            case GemTypes.Skull:
                skullGemButton.colors = cb;
                break;
            case GemTypes.Purple:
                purpleGemButton.colors = cb;
                break;
            case GemTypes.Blue:
                blueGemButton.colors = cb;
                break;
            case GemTypes.WildCard:
                wildCardGemButton.colors = cb;
                break;
            case GemTypes.Skull5:
                skull5GemButton.colors = cb;
                break;
            case GemTypes.Gremlin:
                gremlinGemButton.colors = cb;
                break;
            case GemTypes.Block:
                blockGemButton.colors = cb;
                break;
            case GemTypes.WishGem:
                wishGemButton.colors = cb;
                break;
            case GemTypes.UmbralStar:
                umbralStarGemButton.colors = cb;
                break;
            case GemTypes.ElementalStar:
                elementalStarGemButton.colors = cb;
                break;

            case GemTypes.RedGiant:
                redGiantButton.colors = cb;
                break;
            case GemTypes.YellowGiant:
                yellowGiantButton.colors = cb;
                break;
            case GemTypes.GreenGiant:
                greenGiantButton.colors = cb;
                break;
            case GemTypes.BlueGiant:
                blueGiantButton.colors = cb;
                break;
            case GemTypes.BrownGiant:
                brownGiantButton.colors = cb;
                break;
            case GemTypes.PurpleGiant:
                purpleGiantButton.colors = cb;
                break;

        }
        

    }

    public void RedGem() {
        board.UpdateSelectedGem(GemTypes.Red);
        ChangeButtonColor(GemTypes.Red);
    }
    public void YellowGem() {
        board.UpdateSelectedGem(GemTypes.Yellow);
        ChangeButtonColor(GemTypes.Yellow);
    }
    public void BlueGem() {
        board.UpdateSelectedGem(GemTypes.Blue);
        ChangeButtonColor(GemTypes.Blue);
    }
    public void GreenGem() {
        board.UpdateSelectedGem(GemTypes.Green);
        ChangeButtonColor(GemTypes.Green);
    }
    public void PurpleGem() {
        board.UpdateSelectedGem(GemTypes.Purple);
        ChangeButtonColor(GemTypes.Purple);
    }
    public void SkullGem() {
        board.UpdateSelectedGem(GemTypes.Skull);
        ChangeButtonColor(GemTypes.Skull);
    }
    public void Skull5Gem() {
        board.UpdateSelectedGem(GemTypes.Skull5);
        ChangeButtonColor(GemTypes.Skull5);
    }
    public void WildCard() {
        board.UpdateSelectedGem(GemTypes.WildCard);
        ChangeButtonColor(GemTypes.WildCard);
    }
    public void GremlinGem() {
        board.UpdateSelectedGem(GemTypes.Gremlin);
        ChangeButtonColor(GemTypes.Gremlin);
    }
    public void BlockGem() {
        board.UpdateSelectedGem(GemTypes.Block);
        ChangeButtonColor(GemTypes.Block);
    }

    public void WishGem() {
        board.UpdateSelectedGem(GemTypes.WishGem);
        ChangeButtonColor(GemTypes.WishGem);
    }

    public void ElementalGem() {
        board.UpdateSelectedGem(GemTypes.ElementalStar);
        ChangeButtonColor(GemTypes.ElementalStar);
    }

    public void UmbralStarGem() {
        board.UpdateSelectedGem(GemTypes.UmbralStar);
        ChangeButtonColor(GemTypes.UmbralStar);
    }

    public void RedGiant() {
        board.UpdateSelectedGem(GemTypes.RedGiant);
        ChangeButtonColor(GemTypes.RedGiant);
    }
    public void YellowGiant() {
        board.UpdateSelectedGem(GemTypes.YellowGiant);
        ChangeButtonColor(GemTypes.YellowGiant);
    }
    public void GreenGiant() {
        board.UpdateSelectedGem(GemTypes.GreenGiant);
        ChangeButtonColor(GemTypes.GreenGiant);
    }
    public void BlueGiant() {
        board.UpdateSelectedGem(GemTypes.BlueGiant);
        ChangeButtonColor(GemTypes.BlueGiant);
    }
    public void BrownGiant() {
        board.UpdateSelectedGem(GemTypes.BrownGiant);
        ChangeButtonColor(GemTypes.BrownGiant);
    }
    public void PurpleGiant() {
        board.UpdateSelectedGem(GemTypes.PurpleGiant);
        ChangeButtonColor(GemTypes.PurpleGiant);
    }




}
