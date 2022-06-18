using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectStranded
{
	static class Util
	{
		public static void foreachXY(int px, int py, Action<int, int> act)
		{
			for(int y = 0; y < py; y++)
			{
				for(int x = 0; x < px; x++)
				{
					act(x, y);
				}
			}
		}

		static public int mod(int x, int m)
		{
			return (x % m + m) % m;
		}
	}
}
