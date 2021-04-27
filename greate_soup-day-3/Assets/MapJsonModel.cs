using System;

[Serializable]
public class MapJsonModel
{
    public int Id;
    public EMapType Type;
    public JsonMap Map;
}

public class JsonMap
{
    public BuildingJson[] buildings;
    public SignJson[] signs;
    public RoadJson road;
}

public enum EMapType
{
    TEST,
    SAMPLE
}

public class BuildingJson
{
    public EBuildingTypeJson type;
    
    public int id;
    public int[] position;
    public int[] size;
}

public enum EBuildingTypeJson
{
    FACTORY,
    HOME
}

public class SignJson
{
    public ESignJsonType type;
    public int value;
    public int[] position;
}

public enum ESignJsonType
{
    STOP,
    SPEED_LIMIT
}

public class RoadJson
{
    public VertexJson[] vertexes;
    public EdgesJson[] edges;
}

public class VertexJson
{
    public int id;
    public int[] position;
}

public class EdgesJson
{
    public int from;
    public int to;
}