using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yc.Sql.Entity.Data.Core.Framework.Model.Attributes;
using Yc.Sql.Entity.Data.Core.Framework.Model.Context;

namespace Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Models
{
    [Serializable]
    public class Employee : BaseContext, IEmployee
    {
        [Mandatory("Employee Id", EmployeeFunction.Update, EmployeeFunction.GetById)]
        [Functions(EmployeeFunction.Update, EmployeeFunction.GetById)]
        [SqlParameter("@ID")]
        [ColumnNames("ID")]
        [Required]
        public int Id { get; set; }

        [Mandatory("Employee First Name", EmployeeFunction.Create, EmployeeFunction.Update)]
        [Functions(EmployeeFunction.Create, EmployeeFunction.Update, EmployeeFunction.GetEmployeeByName)]
        [SqlParameter("@FIRST_NAME")]
        [ColumnNames("FIRST_NAME")]
        [Required]
        public string FirstName { get; set; }

        [Mandatory("Employee Last Name", EmployeeFunction.Create, EmployeeFunction.Update)]
        [Functions(EmployeeFunction.Create, EmployeeFunction.Update, EmployeeFunction.GetEmployeeByName)]
        [SqlParameter("@LAST_NAME")]
        [ColumnNames("LAST_NAME")]
        [Required]
        public string LastName { get; set; }

        [Functions(EmployeeFunction.Create, EmployeeFunction.Update)]
        [SqlParameter("@ADDRESS")]
        [ColumnNames("ADDRESS")]
        public string Address { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public interface IEmployee
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Address { get; set; }
    }
}
