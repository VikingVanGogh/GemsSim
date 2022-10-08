using System.Collections;
using System.Collections.Generic;
using System.IO;  
using UnityEngine;
using UnityEngine.UI;
using SmartDLL;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;



public class FileManager : MonoBehaviour
{
    string path;
    string configPath ="Assets/Config/Config.txt";
    public Text eText;
    public RawImage image;
    public RawImage rawImage2;
    public Color color = Color.red;
    public Color[,] colors;
    public Board board;
    UpdateColor boardColor;
    public GameObject[,] allGems;
    public GemTypes[,] gems;
    public Button b1;
    public TextMeshProUGUI b1text;

    public bool hidden = true;
    public GameObject gameImage;

    public int width;
    public int height;
    public int offSet;
    public SmartFileExplorer fileExplorer = new SmartFileExplorer();
    public const string BaseFolder = "BaseFolder";

    public void OpenExplore() {
        string initialDir = @"C:/";
        bool restoreDir = true;
        string title = "Open a PNG File";
        string defExt = "png";
        string filter = "png files(*.png)|*png";
        
        initialDir = ReadConfig(initialDir);

        fileExplorer.OpenExplorer(initialDir, restoreDir, title, defExt, filter);

        if (fileExplorer.resultOK) {
            path = fileExplorer.fileName;
            GetImage();
        }
        WriteToConfig();
        boardColor = FindObjectOfType<UpdateColor>();
        board = FindObjectOfType<Board>();
        allGems = FindObjectOfType<Board>().allGems;
    }

    private void WriteToConfig() {
        //Debug.Log("in Writer");
        StreamWriter writer = new StreamWriter(@configPath, false);
        //Debug.Log(@configPath);
        writer.Write(Cleanup(path));
        //Debug.Log(Cleanup(path));
        writer.Close();
    }
    private string ReadConfig(string initialDir) {
        //Debug.Log("in reader");
        StreamReader reader = new StreamReader(@configPath);
        initialDir = reader.ReadLine();
        //Debug.Log(initialDir);
        reader.Close();
        return initialDir;
    }

    private string Cleanup(string s) {
        bool done =false;
        if (s != null) {
            int i = s.Length;
            if (s.Length == 0) {
                return "";
            }
            while (i > 0 && !done) {
                s = s[..^1];
                //Debug.Log(s);
                if (s.Length - 1 > 0 && s[s.Length - 1] == '\\') {
                    done = true;
                }
                i--;
            }
        } else {
            s= "C:/";
        }

        return s;
    }


    void GetImage() {
        if (path != null) {
            allGems = FindObjectOfType<Board>().allGems;
            UpdateImage();

        }
    }

    public void HideShowImage() {
        if (hidden) {
            gameImage.SetActive(true);
            hidden = false;
            b1text.text = "Hide Image";
        } else {
            gameImage.SetActive(false);
            hidden = true;
            b1text.text = "Show Image";
        }
    }


