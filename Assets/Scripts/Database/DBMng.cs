
using UnityEngine;

public static class DBMng
{
    private const string LEVEL_DATA = "level_data-";
    private const string HABILITA_LEVEL = "habilita_level-";
    private const string MEDALHA_LEVEL = "medalha_level-";
    private const string VOLUME = "volume";
    
    public static int BuscarQtdItensColetaveisLevel(int idLevel)
    {
        return PlayerPrefs.GetInt(LEVEL_DATA + idLevel);
    }
}
