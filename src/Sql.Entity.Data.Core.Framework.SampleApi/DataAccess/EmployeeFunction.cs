using System;
using Yc.Sql.Entity.Data.Core.Framework.Model.Controller;

namespace Sql.Entity.Data.Core.Framework.SampleApi.DataAccess
{
    [Serializable]
    public class EmployeeFunction: BaseFunction
    {
        //Employee Specific Functions
        public const string GetEmployeeByName = "GET_EMPLOYEE_BY_NAME";
    }
}
