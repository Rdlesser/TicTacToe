using System.IO;
using UnityEditor;
using UnityEngine;

public class MyWindow : EditorWindow
{
    private Sprite xSymbol;
    private string xSymbolPath;
    private Sprite oSymbol;
    private string oSymbolPath;
    private Sprite background;
    private string backgroundPath;
    private string assetBundleName;
    private string buildAssetBundleButton = "Build asset bundle";
    
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/My Window")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        GetWindow(typeof(MyWindow));
    }
    
    void OnGUI()
    {
        xSymbol = (Sprite) EditorGUILayout.ObjectField("X symbol",
                                                                  xSymbol,
                                                                  typeof (Sprite),
                                                                  false);
        xSymbolPath = AssetDatabase.GetAssetPath(xSymbol);
        
        oSymbol = (Sprite) EditorGUILayout.ObjectField("O symbol",
                                                       oSymbol,
                                                       typeof (Sprite),
                                                       false);
        oSymbolPath = AssetDatabase.GetAssetPath(oSymbol);
        
        
        background = (Sprite) EditorGUILayout.ObjectField("Background",
                                                       background,
                                                       typeof (Sprite),
                                                       false);
        backgroundPath = AssetDatabase.GetAssetPath(background);
        
        assetBundleName = EditorGUILayout.TextField ("Asset bundle name", assetBundleName);

        if (GUILayout.Button(buildAssetBundleButton))
        {
            BuildAllAssetBundles();
        }


    }

    private void BuildAllAssetBundles()
    {
        // Create the array of bundle build details.
        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];
        buildMap[0].assetBundleName = assetBundleName;
        string[] assetPaths = {xSymbolPath, oSymbolPath, backgroundPath};
        buildMap[0].assetNames = assetPaths;

        string assetBundleDirectory = Application.streamingAssetsPath;
        Debug.Log("Streamin assets path: " + assetBundleDirectory);
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }


        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, buildMap, BuildAssetBundleOptions.None,
                                        BuildTarget.StandaloneOSX);
    }
}