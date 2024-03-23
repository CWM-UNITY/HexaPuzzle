using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Superpow;

public class TileRegion2 : MonoBehaviour {
    public Vector2 size;

    private Dictionary<Vector2, Tile> slots = new Dictionary<Vector2, Tile>();
    private Dictionary<Vector2, Tile> bottomSlots = new Dictionary<Vector2, Tile>();


    public List<Piece> pieces = new List<Piece>();
    private List<Piece> hintPieces = new List<Piece>();

    public static TileRegion2 instance;
    private LevelPrefs levelPrefs;
    private GameLevel gameLevel;

    private void Awake()
    {
        instance = this;
    }

	private void Start()
    {
        //Testing();
        //Testing2();
    }

    public void Testing()
    {
        gameLevel = MakeLevelController.instance.gameLevel;
        if (string.IsNullOrEmpty(gameLevel.positions))
        {
            for (int col = 0; col < size.y; col++)
            {
                for (int row = 0; row < size.x; row++)
                {
                    Tile tile = Instantiate(MonoUtils.instance.tile_background);
                    tile.transform.SetParent(transform.Find("Backgrounds"));
                    tile.transform.localScale = Vector3.one;
                    Vector3 position = GetLocalPosition(col, row);
                    tile.transform.localPosition = position;
                    tile.position = new Vector2(col, row);
                    tile.transform.GetChild(0).GetComponent<Text>().text = col + "," + row;
                    tile.type = Tile.Type.Background;
                }
            }
        }

        else
        {
            List<string> positions = CUtils.BuildListFromString<string>(gameLevel.positions);
            foreach (var value in positions)
            {
                string[] values = value.Split(',');
                int col = int.Parse(values[0]);
                int row = int.Parse(values[1]);
                if (values.Length == 2)
                {
                    Tile tile = Instantiate(MonoUtils.instance.tile_background);
                    tile.transform.SetParent(MonoUtils.instance.backgroundTilesTransform);
                    tile.transform.localScale = Vector3.one;
                    Vector3 position = GetLocalPosition(col, row);
                    tile.transform.localPosition = position;
                    tile.position = new Vector2(col, row);
                    tile.transform.GetChild(0).GetComponent<Text>().text = col + "," + row;
                    tile.type = Tile.Type.Background;
                    slots.Add(tile.position, tile);
                }
            }
        }
    }

    public void Testing2()
    {
        for (int col = 0; col < 17; col++)
        {
            for (int row = 0; row < 7; row++)
            {
                Tile tile = Instantiate(MonoUtils.instance.tile_background);
                tile.transform.SetParent(MonoUtils.instance.bottomRegion);
                tile.transform.localScale = Vector3.one * Const.SCALED_TILE;
                Vector3 position = GetLocalPosition(col, row);
                tile.transform.localPosition = position * Const.SCALED_TILE;
                tile.position = new Vector2(col, row);
                //tile.transform.GetChild(0).GetComponent<Text>().text = col + "," + row;
                //tile.transform.GetChild(0).GetComponent<Text>().fontSize = 25;
                tile.transform.GetComponent<Image>().SetColorAlpha(0.4f);
                tile.type = Tile.Type.Background;
                bottomSlots.Add(tile.position, tile);
            }
        }
    }

    public void Load(GameLevel gameLevel)
    {
        levelPrefs = MakeLevelController.instance.levelPrefs;

        List <string> positions = CUtils.BuildListFromString<string>(gameLevel.positions);
        float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
        foreach(var value in positions)
        {
            string[] values = value.Split(',');
            int col = int.Parse(values[0]);
            int row = int.Parse(values[1]);
            Vector3 position = GetLocalPosition(col, row);

            if (values.Length == 2)
            {
                Tile tile = Instantiate(MonoUtils.instance.tile_background);
                tile.transform.SetParent(MonoUtils.instance.backgroundTilesTransform);
                tile.transform.localScale = Vector3.one;
                tile.transform.localPosition = position;
                tile.position = new Vector2(col, row);
                //tile.transform.GetChild(0).GetComponent<Text>().text = col + "," + row;
                tile.type = Tile.Type.Background;
                slots.Add(tile.position, tile);
            }

            if (position.x < minX) minX = position.x;
            if (position.x > maxX) maxX = position.x;
            if (position.y < minY) minY = position.y;
            if (position.y > maxY) maxY = position.y;
        }

        float regionWidth = maxX - minX + Tile.WIDTH;
        float regionHeight = maxY - minY + Tile.HEIGHT;

        GetComponent<RectTransform>().sizeDelta = new Vector2(regionWidth, regionHeight);
        transform.localPosition = GetComponent<RectTransform>().localPosition - new Vector3(regionWidth / 2, regionHeight / 2);

        LoadPieces(gameLevel);
    }

