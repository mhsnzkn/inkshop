using AutoMapper;
using Business.Abstract;
using Data.Entities;
using Data.ViewModels;
using DataAccess.Abstract;
using System;
using System.Threading.Tasks;
using Business.Generic;

namespace Business.Concrete
{
    public class AccountTypeManager : DefinitionManager<AccountType, IAccountTypeDal>, IAccountTypeManager
    {
        private readonly IAccountTypeDal entityDal;
        private readonly IMapper mapper;

        public AccountTypeManager(IAccountTypeDal entityDal, IMapper mapper) : base(entityDal)
        {
            this.entityDal = entityDal;
            this.mapper = mapper;
        }

        public async Task<AccountTypeModel> GetModelByIdAsync(int id)
        {
            AccountTypeModel result = null;
            try
            {
                result = mapper.Map<AccountTypeModel>(await entityDal.GetByIdAsync(id));
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
