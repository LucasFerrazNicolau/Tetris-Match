using UnityEngine;

public class Bag : MonoBehaviour
{
	[SerializeField]
	private int height;

	[SerializeField]
	private Tetramino[] tetraminoes;

	private Tetramino[] bag;
	private int selectedTetramino;
	private bool isEmpty;

	private void Awake()
	{
		LoadTetraminoes();

		selectedTetramino = -1;
		isEmpty = false;
	}

	private void Update()
	{
		if (isEmpty)
			ReloadTetraminoes();
	}

	private void LoadTetraminoes()
	{
		bag = new Tetramino[tetraminoes.Length];

		for (int i = 0; i < bag.Length; i++)
		{
			float offset = i * (height / bag.Length);
			Tetramino tetramino = Instantiate(tetraminoes[i], transform.position + offset * Vector3.down, Quaternion.identity, transform);
			bag[i] = tetramino;
		}
	}

	private void ReloadTetraminoes()
	{
		for (int i = 0; i < bag.Length; i++)
		{
			bag[i].gameObject.SetActive(true);
		}
	}

	private void CheckEmptiness()
	{
		bool isEmpty = true;

		for (int i = 0; i < bag.Length; i++)
		{
			if (bag[i].gameObject.activeInHierarchy)
				isEmpty = false;
		}

		this.isEmpty = isEmpty;
	}

	public Tetramino SelectTetramino(Tetramino tetramino)
	{
		if (selectedTetramino >= 0)
		{
			bag[selectedTetramino].gameObject.SetActive(true);
			selectedTetramino = -1;
		}

		selectedTetramino = System.Array.FindIndex(bag, x => x == tetramino);
		//selectedTetramino = Array.FindIndex(tetraminoBag, x => Object.ReferenceEquals(x, tetramino));
		tetramino.gameObject.SetActive(false);

		return tetraminoes[selectedTetramino];
	}

	public void ConsumeTetramino()
	{
		selectedTetramino = -1;

		CheckEmptiness();
	}
}
