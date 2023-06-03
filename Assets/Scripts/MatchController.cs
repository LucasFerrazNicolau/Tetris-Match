using System.Linq;
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

    private GameObject[,] grid;

    private void Awake()
    {
        grid = new GameObject[2 * width + 1, 2 * height + 1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (tetramino != null && IsValidHorizontalMove(Vector3.left))
                tetramino.transform.position += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (tetramino != null && IsValidHorizontalMove(Vector3.right))
                tetramino.transform.position += Vector3.right;
        }
        else if (tetramino != null && Input.GetKeyDown(KeyCode.W))
        {
            tetramino.Rotate();
        }
        else if (tetramino != null && Input.GetKeyDown(KeyCode.S))
        {
            tetramino.transform.position = GetFinalDownPosition();

            foreach (Transform block in tetramino.transform.GetComponentsInChildren<Transform>())
            {
                int x = (int)block.position.x + width;
                int y = (int)block.position.y + height;
                grid[x, y] = block.gameObject;
            }

            bag.ConsumeTetramino();
            tetramino = null;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D raycastHit = Physics2D.GetRayIntersection(ray);
            if (raycastHit.transform != null && raycastHit.transform.parent.gameObject.TryGetComponent(out Tetramino newTetramino))
            {
                if (newTetramino.transform.parent == bag.transform)
                {
                    if (tetramino != null)
                        Destroy(tetramino.gameObject);
                    tetramino = Instantiate(bag.SelectTetramino(newTetramino), spawnPosition, Quaternion.identity, transform);
                }
            }
        }
    }

    private bool IsValidHorizontalMove(Vector3 position)
    {
        int leftBound = (int)spawnPosition.x - width;
        int rightBound = (int)spawnPosition.x + width;
        bool existInvalidBlock = false;

        foreach (Transform block in tetramino.transform.GetComponentsInChildren<Transform>().Skip(1))
        {
            if (block.position.x + position.x < leftBound || block.position.x + position.x > rightBound)
            {
                existInvalidBlock = true;
                break;
            }
        }

        return !existInvalidBlock;
    }

    private bool IsValidVerticalMove(Vector3 position)
    {
        bool existInvalidBlock = false;

        foreach (Transform block in tetramino.transform.GetComponentsInChildren<Transform>().Skip(1))
        {
            Vector3 newBlockPosition = block.position - tetramino.transform.position + position;
            int indexX = (int)newBlockPosition.x + width;
            int indexY = (int)newBlockPosition.y + height;

            if (indexY < 0 || (indexY <= 2 * height && grid[indexX, indexY] != null))
            {
                existInvalidBlock = true;
                break;
            }
        }

        return !existInvalidBlock;
    }

    private Vector3 GetFinalDownPosition()
    {
        Vector3 downIncrement = Vector3.zero;

        while (IsValidVerticalMove(tetramino.transform.position + downIncrement + Vector3.down))
            downIncrement += Vector3.down;

        return tetramino.transform.position + downIncrement;
    }
}
