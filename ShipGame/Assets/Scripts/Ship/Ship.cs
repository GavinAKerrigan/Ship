using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Presets")]
    [SerializeField] ShipType type;
    public enum ShipType
    {
        Angel,
        Atlas,
        Dart,
        Eagle,
        Plastic
    }

    [Header("Sprites and Prefabs")]
    [SerializeField] Particle thrustPrefab;
    [SerializeField] Sprite angelSprite;
    [SerializeField] Sprite atlasSprite;
    [SerializeField] Sprite dartSprite;
    [SerializeField] Sprite eagleSprite;
    [SerializeField] Sprite plasticSprite;

    [Header("References")]
    public TextMeshProUGUI fuelDisplay;
    public string nextScene = "Level Select";

    Rigidbody2D rb;
    SpriteRenderer sr;
    Movement mv;
    Effects fx;
    Special sp;

    void Start()
    {
        // rigidbody
        rb = gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<PolygonCollider2D>();
        rb.gravityScale = 0;
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        // sprite
        sr = GetComponent<SpriteRenderer>();
        switch (type)
        {
            case ShipType.Angel:
                sr.sprite = angelSprite;
                break;
            case ShipType.Atlas:
                sr.sprite = atlasSprite;
                break;
            case ShipType.Dart:
                sr.sprite = dartSprite;
                break;
            case ShipType.Eagle:
                sr.sprite = eagleSprite;
                break;
            case ShipType.Plastic:
                sr.sprite = plasticSprite;
                break;
        }

        // scripts
        fx = gameObject.AddComponent<Effects>();
        mv = gameObject.AddComponent<Movement>();
        sp = gameObject.AddComponent<Special>();
        fx.particlePrefab = thrustPrefab;
        mv.fuelDisplay = fuelDisplay;
        mv.nextScene = nextScene;

    }

    void Update()
    {
        
    }
}
