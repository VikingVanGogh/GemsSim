using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour {

    private Board board;
    private const string WILDCARD = "Wildcard Gem";
    private const string SKULL5 = "Skull5 Gem";
    private const string SKULL = "Skull Gem";
    private const string RED = "Red Gem";
    private const string BLUE = "Blue Gem";
    private const string YELLOW = "Yellow Gem";
    private const string PURPLE = "Purple Gem";
    private const string BROWN = "Brown Gem";
    private const string GREEN = "Green Gem";

    private const string WISH = "Wish Gem";
    private const string GREMLIN= "Gremlin Gem";
    private const string UMBRAL = "UmbralStar Gem";
    private const string ELEMENTAL = "ElementalStar Gem";



    public List<GameObject> currentMatches = new List<GameObject>();

    // Use this for initialization
    void Start() {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches() {
        StartCoroutine(FindAllMatchesCo());
    }

    private List<GameObject> IsRowBomb(Gem gem1, Gem gem2, Gem gem3) {
        List<GameObject> currentGems = new List<GameObject>();
        if (gem1.isRowBomb) {
            currentMatches.Union(GetRowPieces(gem1.row));
        }
        if (gem2.isRowBomb) {
            currentMatches.Union(GetRowPieces(gem2.row));
        }
        if (gem3.isRowBomb) {
            currentMatches.Union(GetRowPieces(gem3.row));
        }
        return currentGems;
    }

    private List<GameObject> IsColumnBomb(Gem gem1, Gem gem2, Gem gem3) {
        List<GameObject> currentGems = new List<GameObject>();
        if (gem1.isColumnBomb) {
            currentMatches.Union(GetColumnPieces(gem1.column));
        }

        if (gem2.isColumnBomb) {
            currentMatches.Union(GetColumnPieces(gem2.column));
        }

        if (gem3.isColumnBomb) {
            currentMatches.Union(GetColumnPieces(gem3.column));
        }
        return currentGems;
    }

    private void AddToListAndMatch(GameObject gem) {
        if (!currentMatches.Contains(gem)) {
            currentMatches.Add(gem);
        }

        gem.GetComponent<Gem>().isMatched = true;
    }
    private void BlowupBlock(int x, int y) {
        int left = x - 1;
        int right = x + 1;
        int up = y + 1;
        int down = y - 1;
        //up
        if (CheckArray(x, up) && board.allGems[x, up] != null && !currentMatches.Contains(board.allGems[x, up])) {
            currentMatches.Add(board.allGems[x, up]);
            board.allGems[x, up].GetComponent<Gem>().isMatched = true;
            CheckForSkull5(x, up);
        }
        //down
        if (CheckArray(x, down) && board.allGems[x, down] != null && !currentMatches.Contains(board.allGems[x, down])) {
            currentMatches.Add(board.allGems[x, down]);
            board.allGems[x, down].GetComponent<Gem>().isMatched = true;
            CheckForSkull5(x, down);
        }
        //left
        if (CheckArray(left, y) && board.allGems[left, y] != null && !currentMatches.Contains(board.allGems[left, y])) {
            currentMatches.Add(board.allGems[left, y]);
            board.allGems[left, y].GetComponent<Gem>().isMatched = true;
            CheckForSkull5(left, y);
        }

        //down left
        if (CheckArray(left, down) && board.allGems[left, down] != null && !currentMatches.Contains(board.allGems[left, down])) {
            currentMatches.Add(board.allGems[left, down]);
            board.allGems[left, down].GetComponent<Gem>().isMatched = true;
            CheckForSkull5(left, down);
        }
        //up left
        if (CheckArray(left, up) && board.allGems[left, up] != null && !currentMatches.Contains(board.allGems[left, up])) {
            currentMatches.Add(board.allGems[left, up]);
            CheckForSkull5(left, up);

        }
        //Debug.Log("board x = " + board.width + "right =" + right);
        //down right
        if (CheckArray(right, down) && board.allGems[right, down] != null && !currentMatches.Contains(board.allGems[right, down])) {
            currentMatches.Add(board.allGems[right, down]);
            board.allGems[right, down].GetComponent<Gem>().isMatched = true;
            CheckForSkull5(right, down);
        }

        //up right
        if (CheckArray(right, up) && board.allGems[right, up] != null && !currentMatches.Contains(board.allGems[right, up])) {
            currentMatches.Add(board.allGems[right, up]);
            board.allGems[right, up].GetComponent<Gem>().isMatched = true;
            CheckForSkull5(right, up);
        }

        //right
        if (CheckArray(right, y) && board.allGems[right, y] != null && !currentMatches.Contains(board.allGems[right, y])) {
            currentMatches.Add(board.allGems[right, y]);
            board.allGems[right, y].GetComponent<Gem>().isMatched = true;
            CheckForSkull5(right, y);
        }

        //up
        if (CheckArray(x, up) && board.allGems[x, up] != null ) {
            board.allGems[x, up].GetComponent<Gem>().isMatched = true;
        }
        //down
        if (CheckArray(x, down) && board.allGems[x, down] != null ) {
            board.allGems[x, down].GetComponent<Gem>().isMatched = true;
        }
        //left
        if (CheckArray(left, y) && board.allGems[left, y] != null ) {
            board.allGems[left, y].GetComponent<Gem>().isMatched = true;
        }
        //right
        if (CheckArray(right, y) && board.allGems[right, y] != null ) {
            board.allGems[right, y].GetComponent<Gem>().isMatched = true;
        }
        //down left
        if (CheckArray(left, down) && board.allGems[left, down] != null) {
            board.allGems[left, down].GetComponent<Gem>().isMatched = true;
        }

        //down right
        if (CheckArray(right, down) && board.allGems[right, down] != null ) {
            board.allGems[right,down].GetComponent<Gem>().isMatched = true;
        }

        //up right
        if (CheckArray(right, up) && board.allGems[right, up] != null ) {
            board.allGems[right, up].GetComponent<Gem>().isMatched = true;
        }

        //up left
        if (CheckArray(left,up) && board.allGems[left, up] != null ) {
            board.allGems[left, up].GetComponent<Gem>().isMatched = true;
            
        }

    }
    private bool CheckArray(int x, int y) {
        if (x < board.width && !(x >= board.height) && !(x < 0)) {
            //Debug.Log("board x = " + board.width + " , x = " + x);
            if (y < board.height && !(y >= board.height) && !(y < 0)) {
                return true;
            }
        }
        return false;

    }

    private void CheckForSkull5(int x, int y) {
        if (board.allGems[x,y].tag == SKULL5) {
            BlowupBlock(x,y);
        }
    }

    private void Skull5Match(GameObject gem1,int i1, int j1, GameObject gem2, int i2 ,int j2, GameObject gem3, int i3, int j3) {
        if (gem1.tag == SKULL5) {
            BlowupBlock(i1,j1);
        }

        if (gem2.tag == SKULL5) {
            BlowupBlock( i2, j2);
        }

        if (gem3.tag == SKULL5) {
            BlowupBlock(i3, j3);
        }


    }

    private void GetNearbyPieces(GameObject gem1, GameObject gem2, GameObject gem3) {
        AddToListAndMatch(gem1);
        AddToListAndMatch(gem2);
        AddToListAndMatch(gem3);
    }

    private IEnumerator FindAllMatchesCo() {
        yield return new WaitForSeconds(.4f);
        for (int i = 0; i < board.width; i++) {
            for (int j = 0; j < board.height; j++) {
                GameObject currentGem = null;
                if (board.allGems[i, j] != null) {
                    currentGem = board.allGems[i, j];
                }
                Gem currentGemGem = null;
                if(currentGem != null && currentGem.GetComponent<Gem>()) {
                    currentGemGem = currentGem.GetComponent<Gem>();
                }
                if (currentGem != null) {
                    if (i > 0 && i < board.width - 1) {
                        GameObject leftGem = null;
                        if(board.allGems[i - 1, j] != null) {
                            leftGem = board.allGems[i - 1, j];
                        }
                        Gem leftGemGem = null;
                        if (leftGem!=null && leftGem.GetComponent<Gem>()!=null) {
                            leftGemGem = leftGem.GetComponent<Gem>();
                        }
                        GameObject rightGem = null;
                        if(board.allGems[i + 1, j] != null) {
                            rightGem = board.allGems[i + 1, j];
                        }
                        Gem rightGemGem = null;
                        if (rightGem!= null && rightGem.GetComponent<Gem>() != null) {
                            rightGemGem = rightGem.GetComponent<Gem>();
                        }

                        if (leftGem != null && rightGem != null) {
                            if (SpecialGems( leftGem,  rightGem,  currentGem)) {

                                //currentMatches.Union(IsRowBomb(leftGemGem, currentGemGem, rightGemGem));

                                //currentMatches.Union(IsColumnBomb(leftGemGem, currentGemGem, rightGemGem));
                                Skull5Match(leftGem,i-1,j, currentGem,i,j, rightGem, i + 1, j);
                                GetNearbyPieces(leftGem, currentGem, rightGem);
                                

                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1) {
                        GameObject upGem = null;
                        if(board.allGems[i, j + 1] != null) {
                            upGem = board.allGems[i, j + 1];
                        }                        
                        if (board.allGems[i, j + 1] != null) {
                            upGem = board.allGems[i, j + 1];
                        }

                        Gem upGemGem = null;
                        if (upGem != null && upGem.GetComponent<Gem>() != null) {
                            upGemGem = upGem.GetComponent<Gem>();
                        }
                        GameObject downGem = null;
                        if (board.allGems[i, j - 1] != null) {
                            downGem = board.allGems[i, j - 1];
                        }

                        Gem downGemGem = null;
                        if (downGem != null && downGem.GetComponent<Gem>()!=null) {
                            downGemGem = downGem.GetComponent<Gem>();
                        }                        
                        if (upGem != null && downGem != null) {
                            if (SpecialGems(upGem, downGem, currentGem)) {
                                //currentMatches.Union(IsColumnBomb(upGemGem, currentGemGem, downGemGem));
                                //currentMatches.Union(IsRowBomb(upGemGem, currentGemGem, downGemGem));
                                Skull5Match(upGem,i,j+1 , currentGem,i,j, downGem, i, j-1);
                                GetNearbyPieces(upGem, currentGem, downGem);
                                
                            }
                        }
                    }

                }
            }
        }

    }

    public void MatchPiecesOfColor(string color) {
        for (int i = 0; i < board.width; i++) {
            for (int j = 0; j < board.height; j++) {
                //Check if that piece exists
                if (board.allGems[i, j] != null) {
                    //Check the tag on that gem
                    if (board.allGems[i, j].tag == color) {
                        //Set that gem to be matched
                        board.allGems[i, j].GetComponent<Gem>().isMatched = true;
                    }
                }
            }
        }
    }

    private bool SpecialGems(GameObject gem1, GameObject gem2, GameObject currentGem) {
        if (Wildcard(gem1, gem2, currentGem)) {  
            return true;
        }
        if (ElementalStarGem(gem1, gem2, currentGem)) {
            return true;
        }
        if (UmbralStarGem(gem1, gem2, currentGem)) {
            return true;
        }

        if (Skull5(gem1, gem2, currentGem)) {
            return true;
        }
        if(WishGem(gem1, gem2, currentGem)) {
            return false;
        }
        if (GremlinGem(gem1, gem2, currentGem)) {
            return false;
        }

        if (gem1.tag == currentGem.tag && gem2.tag == currentGem.tag) {
            return true;
        }
        return false;
    }
    private bool WishGem(GameObject gem1, GameObject gem2, GameObject currentGem) {
        if (gem1.tag == WISH || gem2.tag == WISH || currentGem.tag == WISH) {
            return true;
        }
        return false;
    }
    private bool UmbralStarGem(GameObject gem1, GameObject gem2, GameObject currentGem) {
        if (NotSkullAndIsUmbralGem(currentGem, gem2) && (currentGem.tag == gem2.tag && gem1.tag == UMBRAL)) {
            return true;
        }
        if (currentGem.tag == UMBRAL && NotSkullAndIsUmbralGem(gem1, gem2) && gem1.tag == gem2.tag) {
            return true;
        }
        if (gem2.tag == UMBRAL && gem1.tag == currentGem.tag && NotSkullAndIsUmbralGem(gem1, currentGem)) {
            return true;
        }
        if (currentGem.tag == UMBRAL && gem2.tag == UMBRAL && UmbralGems(gem1.tag)) {
            return true;
        }
        if (UmbralGems(currentGem.tag) && gem1.tag == UMBRAL && gem2.tag == UMBRAL) {
            return true;
        }
        if (UmbralGems(gem2.tag) && gem1.tag == UMBRAL && currentGem.tag == UMBRAL) {
            return true;
        }

        return false;
    }
    private bool UmbralGems(string gem) {
        if ( gem == PURPLE || gem == YELLOW) {
            return true;
        }

        return false;

    }
    private bool ElementalGems(string gem) {
        if (gem == BLUE || gem == BROWN || gem == RED ||  gem == GREEN ) {
            return true;
        }

        return false;

    }

    private bool ElementalStarGem(GameObject gem1, GameObject gem2, GameObject currentGem) {
        if (NotSkullAndIsElementalGem(currentGem, gem2) && ( currentGem.tag == gem2.tag && gem1.tag == ELEMENTAL)) {
            return true;
        }
        if (currentGem.tag == ELEMENTAL && NotSkullAndIsElementalGem(gem1,gem2) && gem1.tag == gem2.tag) {
            return true;
        }
        if (gem2.tag == ELEMENTAL && gem1.tag == currentGem.tag  && NotSkullAndIsElementalGem(gem1, currentGem)) {
            return true;
        }
        if (currentGem.tag == ELEMENTAL && gem2.tag == ELEMENTAL && ElementalGems(gem1.tag)) {
            return true;
        }
        if (ElementalGems(currentGem.tag) && gem1.tag == ELEMENTAL && gem2.tag == ELEMENTAL) {
            return true;
        }
        if (ElementalGems(gem2.tag) && gem1.tag == ELEMENTAL && currentGem.tag == ELEMENTAL) {
            return true;
        }

        return false;
    }

    private bool NotSkull(GameObject gem1, GameObject gem2) {
        if (gem1.tag != SKULL || gem2.tag != SKULL) {
            return true;
        }
        return false;
    }
    private bool NotSkullAndIsElementalGem(GameObject gem1, GameObject gem2) {
        if (NotSkull(gem1,gem2) && (ElementalGems(gem1.tag) && ElementalGems(gem2.tag))) {
            return true;
        }
        return false;
    }

    private bool NotSkullAndIsUmbralGem(GameObject gem1, GameObject gem2) {
        if (NotSkull(gem1, gem2) && (UmbralGems(gem1.tag) && UmbralGems(gem2.tag))) {
            return true;
        }
        return false;
    }

    private bool GremlinGem(GameObject gem1, GameObject gem2, GameObject currentGem) {
        if (gem1.tag == GREMLIN || gem2.tag == GREMLIN || currentGem.tag == GREMLIN) {
            return true;
        }
        return false;
    }

    private bool Wildcard(GameObject gem1, GameObject gem2, GameObject currentGem) {
        if (NotSkull(currentGem, gem2) && (currentGem.tag == gem2.tag && gem1.tag == WILDCARD)) {
            return true;
        }
        if (currentGem.tag == WILDCARD && NotSkull(gem1, gem2) && gem1.tag == gem2.tag) {
            return true;
        }
        if (gem2.tag == WILDCARD && gem1.tag == currentGem.tag && NotSkull(gem1, currentGem)) {
            return true;
        }
        if (currentGem.tag == WILDCARD && gem2.tag == WILDCARD && WildcardGems(gem1.tag)) {
            return true;
        }
        if (WildcardGems(currentGem.tag) && gem1.tag == WILDCARD && gem2.tag == WILDCARD) {
            return true;
        }
        if (WildcardGems(gem2.tag) && gem1.tag == WILDCARD && currentGem.tag == WILDCARD) {
            return true;
        }

        return false;
    }


    private bool Skull5(GameObject gem1, GameObject gem2, GameObject currentGem) {
        if (currentGem.tag == SKULL && gem2.tag ==SKULL && gem1.tag == SKULL5) {
            return true;
        }
        if (currentGem.tag == SKULL5 && gem1.tag == SKULL && gem2.tag == SKULL) {
            return true;
        }
        if (gem2.tag == SKULL5 && gem1.tag == SKULL && currentGem.tag == SKULL) {
            return true;
        }
        if (currentGem.tag == SKULL5 && gem2.tag == SKULL5 && gem1.tag == SKULL) {
            return true;
        }
        if (currentGem.tag== SKULL && gem1.tag == SKULL5 && gem2.tag == SKULL5) {
            return true;
        }
        if (gem2.tag == SKULL && gem1.tag == SKULL5 && currentGem.tag == SKULL5) {
            return true;
        }

        return false;
    }




    private bool WildcardGems(string gem) {
        if (gem == BLUE || gem == BROWN || gem == RED || gem == PURPLE || gem == GREEN || gem == YELLOW  ) {
            return true;
        }

        return false;

    }


    List<GameObject> GetColumnPieces(int column) {
        List<GameObject> gems = new List<GameObject>();
        for (int i = 0; i < board.height; i++) {
            if (board.allGems[column, i] != null) {
                gems.Add(board.allGems[column, i]);
                board.allGems[column, i].GetComponent<Gem>().isMatched = true;
            }
        }
        return gems;
    }

    List<GameObject> GetRowPieces(int row) {
        List<GameObject> gems = new List<GameObject>();
        for (int i = 0; i < board.width; i++) {
            if (board.allGems[i, row] != null) {
                gems.Add(board.allGems[i, row]);
                board.allGems[i, row].GetComponent<Gem>().isMatched = true;
            }
        }
        return gems;
    }

    public void CheckBombs() {
        //Did the player move something?
        /*
        if (board.currentGem != null) {
            //Is the piece they moved matched?
            if (board.currentGem.isMatched) {
                //make it unmatched
                board.currentGem.isMatched = false;
                //Decide what kind of bomb to make
                
                int typeOfBomb = Random.Range(0, 100);
                if(typeOfBomb < 50){
                    //Make a row bomb
                    board.currentGem.MakeRowBomb();
                }else if(typeOfBomb >= 50){
                    //Make a column bomb
                    board.currentGem.MakeColumnBomb();
                }
                
                if ((board.currentGem.swipeAngle > -45 && board.currentGem.swipeAngle <= 45)
                   || (board.currentGem.swipeAngle < -135 || board.currentGem.swipeAngle >= 135)) {
                    //board.currentGem.MakeRowBomb();
                } else {
                    //board.currentGem.MakeColumnBomb();
                }
            }
            //Is the other piece matched?
            else if (board.currentGem.otherGem != null) {
                Gem otherGem = null;
                if (board.currentGem.otherGem.GetComponent<Gem>() != null) {
                    otherGem = board.currentGem.otherGem.GetComponent<Gem>();
                }
                //Is the other Gem matched?
                if (otherGem != null && otherGem.isMatched) {
                    //Make it unmatched
                    otherGem.isMatched = false;
                    
                    //Decide what kind of bomb to make
                    int typeOfBomb = Random.Range(0, 100);
                    if (typeOfBomb < 50)
                    {
                        //Make a row bomb
                        otherGem.MakeRowBomb();
                    }
                    else if (typeOfBomb >= 50)
                    {
                        //Make a column bomb
                        otherGem.MakeColumnBomb();
                    }
                    
                    if ((board.currentGem.swipeAngle > -45 && board.currentGem.swipeAngle <= 45)
                   || (board.currentGem.swipeAngle < -135 || board.currentGem.swipeAngle >= 135)) {
                        otherGem.MakeRowBomb();
                    } else {
                        otherGem.MakeColumnBomb();
                    }
                }
            }

        }
        */
    }

}