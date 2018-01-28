using System;
using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public class PubHouseRepository : IPubHouseRepository
	{
		private readonly List<PubHouse> _items  = new List<PubHouse>();

		public void Delete(long id)
		{
			throw new NotImplementedException();
		}

		public List<PubHouse> Get()
		{
			return _items;
		}

		public void Add(PubHouse t)
		{
			long id = (_items.Any() ? _items.Max(a => a.PubHouseId) : 0) + 1;
			t.PubHouseId = id;
			_items.Add(t);
		}


		public PubHouse Get(long id)
		{
			return _items.Single(t => t.PubHouseId == id);
			;
		}
 
		public void Update(PubHouse t)
		{
			throw new NotImplementedException();
		}
	}
}
