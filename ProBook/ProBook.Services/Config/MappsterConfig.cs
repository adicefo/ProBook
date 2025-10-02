using Mapster;
using ProBook.Model.DTO;
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

        public static void RegisterMappings()
        {
            // Global settings to prevent circular references and stack overflow
            TypeAdapterConfig.GlobalSettings.Default.MaxDepth(2);
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);

            // Configure Notebook mapping - auto maps only matching properties
            // Collections are automatically ignored since they don't exist in the Model
            TypeAdapterConfig<Database.Notebook, Model.Model.Notebook>
                .NewConfig();

            // Configure Page mapping - auto maps only matching properties
            // Comments collection is automatically ignored since it doesn't exist in the Model
            TypeAdapterConfig<Database.Page, Model.Model.Page>
                .NewConfig()
                .MaxDepth(2); // Prevent deep nesting

            // Configure User mapping - auto maps only matching properties
            // Collections are automatically ignored since they don't exist in the Model
            TypeAdapterConfig<Database.User, Model.Model.User>
                .NewConfig()
                .MaxDepth(2); // Prevent deep nesting

            // Configure Comment mapping - explicitly map Page and User
            TypeAdapterConfig<Database.Comment, Model.Model.Comment>
                .NewConfig()
                .Map(dest => dest.Page, src => src.Page)
                .Map(dest => dest.User, src => src.User);
        }
    }
}
