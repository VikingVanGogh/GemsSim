using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour {

    [Header("Board Variables")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;

    


    private FindMatches findMatches;
    private Board board;
    public GameObject otherGem;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    [Header("Swipe Stuff")]
    public float swipeAngle = 0;
    public float swipeResist = 1f;

    [Header("Powerup Stuff")]
    public bool isColorBomb;
    public bool isColumnBomb;
    public bool isRowBomb;
    public GameObject rowArrow;
    public GameObject columnArrow;
    public GameObject colorBomb;

    [Header("Color Variables")]
    public bool Yellow = false;
    public bool Blue = false;
    public bool Red = false;
    public bool Green = false;
    public bool Purple = false;
    public bool Brown = false;
    public bool Skull = false;


    // Use this for initialization
    void Start() {

        isColumnBomb = false;
        isRowBomb = false;

        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
        //targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //row = targetY;
        //column = targetX;
        //previousRow = row;
        //previousColumn = column;

    }


    //This is for testing and Debug only.
    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(1)) {
            isColorBomb = true;
            GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
            color.transform.parent = this.transform;
        }
    }


    // Update is called once per frame
    void Update() {
        /*
        if(isMatched){
            
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            Color currentColor = mySprite.color;
            mySprite.color = new Color(currentColor.r, currentColor.g, currentColor.b, .5f);
        }
        */
        targetX = column;
        targetY = row;
        if (Mathf.Abs(targetX - transform.position.x) > .1) {
            //Move Towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .2f);
            if (board.allGems[column, row] != this.gameObject) {
                board.allGems[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();


        } else {
            //Directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;

        }
        if (Mathf.Abs(targetY - transform.position.y) > .1) {
            //Move Towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .2f);
            if (board.allGems[column, row] != this.gameObject) {
                board.allGems[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();

        } else {
            //Directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;

        }

    }

    public IEnumerator CheckMoveCo() {
        yield return new WaitForSeconds(board.GetWaitTime());
        if (otherGem != null) {
            if (!isMatched && !otherGem.GetComponent<Gem>().isMatched) {
                otherGem.GetComponent<Gem>().row = row;
                otherGem.GetComponent<Gem>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(board.GetWaitTime());
                board.currentGem = null;
            } else {
                board.DestroyMatches();
            }
            //otherGem = null;
        }
        board.currentState = GameState.move;

    }

    private void OnMouseDown() {
        Debug.Log("GameState = "+ board.currentState.ToString());
        if (board.currentState == GameState.move) {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
    }

    private void OnMouseUp() {
        if (board.currentState == GameState.move) {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (board.play) {
                CalculateAngle();
            } else {
                if (board.allGems[column, row]!=null) {
                    Destroy(board.allGems[column, row]);
                }
                board.allGems[column, row] = null;

                board.allGems[column, row] = board.SelectedGem(column, row);
            }
            
        }
        //Debug.Log("Column = " + column + ", Row = " + row);
    }

    void CalculateAngle() {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist) {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentState = GameState.wait;
            //Debug.Log("CalculateAngle " + ", wait");
            if (this != null)
                board.currentGem = this;
            

        } else {
            //Debug.Log("CalculateAngle " + ", move");
            board.currentState = GameState.move;

        }
    }

    void MovePieces() {
        if (CheckValidGem(board.allGems[column, row])) {
            if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1 && CheckValidGem(board.allGems[column + 1, row])) {
                //Right Swipe
                //Debug.Log("otherGem -Right");
                if (board.allGems[column + 1, row] != null)
                    otherGem = board.allGems[column + 1, row];

                    //Debug.Log(otherGem);
                    previousRow = row;

                    previousColumn = column;
                    otherGem.GetComponent<Gem>().column -= 1;
                    column += 1;
                

            } else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1 && CheckValidGem(board.allGems[column, row + 1])) {
                //Up Swipe
                //Debug.Log("otherGem up");
                if (board.allGems[column, row + 1] != null)
                    otherGem = board.allGems[column, row + 1];

                    previousRow = row;
                    previousColumn = column;
                    otherGem.GetComponent<Gem>().row -= 1;
                    //Debug.Log(otherGem);
                    row += 1;
                

            } else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0 && CheckValidGem(board.allGems[column - 1, row])) {
                //Left Swipe
                //Debug.Log("otherGem left");
                if (board.allGems[column - 1, row] != null)
                    otherGem = board.allGems[column - 1, row];

                //Debug.Log(otherGem);

                    previousRow = row;
                    previousColumn = column;
                    otherGem.GetComponent<Gem>().column += 1;
                    column -= 1;
                
            } else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0 && CheckValidGem(board.allGems[column, row - 1])) {
                //Down Swipe
                //Debug.Log("otherGem Down");
                if (board.allGems[column, row - 1] != null)
                    otherGem = board.allGems[column, row - 1];

                    previousRow = row;
                    previousColumn = column;
                    otherGem.GetComponent<Gem>().row += 1;
                    row -= 1;

            }
        }
        StartCoroutine(CheckMoveCo());
        
    }

    public bool CheckValidGem(GameObject gem) {
        if (gem != null && gem.tag != "Block Gem") {
            return true;
        }
        if (gem == null) {
            return true;
        }
        return false;
    }

    void FindMatches() {
        if (column > 0 && column < board.width - 1) {
            GameObject leftGem1 = board.allGems[column - 1, row];
            GameObject rightGem1 = board.allGems[column + 1, row];
            if (leftGem1 != null && rightGem1 != null) {
                if (leftGem1.tag == this.gameObject.tag && rightGem1.tag == this.gameObject.tag) {
                    leftGem1.GetComponent<Gem>().isMatched = true;
                    rightGem1.GetComponent<Gem>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < board.height - 1) {
            GameObject upGem1 = board.allGems[column, row + 1];
            GameObject downGem1 = board.allGems[column, row - 1];
            if (upGem1 != null && downGem1 != null) {
                if (upGem1.tag == this.gameObject.tag && downGem1.tag == this.gameObject.tag) {
                    upGem1.GetComponent<Gem>().isMatched = true;
                    downGem1.GetComponent<Gem>().isMatched = true;
                    isMatched = true;
                }
            }
        }

    }

    public void MakeRowBomb() {
        isRowBomb = true;
        GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColumnBomb() {
        isColumnBomb = true;
        GameObject arrow = Instantiate(columnArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColorBomb() {
        isColorBomb = true;
        GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
        color.transform.parent = this.transform;
    }

}
