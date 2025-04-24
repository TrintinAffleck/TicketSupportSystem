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
	public class UserServices
	{
		private readonly UserServicesContext _userServicesContext;

		internal UserServices(UserServicesContext userServicesContext)
		{
			_userServicesContext = userServicesContext;
		}

		public AspNetUser? GetUserById(string id)
		{
			return _userServicesContext.AspNetUsers.Select(s => s).Where(t => t.Id == id).FirstOrDefault();
		}
	}
}
