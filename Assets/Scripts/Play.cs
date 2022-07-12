using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    private Board board;
    public Sprite playIcon;
    public Sprite stopIcon;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
        mySprite.sprite = stopIcon;
        mySprite.color = Color.red;
    }

    private void OnMouseDown() {
        
        if (board.play == false) {
            board.play = true;
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.sprite = stopIcon;
            mySprite.color = Color.red;
            //Debug.Log("trying to set color");
            //Debug.Log(mySprite);
        } else {
           // Debug.Log("not trying to set color");
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.sprite = playIcon;
            mySprite.color = Color.green;
            board.play = false;
        }
        
    }
}
