using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField]
    private int width;
    public int Width => width;

    [SerializeField]
    private int height;
    public int Height => height;

    public Tetramino tetramino;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (IsValidPosition(tetramino.transform.position + Vector3.left))
                tetramino.transform.position += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (IsValidPosition(tetramino.transform.position + Vector3.right))
                tetramino.transform.position += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            tetramino.transform.Rotate(new Vector3(0, 0, 90));
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            while (IsValidPosition(tetramino.transform.position + Vector3.down))
                tetramino.transform.position += Vector3.down;
        }
    }

    private bool IsValidPosition(Vector3 position)
    {
        return position.x >= -width
            && position.x <= width
            && position.y >= -height
            && position.y <= height;
    }
}
