using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour {

    private Board board;
    private const string SKULL5 = "Skull5 Gem";
    private const string WILDCARD = "Wildcard Gem";
    private const string SKULL = "Skull Gem";
    private const string RED = "Red Gem";
    private const string BLUE = "Blue Gem";
    private const string YELLOW = "Yellow Gem";
    private const string PURPLE = "Purple Gem";
    private const string BROWN = "Brown Gem";
    private const string GREEN = "Green Gem";
    private const string BLOCK = "Block Gem";
    private const string GREMLIN = "Gremlin Gem";
    private const string WISH = "WISH Gem";

    public List<GameObject> currentMatches = new List<GameObject>();
    private List<GameObject> match4Plus = new List<GameObject>();




    // Use this for initialization
    void Start() {
        board = FindObjectOfType<Board>();
    }
   public void CheckMatch4Plus(ref Match4PlusMoves move) {
        move.count = 0;
        bool moveFound = false;
        for (int x = 0; x < board.width; x++) {
            for (int y = 0; y < board.height; y++) {
                if (!moveFound && board.allGems[x, y] != null) {
                    CheckCross(x, y, ref move);
                    CheckL(x, y, ref move);
                    CheckBackwardsL(x, y, ref move);
                    CheckUpsideDownL(x, y, ref move);
                    CheckUpsideDownBackWardsL(x, y, ref move);
                    CheckRight(x, y, ref move);
                    CheckLeft(x, y, ref move);
                    CheckUp(x, y, ref move);
                    CheckDown(x, y, ref move);
                    CheckT(x, y, ref move);
                    CheckLeftT(x, y, ref move);
                    CheckRightT(x, y, ref move);
                    CheckupSideDownT(x, y, ref move);
                    if (move.count > 3) {
                        //Debug.Log("foundMove at x= "+x+", y ="+y );
                        moveFound = true;
                    } else {
                        move.Clear();
                    }
                }
            }
        }
        match4Plus.Clear();
    }
    private void CheckLeftT(int x, int y, ref Match4PlusMoves move) {
        if (CheckArray(x, y) && CheckArray(x, y-1 ) && CheckArray(x,  y-2) && CheckArray(x+1, y - 1) && CheckArray(x + 2, y - 1) &&
            SpecialGems5(board.allGems[x, y], board.allGems[x, y - 1], board.allGems[x, y - 2], board.allGems[x + 1, y-1], board.allGems[x+2, y-1], ref move) ) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y - 1], ref move);
            AddMove(board.allGems[x, y - 2], ref move);
            AddMove(board.allGems[x + 1, y-1], ref move);
            AddMove(board.allGems[x +2, y-1], ref move);
            CheckRight(x, y-1, ref move);
            CheckLeft(x, y-1, ref move);
            CheckDown(x, y, ref move);
            CheckUp(x, y, ref move);
            //Debug.Log("move.count = " + move.count);
        }
    }
    private void CheckRightT(int x, int y, ref Match4PlusMoves move) {
        if (CheckArray(x, y) && CheckArray(x, y - 1) && CheckArray(x, y - 2) && CheckArray(x - 1, y - 1) && CheckArray(x - 2, y - 1) &&
            SpecialGems5(board.allGems[x, y], board.allGems[x, y - 1], board.allGems[x, y - 2], board.allGems[x - 1, y - 1], board.allGems[x - 2, y - 1], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y - 1], ref move);
            AddMove(board.allGems[x, y - 2], ref move);
            AddMove(board.allGems[x - 1, y - 1], ref move);
            AddMove(board.allGems[x - 2, y - 1], ref move);
            CheckRight(x, y - 1, ref move);
            CheckLeft(x, y - 1, ref move);
            CheckDown(x, y, ref move);
            CheckUp(x, y, ref move);
            //Debug.Log("move.count = " + move.count);
        }
    }

    private void CheckT(int x, int y, ref Match4PlusMoves move) {
        if (CheckArray(x + 1, y) && CheckArray(x - 1, y) && CheckArray(x, y - 2) &&
            SpecialGems5(board.allGems[x, y], board.allGems[x, y - 1], board.allGems[x, y - 2], board.allGems[x + 1, y], board.allGems[x - 1, y], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y - 1], ref move);
            AddMove(board.allGems[x, y - 2], ref move);
            AddMove(board.allGems[x + 1, y], ref move);
            AddMove(board.allGems[x - 1, y], ref move);
            CheckRight(x, y, ref move);
            CheckLeft(x, y, ref move);
            CheckDown(x, y, ref move);
            //Debug.Log("move.count = " + move.count);
        }
    }

    private void CheckupSideDownT(int x, int y, ref Match4PlusMoves move) {
        if (CheckArray(x + 1, y) && CheckArray(x - 1, y) && CheckArray(x, y + 2) &&
            SpecialGems5(board.allGems[x, y], board.allGems[x, y + 1], board.allGems[x, y + 2], board.allGems[x + 1, y], board.allGems[x - 1, y], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y + 1], ref move);
            AddMove(board.allGems[x, y + 2], ref move);
            AddMove(board.allGems[x + 1, y], ref move);
            AddMove(board.allGems[x - 1, y], ref move);
            CheckRight(x, y, ref move);
            CheckUp(x, y, ref move);
            CheckLeft(x, y, ref move);
            //Debug.Log("move.count = " + move.count);
        }
    }
    private void CheckL(int x, int y, ref Match4PlusMoves move) {
        if (CheckArray(x + 2, y) && CheckArray(x, y + 2) &&
             SpecialGems5(board.allGems[x, y], board.allGems[x, y + 1], board.allGems[x, y + 2], board.allGems[x + 1, y], board.allGems[x + 2, y], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y+1], ref move);
            AddMove(board.allGems[x, y + 2], ref move);
            AddMove(board.allGems[x + 1, y], ref move);
            AddMove(board.allGems[x + 2, y], ref move);
            CheckRight(x, y, ref move);
            CheckUp(x, y, ref move);
           //Debug.Log("move.count = "+move.count);
        }
    }
    private void CheckUpsideDownBackWardsL(int x, int y, ref Match4PlusMoves move) {
        if (CheckArray(x - 2, y) && CheckArray(x, y - 2) &&
             SpecialGems5(board.allGems[x, y], board.allGems[x, y - 1], board.allGems[x, y - 2], board.allGems[x - 1, y], board.allGems[x - 2, y], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y - 1], ref move);
            AddMove(board.allGems[x, y - 2], ref move);
            AddMove(board.allGems[x - 1, y], ref move);
            AddMove(board.allGems[x - 2, y], ref move);
            CheckLeft(x, y, ref move);
            CheckDown(x, y, ref move);
        }
    }

    private void CheckUpsideDownL(int x, int y, ref Match4PlusMoves move) {
        if (CheckArray(x + 2, y) && CheckArray(x, y - 2) &&
            SpecialGems5(board.allGems[x, y], board.allGems[x, y - 1], board.allGems[x, y - 2], board.allGems[x + 1, y], board.allGems[x +2, y], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y - 1], ref move);
            AddMove(board.allGems[x, y - 2], ref move);
            AddMove(board.allGems[x + 1, y], ref move);
            AddMove(board.allGems[x + 2, y], ref move);
            CheckRight(x, y, ref move);
            CheckDown(x, y, ref move);
        }
    }

    private void CheckBackwardsL(int x, int y, ref Match4PlusMoves move) {
        if (CheckArray(x - 2, y) && CheckArray(x, y + 2) &&
            SpecialGems3(board.allGems[x, y], board.allGems[x, y + 1], board.allGems[x, y + 2], ref move) &&
            SpecialGems3(board.allGems[x, y], board.allGems[x - 1, y], board.allGems[x - 2, y], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y + 1], ref move);
            AddMove(board.allGems[x, y + 2], ref move);
            AddMove(board.allGems[x - 1, y], ref move);
            AddMove(board.allGems[x - 2, y], ref move);
            CheckLeft(x, y, ref move);
            CheckUp(x, y, ref move);
        }
    }



    private void CheckCross(int x,int y,ref Match4PlusMoves move) {
        if (CheckArray(x - 1, y) && CheckArray(x + 1, y) && CheckArray(x, y - 1) && CheckArray(x, y + 1) &&
           SpecialGems5(board.allGems[x, y], board.allGems[x, y - 1], board.allGems[x, y + 1 ], board.allGems[x + 1, y], board.allGems[x - 1, y], ref move)) {
            AddMove(board.allGems[x, y + 1],ref move);
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y - 1], ref move);
            AddMove(board.allGems[x + 1, y],ref  move);
            AddMove(board.allGems[x - 1, y], ref move);
            CheckRight(x - 1, y, ref move);
            CheckLeft(x + 1, y, ref move);
            CheckUp(x, y - 1, ref move);
            CheckDown(x, y + 1, ref move);
        }
    }
    private void CheckRight(int x, int y, ref Match4PlusMoves move) {
        while (CheckArray(x + 3, y) && SpecialGems4(board.allGems[x, y], board.allGems[x + 1, y], board.allGems[x + 2, y], board.allGems[x + 3, y], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x+1, y], ref move);
            AddMove(board.allGems[x+2, y], ref move);
            AddMove(board.allGems[x + 3, y], ref move);
            x++;
        }
    }
    private void CheckLeft(int x, int y, ref Match4PlusMoves move) {
        while (CheckArray(x - 3, y) && SpecialGems4(board.allGems[x, y], board.allGems[x - 1, y], board.allGems[x - 2, y], board.allGems[x - 3, y], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x - 1, y], ref move);
            AddMove(board.allGems[x - 2, y], ref move);
            AddMove(board.allGems[x - 3, y], ref move);
            x++;
        }
    }
    private void CheckUp(int x, int y, ref Match4PlusMoves move) {
        while (CheckArray(x, y + 3) && SpecialGems4(board.allGems[x, y], board.allGems[x, y + 1], board.allGems[x, y + 2], board.allGems[x, y + 3], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y + 1], ref move);
            AddMove(board.allGems[x, y + 2], ref move);
            AddMove(board.allGems[x, y + 3], ref move);
            x++;
        }
    }
    private void CheckDown(int x, int y, ref Match4PlusMoves move) {
        while (CheckArray(x, y - 3) && SpecialGems4(board.allGems[x, y], board.allGems[x, y - 1], board.allGems[x, y - 2], board.allGems[x, y - 3], ref move)) {
            AddMove(board.allGems[x, y], ref move);
            AddMove(board.allGems[x, y - 1], ref move);
            AddMove(board.allGems[x, y - 2], ref move);
            AddMove(board.allGems[x, y - 3], ref move);
            x++;
        }
    }
    private void AddMove(GameObject gem, ref Match4PlusMoves move) {
        if (!match4Plus.Contains(gem)) {
            match4Plus.Add(gem);
            move.count++;
            AddColor(gem, ref move);
        }
    }
    private void AddColor(GameObject gem,ref Match4PlusMoves move) {
        if (move.color != null) {
            switch (move.color) {
                case BLUE:
                    move.blue++;
                    break;
                case YELLOW:
                    move.yellow++;
                    break;
                case RED:
                    move.red++;
                    break;
                case SKULL:
                    if (gem.tag ==SKULL5) {
                        move.skull5++;
                    } else {
                        move.skull++;
                    }                    
                    break;
                case GREEN:
                    move.green++;
                    break;
                case PURPLE:
                    move.purple++;
                    break;
                case BROWN:
                    move.brown++;
                    break;
            }
        }
    }
    private bool SpecialGems5(GameObject gem1, GameObject gem2, GameObject gem3, GameObject gem4, GameObject gem5, ref Match4PlusMoves move) {
        if (gem1 != null && gem2 != null && gem3 != null && gem4 != null && gem5!=null && NotThisGem(gem1, gem2, gem3)) {
            if (gem1.GetComponent<Gem>().Blue && gem2.GetComponent<Gem>().Blue && gem3.GetComponent<Gem>().Blue && gem4.GetComponent<Gem>().Blue && gem5.GetComponent<Gem>().Blue) {
                move.color = BLUE;
                return true;
            }
            if (gem1.GetComponent<Gem>().Yellow && gem2.GetComponent<Gem>().Yellow && gem3.GetComponent<Gem>().Yellow && gem4.GetComponent<Gem>().Yellow && gem5.GetComponent<Gem>().Yellow) {
                move.color = YELLOW;
                return true;
            }
            if (gem1.GetComponent<Gem>().Red && gem2.GetComponent<Gem>().Red && gem3.GetComponent<Gem>().Red && gem4.GetComponent<Gem>().Red && gem5.GetComponent<Gem>().Red) {
                move.color = RED;
                return true;
            }
            if (gem1.GetComponent<Gem>().Brown && gem2.GetComponent<Gem>().Brown && gem3.GetComponent<Gem>().Brown && gem4.GetComponent<Gem>().Brown && gem5.GetComponent<Gem>().Brown) {
                move.color = BROWN;
                return true;
            }
            if (gem1.GetComponent<Gem>().Purple && gem2.GetComponent<Gem>().Purple && gem3.GetComponent<Gem>().Purple && gem4.GetComponent<Gem>().Purple && gem5.GetComponent<Gem>().Purple) {
                move.color = PURPLE;
                return true;
            }
            if (gem1.GetComponent<Gem>().Green && gem2.GetComponent<Gem>().Green && gem3.GetComponent<Gem>().Green && gem4.GetComponent<Gem>().Green && gem5.GetComponent<Gem>().Green) {
                move.color = GREEN;
                return true;
            }
            if (gem1.GetComponent<Gem>().Skull && gem2.GetComponent<Gem>().Skull && gem3.GetComponent<Gem>().Skull && gem4.GetComponent<Gem>().Skull && gem5.GetComponent<Gem>().Skull) {
                move.color = SKULL;
                return true;
            }
        }

        return false;
    }

    private bool SpecialGems4(GameObject gem1, GameObject gem2, GameObject gem3, GameObject gem4, ref Match4PlusMoves move) {
        if (gem1 != null && gem2 != null && gem3 != null && gem4 != null && NotThisGem(gem1, gem2, gem3)) {
            if (gem1.GetComponent<Gem>().Blue && gem2.GetComponent<Gem>().Blue && gem3.GetComponent<Gem>().Blue && gem4.GetComponent<Gem>().Blue) {
                move.color = BLUE;
                return true;
            }
            if (gem1.GetComponent<Gem>().Yellow && gem2.GetComponent<Gem>().Yellow && gem3.GetComponent<Gem>().Yellow && gem4.GetComponent<Gem>().Yellow) {
                move.color = YELLOW;
                return true;
            }
            if (gem1.GetComponent<Gem>().Red && gem2.GetComponent<Gem>().Red && gem3.GetComponent<Gem>().Red && gem4.GetComponent<Gem>().Red) {
                move.color = RED;
                return true;
            }
            if (gem1.GetComponent<Gem>().Brown && gem2.GetComponent<Gem>().Brown && gem3.GetComponent<Gem>().Brown && gem4.GetComponent<Gem>().Brown) {
                move.color = BROWN;
                return true;
            }
            if (gem1.GetComponent<Gem>().Purple && gem2.GetComponent<Gem>().Purple && gem3.GetComponent<Gem>().Purple && gem4.GetComponent<Gem>().Purple) {
                move.color = PURPLE;
                return true;
            }
            if (gem1.GetComponent<Gem>().Green && gem2.GetComponent<Gem>().Green && gem3.GetComponent<Gem>().Green && gem4.GetComponent<Gem>().Green) {
                move.color = GREEN;
                return true;
            }
            if (gem1.GetComponent<Gem>().Skull && gem2.GetComponent<Gem>().Skull && gem3.GetComponent<Gem>().Skull && gem4.GetComponent<Gem>().Skull) {
                move.color = SKULL;
                return true;
            }
        }

        return false;
    }
    private bool SpecialGems3(GameObject gem1, GameObject gem2, GameObject gem3, ref Match4PlusMoves move) {
        if (gem1 != null && gem2 != null && gem3 != null && NotThisGem(gem1, gem2, gem3)) {
            if (gem1.GetComponent<Gem>().Blue && gem2.GetComponent<Gem>().Blue && gem3.GetComponent<Gem>().Blue) {
                move.color = BLUE;
                return true;
            }
            if (gem1.GetComponent<Gem>().Yellow && gem2.GetComponent<Gem>().Yellow && gem3.GetComponent<Gem>().Yellow) {
                move.color = YELLOW;
                return true;
            }
            if (gem1.GetComponent<Gem>().Red && gem2.GetComponent<Gem>().Red && gem3.GetComponent<Gem>().Red) {
                move.color = RED;
                return true;
            }
            if (gem1.GetComponent<Gem>().Brown && gem2.GetComponent<Gem>().Brown && gem3.GetComponent<Gem>().Brown) {
                move.color = BROWN;
                return true;
            }
            if (gem1.GetComponent<Gem>().Purple && gem2.GetComponent<Gem>().Purple && gem3.GetComponent<Gem>().Purple) {
                move.color = PURPLE;
                return true;
            }
            if (gem1.GetComponent<Gem>().Green && gem2.GetComponent<Gem>().Green && gem3.GetComponent<Gem>().Green) {
                move.color = GREEN;
                return true;
            }
            if (gem1.GetComponent<Gem>().Skull && gem2.GetComponent<Gem>().Skull && gem3.GetComponent<Gem>().Skull) {
                move.color = SKULL;
                return true;
            }
        }
        return false;
    }

    public void FindAllMatches() {
        StartCoroutine(FindAllMatchesCo());
    }

    public void FindAllMatchesCheck() {
       FindAllMatchesNoCo();
    }

    private void AddToListAndMatch(GameObject gem) {
        if (!currentMatches.Contains(gem)) {
            currentMatches.Add(gem);
        }

        gem.GetComponent<Gem>().isMatched = true;
    }
    public void BlowUpBlock(int x, int y) {
        AddToListAndMatch(board.allGems[x, y]);
        BlowupBlock(x, y);
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
        if ((x >= 0 && x < board.width) && (y >= 0 && y < board.height)) {
            return true;
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

    private void FindAllMatchesNoCo() {
        for (int i = 0; i < board.width; i++) {
            for (int j = 0; j < board.height; j++) {
                GameObject currentGem = null;
                if (board.allGems[i, j] != null) {
                    currentGem = board.allGems[i, j];
                }
                Gem currentGemGem = null;
                if (currentGem != null && currentGem.GetComponent<Gem>()) {
                    currentGemGem = currentGem.GetComponent<Gem>();
                }
                if (currentGem != null) {
                    if (i > 0 && i < board.width - 1) {
                        GameObject leftGem = null;
                        if (board.allGems[i - 1, j] != null) {
                            leftGem = board.allGems[i - 1, j];
                        }
                        Gem leftGemGem = null;
                        if (leftGem != null && leftGem.GetComponent<Gem>() != null) {
                            leftGemGem = leftGem.GetComponent<Gem>();
                        }
                        GameObject rightGem = null;
                        if (board.allGems[i + 1, j] != null) {
                            rightGem = board.allGems[i + 1, j];
                        }
                        Gem rightGemGem = null;
                        if (rightGem != null && rightGem.GetComponent<Gem>() != null) {
                            rightGemGem = rightGem.GetComponent<Gem>();
                        }

                        if (leftGem != null && rightGem != null) {
                            if (SpecialGems3(leftGem, rightGem, currentGem)) {

                                //currentMatches.Union(IsRowBomb(leftGemGem, currentGemGem, rightGemGem));

                                //currentMatches.Union(IsColumnBomb(leftGemGem, currentGemGem, rightGemGem));
                                Skull5Match(leftGem, i - 1, j, currentGem, i, j, rightGem, i + 1, j);
                                GetNearbyPieces(leftGem, currentGem, rightGem);


                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1) {
                        GameObject upGem = null;
                        if (board.allGems[i, j + 1] != null) {
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
                        if (downGem != null && downGem.GetComponent<Gem>() != null) {
                            downGemGem = downGem.GetComponent<Gem>();
                        }
                        if (upGem != null && downGem != null) {
                            if (SpecialGems3(upGem, downGem, currentGem)) {
                                //currentMatches.Union(IsColumnBomb(upGemGem, currentGemGem, downGemGem));
                                //currentMatches.Union(IsRowBomb(upGemGem, currentGemGem, downGemGem));
                                Skull5Match(upGem, i, j + 1, currentGem, i, j, downGem, i, j - 1);
                                GetNearbyPieces(upGem, currentGem, downGem);

                            }
                        }
                    }

                }
            }
        }

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
                            if (SpecialGems3( leftGem,  rightGem,  currentGem)) {

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
                            if (SpecialGems3(upGem, downGem, currentGem)) {
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
    private bool NotThisGem(GameObject gem1, GameObject gem2, GameObject gem3 ) {
        if ((gem1.tag == BLOCK || gem1.tag == WISH) ||
            (gem2.tag == BLOCK || gem2.tag == WISH) ||
            (gem3.tag == BLOCK || gem3.tag == WISH)) {
            return false;
        }
        return true;

    }

    private bool SpecialGems3(GameObject gem1, GameObject gem2, GameObject gem3) {
        if (gem1 != null && gem2 != null && gem3 != null && NotThisGem(gem1,gem2,gem3)) {
            if (gem1.GetComponent<Gem>().Blue && gem2.GetComponent<Gem>().Blue && gem3.GetComponent<Gem>().Blue) {
                return true;
            }
            if (gem1.GetComponent<Gem>().Yellow && gem2.GetComponent<Gem>().Yellow && gem3.GetComponent<Gem>().Yellow) {
                return true;
            }
            if (gem1.GetComponent<Gem>().Red && gem2.GetComponent<Gem>().Red && gem3.GetComponent<Gem>().Red) {
                return true;
            }
            if (gem1.GetComponent<Gem>().Brown && gem2.GetComponent<Gem>().Brown && gem3.GetComponent<Gem>().Brown) {
                return true;
            }
            if (gem1.GetComponent<Gem>().Purple && gem2.GetComponent<Gem>().Purple && gem3.GetComponent<Gem>().Purple) {
                return true;
            }
            if (gem1.GetComponent<Gem>().Green && gem2.GetComponent<Gem>().Green && gem3.GetComponent<Gem>().Green) {
                return true;
            }
            if (gem1.GetComponent<Gem>().Skull && gem2.GetComponent<Gem>().Skull && gem3.GetComponent<Gem>().Skull) {
                return true;
            }
        }
        return false;
    }

}