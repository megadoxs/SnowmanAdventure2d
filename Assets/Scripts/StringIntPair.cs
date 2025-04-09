using System.Collections.Generic;

[System.Serializable]
public class StringIntPair
{
    public string key;
    public int value;

    public StringIntPair(string key, int value)
    {
        this.key = key;
        this.value = value;
    }
}

[System.Serializable]
public class StringIntPairList
{
    public List<StringIntPair> stringIntPairs = new();
}