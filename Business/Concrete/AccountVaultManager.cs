using Business.Abstract;
using Data.Entities;
using DataAccess.Abstract;
using Business.Generic;

namespace Business.Concrete
{
    public class AccountVaultManager : DefinitionManager<AccountVault, IAccountVaultDal>, IAccountVaultManager
    {
        public AccountVaultManager(IAccountVaultDal dal) : base(dal)
        {

        }
    }
}
