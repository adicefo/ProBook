using ProBook.Model.Helper;
using ProBook.Model.SearchObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Interface
{
    public interface IService<TModel,TSearch> where TModel : class where TSearch : BaseSearchObject
    {
          Task<PagedResult<TModel>> GetAsync(TSearch search);

          Task<TModel> GetByIdAsync(int id);
    }
}
