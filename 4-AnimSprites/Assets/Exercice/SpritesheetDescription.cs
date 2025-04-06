using System.Collections.Generic;
using UnityEngine;
using YamlDotNet.Serialization;

namespace TextureFile
{
    public struct RectI
    {
        public int h;
        public int w;
        public int x;
        public int y;
    }

    public struct PointF
    {
        public float x;
        public float y;
    }

    public struct SizeI
    {
        public int h;
        public int w;
    }

    public struct FrameDescr
    {
        public RectI frame;
        public PointF pivot;
        public bool rotated;
        public SizeI sourceSize;
        public RectI spriteSourceSize;
        public bool trimmed;
    }

    public class File
    {
        public Dictionary<string, FrameDescr> frames;
        public Dictionary<string, object> meta;
    }
}

[CreateAssetMenu(fileName = "Spritesheet", menuName = "ScriptableObjects/2D Anim/Spritesheet", order = 1)]
public class SpritesheetDescription : ScriptableObject
{
    public TextAsset descriptionFile;
    public Texture2D texture;

    public class SpriteDescr
    {
        public RectInt frame;
        public Vector2Int sourceSize;
        public Vector2 pivot;

        public SpriteDescr(TextureFile.FrameDescr descr, Texture2D texture)
        {
            var frameDesc = descr.frame;
            var sourceSizeDesc = descr.sourceSize;
            var pivotDesc = descr.pivot;

            frame = new RectInt(frameDesc.x, frameDesc.y, frameDesc.w, frameDesc.h);

            // L'axe Y de la texture est inversé par rapport aux données de la sprite sheet
            frame.y = texture.height - frame.yMax;

            sourceSize = new Vector2Int(sourceSizeDesc.w, sourceSizeDesc.h);
            pivot = new Vector2(pivotDesc.x, pivotDesc.y);
        }
    }

    private readonly Dictionary<string, SpriteDescr> sprites = new Dictionary<string, SpriteDescr>();

    public void OnEnable()
    {
        if (descriptionFile == null)
            return;

        var deserializer = new DeserializerBuilder().Build();
        var descr = deserializer.Deserialize<TextureFile.File>(descriptionFile.text);
        foreach (var entry in descr.frames)
        {
            sprites.Add(entry.Key, new SpriteDescr(entry.Value, texture));
        }
    }

    public SpriteDescr GetFrame(string id)
    {
        SpriteDescr val;
        sprites.TryGetValue(id, out val);
        return val;
    }
}
