using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Puzzle
{
	public class Lines : MonoBehaviour {

	public GameObject cellSprite;
	public GameObject ballSprite;
	public Color[] colors;
	public float shift;
	[SerializeField] public int size = 9;
	public Text scoreText;
	private int id;
	private GameObject[,] cells;
	private GameObject[,] tmp;
	private GameObject ball;
	private bool gameOver;
	private int score;
	private int scoreOld;

	void Start () 
	{
		shift = Mathf.Abs(shift);
		tmp = new GameObject[size,size];
		id = 0;
		float posX = -shift*(size-1)/2-shift;
		float posY = Mathf.Abs(posX+3); //смещение всей клетки вниз или вверх
		float Xreset = posX; //смещение всей клетки влево или вправо
		cells = new GameObject[size,size];
		for(int y = 0; y < size; y++)
		{
			posY -= shift;       //смещение между клетками по y 
			for(int x = 0; x < size; x++)
			{
				posX += shift;
				cells[x,y] = Instantiate(cellSprite, new Vector3(posX, posY, 0), Quaternion.identity) as GameObject;
				cells[x,y].GetComponent<LinesCell>()._lines = this;
				cells[x,y].GetComponent<LinesCell>().cellID = id;
				cells[x,y].transform.parent = transform;
				id++;
			}
			posX = Xreset;
		}
		gameOver = false;
		AddBalls();
	}

	public void FindBall(int ball_id)
	{
		if(!gameOver)
		{
			for(int y = 0; y < size; y++)
			{
				for(int x = 0; x < size; x++)
				{
					if(ball == null)
					{
						if(cells[x,y].GetComponent<LinesCell>().cellID == ball_id && tmp[x,y])
						{
							tmp[x,y].GetComponent<SpriteRenderer>().color = tmp[x,y].GetComponent<Balls>().color;
							ball = tmp[x,y];
							tmp[x,y] = null;
						}
					}
					else
					{
						if(cells[x,y].GetComponent<LinesCell>().cellID == ball_id && tmp[x,y] == null)
						{
							Color color = ball.GetComponent<Balls>().color;
							color.a = 0.5f;
							ball.GetComponent<SpriteRenderer>().color = color;
							ball.transform.position = cells[x,y].transform.position;
							tmp[x,y] = ball;
							ball = null;
							FindLines(true);
						}
					}
				}
			}
		}
	}

	IEnumerator ClearField()
	{
		yield return new WaitForSeconds (1);
		gameOver = false;
		int j = 0;
		while(j < id)
		{
			for(int y = 0; y < size; y++)
			{
				for(int x = 0; x < size; x++)
				{
					j++;
					Destroy(tmp[x,y].gameObject);
					tmp[x,y] = null;
				}
			}
		}
		Debug.Log("New Game");
		AddBalls();
	}

	void FindLines(bool isBall)
	{
		List<GameObject> arr = new List<GameObject>();
		bool getBall = true;
		int z = 0;
		int index = 0;
		while(z < id)
		{
			for(int y = 0; y < size; y++)
			{
				for(int x = 0; x < size; x++)
				{
					z++;
					if(x < size-1)
					{
						if(tmp[x,y] && tmp[x+1,y] && tmp[x,y].GetComponent<Balls>().color == tmp[x+1,y].GetComponent<Balls>().color && getBall)
						{
							if(index == 0) 
							{
								index = 2; 
								arr.Add(tmp[x,y]);
								arr.Add(tmp[x+1,y]);
							}
							else 
							{
								index++;
								arr.Add(tmp[x+1,y]);
							}
						}
						else if(index < 5)
						{
							index = 0;
							arr = new List<GameObject>();
						}
						else
						{
							getBall = false;
						}
					}

				}
			}
		}
		if(index == 0)
		{
			arr = new List<GameObject>();
			getBall = true;
			z = 0;
			while(z < id)
			{
				for(int y = 0; y < size; y++)
				{
					for(int x = 0; x < size; x++)
					{
						z++;
						if(x < size-1)
						{
							if(tmp[y,x] && tmp[y,x+1] && tmp[y,x].GetComponent<Balls>().color == tmp[y,x+1].GetComponent<Balls>().color && getBall)
							{
								if(index == 0) 
								{
									index = 2; 
									arr.Add(tmp[y,x]);
									arr.Add(tmp[y,x+1]);
								}
								else 
								{
									index++;
									arr.Add(tmp[y,x+1]);
								}
							}
							else if(index < 5)
							{
								index = 0;
								arr = new List<GameObject>();
							}
							else
							{
								getBall = false;
							}
						}
					}
				}
			}
		}

		if(isBall)
		{
			if(index < 5) AddBalls(); else StartCoroutine (WaitDestroy(arr));
		}
		else
		{
			if(index >= 5) StartCoroutine (WaitDestroy(arr));
		}
	}

	IEnumerator WaitDestroy(List<GameObject> item)
	{

		yield return new WaitForSeconds (0.2f);
		foreach(GameObject obj in item)
		{
			Destroy(obj.gameObject);
			score++;
		}
	}
	
	void AddBalls()
	{
		int ballCount = 0;
		int e = 0;
		while(e < id)
		{
			for(int y = 0; y < size; y++)
			{
				for(int x = 0; x < size; x++)
				{
					if(tmp[x,y] == null) ballCount++;
					e++;
				}
			}
		}
		if(ballCount > 2) ballCount = 2;
		int i = 0;
		while(i < ballCount)
		{
			int j = Random.Range(0, id);
			for(int y = 0; y < size; y++)
			{
				for(int x = 0; x < size; x++)
				{
					if(cells[x,y].GetComponent<LinesCell>().cellID == j && tmp[x,y] == null)
					{
						tmp[x,y] = Instantiate(ballSprite, cells[x,y].transform.position, Quaternion.identity) as GameObject;
						Color _color  = colors[Random.Range(0, colors.Length)];
						tmp[x,y].GetComponent<Balls>().color = _color;
						_color.a = 0.5f;
						tmp[x,y].GetComponent<SpriteRenderer>().color = _color;
						i++;
					}
				}
			}
		}
		ballCount = 0;
		e = 0;
		while(e < id)
		{
			for(int y = 0; y < size; y++)
			{
				for(int x = 0; x < size; x++)
				{
					if(tmp[x,y] == null) ballCount++;
					e++;
				}
			}
		}
	    if(ballCount == 0)
		{
			gameOver = true;
			Debug.Log("Game Over");
			scoreOld = score;
			score = 0;
			StartCoroutine (ClearField());
		}
		FindLines(false);
	}

	void OnGUI () 
	{
		scoreText.text = "Текущий Счет:\n" + score + "\n\nПрошлый счет:\n" + scoreOld;
	}
}

}