    // This function takes a 16x9, preferably 1920x1080, if it is not it will be scaled to 1920x1080.
    // It then looks in a grid pattern (hopefully dead center of where each gem should be located) 
    // and sets the 2d array of allGems[,] from the board class using a series of functions
    void UpdateImage() {
        WWW www = new WWW("file://" + path);
        image.texture = www.texture;
        rawImage2.texture = www.texture;

        Texture2D img = (Texture2D)image.texture;
        img = Resize(img, 1920, 1080);
        Color[] pixels = img.GetPixels();
        System.Array.Reverse(pixels);
        img.SetPixels(pixels);
        img.Apply();
        img = FlipTexture(img);

        Debug.Log("setting colors");
        int xOffset = 0;
        int xValue = 544; //was 550
        int yValue = 972;
        int yOffset = 0;
        colors = new Color[board.width, board.height];
        gems = new GemTypes[board.width, board.height];
        string str ="";
        for (int x = 0; x < board.width; x++) {
            xOffset  =0;
            for (int y =0; y < board.height ; y++){
                Color pixel = img.GetPixel(xValue + xOffset, yValue + yOffset);
                GemTypes tmpGem = SetGem(pixel, ref str);
                if (tmpGem == GemTypes.Black) {
                    tmpGem = GemTypes.Gremlin;
                }
                CheckWildCard(img, ref tmpGem, x, y, xValue + xOffset, yValue + yOffset);
                CheckElementalStar(img, ref tmpGem, x, y, xValue + xOffset, yValue + yOffset);
                CheckUmbralStar(img, ref tmpGem, x, y, xValue + xOffset, yValue + yOffset);                
                CheckGiants(img, ref tmpGem, x, y, xValue + xOffset, yValue + yOffset);
                CheckSkull5(img, ref tmpGem, x, y, xValue + xOffset, yValue + yOffset);
                CheckRedGem(img, ref tmpGem, x, y, xValue + xOffset, yValue + yOffset);
                gems[x, y] = tmpGem;
                SetGem(pixel, ref str);
              
                   Debug.Log("xValue+offset= "+(xValue + xOffset) + ", yValue+yOffset = " + (yValue + yOffset));
                    //Debug.Log("[x,y] gem = " + tmpGem.ToString() + " , x = " + y + ", y = " + x + ", str = " + str);
                
                colors[x, y] = new Color();
                colors[x,y] =  pixel;
                xOffset +=119;
            }
            yOffset -= 119;
        }
        board.SetBoardColors(colors, gems);
    }

    private void CheckWildCard( Texture2D img, ref GemTypes tmpGem, int x, int y, int xValue, int yValue) {
        string str ="";
        int xOffset1 = 33;
        int yOffset1 = 6;
        if (tmpGem == GemTypes.Skull|| tmpGem == GemTypes.WildCard || tmpGem == GemTypes.Skull5) {
            Color pixel2 = img.GetPixel(xValue - xOffset1, yValue+ yOffset1);
            Color pixel3 = img.GetPixel(xValue + xOffset1, yValue + yOffset1);
            //Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = " + SetGem(pixel2, ref str).ToString()  + ", str = " + str);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel3 = " + SetGem(pixel3, ref str).ToString()  + ", str = " + str);

            if (SetGem(pixel2, ref str) == GemTypes.Purple && SetGem(pixel3, ref str) == GemTypes.Yellow) {
                tmpGem = GemTypes.WildCard;
            }
        }
    }
    private void CheckRedGem(Texture2D img, ref GemTypes tmpGem, int x, int y, int xValue, int yValue) {
        string str = "";
        int xOffset1 = 27;
        int yOffset1 = 33;
        int yOffset2 = 4;
        if (tmpGem == GemTypes.Skull|| tmpGem == GemTypes.Skull5) {
            Color pixel2 = img.GetPixel(xValue + xOffset1, yValue + yOffset2);
            Color pixel3 = img.GetPixel(xValue + xOffset1, yValue + yOffset1);
            //Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = " + SetGem(pixel2, ref str).ToString()  + ", str = " + str);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel3 = " + SetGem(pixel3, ref str).ToString()  + ", str = " + str);

            if (SetGem(pixel2, ref str) == GemTypes.Red && SetGem(pixel3, ref str) == GemTypes.Red) {
                tmpGem = GemTypes.Red;
            }
        }

    }

