using Mapster;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProBook.Services.Config
{
    public static class MappsterConfig
    {

        public static void Configure()
        {
            TypeAdapterConfig<Database.Comment, Model.Model.Comment>
    .NewConfig()
    .Map(dest => dest.Page, src => src.Page)
    .Map(dest => dest.User, src => src.User)
    .Ignore(dest => dest.Page); // prevent cycles


        }
    }
}
