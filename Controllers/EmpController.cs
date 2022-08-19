using CRUD_Dapper.Models;
using CRUD_Dapper.Repository;
using OfficeOpenXml;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_Dapper.Controllers
{
    public class EmpController : Controller
    {
        // GET all the deatils of EMP table
        public ActionResult GetAllEmpDetails(string SortOrder,string SortBy)
        {
            ViewBag.SortOrder = SortOrder;
            EmpRepository EmpRepo = new EmpRepository();
          
            var emps = EmpRepo.GetAllEmployees().ToList();
            switch (SortBy)
            {
                case "Name":
                    {
                        switch(SortOrder)
                        {
                            case "Asc":
                                {
                                    emps = emps.OrderBy(x => x.Name).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    emps = emps.OrderByDescending(x => x.Name).ToList();
                                    break;
                                }
                            default:
                                {
                                    emps = emps.OrderBy(x => x.Name).ToList();
                                    break;
                                }
                        }
                        break;
                    }

                case "Empid":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    emps = emps.OrderBy(x => x.Empid).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    emps = emps.OrderByDescending(x => x.Empid).ToList();
                                    break;
                                }
                            default:
                                {
                                    emps = emps.OrderBy(x => x.Empid).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "Address":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    emps = emps.OrderBy(x => x.Address).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    emps = emps.OrderByDescending(x => x.Address).ToList();
                                    break;
                                }
                            default:
                                {
                                    emps = emps.OrderBy(x => x.Address).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "cityName":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    emps = emps.OrderBy(x => x.cityName).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    emps = emps.OrderByDescending(x => x.cityName).ToList();
                                    break;
                                }
                            default:
                                {
                                    emps = emps.OrderBy(x => x.cityName).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "Gender":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    emps = emps.OrderBy(x => x.Gender).ToList();
                                    break;
                                }
                            case "Desc":
                                {
                                    emps = emps.OrderByDescending(x => x.Gender).ToList();
                                    break;
                                }
                            default:
                                {
                                    emps = emps.OrderBy(x => x.Gender).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        emps = emps.OrderBy(x => x.Name).ToList();
                        break;
                    }

            }
            return View(emps);            
        }
        [HttpPost]
        public ActionResult GetAllEmpDetails(string searchTxt)
        {
            EmpRepository EmpRepo = new EmpRepository();

            var emps = EmpRepo.GetAllEmployees().ToList();
            if (searchTxt != null)
            {
                emps = EmpRepo.SearchEmployee(searchTxt).ToList();
            }
            return View(emps);
        }

        public ActionResult AddEmployee()
        {
            EmpRepository EmpRepo = new EmpRepository();

            //store City table date in cityList variable
            var cityList = EmpRepo.GetAllCity().ToList();
            //passing city table data to viewBag for displaying on page
            ViewBag.CityList = new SelectList(cityList, "cityId", "cityName");

            return View();
        }

        [HttpPost]
        public ActionResult AddEmployee(EmpModel Emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpRepository EmpRepo = new EmpRepository();
                    EmpRepo.AddEmployee(Emp);                   
                }
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditEmpDetails(int id)
        {
            EmpRepository EmpRepo = new EmpRepository();

            var cityList = EmpRepo.GetAllCity().ToList();
            ViewBag.CityList1 = new SelectList(cityList, "cityId", "cityName");

            return View(EmpRepo.GetAllEmployees().Find(Emp => Emp.Empid == id));
        }

        [HttpPost]
        public ActionResult EditEmpDetails(int id, EmpModel obj)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();
                EmpRepo.UpdateEmployee(id, obj);
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult DeleteEmp(int id)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();
                if (EmpRepo.DeleteEmployee(id))
                {
                    ViewBag.Message = "Employee details deleted successfully";
                }
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return RedirectToAction("GetAllEmpDetails");
            }
        }

        public void ExportListUsingEPPlus()
        {

            EmpRepository EmpRepo = new EmpRepository();
            var data = EmpRepo.GetAllEmployees();
            
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";                
                Response.AddHeader("content-disposition", "attachment;  filename=Employees_List.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }

        public ActionResult PrintViewToPdf()
        {
            var report = new ActionAsPdf("GetAllEmpDetails");
            return report;
        }

    }
}