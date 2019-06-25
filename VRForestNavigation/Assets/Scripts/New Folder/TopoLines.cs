using UnityEngine;
using System.Collections;

public class TopoLines : MonoBehaviour
{
    public string heightmapPath = "/Users/dave/desktop/terrain.raw";

    public Texture2D topoMap;

    public Material outputMaterial;

    void Start()
    {
        topoMap = ContourMap.FromRawHeightmap16bpp(heightmapPath);

        if (topoMap == null)
        {
            Debug.Log("Creation of topomap failed.");
        }
        else
        {
            Debug.Log("Creation of topomap was successful.");
        }

        if (outputMaterial != null)
        {
            outputMaterial.mainTexture = topoMap;
            SaveTextureAsPNG(topoMap, @"\Users\patow\Desktop\Forest\ForestProjectUnity\VRForestNavigation\Assets\Art");
        }
    }

    public static void SaveTextureAsPNG(Texture2D _texture, string _fullPath)
    {
        byte[] _bytes = _texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(_fullPath, _bytes);
        Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + _fullPath);
    }
}