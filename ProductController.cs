using FIXED_ASSET.DAL;
using FIXED_ASSET.DTO;
using POSX.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIXED_ASSET.Controllers
{
    public class ProductController : BaseController
    {

        UserDAL userDAL = new UserDAL();
        BasicUtilities _BasicUtilities = new BasicUtilities();
        ProductDAL productDAL = new ProductDAL();



        public ActionResult InsertList()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }









        public ActionResult Brand()
        {
            if (Session[POSxSession.LoggedUser] == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }
        // GET: Product
        public ActionResult Category()
        {
            if (Session[POSxSession.LoggedUser] == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }

        public ActionResult Type()
        {
            if (Session[POSxSession.LoggedUser] == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }
        public ActionResult OperationCost()
        {
            if (Session[POSxSession.LoggedUser] == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }

        public ActionResult RawMaterial()
        {
            return View();
        }

        public ActionResult RawMaterials()
        {
            return View();
        }


        public ActionResult Unit()
        {
            if (Session[POSxSession.LoggedUser] == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }

        public ActionResult SaveProduct( ProProduct Obj, List<ProVendor> vendorList)
        {
            string ProId = string.Empty;            
            string _Created_By = GetLoggedUserID();
            //Session["UserID"] = _Created_By;
            DataTable dt = new DataTable();
            DataTable dtn = new DataTable();
            SqlConnection con = new SqlConnection(DBConnection.GetConnectionString());
            //using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
            //{
               
                try
                {
                    if (con.State == 0)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("Insrt_SP_PRODUCT", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_typeId", Obj.PTYP_ID);
                    cmd.Parameters.AddWithValue("@p_categoryId ", Obj.PRDCAT_ID);
                    cmd.Parameters.AddWithValue("@P_brandId", Obj.BRND_ID);
                    cmd.Parameters.AddWithValue("@p_name", Obj.NAME);
                    cmd.Parameters.AddWithValue("@p_code", Obj.PROD_CODE);
                    //cmd.Parameters.AddWithValue("@p_barcode", p_barcode);
                    cmd.Parameters.AddWithValue("@gi_SizeId", Obj.SIZE_UOMID);
                    cmd.Parameters.AddWithValue("@gi_WeightId", 1);
                    cmd.Parameters.AddWithValue("@gi_ColourId", 1);

                    cmd.Parameters.AddWithValue("@p_size", 1);
                    cmd.Parameters.AddWithValue("@p_weight", 1);
                    cmd.Parameters.AddWithValue("@p_colour", 1);
                    cmd.Parameters.AddWithValue("@p_price", 1);
                    cmd.Parameters.AddWithValue("@p_quantity", 1);
                    cmd.Parameters.AddWithValue("@p_height", 1);
                    cmd.Parameters.AddWithValue("@p_width", 1);
                    cmd.Parameters.AddWithValue("@p_length", 1);
                    cmd.Parameters.AddWithValue("@p_uomDimension", 1);
                    cmd.Parameters.AddWithValue("@cBy", _Created_By);
                    cmd.Parameters.AddWithValue("@sT", 1);
                    SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                    adpt.Fill(dtn);
                    //return dtn;
                }

                catch (Exception ex)
                {

                    throw ex;
                }

            //}






            //var cat = productDAL.SaveProduct(Obj, _Created_By);
            //string _Created_By = (string)Session[POSxSession.LoggedUser];

            //DataTable cat = productDAL.SaveProduct(p_typeId, p_categoryId, P_brandId, p_name, p_code, p_barcode, gi_SizeId, gi_WeightId, gi_ColourId, p_size,
            //   p_weight, p_colour, p_price, p_quantity, p_height, p_width, p_length, p_uomDimension, _Created_By);

            if (dtn.Rows.Count > 0)
            {
                ProId = dtn.Rows[0]["ProductId"].ToString();

            }

            if (Int32.Parse(ProId) > 0)
            {
                foreach (ProVendor objExtract in vendorList)
                {

                    DataTable dt1 = new DataTable();

                    try
                    {
                        //using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
                        //{
                            if (con.State == 0)
                            {
                                con.Open();
                            }
                            SqlCommand cmd = new SqlCommand("Insert_SP_PRODUCT_SUPPLIER", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@pRODUCT_ID", ProId);
                            cmd.Parameters.AddWithValue("@sUPP_ID ", objExtract.SUPP_ID);
                            cmd.Parameters.AddWithValue("@cOST_PRICE", objExtract.reOrderPrice);
                            cmd.Parameters.AddWithValue("@lCOST_PRICE", 1);
                            cmd.Parameters.AddWithValue("@mORD_QTY", objExtract.min_ord_qty);
                            //cmd.Parameters.AddWithValue("@p_barcode", p_barcode);
                            cmd.Parameters.AddWithValue("@uOM_ID", objExtract.UNIT_ID);
                            cmd.Parameters.AddWithValue("@sTatus", 1);
                            cmd.Parameters.AddWithValue("@cBy", _Created_By);
                            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                            adpt.Fill(dt1);
                            //return dt;
                        //}
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }
            }
            

            return Json(ProId);
        }


        public ActionResult SupplierSave(
     string vendorList,
     string reOrderPrice,
     string uOMList,
     string min_ord_qty
    )
        {
            string _msg = string.Empty;

            try
            {
                string _Created_By = GetLoggedUserID();
                DataTable cat = productDAL.SupplierSave(vendorList, reOrderPrice, uOMList, min_ord_qty, _Created_By);

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

        public JsonResult GetUOMList()
        {
            try
            {
                string _Msg;
                DataTable dt_getBrandList = productDAL.GetUOMList();

                if (dt_getBrandList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> data = _BasicUtilities.GetTableRows(dt_getBrandList);
                    return Json(data);
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
        public JsonResult GetPBrandList()
        {
            try
            {
                string _Msg;
                DataTable dt_getBrandList = productDAL.GetBrandList();

                if (dt_getBrandList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> data = _BasicUtilities.GetTableRows(dt_getBrandList);
                    return Json(data);
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
        public JsonResult GetCategoryByp_typeId(string proTypeId)
        {
            try
            {
                string _Msg;
                DataTable dt_ChannelList = productDAL.GetCategoryByp_typeId(proTypeId.ToString());

                if (dt_ChannelList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> _ChannelList = _BasicUtilities.GetTableRows(dt_ChannelList);
                    return Json(_ChannelList);
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
        public JsonResult GetPTypeList()
        {
            try
            {
                string _Msg;
                DataTable dt_getTypeList = productDAL.GetProductTypeList();

                if (dt_getTypeList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> data = _BasicUtilities.GetTableRows(dt_getTypeList);
                    return Json(data);
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
        public JsonResult GetPCategoryList()
        {
            try
            {
                string _Msg;
                DataTable dt_getTypeList = productDAL.GetPCategoryList();

                if (dt_getTypeList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> data = _BasicUtilities.GetTableRows(dt_getTypeList);
                    return Json(data);
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
        public JsonResult GetProductUnitList()
        {
            try
            {
                string _Msg;
                DataTable dt_getUnitList = productDAL.GetProductUnitList();

                if (dt_getUnitList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> _unitList = _BasicUtilities.GetTableRows(dt_getUnitList);
                    return Json(new { data = _unitList }, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetProductTypeList()
        {
            try
            {
                string _Msg;
                DataTable dt_getTypeList = productDAL.GetProductTypeList();

                if (dt_getTypeList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> _typeList = _BasicUtilities.GetTableRows(dt_getTypeList);
                    return Json(new { data = _typeList }, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetBrandList()
        {
            try
            {
                string _Msg;
                DataTable dt_getBrandList = productDAL.GetBrandList();

                if (dt_getBrandList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> _brandList = _BasicUtilities.GetTableRows(dt_getBrandList);
                    return Json( new { data = _brandList }, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetProductCategoryList()
        {

            var category = productDAL.GetProductCategory();

            return Json(new { data = category }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductList()
        {
            try
            {
                string _Msg;
                DataTable dt_ChannelList = productDAL.GetProductList();

                if (dt_ChannelList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> _ChannelList = _BasicUtilities.GetTableRows(dt_ChannelList);
                    return Json(_ChannelList);
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
        public ActionResult EditBrand(int brandId)
        {
            var dataGet = productDAL.getByBrand(brandId);
            return Json(new { data = dataGet });

        }
        public ActionResult EditType(int typeId)
        {
            var dataGet = productDAL.getByType(typeId);
            return Json(new { data = dataGet });

        }
        public ActionResult EditUnit(int unitId)
        {
            //int unit = Convert.ToInt32(unitId);
            var dataGet = productDAL.getByUnit(unitId);
            return Json(new { data = dataGet });

        }
        public ActionResult EditCategory(int catID)
        {
            var dataGet = productDAL.getByCategory(catID);
            return Json(new { data = dataGet });

        }

        public JsonResult UpdateBrand(
     string brandId,
     string brandName,
     bool activeStatus
  )
        {
            try
            {
                string _msg = string.Empty;

                string _Created_By = GetLoggedUserID();

                DataTable cat = productDAL.UpdateBrand(brandId, brandName, _Created_By, activeStatus);
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
        public JsonResult UpdateType(
    string typeId,
    string typeDescription,
    bool activeStatus
 )
        {
            try
            {
                string _msg = string.Empty;

                string _Created_By = GetLoggedUserID();

                DataTable cat = productDAL.UpdateType(typeId, typeDescription, _Created_By, activeStatus);
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
        [HttpPost]
        public JsonResult UpdateUnit( string unitId, string Unit, string shortUnitName,string activeStatus)
        {
            try
            {
                string _msg = string.Empty;

                string _Created_By = GetLoggedUserID();

                DataTable cat = productDAL.UpdateUnit(unitId, Unit,shortUnitName, _Created_By, activeStatus);
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
        [HttpPost]
        public JsonResult updateCategory(string categoryId, string categoryShortName, string categoryName, bool activeStatus )
            {
            try
            {
                string _msg = string.Empty;

                string _Created_By = GetLoggedUserID();

                DataTable cat = productDAL.UpdateCategory(categoryId, categoryShortName, categoryName, _Created_By, activeStatus);
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
        public JsonResult updateOperationCost(string OperationCostId, string description, string OperationCost, bool activeStatus)
        {
            try
            {
                string _msg = string.Empty;

                string _Created_By = GetLoggedUserID();

                DataTable cat = productDAL.UpdateOperationCost(OperationCostId, description, OperationCost, _Created_By, activeStatus);
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







        [HttpPost]
        public ActionResult SaveBrand(
        string brandName,
        bool activeStatus
  )
        {
            string _msg = string.Empty;

            try
            {        
                string _Created_By = GetLoggedUserID();
                DataTable cat = productDAL.SaveBrand(brandName, _Created_By, activeStatus);

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

        [HttpPost]
        public ActionResult SaveType(
 string typeDescription,
 bool activeStatus
)
        {
            string _msg = string.Empty;

            try
            {
                //string _Created_By = GetLoggedUserID();
                DataTable cat = productDAL.SaveType(typeDescription, activeStatus);

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


        [HttpPost]
        public ActionResult SaveUnit(string Unit,string shortUnitName,bool activeStatus)
        {
            string _msg = string.Empty;

            try
            {
                string _Created_By = GetLoggedUserID();
                DataTable cat = productDAL.SaveUnit(Unit, shortUnitName,_Created_By, activeStatus);

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



        [HttpPost]
        public ActionResult SaveCategory(string productId,string categoryName,string categoryShortName,string ProductList, bool activeStatus )
        {
            string _msg = string.Empty;

            try
            {

                string _Created_By = GetLoggedUserID();
                DataTable cat = productDAL.SaveCategory(productId, categoryName, categoryShortName, ProductList, _Created_By, activeStatus);

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

        public ActionResult EditOperationCost(int OperationCostId)
        {
            var dataGet = productDAL.getByOperationCost(OperationCostId);
            return Json(new { data = dataGet });

        }
        public ActionResult DeleteOperationCost(int OperationCostId)
        {
            var dataGet = productDAL.deleteByOperationCost(OperationCostId);
            return Json(new { data = dataGet });

        }

        public ActionResult SaveOperationCost(string description, string cost_Per_H, bool activeStatus)
        {
            string _msg = string.Empty;

            try
            {
                string _Created_By = GetLoggedUserID();
                DataTable cat = productDAL.SaveOperationCost(description, cost_Per_H, _Created_By, activeStatus);

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

        public JsonResult GetOperationCostList()
        {
            try
            {
                string _Msg;
                DataTable dt_getOPCostList = productDAL.GetOperationCostList();

                if (dt_getOPCostList.Rows.Count > 0)
                {
                    _Msg = "DONE";
                    List<Dictionary<string, object>> list = _BasicUtilities.GetTableRows(dt_getOPCostList);
                    return Json(new { data = list }, JsonRequestBehavior.AllowGet);
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


    }








}