using Microsoft.EntityFrameworkCore;
using SMovie.Domain.Entity;
using SMovie.Domain.Repository;
using SMovie.Infrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMovie.Infrastructure.Repository
{
    public class NationRepository : SQLRepository<Nation>, INationRepository
    {
		private readonly SMovieSQLContext _context;

        public NationRepository(SMovieSQLContext context) : base(context)
        {
			_context = context;
        }

		public async Task<string> CheckNation(string nationId)
		{
			var nation = await _context.Nations.FirstOrDefaultAsync(x => x.NationId.Equals(nationId.Trim().ToUpper()));

			if(nation == null)
			{
				return string.Empty;
			}

			return nation.NationId;
		}
	}
}
