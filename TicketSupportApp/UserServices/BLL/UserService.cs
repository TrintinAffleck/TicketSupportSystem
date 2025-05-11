using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServices.DAL;
using UserServices.Models;

namespace UserServices.BLL
{
	public class UserService
	{
		private readonly UserServicesContext _userServicesContext;

		internal UserService(UserServicesContext userServicesContext)
		{
			_userServicesContext = userServicesContext;
		}

		public async Task<AspNetUser?> GetUserById(Guid id)
		{
			try
			{
				if (id == Guid.Empty)
					return null;

				var user =  await _userServicesContext.AspNetUsers.Select(a => a)
					.Where(t => t.Id == id.ToString()).FirstOrDefaultAsync();

				if (user == null) 
					return null;

				return user;
				
			}
			catch (ArgumentNullException ex)
			{
				return null;
			}
			catch (InvalidOperationException ex)
			{
				return null;
			}
		}

		public async Task<List<AspNetUser>>? GetAllUsers()
		{
			return await _userServicesContext.AspNetUsers.Select(user => user).ToListAsync();
		}
	}
}