    private void CheckGiants(Texture2D img, ref GemTypes tmpGem, int x, int y, int xValue, int yValue) {
        string str = "";
        int xOffset1 = 37;
        int yOffset1 = 0;
        int xOffset2;
        int yOffset2;
        if (tmpGem == GemTypes.Skull) {
            Color pixel2 = img.GetPixel(xValue - xOffset1, yValue );
            Color pixel3 = img.GetPixel(xValue + xOffset1, yValue );
            //Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = "+ SetGem(pixel2, ref str).ToString() + ", str = " + str);            
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel3 = "+ SetGem(pixel3, ref str).ToString() + ", str = " + str);

            if (SetGem(pixel2, ref str) == GemTypes.Red && SetGem(pixel3, ref str) == GemTypes.Red) {
                tmpGem = GemTypes.RedGiant;
            }
        }
        if (tmpGem == GemTypes.Purple || tmpGem == GemTypes.Red) {

            xOffset2 = 10;
            yOffset2 = 15;

            Color pixel4 = img.GetPixel(xValue + xOffset2, yValue + yOffset2);

            //Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel4 = " + SetGem(pixel4, ref str).ToString() + ", str = " + str);

            if (SetGem(pixel4, ref str) == GemTypes.Skull) {
                tmpGem = GemTypes.PurpleGiant;
            }
        }
        if (tmpGem == GemTypes.Skull) {
               xOffset2 = 39;
               yOffset2 = 15;
            Color pixel2 = img.GetPixel(xValue - xOffset2, yValue - yOffset2);
            Color pixel3 = img.GetPixel(xValue + xOffset2, yValue - yOffset2);
           
            //Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = " + SetGem(pixel2, ref str).ToString()  + ", str = " + str);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel3 = " + SetGem(pixel3, ref str).ToString()  + ", str = " + str);

            if ((SetGem(pixel2, ref str) == GemTypes.Yellow|| SetGem(pixel2, ref str) == GemTypes.Red) && SetGem(pixel3, ref str) == GemTypes.Yellow ) {
                tmpGem = GemTypes.YellowGiant;
            }
        }
        if (tmpGem == GemTypes.Skull) {
            xOffset2 = 39;
            yOffset2 = 9;
            Color pixel2 = img.GetPixel(xValue - xOffset2, yValue - yOffset2);
            Color pixel3 = img.GetPixel(xValue + xOffset2, yValue - yOffset2);
            //Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = " + SetGem(pixel2, ref str).ToString()  + ", str = " + str);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel3 = " + SetGem(pixel3, ref str).ToString()  + ", str = " + str);

            if (SetGem(pixel2, ref str) == GemTypes.Green && SetGem(pixel3, ref str) == GemTypes.Green) {
                tmpGem = GemTypes.GreenGiant;
            }
        }
        if (tmpGem == GemTypes.Skull) {
            xOffset2 = 27;
            yOffset2 = 25;
            int xOffset3 = 45;
            int yOffset3 = 45;

            Color pixel2 = img.GetPixel(xValue - xOffset2, yValue + yOffset2);
            Color pixel3 = img.GetPixel(xValue + xOffset2, yValue + yOffset2);
            Color pixel4 = img.GetPixel(xValue + xOffset3, yValue + yOffset3);
            //Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", yValue - yOffset2 ="+(yValue - yOffset2) + ", y = " + x + ",xValue - xOffset2="+ (xValue - xOffset2) + "  skull pixel2 = " + SetGem(pixel2, ref str).ToString()  + ", str = " + str);
            // Debug.Log("Gem cord, x = " + y + ", yValue - yOffset2 =" + (yValue - yOffset2) + ", y = " + x + ",xValue - xOffset2=" + (xValue + xOffset2) + " skull pixel3 = " + SetGem(pixel3, ref str).ToString()  + ", str = " + str);
            //Debug.Log("Gem cord, x = " + y + ", yValue - yOffset3 =" + (yValue - yOffset3) + ", y = " + x + ",xValue - xOffset3=" + (xValue + xOffset3) + " skull pixel4 = " + SetGem(pixel4, ref str).ToString() + ", str = " + str);
            if (SetGem(pixel2, ref str) == GemTypes.Black && SetGem(pixel3, ref str) == GemTypes.Black && SetGem(pixel4, ref str) == GemTypes.Red) {
                tmpGem = GemTypes.BrownGiant;
            }
        }
        if (tmpGem == GemTypes.Skull || tmpGem == GemTypes.Blue) {
            xOffset2 = 0;
            yOffset2 = 15;
            Color pixel2 = img.GetPixel(xValue - xOffset1, yValue - yOffset2);
            Color pixel3 = img.GetPixel(xValue + xOffset1, yValue - yOffset2);
            Color pixel4 = img.GetPixel(xValue + xOffset2, yValue + yOffset2);
           // Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
           // Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = " + SetGem(pixel2, ref str).ToString()  + ", str = " + str);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel3 = " + SetGem(pixel3, ref str).ToString()  + ", str = " + str);

            if (SetGem(pixel2, ref str) == GemTypes.Blue && SetGem(pixel3, ref str) == GemTypes.Blue && SetGem(pixel4, ref str) == GemTypes.Skull) {
                tmpGem = GemTypes.BlueGiant;
            }
        }

    }

