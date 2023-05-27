using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField]
    private int width;
    public int Width => width;

    [SerializeField]
    private int height;
    public int Height => height;

    [SerializeField]
    private Vector3 spawnPosition;

    public Bag bag;
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
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D raycastHit = Physics2D.GetRayIntersection(ray);
            if (raycastHit.transform != null && raycastHit.transform.parent.gameObject.TryGetComponent(out Tetramino newTetramino))
            {
                Debug.Log("Encontrou tetraminó");
                if (tetramino != null)
                    Destroy(tetramino.gameObject);
                Instantiate(bag.SelectTetramino(newTetramino), spawnPosition, Quaternion.identity, transform);
            }
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
