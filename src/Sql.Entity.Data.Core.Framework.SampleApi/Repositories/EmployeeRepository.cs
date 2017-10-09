﻿using Microsoft.Extensions.Logging;
using Sql.Entity.Data.Core.Framework.SampleApi.DataAccess;
using Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Controllers;
using Sql.Entity.Data.Core.Framework.SampleApi.DataAccess.Models;
using System;
using System.Collections.Generic;
using Yc.Sql.Entity.Data.Core.Framework.Model.Context;
using Yc.Sql.Entity.Data.Core.Framework.Model.Controller;

namespace Sql.Entity.Data.Core.Framework.SampleApi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        IEmployeeDataController dataController;
        ILogger<EmployeeRepository> logger;

        public EmployeeRepository(IEmployeeDataController dataController, ILogger<EmployeeRepository> logger)
        {
            this.dataController = dataController;
            this.logger = logger;
        }

        public List<Employee> GetAllEmployees(string correlation, string requestor)
        {
            logger.LogDebug($"Executing GetAllEmployees, Correlation: {correlation}, Requestor: {requestor}...");

            var responseInfo = dataController.GetEntities(
                new Employee
                {
                    ControllerFunction = EmployeeFunction.GetAll
                },
                GenerateRequestInfo(correlation, requestor));

            if (responseInfo.Status == Status.Failure)
                logger.LogError($"Executing GetAllEmployees failed, Message: {responseInfo.Message}, Correlation: {correlation}, Requestor: {requestor}...");

            else if (responseInfo.Status == Status.Success && responseInfo.Data != null)
                return (List<Employee>)responseInfo.Data;

            return null;
        }

        public Employee GetEmployeeById(int employeeId, string correlation, string requestor)
        {
            logger.LogDebug($"Executing GetEmployeeById, EmployeeId: {employeeId}, Correlation: {correlation}, Requestor: {requestor}...");

            var responseInfo = dataController.GetEntity(
                new Employee
                {
                    ControllerFunction = EmployeeFunction.GetById,
                    Id = employeeId
                },
                GenerateRequestInfo(correlation, requestor));

            if (responseInfo.Status == Status.Failure)
                logger.LogError($"Executing GetEmployeeById failed, Message: {responseInfo.Message}, Correlation: {correlation}, Requestor: {requestor}...");

            else if (responseInfo.Status == Status.Success && responseInfo.Data != null)
                return (Employee)responseInfo.Data;

            return null;
        }

        public List<Employee> GetEmployeesByName(string employeeName, string correlation, string requestor)
        {
            logger.LogDebug($"Executing GetEmployeesByName, Name: {employeeName}, Correlation: {correlation}, Requestor: {requestor}...");

            var responseInfo = dataController.GetEntities(
                new Employee
                {
                    ControllerFunction = EmployeeFunction.GetEmployeeByName,
                    FirstName = employeeName,
                    LastName = employeeName
                },
                GenerateRequestInfo(correlation, requestor));

            if (responseInfo.Status == Status.Failure)
                logger.LogError($"Executing GetEmployeesByName failed, Message: {responseInfo.Message}, Correlation: {correlation}, Requestor: {requestor}...");

            else if (responseInfo.Status == Status.Success && responseInfo.Data != null)
                return (List<Employee>)responseInfo.Data;

            return null;
        }

        public Employee InsertEmployee(Employee employee, string correlation, string requestor)
        {
            logger.LogDebug($"Executing InsertEmployee, Employee: {employee}, Correlation: {correlation}, Requestor: {requestor}...");

            employee.ControllerFunction = EmployeeFunction.Create;

            var responseInfo = dataController.SubmitChanges(employee, GenerateRequestInfo(correlation, requestor));

            if (responseInfo.Status == Status.Failure)
            {
                logger.LogError($"Executing InsertEmployee failed, Message: {responseInfo.Message}, Correlation: {correlation}, Requestor: {requestor}...");
                return null;
            }

            responseInfo = dataController.GetEntity(
                new Employee
                {
                    ControllerFunction = EmployeeFunction.GetById,
                    Id = Convert.ToInt32(responseInfo.Data)
                },
                GenerateRequestInfo(correlation, requestor));

            return (Employee)responseInfo.Data;
        }

        public Employee UpdateEmployee(Employee employee, string correlation, string requestor)
        {
            logger.LogDebug($"Executing UpdateEmployee, Employee: {employee}, Correlation: {correlation}, Requestor: {requestor}...");

            employee.ControllerFunction = EmployeeFunction.Update;

            var responseInfo = dataController.SubmitChanges(employee, GenerateRequestInfo(correlation, requestor));

            if (responseInfo.Status == Status.Failure)
            {
                logger.LogError($"Executing UpdateEmployee failed, Message: {responseInfo.Message}, Correlation: {correlation}, Requestor: {requestor}...");
                return null;
            }

            responseInfo = dataController.GetEntity(
                new Employee
                {
                    ControllerFunction = EmployeeFunction.GetById,
                    Id = Convert.ToInt32(responseInfo.Data)
                },
                GenerateRequestInfo(correlation, requestor));

            return (Employee)responseInfo.Data;
        }

        private IDataRequestInfo GenerateRequestInfo(string correlation, string requestor)
        {
            return new DataRequestInfo { CorrelationId = correlation, RequestorName = requestor };
        }
    }

    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees(string correlation, string requestor);
        Employee GetEmployeeById(int employeeId, string correlation, string requestor);
        List<Employee> GetEmployeesByName(string employeeName, string correlation, string requestor);
        Employee InsertEmployee(Employee employee, string correlation, string requestor);
        Employee UpdateEmployee(Employee employee, string correlation, string requestor);
    }
}
