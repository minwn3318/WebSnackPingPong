using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MyTile : Tile
{
    // 여기에 커스텀 기능을 추가할 수 있음 (없어도 됨)

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
