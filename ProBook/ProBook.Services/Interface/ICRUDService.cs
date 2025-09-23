using ProBook.Model.SearchObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Interface
{
    public interface ICRUDService<TModel,TSearch,TInsert,TUpdate>: IService<TModel,TSearch> where TModel : class where TSearch:BaseSearchObject
    {
        TModel Insert(TInsert request);
        TModel Update(int id,TUpdate request);
        TModel Delete(int id);
    }
}
