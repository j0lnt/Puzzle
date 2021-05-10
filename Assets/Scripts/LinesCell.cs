using UnityEngine;
using System.Collections;

namespace Puzzle
{
	public class LinesCell : MonoBehaviour {

		public int cellID;
		public Lines _lines;

		void OnMouseDown()
		{
			_lines.FindBall(cellID);
		}
	}
}

