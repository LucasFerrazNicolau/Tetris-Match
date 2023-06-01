using System.Linq;
using UnityEngine;

public class Tetramino : MonoBehaviour
{
    [SerializeField]
    private TetraminoType type;

    public void Rotate()
    {
        foreach (Transform block in transform.GetComponentsInChildren<Transform>().Skip(1))
        {
            float oldX = block.position.x - transform.position.x;
            float oldY = block.position.y - transform.position.y;

            if (type == TetraminoType.I)
            {
                block.position = transform.position + new Vector3(oldY, oldX, 0);
            }
            else if (type != TetraminoType.O)
            {
                block.position = transform.position + new Vector3(oldY, -oldX, 0);
            }
        }
    }

    enum TetraminoType
    {
        I,
        J,
        L,
        O,
        S,
        T,
        Z
    }
}
