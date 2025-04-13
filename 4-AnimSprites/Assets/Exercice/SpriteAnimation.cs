using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(RawImage))]
public class SpriteAnimation : MonoBehaviour
{
    public SpritesheetDescription spritesheet;
    public string sprite;
    public string defaultAnimation;
    public float frameLength;
    public AnimationScenario scenario;

    private Queue<AnimationScenario.SequenceItem> sequence;
    private AnimationScenario.SequenceItem currentAction;
    private float elapsed;
    private float frameElapsed;
    private string anim;
    private int frame;

    private RectTransform rectTransform;
    private RawImage image;
    private Texture2D spritesheetTexture;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<RawImage>();
        spritesheetTexture = image.texture as Texture2D;

        sequence = new Queue<AnimationScenario.SequenceItem>(scenario.sequence);
        anim = defaultAnimation;
    }

    public void Update()
    {
        if (sequence.Count > 0 && elapsed >= sequence.Peek().time)
        {
            currentAction = sequence.Dequeue();
        }

        elapsed += Time.deltaTime;
        DoAction();

        // Gestion de l'avancement du frame d'animation
        frameElapsed += Time.deltaTime;
        if (frameElapsed >= frameLength)
        {
            frameElapsed = 0f;
            frame++;

            if (spritesheet.GetRawFrame(GetFrameName()) == null)
            {
                frame = 0;
            }
        }

        // Affichage de la frame actuelle
        var frameId = GetFrameName();
        var frameDescr = spritesheet.GetRawFrame(frameId);

        if (frameDescr.HasValue && spritesheetTexture != null)
        {
            var frame = frameDescr.Value.frame;
            var sourceSize = frameDescr.Value.sourceSize;
            var spriteSourceSize = frameDescr.Value.spriteSourceSize;

            float texWidth = spritesheetTexture.width;
            float texHeight = spritesheetTexture.height;

            float u = (float)frame.x / texWidth;
            float v = (float)(texHeight - frame.y - frame.h) / texHeight;
            float uWidth = (float)frame.w / texWidth;
            float vHeight = (float)frame.h / texHeight;

            image.uvRect = new Rect(u, v, uWidth, vHeight);

            rectTransform.sizeDelta = new Vector2(sourceSize.w, sourceSize.h);

            float pivotX = (float)spriteSourceSize.x / sourceSize.w;
            float pivotY = 1f - (float)(spriteSourceSize.y + spriteSourceSize.h) / sourceSize.h;

            rectTransform.pivot = new Vector2(pivotX, pivotY);
        }
    }




    void DoAction()
    {
        if (currentAction.setAnim)
        {
            SetAnim(currentAction.newAnim);
        }

        rectTransform.anchoredPosition += currentAction.motion * Time.deltaTime;
    }

    string GetFrameName()
    {
        return string.Format("{0}/{1}/{2}", sprite, anim, frame);
    }

    void SetAnim(string newAnim)
    {
        if (anim == newAnim)
            return;

        anim = newAnim;
        frame = 0;
    }
}