    public void LoadPieces(GameLevel gameLevel)
    {
        List<string> data = CUtils.BuildListFromString<string>(gameLevel.pieces);
        int id = 0;
        foreach(var oneData in data)
        {
            List<string> positions = CUtils.BuildListFromString<string>(oneData, '-');
            Vector2 bottomPosition = Vector2.zero;

            List<Vector2> defaultPositions = new List<Vector2>();

            bool isRedundant = positions[positions.Count - 1] == "r";

            if (isRedundant)
                positions.RemoveAt(positions.Count - 1);

            for (int i = 0; i < positions.Count; i++)
            {
                string[] values = positions[i].Split(',');
                int col = int.Parse(values[0]);
                int row = int.Parse(values[1]);
                Vector2 position = new Vector2(col, row);

                if (i ==  positions.Count - 1)
                {
                    bottomPosition = position;
                }

                if (i != positions.Count - 1)
                {
                    defaultPositions.Add(position);
                }
            }

            //PiecePrefs piecesPrefs = levelPrefs.piecesPrefs.Find(x => x.id == id);
            PiecePrefs piecesPrefs = null;

            bool isOnBoard = piecesPrefs != null;
            float scaleFactor = isOnBoard ? 1 : Const.SCALED_TILE;
            Transform parent = isOnBoard ? MonoUtils.instance.piecesTransform : MonoUtils.instance.bottomRegion;

            Piece piece = CreatePiece(defaultPositions, parent);
            piece.boardPositions = GetMatchPositions(piece, bottomPosition);
            piece.isRedundant = isRedundant;
            pieces.Add(piece);

            piece.id = id++;
            piece.bottomPosition = bottomPosition;

            piece.transform.localScale = Vector3.one * scaleFactor;

            if (isOnBoard)
            {
                piece.status = Piece.Status.OnBoard;

                Vector2 boardPosition = Utils.ConvertToVector2(piecesPrefs.boardPosition);
                piece.transform.localPosition = GetLocalPosition(boardPosition);
                piece.boardPositions = GetMatchPositions(piece, boardPosition);
                foreach (var pos in piece.boardPositions) slots[pos].hasCover = true;
            }
            else
            {
                piece.transform.localPosition = GetLocalPosition(bottomPosition) * Const.SCALED_TILE;
            }
            
            //foreach(var position in GetMatchPositions(piece, piece.bottomPosition))
            //{
            //    Tile tile = Instantiate(MonoUtils.instance.tile_background);
            //    tile.transform.SetParent(MonoUtils.instance.bottomRegion);
            //    tile.transform.localScale = Vector3.one * Const.SCALED_TILE;
            //    tile.transform.localPosition = GetLocalPosition(position) * Const.SCALED_TILE;
            //    tile.position = position;
            //    tile.type = Tile.Type.Background;
            //}
        }
    }

    private Piece CreatePiece(List<Vector2> positions, Transform parent)
    {
        Piece2 piece = Instantiate(MonoUtils.instance.piece2Prefab);
        piece.center = positions[0];
        piece.transform.SetParent(parent);
        piece.defaultPositions.AddRange(positions);

        Vector3 translateVector = GetLocalPosition(piece.center);

        foreach (var position in piece.defaultPositions)
        {
            int col = (int)position.x;
            int row = (int)position.y;

            Tile tile = Instantiate(MonoUtils.instance.tile_normal);
            tile.transform.SetParent(piece.transform);
            tile.transform.localScale = Vector3.one;
            Vector3 localPosition = GetLocalPosition(col, row);
            tile.transform.localPosition = localPosition - translateVector;
            tile.position = new Vector2(col, row);
            tile.piece2 = piece;
            tile.type = Tile.Type.Normal;

            piece.tiles.Add(tile);
        }

        foreach (var pos in piece.defaultPositions)
        {
            piece.tilePositions.Add(pos - positions[0]);
        }

        return piece;
    }

    private Vector3 GetLocalPosition(int col, int row)
    {
        return col % 2 == 0 ? new Vector3((col * 1.5f + 1) * Tile.EDGE_SIZE, (row + 0.5f) * Tile.HEIGHT) :
                        new Vector3((col * 1.5f + 1) * Tile.EDGE_SIZE, (row + 1) * Tile.HEIGHT);
    }

    private Vector3 GetLocalPosition(Vector2 position)
    {
        return GetLocalPosition((int)position.x, (int)position.y);
    }

