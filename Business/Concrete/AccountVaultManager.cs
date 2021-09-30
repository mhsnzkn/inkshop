using Business.Abstract;
using Data.Entities;
using DataAccess.Abstract;
using Business.Generic;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class AccountVaultManager : DefinitionManager<AccountVault, IAccountVaultDal>, IAccountVaultManager
    {
        private IAccountVaultDal dal { get; }
        public AccountVaultManager(IAccountVaultDal dal) : base(dal)
        {
            this.dal = dal;
        }

        public override async Task<List<SelectListItem>> GetForDropDown()
        {
            return await dal.Get().OrderBy(a => a.Name).Select(a => new SelectListItem
            {
                Text = a.Name+"-"+a.Description,
                Value = a.Id.ToString()
            }).ToListAsync();
        }
    }
}
