using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MyTile : Tile
{
    // ���⿡ Ŀ���� ����� �߰��� �� ���� (��� ��)

#if UNITY_EDITOR
    [MenuItem("Assets/Create/2D/Tiles/Basic Tile")]
    public static void CreateTile()
    {
        var tile = ScriptableObject.CreateInstance<MyTile>();
        AssetDatabase.CreateAsset(tile, "Assets/NewTile.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = tile;
    }
#endif
}
