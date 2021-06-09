using Business.Abstract;
using Core.Utility;
using Data.Entities;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CountryManager : ICountryManager
    {
        private readonly ICountryDal countryDal;

        public CountryManager(ICountryDal countryDal)
        {
            this.countryDal = countryDal;
        }
        
        public async Task<List<Country>> Get(System.Linq.Expressions.Expression<Func<Country, bool>> expression = null)
        {
            return await countryDal.Get(expression).ToListAsync();
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            Country result = null;
            try
            {
                result = await countryDal.GetByIdAsync(id);
            }
            catch (Exception)
            {
            }
            return result;
        }

        public async Task<Result> Add(Country entity)
        {
            var result = new Result();
            try
            {
                countryDal.Add(entity);
                await countryDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> Delete(Country entity)
        {
            var result = new Result();
            try
            {
                countryDal.Delete(entity);
                await countryDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }

        public async Task<Result> Update(Country entity)
        {
            var result = new Result();
            try
            {
                countryDal.Update(entity);
                await countryDal.Save();
            }
            catch (Exception ex)
            {
                result.SetError(ex.ToString());
            }

            return result;
        }
        public async Task<List<SelectListItem>> GetForDropDown()
        {
            return await countryDal.Get().Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.Id.ToString()
            }).ToListAsync();
        }
    }
}
