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
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (IsValidPosition(tetramino.transform.position + Vector3.left * 0.1f))
                tetramino.transform.position += Vector3.left * 0.1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (IsValidPosition(tetramino.transform.position + Vector3.right * 0.1f))
                tetramino.transform.position += Vector3.right * 0.1f;
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
