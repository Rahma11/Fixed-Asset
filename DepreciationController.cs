using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using POSX.Utilities;
using System.Data;
using POSX.DAL;

namespace FIXED_ASSET.Controllers
{
    public class DepreciationController : BaseController
    {
        AssetPOrderDAL _APOrderDAL = new AssetPOrderDAL();
        BasicUtilities _BasicUtilities = new BasicUtilities();

        // GET: Depreciation
        public ActionResult Depreciation()
        {
            return View();
        }
        public ActionResult Revaluation()
        {
            return View();
        }

        public ActionResult Imperiment()
        {
            return View();
        }

        public JsonResult GetMethodList()
        {
            try
            {
                string _Msg;
                DataTable dt_getMethodList = _APOrderDAL.GetMethodList();

                if (dt_getMethodList.Rows.Count > 0)
                {
                    _Msg = "DONE"; 
                    List<Dictionary<string, object>> _methodList = _BasicUtilities.GetTableRows(dt_getMethodList);
                    return Json(new { data = _methodList }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _Msg = "ERROR";
                    return Json(_Msg);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);

            }
        }

        [HttpPost]
        public ActionResult SaveMethod(string Method_Name, string calculation_basic, string description, string basic_rate, string adj_rate)
        {
            string _msg = "Success"; 

            try
            {
                string _Created_By = GetLoggedUserID();
               //int vcalculation_basic = Convert.ToInt32(calculation_basic);
               //int vbasic_rate = Convert.ToInt32(basic_rate);
               // int vadj_rate = Convert.ToInt32(adj_rate);

                DataTable cat = _APOrderDAL.SaveMethod(Method_Name, calculation_basic, description, basic_rate, adj_rate,  _Created_By);

                if (cat.Rows.Count > 0)
                {
                    _msg = cat.Rows[0]["result"].ToString();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Json(_msg);
        }
        public ActionResult EditMethod(int DEPID)
        {
            var dataGet = _APOrderDAL.getByMethod(DEPID);
            return Json(new { data = dataGet });

        }

        public ActionResult DeleteMethod(int DEPID)
        {
            var dataGet = _APOrderDAL.deleteByMethod(DEPID);
            return Json(new { data = dataGet });

        }

        public JsonResult UpdateMethod(string DEPID, string Method_Name, string calculation_basic, string description, string basic_rate, string adj_rate)
        {
            try
            {
                string _msg = string.Empty;

                string _Created_By = GetLoggedUserID();

                DataTable cat = _APOrderDAL.UpdateMethod(DEPID, Method_Name, calculation_basic, description, basic_rate, adj_rate, _Created_By);
                if (cat.Rows.Count > 0)
                {

                    _msg = cat.Rows[0]["result"].ToString();

                }

                return Json(_msg);
            }
            catch (System.Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}