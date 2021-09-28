using Data.Entities;
using Data.ViewModels;
using System.Threading.Tasks;
using Business.Generic;

namespace Business.Abstract
{
    public interface IAccountTypeManager : IDefinitionManager<AccountType>
    {
        Task<AccountTypeModel> GetModelByIdAsync(int id);
    }
}
