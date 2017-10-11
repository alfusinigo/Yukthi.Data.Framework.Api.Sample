using Microsoft.Extensions.Logging;
using Yc.Sql.Entity.Data.Core.Framework.Access;
using Yc.Sql.Entity.Data.Core.Framework.Cache;
using Yc.Sql.Entity.Data.Core.Framework.Mapper;
using Yc.Sql.Entity.Data.Core.Framework.Model.Context;
using System.Data;

namespace Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Mappers
{
    public class EmployeeDataMapper : DataMapperBase, IEmployeeDataMapper
    {
        public EmployeeDataMapper(IDatabase database, ICacheRepository cacheRepository, ILogger<DataMapperBase> logger) 
            : base(database, cacheRepository, logger)
        {
        }

        public EmployeeDataMapper(IDatabase database, ILogger<DataMapperBase> logger)
            : base(database, logger)
        {
        }

        public override void SetFunctionSpecificEntityMappings(IBaseContext context)
        {
            switch (context.ControllerFunction)
            {
                case EmployeeFunction.Create:
                    context.Command = "dbo.InsertEmployee";
                    context.CommandType = CommandType.StoredProcedure;
                    break;
                case EmployeeFunction.Update:
                    context.Command = "UPDATE dbo.EMPLOYEE SET FIRST_NAME = ISNULL(@FIRST_NAME, FIRST_NAME), LAST_NAME = ISNULL(@LAST_NAME, LAST_NAME), ADDRESS = ISNULL(@ADDRESS, ADDRESS) WHERE ID=@ID";
                    context.CommandType = CommandType.Text;
                    break;
                case EmployeeFunction.GetById:
                    context.Command = "SELECT * FROM dbo.EMPLOYEE WHERE ID=@ID";
                    context.CommandType = CommandType.Text;
                    context.DependingDbTableNamesInCsv = "dbo.EMPLOYEE";
                    break;
                case EmployeeFunction.GetEmployeeByName:
                    context.Command = "SELECT * FROM dbo.EMPLOYEE WHERE FIRST_NAME=@FIRST_NAME OR LAST_NAME=@LAST_NAME";
                    context.CommandType = CommandType.Text;
                    context.DependingDbTableNamesInCsv = "dbo.EMPLOYEE";
                    break;
                case EmployeeFunction.GetAll:
                    context.Command = "SELECT * FROM dbo.EMPLOYEE";
                    context.CommandType = CommandType.Text;
                    context.DependingDbTableNamesInCsv = "dbo.EMPLOYEE";
                    break;
            }
        }
    }

    public interface IEmployeeDataMapper : IDataMapper
    {
    }
}