    private void CheckUmbralStar(Texture2D img, ref GemTypes tmpGem, int x, int y, int xValue, int yValue) {
        string str = "";
        int xOffset1 = 33;
        if (tmpGem == GemTypes.Skull|| tmpGem == GemTypes.Purple || tmpGem == GemTypes.Gremlin) {
            Color pixel2 = img.GetPixel(xValue - xOffset1, yValue);
            Color pixel3 = img.GetPixel(xValue + xOffset1, yValue );
            //Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = " + SetGem(pixel2, ref str) + ", str = " + str);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel3 = " + SetGem(pixel3, ref str) + ", str = " + str);
            if ((SetGem(pixel2, ref str) == GemTypes.Yellow || SetGem(pixel2, ref str) == GemTypes.Red) && SetGem(pixel3, ref str) == GemTypes.Purple) {
                tmpGem = GemTypes.UmbralStar;
            }
        }
    }

    private void CheckElementalStar(Texture2D img, ref GemTypes tmpGem, int x, int y, int xValue, int yValue) {
        string str = "";
        int xOffset1 = 18;
        int yOffset1 = 23;
        if (tmpGem == GemTypes.Skull || tmpGem == GemTypes.Blue || tmpGem == GemTypes.Gremlin) {
            Color pixel2 = img.GetPixel(xValue - xOffset1, yValue - yOffset1);
            Color pixel3 = img.GetPixel(xValue + xOffset1, yValue - yOffset1);
            //Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = " + SetGem(pixel2, ref str) + ", str = " + str);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel3 = " + SetGem(pixel3, ref str) + ", str = " + str);
            if ((SetGem(pixel2, ref str) == GemTypes.Brown || SetGem(pixel2, ref str) == GemTypes.Purple) && (SetGem(pixel3, ref str) == GemTypes.Blue)) {
                tmpGem = GemTypes.ElementalStar;
            }
        }

    }

    private void CheckSkull5(Texture2D img, ref GemTypes tmpGem, int x, int y, int xValue, int yValue) {
        string str = "";
        int yOffset1 = 9;
        int xOffset1 = 16;
        if (tmpGem == GemTypes.Yellow) {
            yOffset1 = 9;
            Color pixel2 = img.GetPixel(xValue - xOffset1, yValue + yOffset1);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = " + SetGem(pixel2, ref str) + ", str = " + str);
            if (SetGem(pixel2, ref str) == GemTypes.Red) {
                tmpGem = GemTypes.Skull5;
            }
        }
        if (tmpGem == GemTypes.Skull) {
            yOffset1 = 9;
            xOffset1 = 16;
            Color pixel2 = img.GetPixel(xValue - xOffset1, yValue+ yOffset1);
            Color pixel3 = img.GetPixel(xValue + xOffset1, yValue+ yOffset1);
           // Debug.Log("Pixel,  x = " + xValue + ", " + yValue);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel2 = " + SetGem(pixel2, ref str) + ", str = " + str);
            //Debug.Log("Gem cord, x = " + y + ", y = " + x + " skull pixel3 = " + SetGem(pixel3, ref str) + ", str = " + str);
            if ((SetGem(pixel2, ref str) == GemTypes.Yellow || SetGem(pixel2, ref str) == GemTypes.Red || SetGem(pixel2, ref str) == GemTypes.Skull5) && 
                (SetGem(pixel3, ref str) == GemTypes.Yellow || SetGem(pixel3, ref str) == GemTypes.Red || SetGem(pixel3, ref str) == GemTypes.Skull5)) {
                tmpGem = GemTypes.Skull5;
            }
        }

    }


