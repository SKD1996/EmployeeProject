using EmployeeDataAccess.Context;
using EmployeeDataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeBusinessLogic.Repository
{
    public class EmployeeRepository
    {
        private readonly DatabaseContext _DBContext;

        public EmployeeRepository(DatabaseContext DBContext)
        {
            _DBContext = DBContext;
        }

        public async Task<bool> AddNewMultipli(List<Employee> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    var employee = await this.GetByName(item.Name);
                    if (employee.Count != 0)
                    {
                        throw new Exception("That Employee already exist!!!!");
                    }

                }

                this._DBContext.AddRange(entity);
                await this._DBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Employee> AddNew(Employee entity)
        {
            try
            {
                var employee = await this.GetByName(entity.Name);
                if (employee.Count != 0)
                {
                    throw new Exception("That Employee already exist!!!!");
                }
                 this._DBContext.Add(entity);
                await this._DBContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<List<Employee>> GetAll()
        {
            try
            {
                var result = this._DBContext.Employees.ToList();
                return Task.FromResult(result);

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Employee> GetById(int Id)
        {
            try
            {
                var result = await this._DBContext.Employees.FirstOrDefaultAsync(i => i.Id == Id);
                if (result == null)
                {
                    throw new Exception("Employee ID doesn't exist!!!");
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<Employee>> GetByName(String Name)
        {
            try
            {
                var result = await this._DBContext.Employees.Where(i=>i.Name==Name).ToListAsync();
               
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<bool> Update(Employee employee)
        {
            try
            {
                var entity = await this.GetByName(employee.Name);
                if (entity.Count != 0)
                {   
                    throw new Exception("That Employee already exist!!!!");
                }
                _DBContext.Employees.Update(employee);
                _DBContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var employee = await this.GetById(id);
                if (employee == null)
                {
                    throw new Exception("Employee ID doesn't exist!!!");
                }
                this._DBContext.Remove(employee);
                await this._DBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
