using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    wait,
    move
}

public enum GemTypes {
    Unknown, Red, Skull, Yellow, Green, Blue, Purple, Brown, Copper, Silver, Gold, Bag, Chest, GreenChest, RedChest, Vault, Block, WildCard, Gremlin, Skull10, WishGem, Skull5, Black, UmbralStar, ElementalStar
}


public class Board : MonoBehaviour {


    public GameState currentState = GameState.move;
    public int width;
    public int height;
    public int offSet;

    public float colorOffSet;
    public GameObject colorPrefab;
    public GameObject tilePrefab;
    public GameObject[] gems;
    public GameObject destroyParticle;
    private BackgroundTile[,] allTiles;
    private int[,] gemInts;
    public GameObject[,] allGems;
    public GameObject[,] allColors;
    public Gem currentGem;
    private FindMatches findMatches;
    public bool play;
    public Color color = Color.red;
   public GemTypes selectedGem;
    public float WAIT_TIME = .5f;
    private int[,] saveInts;



    // Use this for initialization
    void Start() {
        findMatches = FindObjectOfType<FindMatches>();
        allTiles = new BackgroundTile[width, height];
        allGems = new GameObject[width, height];
        saveInts = new int[width, height];
        allColors = new GameObject[width, height];
        gemInts = new int[width, height];
        selectedGem = GemTypes.Unknown;
        play = true;
    //SetUp();
}

    private void SetUp() {

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
                    allGems[i, j] = gem;
                }
            }
        }
        SaveBoard();

    }
    public float  GetWaitTime() {
        return WAIT_TIME;
    }

    public void UpdateSelectedGem(GemTypes gem) {
        selectedGem = gem;
    }
    public GameObject SelectedGem(int j, int i) {
        Vector2 tempPosition = new Vector2(i, j + offSet);
        GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
        backgroundTile.transform.parent = this.transform;
        backgroundTile.name = "( " + i + ", " + j + " )";

        GameObject gem = Instantiate(gems[SetInt(selectedGem)], tempPosition, Quaternion.identity);
        gem.GetComponent<Gem>().row = i;
        gem.GetComponent<Gem>().column = j;
        gem.transform.parent = this.transform;
        gem.name = "( " + i + ", " + j + " )";
        allGems[j, i] = gem;
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

    private bool MatchesAt(int column, int row, GameObject piece) {
        if (column > 1 && row > 1) {
            if (allGems[column - 1, row].tag == piece.tag && allGems[column - 2, row].tag == piece.tag) {
                return true;
            }
            if (allGems[column, row - 1].tag == piece.tag && allGems[column, row - 2].tag == piece.tag) {
                return true;
            }

        } else if (column <= 1 || row <= 1) {
            if (row > 1) {
                if (allGems[column, row - 1].tag == piece.tag && allGems[column, row - 2].tag == piece.tag) {
                    return true;
                }
            }
            if (column > 1) {
                if (allGems[column - 1, row].tag == piece.tag && allGems[column - 2, row].tag == piece.tag) {
                    return true;
                }
            }
        }

        return false;
    }

    private void DestroyMatchesAt(int column, int row) {

        if (allGems[column, row].GetComponent<Gem>().isMatched) {
            //How many elements are in the matched pieces list from findmatches?
            if (findMatches.currentMatches.Count == 4 || findMatches.currentMatches.Count == 7) {
                //findMatches.CheckBombs();
            }
            SpriteRenderer mysprite = allGems[column,row].GetComponent<SpriteRenderer>();
            mysprite.color = new Color(225f, 225f, 225f, .4f);
            
            GameObject particle = Instantiate(destroyParticle,
                                              allGems[column, row].transform.position,
                                              Quaternion.identity);

            // allGems[column, row].GetComponent<Renderer>().material.color.a = .5f;
            Destroy(allGems[column, row], GetWaitTime());

            Destroy(particle, GetWaitTime());
            
           

            
            allGems[column, row] = null;
        }
    }

    public void DestroyMatches() {
        
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
    private IEnumerator Wait() {
        yield return new WaitForSeconds(GetWaitTime()+ GetWaitTime());
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
                    allGems[i, j] = gem;


                }
            }
        }


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
        /*
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
        */
        
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

    private IEnumerator FillBoardCo() {
        RefillBoard();

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