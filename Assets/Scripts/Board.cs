using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState {
    wait,
    move
}

public enum GemTypes {
    Unknown, Red, Skull, Yellow, Green, Blue, Purple, Brown, Copper, Silver, Gold, Bag, 
    Chest, GreenChest, RedChest, Vault, Block, WildCard, Gremlin, Skull10, WishGem, Skull5, Black, 
    UmbralStar, ElementalStar, YellowGiant, GreenGiant, BlueGiant, RedGiant, BrownGiant, PurpleGiant
}
public struct Match4PlusMoves{
    public int x1;
    public int y1;
    public int x2;
    public int y2;
    public int count;
    public int purple;
    public int green;
    public int brown;
    public int blue;
    public int red;
    public int yellow;
    public int skull;
    public int skull5;
    public string color;
    public void Clear() {
        x1 = 0;
        y1 = 0;
        x2 = 0;
        y2 = 0;
        count = 0;
        purple = 0;
        green = 0;
        brown = 0;
        blue = 0;
        red = 0;
        yellow = 0;
        skull = 0;
        skull5 = 0;
        color = null;
    }
}
public struct GemTypeColor {
    public bool brown;
    public bool red;
    public bool green;
    public bool yellow;
    public bool skull;
    public bool purple;
    public bool blue;
    public bool block;
}

public struct GemAmounts {
    public int brown;
    public int red;
    public int green;
    public int yellow;
    public int skull;
    public int purple;
    public int blue;
    public int wildCard;
    public int skull5;
    public int block;
    public int gremlin;
    public int wishGem;
    public int umbraStar;
    public int elementalStar;

    public int redGiant;
    public int yellowGiant;
    public int greenGiant;
    public int blueGiant;
    public int brownGiant;
    public int purpleGiant;
}


public class Board : MonoBehaviour {

    private const string WILDCARD = "Wildcard Gem";
    private const string GREMLIN = "Gremlin Gem";
    private const string BLOCK = "Block Gem";
    private const string WISH = "Wish Gem";
    private const string SKULL10 = "SKull10 Gem";
    private const string SKULL5 = "Skull5 Gem";
    private const string SKULL = "Skull Gem";
    private const string RED = "Red Gem";
    private const string BLUE = "Blue Gem";
    private const string YELLOW = "Yellow Gem";
    private const string PURPLE = "Purple Gem";
    private const string BROWN = "Brown Gem";
    private const string GREEN = "Green Gem";
    private const string UMBRAL = "UmbralStar Gem";
    private const string ELEMENTAL = "ElementalStar Gem";
    private const string UNKOWN = "Unknown Gem";

    private const string RED_GIANT = "Red Giant";
    private const string YELLOW_GIANT = "Yellow Giant";
    private const string GREEN_GIANT = "Green Giant";
    private const string BLUE_GIANT = "Blue Giant";
    private const string BROWN_GIANT = "Brown Giant";
    private const string PURPLE_GIANT = "Purple Giant";

    public GameState currentState = GameState.move;
    public int width;
    public int height;
    public int offSet;
    public TextMeshProUGUI numberOfMatches;
    public GameObject ExtraTurn;
    public TextMeshProUGUI brown;
    public TextMeshProUGUI red;
    public TextMeshProUGUI green;
    public TextMeshProUGUI yellow;
    public TextMeshProUGUI skull;
    public TextMeshProUGUI purple;
    public TextMeshProUGUI blue;
    public TextMeshProUGUI wildCard;
    public TextMeshProUGUI skull5;
    public TextMeshProUGUI block;
    public TextMeshProUGUI gremlin;
    public TextMeshProUGUI wishGem;
    public TextMeshProUGUI umbraStar;
    public TextMeshProUGUI elementalStar;

    public TextMeshProUGUI redGiant;
    public TextMeshProUGUI yellowGiant;
    public TextMeshProUGUI greenGiant;
    public TextMeshProUGUI blueGiant;
    public TextMeshProUGUI brownGiant;
    public TextMeshProUGUI purpleGiant;


