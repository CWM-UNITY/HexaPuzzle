using System.Collections.Generic;

public class LevelPrefs
{
    public List<PiecePrefs> piecesPrefs { get; set; }
}

public class PiecePrefs
{
    public int id { get; set; }
    public string boardPosition { get; set; }
}