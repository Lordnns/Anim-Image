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

        var frameDesc = spritesheet.GetFrame(GetFrameName());

        image.uvRect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
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
