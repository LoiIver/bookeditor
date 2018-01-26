using System;
using System.Collections.Generic;
using System.Linq;
using BookEditor.Data.Contracts;
using BookEditor.Data.Models;

namespace BookEditor.Data.Repositories
{
	public class PubHouseRepository : IPubHouseRepository
	{
		private static readonly List<PubHouse> _pubHouses = new List<PubHouse>()
		{
			new PubHouse {PubHouseId = 1, Name = "Просвещение"},
			new PubHouse {PubHouseId = 2, Name = "Педагогическая книга"},
			new PubHouse {PubHouseId = 3, Name = "Клевер"},
			new PubHouse {PubHouseId = 4, Name = "АСТ"},
			new PubHouse {PubHouseId = 5, Name = "Азбука"}
		};

		public void Delete(long id)
		{
			throw new NotImplementedException();
		}

		public List<PubHouse> Get()
		{
			return _pubHouses;
		}

		public PubHouse Get(long id)
		{
			return _pubHouses.Single(t => t.PubHouseId == id);
			;
		}
 
		public void Update(PubHouse t)
		{
			throw new NotImplementedException();
		}
	}
}
