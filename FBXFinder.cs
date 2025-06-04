using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FBXFinder {

    [MenuItem("GameObject/FindFBX")]
    public static void FindFBX()
    {
        GameObject Selected = Selection.activeGameObject;
        if (Selected == null)
        {
            Debug.LogWarning("No GameObject selected");
            return;
        }

        List<Object> fbxAssets = new List<Object>();

        // FindMethod
        MeshRenderer[] meshRenderers = Selected.GetComponentsInChildren<MeshRenderer>(true);;
        SkinnedMeshRenderer[] skinnedMeshRenderers = Selected.GetComponentsInChildren<SkinnedMeshRenderer>(true);
        
        foreach(MeshRenderer renderer in meshRenderers)
        {
            if(renderer.sharedMaterial != null)
            {
                MeshFilter meshFilter = renderer.GetComponent<MeshFilter>();
                if(meshFilter != null && meshFilter.sharedMesh != null)
                {
                    // findFBXFiles
                    string assetPath = AssetDatabase.GetAssetPath(meshFilter.sharedMesh);
                    if (string.IsNullOrEmpty(assetPath) && assetPath.ToLower().EndsWith(".fbx"))
                    {
                        Object fbxAsset = AssetDatabase.LoadMainAssetAtPath(assetPath);
                        if(fbxAsset != null && !fbxAssets.Contains(fbxAsset))
                        {
                            fbxAssets.Add(fbxAsset);
                        }
                    }
                }
            }
        }

        foreach (SkinnedMeshRenderer renderer in skinnedMeshRenderers)
        {
            if (renderer.sharedMesh != null)
            {
                    // findFBXFiles
                string assetPath = AssetDatabase.GetAssetPath(renderer.sharedMesh);
                if (!string.IsNullOrEmpty(assetPath) && assetPath.ToLower().EndsWith(".fbx"))
                {
                    Object fbxAsset = AssetDatabase.LoadMainAssetAtPath(assetPath);
                    if (fbxAsset != null && !fbxAssets.Contains(fbxAsset))
                    {
                        fbxAssets.Add(fbxAsset);
                    }
                }
            }
        }

        if(fbxAssets.Count > 0)
        {
            Selection.objects = fbxAssets.ToArray();
        }
        else
        {
            Debug.LogWarning("No FBX in object");
        }
    }

}
