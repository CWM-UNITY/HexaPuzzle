using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Superpow;
public class MakeLevelController : BaseController {
    public TileRegion2 tileRegion;
    public GameLevel gameLevel;

    public int world = 1;
    public int level = 1;
    public LevelPrefs levelPrefs;

    public List<Tile> listSlots = new List<Tile>();

    public static MakeLevelController instance;

    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    protected override void Start()
    {
        base.Start();

        gameLevel = Resources.Load<GameLevel>("MakeLevels/World_" + world + "/Level_" + level);
        if (gameLevel == null)
        {
#if UNITY_EDITOR
            gameLevel = ScriptableObject.CreateInstance<GameLevel>();
            AssetDatabase.CreateAsset(gameLevel, "Assets/Resources/MakeLevels/World_" + world + "/Level_" + level + ".asset");
            AssetDatabase.SaveAssets();
#endif
        }

        string strLevelPrefs = Utils.GetLevelData(GameState.chosenWorld, GameState.chosenLevel);
        if (string.IsNullOrEmpty(strLevelPrefs))
        {
            levelPrefs = new LevelPrefs();
            levelPrefs.piecesPrefs = new List<PiecePrefs>();
        }
        else
        {
            levelPrefs = JsonConvert.DeserializeObject<LevelPrefs>(strLevelPrefs);
        }

        //tileRegion.Load(gameLevel);
        tileRegion.Testing();
        tileRegion.Testing2();
        if (!string.IsNullOrEmpty(gameLevel.pieces)) tileRegion.LoadPieces(gameLevel);
        LoadListSlots();
    }

    private void LoadListSlots()
    {
        listSlots.Clear();
        foreach (Transform slot in MonoUtils.instance.backgroundTilesTransform)
        {
            Tile tile = slot.GetComponent<Tile>();
            if (tile.isActive) listSlots.Add(tile);
        }
    }

    public void GeneratePositions()
    {
        string result = "";

        LoadListSlots();

        foreach(var tile in listSlots)
        {
            result += tile.position.x + "," + tile.position.y + "|";
        }
        gameLevel.positions = result;
        print(result);
    }

    private List<List<Tile>> pieces = new List<List<Tile>>();
    private int totalTiles = 0;

    public void GeneratePieces()
    {
        List<Tile> piece = new List<Tile>();
        foreach(var tile in listSlots)
        {
            if (tile.isActive == false && !HasElement(tile))
            {
                piece.Add(tile);
            }
        }

        if (piece.Count == 0) return;

        totalTiles += piece.Count;
        pieces.Add(piece);

        string result = "";
        
        foreach (var aPiece in pieces)
        {
            foreach (var tile in aPiece)
            {
                result += tile.position.x + "," + tile.position.y + "-";
            }
            result += "0,0" + "|";
        }

        if (redundantPiece != "") result += redundantPiece;

        print(result);

        if (totalTiles == listSlots.Count)
        {
            gameLevel.pieces = result;
            tileRegion.LoadPieces(gameLevel);
        }
    }

    public void AdjustPieces()
    {
        string result = "";
        foreach(var aPiece in tileRegion.pieces)
        {
            foreach (var position in aPiece.defaultPositions)
            {
                result += position.x + "," + position.y + "-";
            }
            result += aPiece.boardPositions[0].x + "," + aPiece.boardPositions[0].y;
            if (aPiece.isRedundant) result += "-r";
            result += "|";
        }

        print(result);
        gameLevel.pieces = result;
    }

    private string redundantPiece = "";
    public void GetRedundant()
    {
        List<Tile> piece = new List<Tile>();
        foreach (var tile in listSlots)
        {
            if (tile.isActive == false && !HasElement(tile))
            {
                piece.Add(tile);
            }
        }

        if (piece.Count == 0) return;
        
        foreach (var tile in piece)
        {
            redundantPiece += tile.position.x + "," + tile.position.y + "-";
        }
        redundantPiece += "0,0" + "-r|";
        print(redundantPiece);
    }

    public void ApplyLevel()
    {
#if UNITY_EDITOR
        GameLevel asset2 = ScriptableObject.CreateInstance<GameLevel>();
        asset2.positions = gameLevel.positions;
        asset2.pieces = gameLevel.pieces;

        AssetDatabase.CreateAsset(asset2, "Assets/Resources/Levels/World_" + world + "/Level_" + level + ".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = gameLevel;
#endif
    }

    private bool HasElement(Tile element)
    {
        foreach(var piece in pieces)
        {
            foreach(var tile in piece)
            {
                if (element == tile) return true;
            }
        }
        return false;
    }

    public void ShowHint()
    {
        tileRegion.ShowHint();
    }

    public void OnComplete()
    {
        SavePrefs();
        print("On Complete");
    }

    private void SavePrefs()
    {
        string data = JsonConvert.SerializeObject(levelPrefs);
        Utils.SetLevelData(GameState.chosenWorld, GameState.chosenLevel, data);
    }
}