    public bool CheckMatch(Piece2 piece)
    {
        float minDistance = float.MaxValue;
        Tile matchSlot = null;

        foreach(Tile slot in bottomSlots.Values)
        {
            if (slot.hasCover) continue;
            float distance = Vector3.Distance(piece.tileCenter.transform.position, slot.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                matchSlot = slot;
            }
        }

        piece.ResetMatchColor();
        piece.matches = new List<Tile>();

        if (minDistance < 0.2f)
        {
            bool isMatch = true;

            List<Vector2> matchPositions = GetMatchPositions(piece, matchSlot.position);

            foreach(var matchPos in matchPositions)
            {
                if (!bottomSlots.ContainsKey(matchPos) || bottomSlots[matchPos].hasCover)
                {
                    isMatch = false;
                    break;
                }
                else
                {
                    piece.matches.Add(bottomSlots[matchPos]);
                }
            }
            if (isMatch)
            {
                piece.HighlightMatchColor();
            }
            return isMatch;
        }
        return false;
    }

    public void ClearCovers(Piece piece)
    {
        foreach(var pos in piece.boardPositions)
        {
            slots[pos].hasCover = false;
        }
    }

    private List<Vector2> GetMatchPositions(Piece piece, Vector2 centerPosition)
    {
        List<Vector2> matchPositions = new List<Vector2>();
        int deltaX = (int)(centerPosition.x - piece.tileCenter.position.x);
        if (deltaX % 2 == 0)
        {
            matchPositions.AddRange(piece.tilePositions);
        }
        else
        {
            int modifier = centerPosition.x % 2 == 0 ? -1 : 1;
            foreach (var pos in piece.tilePositions)
            {
                float newY = pos.y;
                if (((int)pos.x) % 2 == 1)
                {
                    newY += modifier;
                }
                matchPositions.Add(new Vector2(pos.x, newY));
            }
        }

        for (int i = 0; i < matchPositions.Count; i++) matchPositions[i] += centerPosition;

        return matchPositions;
    }

    public bool ShowHint()
    {
        foreach(Piece piece in pieces)
        {
            if (hintPieces.Find(x=>x.id == piece.id)) continue;
            if (piece.status == Piece.Status.OnBoard)
            {
                var samePieces = FindSamePieces(piece);
                bool rightPos = false;
                foreach(var samePiece in samePieces)
                {
                    if (piece.boardPositions[0] == samePiece.center)
                    {
                        rightPos = true;
                        break;
                    }
                }
                
                if (!rightPos)
                {
                    ShowHint(piece);
                    return true;
                }
            }
        }

        foreach (Piece piece in pieces)
        {
            if (hintPieces.Find(x => x.id == piece.id)) continue;
            if (piece.status == Piece.Status.OnBottom)
            {
                ShowHint(piece);
                return true;
            }
        }
        return false;
    }

    private void ShowHint(Piece piece)
    {
        var samePieces = FindSamePieces(piece);
        foreach(var samePiece in samePieces)
        {
            Piece onPos = samePieces.Find(x => x.status == Piece.Status.OnBoard && x.boardPositions[0] == samePiece.center);
            if (onPos == null)
            {
                var hintPiece = CreatePiece(samePiece.defaultPositions, MonoUtils.instance.hintPiecesTransform);
                hintPiece.transform.localScale = Vector3.one * 7f;
                hintPiece.transform.localPosition = GetLocalPosition(samePiece.center);
                hintPiece.id = piece.id;
                iTween.ScaleTo(hintPiece.gameObject, Vector3.one, 0.3f);
                hintPieces.Add(hintPiece);
                return;
            }
        }
    }

    private List<Piece> FindSamePieces(Piece sample)
    {
        List<Piece> result = new List<Piece>();
        foreach(Piece piece in pieces)
        {
            if (piece == sample) continue;
            List<Vector2> matchPositions = GetMatchPositions(piece, sample.center);
            if (Compare2List(matchPositions, sample.defaultPositions))
            {
                result.Add(piece);
            }
        }
        result.Insert(0, sample);
        return result;
    }

    private bool Compare2List(List<Vector2> list1, List<Vector2> list2)
    {
        if (list1.Count != list2.Count) return false;
        for(int i = 0; i < list1.Count; i++)
        {
            if (list1[i] != list2[i]) return false;
        }
        return true;
    }

    public void CheckGameComplete()
    {
        bool isComplete = true;
        foreach(var slot in slots.Values)
        {
            if (!slot.hasCover)
            {
                isComplete = false;
                break;
            }
        }

        if (isComplete)
        {
            SavePrefs();
            MainController.instance.OnComplete(0);
        }
    }

    private void SavePrefs()
    {
        levelPrefs.piecesPrefs = new List<PiecePrefs>();
        foreach (Piece piece in pieces)
        {
            if (piece.status == Piece.Status.OnBoard)
            {
                var piecePrefs = new PiecePrefs();
                piecePrefs.id = piece.id;
                piecePrefs.boardPosition = Utils.ConvertToString(piece.boardPositions[0]);
                levelPrefs.piecesPrefs.Add(piecePrefs);
            }
        }
    }
}
