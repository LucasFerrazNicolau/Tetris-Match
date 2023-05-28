using System.Linq;
using UnityEngine;

public class Tetramino : MonoBehaviour
{
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Rotate()
    {
        foreach (Transform block in transform.GetComponentsInChildren<Transform>().Skip(1))
        {
            float oldX = block.position.x - transform.position.x;
            float oldY = block.position.y - transform.position.y;

            block.position = transform.position + new Vector3(oldY, -oldX, 0);
        }
    }
}
