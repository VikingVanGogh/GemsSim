using System.Collections;
using System.Collections.Generic;
using System.IO;  
using UnityEngine;
using UnityEngine.UI;
using SmartDLL;
using UnityEngine.SceneManagement;
using UnityEditor;



public class FileManager : MonoBehaviour
{
    string path;
    string configPath ="Assets/Config/Config.txt";
    public Text eText;
    public RawImage image;
    public Image image2;
    public Color color = Color.red;
    public Color[,] colors;
    public Board board;
    UpdateColor boardColor;
    public GameObject[,] allGems;
    public GemTypes[,] gems;

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
        int i = s.Length;
        if (s.Length == 0) {
            return "";
        }
        while ( i > 0 && !done) {
            s = s[..^1];
            //Debug.Log(s);
            if (s.Length-1 >0 && s[s.Length - 1] == '\\') {
                done = true;
            }
            i--;
        }
        return s;
    }


    void GetImage() {
        if (path != null) {
            allGems = FindObjectOfType<Board>().allGems;
            UpdateImage();

        }
    }
    void UpdateImage() {
        WWW www = new WWW("file://" + path);
        image.texture = www.texture;

        Texture2D img = (Texture2D)image.texture;
        img = Resize(img, 1920, 1080);
        Color[] pixels = img.GetPixels();
        System.Array.Reverse(pixels);
        img.SetPixels(pixels);
        img.Apply();
        img = FlipTexture(img);

        Debug.Log("setting colors");
        float xOffset = 0;
        double yOffset = 0;
        colors = new Color[board.width, board.height];
        gems = new GemTypes[board.width, board.height];
        string str ="";
        for (int x = 0; x < board.width; x++) {
            xOffset  =0;
            for (int y =0; y < board.height ; y++){
                Color pixel = img.GetPixel(550+(int) xOffset, 960 + (int)yOffset);
                GemTypes tmpGem = SetGem(pixel, ref str);
                if (tmpGem  == GemTypes.Skull) {
                    Color pixel2 = img.GetPixel(550 + (int)xOffset -33, 960 + (int)yOffset);
                    Color pixel3 = img.GetPixel(550 + (int)xOffset + 33, 960 + (int)yOffset);
                    //Debug.Log(" pixel2 = " + SetGem(pixel2, ref str) + ", str = " + str + ", Pixel3 = " + SetGem(pixel3, ref str) + " str  =" + str);
                    if (SetGem(pixel2, ref str) == GemTypes.Purple && SetGem(pixel3, ref str) ==GemTypes.Yellow) {
                        
                        tmpGem = GemTypes.WildCard;
                    }
                }

                if (tmpGem == GemTypes.Skull) {
                    Color pixel2 = img.GetPixel(550 - 25 + (int)xOffset , 960 + 15 + (int)yOffset);
                   // Debug.Log(" x = " +(550-20 + (int)xOffset) + ", " +(960 + 15 + (int)yOffset) );
                   // Debug.Log(" pixel2 = " + SetGem(pixel2, ref str) + ", str = " + str );

                    if (SetGem(pixel2, ref str) == GemTypes.Red || SetGem(pixel2, ref str) == GemTypes.Yellow || 
                        SetGem(pixel2, ref str) == GemTypes.Brown || SetGem(pixel2, ref str) == GemTypes.Skull5) {

                        tmpGem = GemTypes.Skull5;
                    }
                }

                if (tmpGem == GemTypes.Block) {
                    Color pixel2 = img.GetPixel(550 - 25 + (int)xOffset, 960 + 15 + (int)yOffset);
                   // Debug.Log(" x = " + (550 - 20 + (int)xOffset) + ", " + (960 + 15 + (int)yOffset));
                    //Debug.Log(" pixel2 = " + SetGem(pixel2, ref str) + ", str = " + str);
                    if (SetGem(pixel2, ref str) == GemTypes.Green)  {

                        tmpGem = GemTypes.Green;
                    }
                }

                if (tmpGem == GemTypes.Purple) {
                    Color pixel2 = img.GetPixel(550 - 25 + (int)xOffset, 960 + (int)yOffset);
                    //Debug.Log(" x = " + (550 - 20 + (int)xOffset) + ", " + (960 + 15 + (int)yOffset));
                    //Debug.Log(" pixel2 = " + SetGem(pixel2, ref str) + ", str = " + str);
                    if (SetGem(pixel2, ref str) == GemTypes.Yellow) {

                        tmpGem = GemTypes.UmbralStar;
                    }
                }

                if (tmpGem == GemTypes.Blue) {
                    Color pixel2 = img.GetPixel(550 - 25 + (int)xOffset, 960 + (int)yOffset);
                   Debug.Log("Pixel,  x = " + (550 - 20 + (int)xOffset) + ", " + (960 + 15 + (int)yOffset));
                   Debug.Log("Gem cord, x = " +x + ", y = "+ y +" BLUE pixel2 = " + SetGem(pixel2, ref str) + ", str = " + str);
                    if (SetGem(pixel2, ref str) == GemTypes.Black) {

                        tmpGem = GemTypes.ElementalStar;
                    }
                }


                gems[x, y] = tmpGem;
                //Debug.Log("[x,y] gem = "+gems[x,y].ToString()+" , x = " +x + ", y = "+ y +", str = "+str );
                colors[x, y] = new Color();
                colors[x,y] =  pixel;
                xOffset +=118.57142857142857142857142857143f;
            }
            yOffset -= 118.57142857142857142857142857143f;
        }
        board.SetBoardColors(colors, gems);
    }


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
            case "633":
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

            case "631":
            case "632":
                resultGem = GemTypes.Skull5;
                break;

            case "611":
            case "622":
            case "511":
            case "411":
            case "612":
            case "621":
            case "311":
            case "521":
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
            case "661":
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
            case "114":
            case "115":
            case "255":

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
            case "616":
            case "636":
            case "646":
            case "516":
            case "426":
            case "415":
            case "515":
            case "626":
            case "316":
            case "326":
            case "416"://for wild card
                resultGem = GemTypes.Purple;
                break;

            case "332":
            case "312":
            case "322":
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
                resultGem = GemTypes.Gremlin;
                break;

            case "126":

                resultGem = GemTypes.ElementalStar;
                break;
        }

        return resultGem;
    }
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




    public Texture2D toTexture2D(RenderTexture rTex) {
        Texture2D dest = new Texture2D(rTex.width, rTex.height, TextureFormat.RGBA32, false);
        dest.Apply(false);
        Graphics.CopyTexture(rTex, dest);
        return dest;
    }

}
