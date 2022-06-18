using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectStranded
{
	class InputHandler
	{
		KeyboardState PrevState;
		KeyboardState NewState;

		public List<Keys> keypresses;

		public void Update(KeyboardState newState)
		{
			keypresses = new List<Keys>();
			NewState = newState;
			if (PrevState == null)
			{
				PrevState = NewState;
				return;
			}
				
			foreach(Keys key in (Keys[])Enum.GetValues(typeof(Keys)))
			{
				if (NewState.IsKeyDown(key) && PrevState.IsKeyUp(key))
					keypresses.Add(key);
			}
		}

		public bool IsPressed(Keys key)
		{
			return keypresses.Contains(key);
		}
	}
}