    public float colorOffSet;
    public GameObject colorPrefab;
    public GameObject tilePrefab;
    public GameObject[] gems;
    public GameObject destroyParticle;
    private BackgroundTile[,] allTiles;
    private int[,] gemInts;
    public GameObject[,] allGems;
    public GameObject[,] allCircles;
    public GameObject[] circles;
    public GameObject[,] allColors;
    public Gem currentGem;
    private FindMatches findMatches;
    public bool play;
    public Color color = Color.red;
    public GemTypes selectedGem;
    public float WAIT_TIME = .5f;
    private int[,] saveInts;
    List<Match4PlusMoves> moves = new List<Match4PlusMoves>();



    // Use this for initialization
    void Start() {
        findMatches = FindObjectOfType<FindMatches>();
        allTiles = new BackgroundTile[width, height];
        allGems = new GameObject[width, height];
        allCircles = new GameObject[width, height];
        saveInts = new int[width, height];
        allColors = new GameObject[width, height];
        gemInts = new int[width, height];
        selectedGem = GemTypes.Unknown;
        play = true;
    //SetUp();
}

    private void SetUp() {
        DestroyCircles();
        ExtraTurn.SetActive(false);
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if(allGems[i, j] != null)
                    Destroy(allGems[i,j]);
                allGems[i,j]=null;
            }
        }
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (gemInts[i, j] < gems.Length ) {
                    /*
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                    backgroundTile.transform.parent = this.transform;
                    backgroundTile.name = "( " + i + ", " + j + " )";

                    GameObject gem = Instantiate(gems[gemInts[j, i]], tempPosition, Quaternion.identity);
                    gem.GetComponent<Gem>().row = j;
                    gem.GetComponent<Gem>().column = i;
                    gem.transform.parent = this.transform;
                    gem.name = "( " + i + ", " + j + " )";
                    SetColors(gem)
                    */
                    allGems[i, j] = GemFromType(i, j,GetGem(gemInts[j, i]));
                }
            }
        }
        SaveBoard();

    }
    public void FindMatch4Plus() {
        SaveBoard();
        DestroyCircles();

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    Match4PlusMoves move1 = new Match4PlusMoves();
                    MoveUp(i, j, ref move1);
                    Match4PlusMoves move2 = new Match4PlusMoves();
                    MoveLeft(i, j, ref move2);
                    Match4PlusMoves move3 = new Match4PlusMoves();
                    MoveRight(i, j, ref move3);
                    Match4PlusMoves move4 = new Match4PlusMoves();
                    MoveDown(i, j, ref move4);

                }
            }
        }
        if (moves.Count > 0) {
            for (int i = 0; i < moves.Count; i++) {
                if (allGems[moves[i].x1, moves[i].y1].tag !=BLOCK && allGems[moves[i].x2, moves[i].y2].tag != BLOCK) {
                    CreateCircles(moves[i].x1, moves[i].y1);
                    CreateCircles(moves[i].x2, moves[i].y2);
                }
            }
            moves.Clear();
        }
        ResetBoard();
    }
    private void DestroyCircles() {            
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allCircles[i, j] != null) {
                    Destroy(allCircles[i, j]);
                }
            }
        }
    }
    private void CreateCircles(int x, int y) {
        if (allCircles[x,y]==null) {
            Vector2 tempPosition = new Vector2(x, y);
            GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
            backgroundTile.transform.parent = this.transform;
            backgroundTile.name = "( " + x + ", " + y + " )";

            GameObject circle = Instantiate(circles[0],tempPosition, Quaternion.identity) as GameObject;
            circle.transform.parent = this.transform;
            circle.name = "( " + x + ", " + y + " )";
            allCircles[x, y] = circle;
            //Debug.Log("circle"+x+", " + y + ", created");
        }
    }
    private void MoveUp(int x, int y,ref Match4PlusMoves move) {
        if (y+1<height && allGems[x, y + 1]!=null) {
            GameObject tempGem = allGems[x, y + 1];
            bool matchFound = false;
            allGems[x, y + 1] = allGems[x, y];
            allGems[x, y] = tempGem;
            move.Clear();
            findMatches.CheckMatch4Plus(ref move);
            DestroyMatchesCheck(ref matchFound, ref move);
            if (matchFound) {
                move.x1 = x;
                move.y1 = y;
                move.x2 = x;
                move.y2 = y+1;
                moves.Add(move);
            }
            ResetBoardCheck();
        }
       
    }

    private void MoveDown(int x, int y, ref Match4PlusMoves move) {
        if (y - 1 >=0  && allGems[x, y - 1] != null) {
            GameObject tempGem = allGems[x, y - 1];
            bool matchFound = false;
            allGems[x, y - 1] = allGems[x, y];
            allGems[x, y] = tempGem;
            move.Clear();
            findMatches.CheckMatch4Plus(ref move);
            DestroyMatchesCheck(ref matchFound, ref move);
            if (matchFound) {
                move.x1 = x;
                move.y1 = y;
                move.x2 = x;
                move.y2 = y - 1;
                moves.Add(move);
            }
            ResetBoardCheck();
        }
    }

    private void MoveLeft(int x, int y, ref Match4PlusMoves move) {
        if (x - 1 >= 0 && allGems[x-1, y] != null) {
            GameObject tempGem = allGems[x-1, y];
            bool matchFound = false;
            allGems[x-1, y] = allGems[x, y];
            allGems[x, y] = tempGem;
            move.Clear();
            findMatches.CheckMatch4Plus(ref move);
            DestroyMatchesCheck(ref matchFound, ref move);
            if (matchFound) {
                move.x1 = x;
                move.y1 = y;
                move.x2 = x-1;
                move.y2 = y;
                moves.Add(move);
            }
            ResetBoardCheck();
        }
    }
    private void MoveRight(int x, int y, ref Match4PlusMoves move) {
        if (x + 1 < width && allGems[x + 1, y] != null) {
            GameObject tempGem = allGems[x + 1, y];
            bool matchFound = false;
            allGems[x + 1, y] = allGems[x, y];
            allGems[x, y] = tempGem;
            move.Clear();
            findMatches.CheckMatch4Plus(ref move);
            DestroyMatchesCheck(ref matchFound, ref move);
            if (matchFound) {
                move.x1 = x;
                move.y1 = y;
                move.x2 = x+1;
                move.y2 = y;
                moves.Add(move);
            }
            ResetBoardCheck();
        }
    }

    public void StartExplode(GemTypes explode1,GemTypes explode2, GemTypes explode3) {
        ExtraTurn.SetActive(false);
        DestroyCircles();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    if (allGems[i, j].tag == GetTag(explode1)) {
                        findMatches.BlowUpBlock(i,j);
                    }else if (allGems[i, j].tag == GetTag(explode2)) {
                        findMatches.BlowUpBlock(i, j);
                    } else if (allGems[i, j].tag == GetTag(explode3)) {
                        findMatches.BlowUpBlock(i, j);
                    }
                }
            }
        }
        DestroyMatches();
    }
    private GemTypeColor SetColors(string tag) {
        GemTypeColor colors = new GemTypeColor();
        colors.blue = false;
        colors.purple = false;
        colors.green = false;
        colors.yellow = false;
        colors.red = false;
        colors.brown = false;
        colors.skull = false;
        colors.block = false;
        switch (tag) {
            case BLUE:
            case BLUE_GIANT:
                colors.blue = true;
                break;
            case GREEN:
            case GREEN_GIANT:
                colors.green = true;
                break;
            case YELLOW:
            case YELLOW_GIANT:
                colors.yellow = true;
                break;
            case RED:
            case RED_GIANT:
                colors.red = true;
                break;
            case PURPLE:
            case PURPLE_GIANT:
                colors.purple= true;
                break;
            case BROWN:
            case BROWN_GIANT:
                colors.brown = true;
                break;
            case SKULL:
            case SKULL5:
            case SKULL10:
                colors.skull = true;
                break;
            case WILDCARD:
                colors.blue = true;
                colors.purple = true;
                colors.green = true;
                colors.yellow = true;
                colors.red = true;
                colors.brown = true;
                break;
            case ELEMENTAL:
                colors.blue = true;
                colors.green = true;
                colors.red = true;
                colors.brown = true;
                break;
            case UMBRAL:
                colors.purple = true;
                colors.yellow = true;
                break;
            case BLOCK:
                colors.block = true;
                break;
        }
        return colors;
    }
    private bool ColorsMatch(GemTypeColor gem1, GemTypeColor gem2) {

        if (gem1.blue && gem2.blue) {
            return true;
        }
        if (gem1.yellow && gem2.yellow) {
            return true;
        }
        if (gem1.red && gem2.red) {
            return true;
        }
        if (gem1.brown && gem2.brown) {
            return true;
        }
        if (gem1.purple && gem2.purple) {
            return true;
        }
        if (gem1.green && gem2.green) {
            return true;
        }
        if (gem1.skull && gem2.skull) {
            return true;
        }

        return false;
    }
    private bool SameColor(string tag1, string tag2) {
        GemTypeColor color1 = SetColors(tag1);
        GemTypeColor color2 = SetColors(tag2);
        if (ColorsMatch(color1,color2)) {
            return true;
        }
        return false;
    }
    public void StartConvert(GemTypes convert, GemTypes convertTo, GemTypes convert2, GemTypes convertTo2) {
        DestroyCircles();
        UnmatchGems();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    if (SameColor(allGems[i,j].tag, GetTag(convert))) {
                        Destroy(allGems[i, j]);
                        allGems[i, j] = GemFromType(i, j, convertTo);
                    }
                    if (SameColor(allGems[i, j].tag, GetTag(convert2))) {
                        Destroy(allGems[i, j]);
                        allGems[i, j] = GemFromType(i, j, convertTo2);
                    }
                }

            }
        }
        SetAmounts();
        findMatches.FindAllMatches();
        DestroyMatches();
    }
    private string GetTag(GemTypes convert) {
        switch (convert) {
            case GemTypes.Blue:
                return BLUE;
                break;
            case GemTypes.Yellow:
                return YELLOW;
                break;
            case GemTypes.Green:
                return GREEN;
                break;
            case GemTypes.Red:
                return RED;
                break;
            case GemTypes.Purple:
                return PURPLE;
                break;
            case GemTypes.Brown:
                return BROWN;
                break;
            case GemTypes.Skull:
                return SKULL;
                break;
            case GemTypes.Skull5:
                return SKULL5;
                break;
            case GemTypes.WildCard:
                return WILDCARD;
                break;
            case GemTypes.ElementalStar:
                return ELEMENTAL;
                break;
            case GemTypes.UmbralStar:
                return UMBRAL;
                break;

            case GemTypes.RedGiant:
                return RED_GIANT;
                break;
            case GemTypes.YellowGiant:
                return YELLOW_GIANT;
                break;
            case GemTypes.GreenGiant:
                return GREEN_GIANT;
                break;
            case GemTypes.BlueGiant:
                return BLUE_GIANT;
                break;
            case GemTypes.BrownGiant:
                return BROWN_GIANT;
                break;
            case GemTypes.PurpleGiant:
                return PURPLE_GIANT;
                break;
            case GemTypes.Block:
                return BLOCK;
                break;
        }
        return UNKOWN;
    }

    private void SetColors(GameObject gem) {
        /*
        switch (gem.tag) {
            case BLUE:
                gem.GetComponent<Gem>().Blue = true;
                break;
            case YELLOW:
                gem.GetComponent<Gem>().Yellow = true;
                break;
            case RED:
                gem.GetComponent<Gem>().Red = true;
                break;
            case BROWN:
                gem.GetComponent<Gem>().Brown = true;
                break;
            case SKULL:
                gem.GetComponent<Gem>().Skull = true;
                break;
            case SKULL5:
                gem.GetComponent<Gem>().Skull = true;
                break;
            case GREEN:
                gem.GetComponent<Gem>().Green = true;
                break;
            case PURPLE:
                gem.GetComponent<Gem>().Purple = true;
                break;

            case WILDCARD:
                gem.GetComponent<Gem>().Blue = true;
                gem.GetComponent<Gem>().Yellow = true;
                gem.GetComponent<Gem>().Red = true;
                gem.GetComponent<Gem>().Brown = true;
                gem.GetComponent<Gem>().Green = true;
                gem.GetComponent<Gem>().Purple = true;
                break;

            case UMBRAL:
                gem.GetComponent<Gem>().Purple = true;
                gem.GetComponent<Gem>().Yellow = true;
                break;

            case ELEMENTAL:
                gem.GetComponent<Gem>().Blue = true;
                gem.GetComponent<Gem>().Green = true;
                gem.GetComponent<Gem>().Red = true;
                gem.GetComponent<Gem>().Brown = true;
                break;

        }
        */
    }
    public float  GetWaitTime() {
        return WAIT_TIME;
    }

    public void UpdateSelectedGem(GemTypes gem) {
        selectedGem = gem;
    }
    public GameObject SelectedGem(int j, int i) {
        UnmatchGems();
        GameObject gem =new GameObject();
        gem =GemFromType(j, i, selectedGem);
        //Debug.Log("gem.tag = "+gem.tag);
        return gem;
    }

    private void  UnmatchGems() {
        ExtraTurn.SetActive(false);
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    allGems[i, j].GetComponent<Gem>().isMatched = false;
                }
            }
        }
    }
    private GameObject GemFromType(int j, int i, GemTypes gemType) {
        Vector2 tempPosition = new Vector2(i, j + offSet);
        GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
        backgroundTile.transform.parent = this.transform;
        backgroundTile.name = "( " + i + ", " + j + " )";

        GameObject gem = Instantiate(gems[SetInt(gemType)], tempPosition, Quaternion.identity);
        gem.GetComponent<Gem>().row = i;
        gem.GetComponent<Gem>().column = j;
        gem.transform.parent = this.transform;
        gem.name = "( " + i + ", " + j + " )";
        allGems[j, i] = gem;
        SetAmounts();
        SetColors(gem);
        return gem;
    }
    private void SetAmounts() {
        GemAmounts gems = new GemAmounts();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    SetGemAMounts(ref gems,allGems[i, j]);
                }
            }
        }
        block.text = gems.block.ToString();
        blue.text = gems.blue.ToString();
        brown.text = gems.brown.ToString();
        green.text = gems.green.ToString();
        skull.text = gems.skull.ToString();
        skull5.text = gems.skull5.ToString();
        yellow.text = gems.yellow.ToString();
        purple.text = gems.purple.ToString();
        red.text = gems.red.ToString();
        elementalStar.text = gems.elementalStar.ToString();
        umbraStar.text = gems.umbraStar.ToString();
        wishGem.text = gems.wishGem.ToString();
        wildCard.text = gems.wildCard.ToString();
        gremlin.text = gems.gremlin.ToString();

        redGiant.text = gems.redGiant.ToString();
        yellowGiant.text = gems.yellowGiant.ToString();
        greenGiant.text = gems.greenGiant.ToString();
        blueGiant.text = gems.blueGiant.ToString();
        brownGiant.text = gems.brownGiant.ToString();
        purpleGiant.text = gems.purpleGiant.ToString();
    }

    public void SetBoardColors(Color[,] colors, GemTypes[,] gems) {
        //Debug.Log("Color = " + color);
        float xPos = 0;
        float yPos = 5.5f;
        Vector2 tempPosition;
       // Debug.Log("in SetBoardColors");
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {

                if (allColors[i, j] != null) {
                    Destroy(allColors[i, j]);
                }                 
                
                allColors[i, j] = null;

                Destroy(allGems[i, j]);
                allGems[i, j] = null;
            }
        }
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (j == 0) {
                    xPos = 8;
                } else if (j > 0) {
                    xPos += colorOffSet;
                }
                tempPosition = new Vector2(xPos, yPos);
                GameObject gem = Instantiate(colorPrefab, tempPosition, Quaternion.identity);
                gem.transform.parent = this.transform;
                gem.GetComponent<SpriteRenderer>().color = colors[i, j];
                gem.name = "( color " + i + ", " + j + " )";
                //Debug.Log("( color " + i + ", " + j + " ) color = " + gem.GetComponent<Renderer>().material.color);
                allColors[i, j] = gem;
                gemInts[i, j] = SetInt(gems[i, j]);
            }
            yPos += colorOffSet;
        }
        SetUp();
    }

    private void SetGemAMounts(ref GemAmounts gems, GameObject gem) {
        switch (gem.tag) {
            case BLUE:
                gems.blue++;
                break;
            case BROWN:
                gems.brown++;
                break;
            case GREEN:
                gems.green++;
                break;
            case PURPLE:
                gems.purple++;
                break;
            case RED:
                gems.red++;
                break;
            case SKULL:
                gems.skull++;
                break;
            case YELLOW:
                gems.yellow++;
                break;
            case GREMLIN:
                gems.gremlin++;
                break;
            case WILDCARD:
                gems.wildCard++;
                break;
            case SKULL5:
                gems.skull5++;
                break;
            case UMBRAL:
                gems.umbraStar++;
                break;
            case ELEMENTAL:
                gems.elementalStar++;
                break;


            case RED_GIANT:
                gems.redGiant++;
                break;
            case YELLOW_GIANT:
                gems.yellowGiant++;
                break;
            case GREEN_GIANT:
                gems.greenGiant++;
                break;
            case BLUE_GIANT:
                gems.blueGiant++;
                break;
            case BROWN_GIANT:
                gems.brownGiant++;
                break;
            case PURPLE_GIANT:
                gems.purpleGiant++;
                break;
        }
    }

    private int SetInt(GemTypes gem) {
        int i = 15;
        switch (gem) {
            case GemTypes.Blue:
                i = 0;
                break;
            case GemTypes.Brown:
                i = 1;
                break;
            case GemTypes.Green:
                i = 2;
                break;
            case GemTypes.Purple:
                i = 3;
                break;
            case GemTypes.Red:
                i = 4;
                break;
            case GemTypes.Skull:
                i = 5;
                break;
            case GemTypes.Yellow:
                i = 6;
                break;
            case GemTypes.Skull10:
                i = 7;
                break;
                /*
            case GemTypes.Mana:
                i = 8;
                */
            case GemTypes.Gremlin:
                i = 9;
                break;
                /*
            case GemTypes.Burning:
                i = 10;
                break;
            case GemTypes.PurpleWolf:
                i = 11;
                break;
                */
            case GemTypes.WildCard:
                i = 12;
                break;
            case GemTypes.WishGem:
                i = 13;
                break;

            case GemTypes.Skull5:
                i = 14;
                break;
            case GemTypes.Block:
                i = 16;
                break;
            case GemTypes.UmbralStar:
                i = 17;
                break;
            case GemTypes.ElementalStar:
                i = 18;
                break;

            case GemTypes.RedGiant:
                i = 19;
                break;
            case GemTypes.YellowGiant:
                i = 20;
                break;
            case GemTypes.GreenGiant:
                i = 21;
                break;
            case GemTypes.BlueGiant:
                i = 22;
                break;
            case GemTypes.BrownGiant:
                i = 23;
                break;
            case GemTypes.PurpleGiant:
                i = 24;
                break;
        }
        return i;
    }

    private GemTypes GetGem(int i) {
        GemTypes type = GemTypes.Unknown;
        switch (i) {
            case 0:
                type = GemTypes.Blue;
                break;
            case 1:
                type = GemTypes.Brown;
                break;
            case 2:
                type = GemTypes.Green;
                break;
            case 3:
                type = GemTypes.Purple;
                break;
            case 4:
                type = GemTypes.Red;
                break;
            case 5:
                type = GemTypes.Skull;
                break;
            case 6: 
                type =GemTypes.Yellow;
                break;
            case 7:
                type = GemTypes.Skull10;
                break;
            /*
        case GemTypes.Mana:
            i = 8;
            */
            case 9:
                type = GemTypes.Gremlin;
                break;
            /*
        case GemTypes.Burning:
            i = 10;
            break;
        case GemTypes.PurpleWolf:
            i = 11;
            break;
            */
            case 12:
                type = GemTypes.WildCard;
                break;
            case 13:
                type = GemTypes.WishGem;
                break;

            case 14:
                type = GemTypes.Skull5;
                break;
            case 16:
                type = GemTypes.Block;
                break;
            case 17:
                type = GemTypes.UmbralStar;
                break;
            case 18:
                type = GemTypes.ElementalStar;
                break;

            case 19:
                type = GemTypes.RedGiant;
                break;
            case 20:
                type = GemTypes.YellowGiant;
                break;

            case 21:
                type = GemTypes.GreenGiant;
                break;
            case 22:
                type = GemTypes.BlueGiant;
                break;
            case 23:
                type = GemTypes.BrownGiant;
                break;
            case 24:
                type = GemTypes.PurpleGiant;
                break;

        }
        return type;
    }



    private void DestroyMatchesAtCheck(int x, int y) {
        if (allGems[x, y].GetComponent<Gem>().isMatched) {        
            Destroy(allGems[x, y]);
            allGems[x, y] = null;
        }
    }

    private void DestroyMatchesAt(int column, int row) {

        if (allGems[column, row].GetComponent<Gem>().isMatched) {
            //How many elements are in the matched pieces list from findmatches?
            if (findMatches.currentMatches.Count == 4 || findMatches.currentMatches.Count == 7) {
                //findMatches.CheckBombs();
            }
            SpriteRenderer mysprite = allGems[column,row].GetComponent<SpriteRenderer>();
            mysprite.color = new Color(225f, 225f, 225f, .4f);
            
         

            // allGems[column, row].GetComponent<Renderer>().material.color.a = .5f;
            Destroy(allGems[column, row], GetWaitTime());  
            
            allGems[column, row] = null;
        }
    }

    public void DestroyMatches() {
        Match4PlusMoves move = new Match4PlusMoves();
        numberOfMatches.text = "0";
        findMatches.CheckMatch4Plus(ref move);
        DestroyCircles();
        if (move.count > 3) {
            numberOfMatches.text = move.count.ToString();
            ExtraTurn.SetActive(true);
        }
        CheckForExplodies();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    DestroyMatchesAt(i, j);
                   
                }
            }
        }
        SetAmounts();
        findMatches.currentMatches.Clear();
        //Wait();
        StartCoroutine(DecreaseRowCo());
        
    }
    private void CheckForExplodies() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    if (allGems[i, j].GetComponent<Gem>().explodable && allGems[i, j].GetComponent<Gem>().isMatched) {
                        findMatches.BlowUpBlock(i, j);
                    }
                }
            }
        }
    }


    public void DestroyMatchesCheck(ref bool matchFound, ref Match4PlusMoves move) {
        findMatches.CheckMatch4Plus(ref move);
        if (move.count > 3) {
            numberOfMatches.text = move.count.ToString();
            matchFound = true;
        }
        CheckForExplodies();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    DestroyMatchesAtCheck(i, j);
                }
            }
        }
        findMatches.currentMatches.Clear();
        //Wait();
        DecreaseRowNoCo(ref matchFound, ref move);
        

    }
    public void SaveBoard() {

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if(allGems[i, j] != null) {
                    saveInts[i, j] = SaveInts(allGems[i, j]);
                } else {
                    saveInts[i, j] = 15;
                }               
            }
        }
    }

    private int SaveInts(GameObject gem) {
        int i = 15;
        string tag = gem.tag;
       // Debug.Log("tag  = "+tag);
        switch (tag) {
            case BLUE :
                i = 0;
                break;
            case BROWN :
                i = 1;
                break;
            case GREEN:
                i = 2;
                break;
            case PURPLE:
                i = 3;
                break;
            case RED:
                i = 4;
                break;
            case SKULL:
                i = 5;
                break;
            case YELLOW:
                i = 6;
                break;
            case SKULL10:
                i = 7;
                break;
            case GREMLIN:
                i = 9;
                break;
            ////***************////
            case WILDCARD:
                i = 12;
                break;
            case WISH:
                i = 13;
                break;

            case SKULL5:
                i = 14;
                break;
            case BLOCK:
                i = 16;
                break;
            case UMBRAL:
                i = 17;
                break;
            case ELEMENTAL:
                i = 18;
                break;

            case RED_GIANT:
                i = 19;
                break;
            case YELLOW_GIANT:
                i = 20;
                break;

            case GREEN_GIANT:
                i = 21;
                break;
            case BLUE_GIANT:
                i = 22;
                break;
            case BROWN_GIANT:
                i = 23;
                break;
            case PURPLE_GIANT:
                i = 24;
                break;

        }
        return i;
    }
    public void Remove(GemTypes remove1, GemTypes remove2) {
        DestroyCircles();
        UnmatchGems();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    if (SameColor(allGems[i, j].tag, GetTag(remove1))) {
                        allGems[i, j].GetComponent<Gem>().isMatched = true;
                    }
                    if (SameColor(allGems[i, j].tag, GetTag(remove2))) {
                        allGems[i, j].GetComponent<Gem>().isMatched = true;
                    }
                }   
            }
        }
        findMatches.FindAllMatches();
        DestroyMatches();
    }

    public void ResetBoard() {
        ExtraTurn.SetActive(false);
        // Debug.Log("in SetBoardColors");
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {

                Destroy(allColors[i, j]);
                allColors[i, j] = null;

                Destroy(allGems[i, j]);
                allGems[i, j] = null;
            }

        }

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (gemInts[i, j] < gems.Length) {
                    /*
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                    backgroundTile.transform.parent = this.transform;
                    backgroundTile.name = "( " + i + ", " + j + " )";

                    GameObject gem = Instantiate(gems[saveInts[i, j]], tempPosition, Quaternion.identity);
                    gem.GetComponent<Gem>().row = j;
                    gem.GetComponent<Gem>().column = i;
                    gem.transform.parent = this.transform;
                    gem.name = "( " + i + ", " + j + " )";
                    SetColors(gem);
                    */

                    allGems[i, j] = GemFromType(i, j, GetGem(saveInts[i, j]));
                }
            }
        }
        UnmatchGems();
    }

    public void ResetBoardCheck() {
        // Debug.Log("in SetBoardColors");
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {

                Destroy(allColors[i, j]);
                allColors[i, j] = null;

                Destroy(allGems[i, j]);
                allGems[i, j] = null;
            }
        }

        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (gemInts[i, j] < gems.Length) {
                    GameObject backgroundTile = Instantiate(tilePrefab) as GameObject;
                    backgroundTile.transform.parent = this.transform;
                    backgroundTile.name = "( " + i + ", " + j + " )";
                    GameObject gem = Instantiate(gems[saveInts[i, j]]);
                    gem.GetComponent<Gem>().row = j;
                    gem.GetComponent<Gem>().column = i;
                    gem.transform.parent = this.transform;
                    gem.name = "( " + i + ", " + j + " )";
                    //SetColors(gem);
                    allGems[i, j] = gem;
                }
            }
        }
        UnmatchGems();
    }

    private void DecreaseRowNoCo(ref bool matchFound, ref Match4PlusMoves move) {
        int nullCount = 0;
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] == null) {
                    nullCount++;
                } else if (nullCount > 0) {
                    if (allGems[i, j] != null) {
                        allGems[i, j].GetComponent<Gem>().row -= nullCount;
                        if (allGems[i, j - nullCount] != null) {
                            Destroy(allGems[i, j - nullCount]);
                        }
                        allGems[i, j - nullCount] = null;
                        allGems[i, j - nullCount] = allGems[i, j];
                        Destroy(allGems[i, j]);
                        allGems[i, j] = null;
                    } else {
                        allGems[i, j] = null;
                    }

                }
            }
            nullCount = 0;
        }
       FillBoardNoCo(ref matchFound, ref  move);
    }

    private IEnumerator DecreaseRowCo() {
        int nullCount = 0;
        yield return new WaitForSeconds(GetWaitTime()+ GetWaitTime());
        for (int i = 0; i < width; i++) {
            
            for (int j = 0; j < height; j++) {
                
                if (allGems[i, j] == null) {
                    nullCount++;
                } else if (nullCount > 0) {
                    allGems[i, j].GetComponent<Gem>().row -= nullCount;
                    allGems[i, j] = null;
                   
                }
                
            }
            nullCount = 0;
        }

        
        StartCoroutine(FillBoardCo());
        yield return new WaitForSeconds(GetWaitTime());
    }

    private void RefillBoard() {
        
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] == null) {
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    int gemToUse = Random.Range(0, gems.Length-8);
                    GameObject piece = Instantiate(gems[gemToUse], tempPosition, Quaternion.identity);
                    allGems[i, j] = piece;
                    piece.GetComponent<Gem>().row = j;
                    piece.GetComponent<Gem>().column = i;

                }
            }
        }
        
        
    }


    private bool MatchesOnBoard() {
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    if (allGems[i, j].GetComponent<Gem>().isMatched) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void FillBoardNoCo(ref bool matchFound, ref Match4PlusMoves moves) {
        //RefillBoard();
        findMatches.FindAllMatchesCheck();
        while (MatchesOnBoard()) {
            
            DestroyMatchesCheck(ref matchFound, ref moves);
        }
        findMatches.currentMatches.Clear();
    }

    private IEnumerator FillBoardCo() {
        //RefillBoard();

        yield return new WaitForSeconds(GetWaitTime());
        while (MatchesOnBoard()) {
            
            DestroyMatches();
            yield return new WaitForSeconds(GetWaitTime());
        }
        
        findMatches.currentMatches.Clear();
        currentGem = null;
        currentState = GameState.move;

    }
}