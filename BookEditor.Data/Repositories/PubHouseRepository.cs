using System;
using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.DataModels;

namespace BookEditor.Data.Repositories
{
	public class PubHouseRepository : IPubHouseRepository
	{
		private readonly List<PubHouse> _items  = new List<PubHouse>();

		public void Delete(long id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<PubHouse> Get()
		{
			return _items;
		}

		public long Add(PubHouse t)
		{
			var id = (_items.Any() ? _items.Max(a => a.PubHouseId) : 0) + 1;
			t.PubHouseId = id;
			_items.Add(t);
			return id;
		}


		public PubHouse GetById(long id)
		{
			return _items.Single(t => t.PubHouseId == id);
		}
 
		public void Update(PubHouse t)
		{
			throw new NotImplementedException();
		}
	}
}
