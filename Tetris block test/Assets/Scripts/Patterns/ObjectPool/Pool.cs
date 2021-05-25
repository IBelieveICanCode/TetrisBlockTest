using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjectPool
{
	public class Pool<T> : IEnumerable where T : IResettable, IDieable
	{
		public List<T> Members = new List<T>();
		public HashSet<T> Unavailable = new HashSet<T>();
        readonly IFactorable<T> _factory;
		public Pool(IFactorable<T> factory) : this(factory, 2) { }

		public Pool(IFactorable<T> factory, int poolSize = 2)
		{
			this._factory = factory;

			for (int i = 0; i < poolSize; i++)
			{
				Create();
			}
		}

		public T Allocate()
		{
			for (int i = 0; i < Members.Count; i++)
			{
				if (!Unavailable.Contains(Members[i]))
				{
					Unavailable.Add(Members[i]);
					return Members[i];
				}
			}
			T newMember = Create();
			Unavailable.Add(newMember);
			return newMember;
		}

		public void Release(T member)
		{
			member.Reset();
			Unavailable.Remove(member);
		}

		T Create()
		{
			T member = _factory.Create();
			member.PoolInit();
			Members.Add(member);
			return member;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Members.GetEnumerator();
		}
	}
}