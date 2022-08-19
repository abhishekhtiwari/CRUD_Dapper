using CRUD_Dapper.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRUD_Dapper.Repository
{
    public class EmpRepository
    {
        public SqlConnection con;
        
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["EmployeeConnectionString"].ToString();
            con = new SqlConnection(constr);
        }
    
        public List<EmpModel> GetAllEmployees()
        {
            try
            {
                connection();
                con.Open();
                IList<EmpModel> EmpList = SqlMapper.Query<EmpModel>(con, "GetEmployees").ToList();
                con.Close();
                return EmpList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddEmployee(EmpModel objEmp)
        {

            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Name", objEmp.Name);                
                param.Add("@Address", objEmp.Address);
                param.Add("@Status", 1);
                param.Add("@cityId", objEmp.cityId);
                param.Add("@Gender", objEmp.Gender);
                param.Add("@CSharp", objEmp.CSharp);
                param.Add("@Java", objEmp.Java);
                param.Add("@Python", objEmp.Python);

                connection();
                con.Open();
                con.Execute("AddNewEmpDetails", param, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Get all data of city table
        public List<CityModel> GetAllCity()
        {
            try
            {
                connection();
                con.Open();
                IList<CityModel> CityList = SqlMapper.Query<CityModel>(con, "GetCity").ToList();
                con.Close();
                return CityList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateEmployee(int id, EmpModel objUpdate)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmpId", id);
                param.Add("@Name", objUpdate.Name);                
                param.Add("@Address", objUpdate.Address);          
                param.Add("@cityId", objUpdate.cityId);
                param.Add("@Gender", objUpdate.Gender);
                param.Add("@CSharp", objUpdate.CSharp);
                param.Add("@Java", objUpdate.Java);
                param.Add("@Python", objUpdate.Python);

                connection();
                con.Open();
                con.Execute("UpdateEmpDetails", param, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteEmployee(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmpId", Id);
                connection();
                con.Open();
                con.Execute("DeleteEmpById", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmpModel> SearchEmployee(string searchTxt)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@Search", searchTxt);
                connection();
                con.Open();
                IList<EmpModel> EmpList = SqlMapper.Query<EmpModel>(con,"SearchEmployeeDetails",param,commandType: CommandType.StoredProcedure).ToList();
                
                con.Close();
                return EmpList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}



