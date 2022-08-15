using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState {
    wait,
    move
}

public enum GemTypes {
    Unknown, Red, Skull, Yellow, Green, Blue, Purple, Brown, Copper, Silver, Gold, Bag, Chest, GreenChest, RedChest, Vault, Block, WildCard, Gremlin, Skull10, WishGem, Skull5, Black, UmbralStar, ElementalStar
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


public class Board : MonoBehaviour {

    private const string WILDCARD = "Wildcard Gem";
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

    public GameState currentState = GameState.move;
    public int width;
    public int height;
    public int offSet;
    public TextMeshProUGUI numberOfMatches;

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
                    Vector2 tempPosition = new Vector2(i, j + offSet);
                    GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                    backgroundTile.transform.parent = this.transform;
                    backgroundTile.name = "( " + i + ", " + j + " )";

                    GameObject gem = Instantiate(gems[gemInts[j, i]], tempPosition, Quaternion.identity);
                    gem.GetComponent<Gem>().row = j;
                    gem.GetComponent<Gem>().column = i;
                    gem.transform.parent = this.transform;
                    gem.name = "( " + i + ", " + j + " )";
                    SetColors(gem);
                    allGems[i, j] = gem;
                }
            }
        }
        SaveBoard();

    }
    public void FindMatch4Plus() {
        SaveBoard();
        DestroyCircles();
        play = false;
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
                CreateCircles(moves[i].x1, moves[i].y1);
                CreateCircles(moves[i].x2, moves[i].y2);
            }
            moves.Clear();
        }
        ResetBoard();
        play = true;
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
    public void StartConvert(GemTypes convert, GemTypes convertTo) {
        DestroyCircles();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    if (allGems[i,j].tag == GetTag(convert)) {
                        Destroy(allGems[i, j]);
                        allGems[i, j] = GemFromType(i, j, convertTo);
                    }
                }

            }
        }
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
        }
        return "Unknown Gem";
    }

    private void SetColors(GameObject gem) {
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
        SetColors(gem);
        return gem;
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
        }
        return i;
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
        DestroyCircles();
        Match4PlusMoves move = new Match4PlusMoves();
        numberOfMatches.text = "0";
        findMatches.CheckMatch4Plus(ref move);
        if (move.count > 3) {
            numberOfMatches.text = move.count.ToString();
            move.Clear();
        }


        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                if (allGems[i, j] != null) {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        findMatches.currentMatches.Clear();
        //Wait();
        StartCoroutine(DecreaseRowCo());
        
    }

    public void DestroyMatchesCheck(ref bool matchFound, ref Match4PlusMoves move) {

        findMatches.CheckMatch4Plus(ref move);
        if (move.count > 3) {
            numberOfMatches.text = move.count.ToString();
            matchFound = true;
        }

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
            case "Blue Gem"  :
                i = 0;
                break;
            case "Brown Gem"  :
                i = 1;
                break;
            case "Green Gem":
                i = 2;
                break;
            case "Purple Gem":
                i = 3;
                break;
            case "Red Gem":
                i = 4;
                break;
            case "Skull Gem":
                i = 5;
                break;
            case "Yellow Gem":
                i = 6;
                break;
            case "Skull10 Gem":
                i = 7;
                break;
            case "Gremlin Gem":
                i = 9;
                break;
            ////***************////
            case "Wildcard Gem":
                i = 12;
                break;
            case "Wish Gem":
                i = 13;
                break;

            case "Skull5 Gem":
                i = 14;
                break;
            case "Block Gem":
                i = 16;
                break;
            case "UmbralStar Gem":
                i = 17;
                break;
            case "ElementalStar Gem":
                i = 18;
                break;

        }
        return i;
    }

    public void ResetBoard() {
        
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
                    allGems[i, j] = gem;
                }
            }
        }
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
                    SetColors(gem);
                    allGems[i, j] = gem;
                }
            }
        }
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