using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectStranded
{
	enum Bodytoken
	{
		Handle,
		Sight,
		Internal,
		Root,
		Mobility
	}
	enum DamageType
	{
		Piercing,
		Slashing,
		Hacking,
		Bludgeoning,

		Fire,
		Cold,
		Corrode
	}
	class Bodypart
	{
		public Bodypart Parent;
		public List<Bodypart> AttachedParts;
		public Bodytoken[] Properties;
		public string Name;
		public bool IsVital;

		public int Health;
		public int MaxHealth;

		public bool RegisterDamage(Random r, DamageType dmgtype, float force)
		{
			bool child_vitaldead = false;
			List<Bodypart> internalorgans = new List<Bodypart>();
			foreach(Bodypart bp in AttachedParts)
			{
				if(bp.Properties.Contains(Bodytoken.Internal))
				{
					internalorgans.Add(bp);
				}
			}

			if(internalorgans.Count != 0 && dmgtype != DamageType.Slashing)
			{
				float appliedforce = force;

				switch(dmgtype)
				{
					case DamageType.Piercing:
						appliedforce *= 0.9f;
						break;
					case DamageType.Bludgeoning:
						appliedforce *= 0.6f;
						break;
					case DamageType.Hacking:
						appliedforce *= 1.2f;
						break;
					default:
						appliedforce /= 2;
						break;
				}

				foreach (Bodypart organ in internalorgans)
				{
					

					if(r.Next(2) == 0)
					{
						bool result = organ.RegisterDamage(r, dmgtype, appliedforce);
						child_vitaldead = child_vitaldead ? true : result;
					}
						

				}
			}

			Health -= (int)force;
			if ((Health <= 0 && IsVital) || child_vitaldead)
				return true;
			return false;
		}

		public Bodypart(int hp, Bodypart parent, List<Bodypart> children, Bodytoken[] props, bool vital, string name)
		{
			Health = MaxHealth = hp;
			Parent = parent;
			AttachedParts = children;
			Properties = props;
			IsVital = vital;
			Name = name;
		}
	}
	class Entity
	{
		public Vector2 Position;
		public Bodypart[] Anatomy;
		public int Size;
		public string Name;

		public bool IsDead;

		public void Draw(SpriteBatch sb, Atlas atlas, Vector2 offset)
		{
			atlas.Draw(sb, Name, offset, Color.White);
		}

		public Entity(Vector2 position, Bodypart[] anatomy, int size, string name)
		{
			Position = position;
			Anatomy = anatomy;
			Size = size;
			Name = name;
			IsDead = false;
		}

		public void Move(Vector2 offset)
		{
			float penalty = 1;
			foreach(Bodypart bp in Anatomy)
			{
				if (bp.Properties.Contains(Bodytoken.Mobility))
					//apply penality in movement equal to the normalized health value of that bodypary (1 for healthy, 0.1 for dead)
					penalty *= (bp.Health / bp.MaxHealth + 0.1f);
			}
			offset *= penalty;
			Position += offset;
		}
	}
}
