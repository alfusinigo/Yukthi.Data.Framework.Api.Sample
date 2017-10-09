using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Yc.Sql.Entity.Data.Core.Framework.Controller;
using Yc.Sql.Entity.Data.Core.Framework.Helper;
using Yc.Sql.Entity.Data.Core.Framework.Mapper;
using Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Mappers;

namespace Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Controllers
{
    public class EmployeeDataController : BaseDataController, IEmployeeDataController
    {
        public EmployeeDataController(IEmployeeDataMapper dataMapper, IConcurrentProcessor concurrentProcessor, ILogger<BaseDataController> logger) 
            : base(dataMapper, concurrentProcessor, logger)
        {
        }
    }

    public interface IEmployeeDataController : IDataController
    {
    }
}
