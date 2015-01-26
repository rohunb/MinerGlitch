using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Glitch : MonoBehaviour 
{
    [SerializeField]
    SpriteRenderer spriteRenderer;

    public delegate void CollisionEvent(GameObject gameObject);
    public event CollisionEvent OnCollision = new CollisionEvent((GameObject) => { });

	void OnTriggerEnter2D(Collider2D other)
    {
        if (TagsAndLayers.GlitchCanCollide(other.gameObject.layer))
        {
            OnCollision(other.gameObject);
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }
    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
