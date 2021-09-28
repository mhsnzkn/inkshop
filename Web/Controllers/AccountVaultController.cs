using Business.Abstract;
using Business.Concrete;
using Core.Utility.Datatables;
using Data.Constants;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers.Generic;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AccountVaultController : DefinitionBaseController<AccountVault, IAccountVaultManager>
    {
        private readonly IAccountVaultManager manager;

        public AccountVaultController(IAccountVaultManager manager) : base(manager)
        {
            this.manager = manager;
        }

    }
}
