using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ShopCoreApp.Data.IRepositories;
using ShopCoreApp.Service.Interfaces;
using ShopCoreApp.Service.ViewModels.Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCoreApp.Service.Implementation
{
    public class FunctionService : IFunctionService
    {
        #region Variable

        private readonly IFunctionRepository _functionRepository;

        #endregion Variable

        #region Ctor

        public FunctionService(IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }

        #endregion Ctor

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region Action

        public Task<List<FunctionViewModel>> GetAll()
        {
            return _functionRepository.FindAll().ProjectTo<FunctionViewModel>().ToListAsync();
        }

        public List<FunctionViewModel> GetAllByPermission(Guid userId)
        {
            return _functionRepository.FindAll()
                .ProjectTo<FunctionViewModel>().ToList();
        }

        #endregion Action
    }
}