    // This function is used to convert the pixel retrieved with the GetPixel function in UpdateImage into a gem. 
    // It is first sent to the getNumberFromComp function to convert it from a rgb color value to a 3 string number value.
    // then that value is than evaluated with a switch statement to assign is a correct gem type.
    public GemTypes SetGem(Color color, ref string str) {
        string identifyerString = "";
        identifyerString += getNumberFromColorComp(color.r);
        identifyerString += getNumberFromColorComp(color.g);
        identifyerString += getNumberFromColorComp(color.b);
        GemTypes resultGem = GemTypes.Unknown;
        str = identifyerString;
        switch (identifyerString) {
            case "665":
            case "655":
            case "642":
            case "666":
            case "654":
            case "653":
            case "643":
            case "555":
            case "554":
            case "644":
            case "466"://uber doomskull
            case "456"://uber doomskull
            case "566"://uber doomskull
            case "333"://for wildcard
            case "443":
                resultGem = GemTypes.Skull;
                break;
            case "632":
            case "633":
                resultGem = GemTypes.Skull5;
                break;

            case "611":
            case "616":
            case "622":
            case "511":
            case "411":
            case "612":
            case "621":
            case "311":
            case "521":
            case "661":
            case "631"://fire gems
                resultGem = GemTypes.Red;
                break;

            case "111":
                resultGem = GemTypes.Black;
                break;

            case "663":
            case "662":
            case "664":
            case "652":
            case "531":
            case "551":
            case "552":
            case "542":
            case "541"://for wild card
                ; resultGem = GemTypes.Yellow;
                break;


            case "251":
            case "252":
            case "141":
            case "262":
            case "131":
            case "362":
            case "352":
            case "351":
            case "361":
            case "463":
            case "464":
            case "353":
            case "565":
            case "142":
            case "151":
            case "152":
            case "253":
            case "161":
            case "261":
            case "232":

                resultGem = GemTypes.Green;
                break;

            case "266":
            case "256":
            case "246":
            case "135":
            case "125":
            case "146":
            case "366":
            case "136":
            case "145":
            case "124":
            case "156":
            case "113":
            case "114":
            case "115":
            case "255":
            case "126":
            case "116"://for elemental star
            case "166"://for elemental star

                // case "566": potion(kinda rare but might cause problems)
                resultGem = GemTypes.Blue;
                break;


            case "215":
            case "216":
            case "214":
            case "314":
            case "315":
            case "213":
            case "526":
            case "656":
            case "536":
            case "636":
            case "646":
            case "516":
            case "426":
            case "415":
            case "515":
            case "626":
            case "316":
            case "326":
            case "323":
            case "416"://for wild card
                resultGem = GemTypes.Purple;
                break;

            case "332":
            case "331":
            case "312":
            case "322":
            case "321":
                resultGem = GemTypes.Brown;
                break;

            case "344":
            case "343":
            case "363":
                resultGem = GemTypes.Block;
                break;

            case "223":
            case "112":
            case "233":
            case "336":
                resultGem = GemTypes.Gremlin;
                break;
            case "211":
                resultGem= GemTypes.WildCard;
                break;

        }

        return resultGem;
    }
    // takes the float value and assigns it a number from 1-6
    private string getNumberFromColorComp(float value) {
        if (value < .255f) {
            return "1";
        } else if (value < .35f) {
            return "2";
        } else if (value < .55f) {
            return "3";
        } else if (value < .45f) {
            return "4";
        } else if (value < .50f) {
            return "5";
        } else {
            return "6";
        }
    }


    Texture2D FlipTexture(Texture2D original) {
        Texture2D flipped = new Texture2D(original.width, original.height);

        int xN = original.width;
        int yN = original.height;


        for (int i = 0; i < xN; i++) {
            for (int j = 0; j < yN; j++) {
                flipped.SetPixel(xN - i - 1, j, original.GetPixel(i, j));
            }
        }
        flipped.Apply();

        return flipped;
    }

    public Texture2D inputtexture2D;
    public RawImage rawImage;

    Texture2D Resize(Texture2D texture2D, int targetX, int targetY) {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }
}
