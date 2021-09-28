using Business.Abstract;
using Data.Entities;
using DataAccess.Abstract;
using Business.Generic;

namespace Business.Concrete
{
    public class CountryManager : DefinitionManager<Country, ICountryDal>, ICountryManager
    {
        private readonly ICountryDal entityDal;

        public CountryManager(ICountryDal entityDal) : base(entityDal)
        {
            this.entityDal = entityDal;
        }
        
        
    }
}
