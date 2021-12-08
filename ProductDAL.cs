using FIXED_ASSET.DTO;
using POSX.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FIXED_ASSET.DAL
{

    public class ProductDAL
    {
        public static SqlConnection conn = new SqlConnection(DBConnection.GetConnectionString());
        public DataTable UpdateBrand(string brandId, string brandName, string _Created_By, bool activeStatus)
        {
            try
            {

                DataTable dt = new DataTable();
                if (conn.State == 0)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("Update_SP_Brand", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bId", brandId);
                cmd.Parameters.AddWithValue("@bName", brandName);
                cmd.Parameters.AddWithValue("@cBy", _Created_By);
                cmd.Parameters.AddWithValue("@sTatus", activeStatus);
                //cmd.Parameters.AddWithValue("@sT", 2);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;

            }
        }
        public DataTable UpdateType(string typeId, string typeDescription, string _Created_By, bool activeStatus)
        {
            try
            {

                DataTable dt = new DataTable();
                if (conn.State == 0)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("Update_SP_Type", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tId", typeId);
                cmd.Parameters.AddWithValue("@tDescription", typeDescription);
                cmd.Parameters.AddWithValue("@cBy", _Created_By);
                cmd.Parameters.AddWithValue("@sTatus", activeStatus);
                //cmd.Parameters.AddWithValue("@sT", 2);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;

            }
        }
        public DataTable UpdateUnit(string unitId, string Unit,string shortUnitName , string _Created_By, string activeStatus)
        {
            try
            {

                DataTable dt = new DataTable();
                if (conn.State == 0)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("Update_SP_Unit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uId", unitId);
                cmd.Parameters.AddWithValue("@uName", Unit);
                cmd.Parameters.AddWithValue("@suName", shortUnitName);
                cmd.Parameters.AddWithValue("@cBy", _Created_By);
                cmd.Parameters.AddWithValue("@sTatus", activeStatus);
                //cmd.Parameters.AddWithValue("@sT", 2);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;

            }
        }
        public DataTable UpdateCategory(string categoryId, string categoryShortName, string categoryName, string _Created_By,  bool sTatus)
        {
            try
            {

                DataTable dt = new DataTable();
                if (conn.State == 0)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("Update_SP_CATEGORY", conn);
                cmd.CommandType = CommandType.StoredProcedure;
  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cID", categoryId);
                cmd.Parameters.AddWithValue("@catSN", categoryShortName);
                cmd.Parameters.AddWithValue("@cName", categoryName);
                cmd.Parameters.AddWithValue("@cBy", _Created_By);
                //cmd.Parameters.AddWithValue("@dEsc", dEsc);
                //cmd.Parameters.AddWithValue("@cBy", userid);
                cmd.Parameters.AddWithValue("@sTatus", sTatus);
                //cmd.Parameters.AddWithValue("@sT", 2);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;

            }
        }
        public DataTable UpdateOperationCost(string OperationCostId, string description, string OperationCost, string _Created_By, bool sTatus)
        {
            try
            {

                DataTable dt = new DataTable();
                if (conn.State == 0)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("Update_SP_OPERATIONCOST", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@opID", OperationCostId);
                cmd.Parameters.AddWithValue("@des", description);
                cmd.Parameters.AddWithValue("@opcost", OperationCost);
                cmd.Parameters.AddWithValue("@cBy", _Created_By);
                //cmd.Parameters.AddWithValue("@dEsc", dEsc);
                //cmd.Parameters.AddWithValue("@cBy", userid);
                cmd.Parameters.AddWithValue("@sTatus", sTatus);
                //cmd.Parameters.AddWithValue("@sT", 2);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex.InnerException;

            }
        }
        public ProductBrandDTO getByBrand(int brandId)
        {
            ProductBrandDTO obj = new ProductBrandDTO();


            if (conn.State == 0)
            {
                conn.Open();
            }

            if (brandId != 0)
            {

                string selectData = "select * from PRODUCT_BRAND where BRND_ID = " + brandId + "";


                SqlCommand cmd = new SqlCommand(selectData, conn);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (obj != null)
                {
                    obj = (from DataRow dr in dt.Rows
                           select new ProductBrandDTO()
                           {
                               BRND_ID = Convert.ToInt32(dr["BRND_ID"]),
                               NAME = dr.IsNull("NAME") ? null : Convert.ToString(dr["NAME"]),
                               STATUS = dr.IsNull("STATUS") ? null : Convert.ToBoolean(dr["STATUS"]) == true ? "Active" : "InActive",

                           }).FirstOrDefault();
                }


            }
            return obj;
        }

        public ProductUnitDTO getByUnit(int unitId)
        {
            ProductUnitDTO obj = new ProductUnitDTO();


            if (conn.State == 0)
            {
                conn.Open();
            }

            if (unitId != 0)
            {

                string selectData = "select * from PRODUCT_UOM where UNIT_ID = " + unitId + "";


                SqlCommand cmd = new SqlCommand(selectData, conn);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (obj != null)
                {
                    obj = (from DataRow dr in dt.Rows
                           select new ProductUnitDTO()
                           {
                               UNIT_ID = Convert.ToInt32(dr["UNIT_ID"]),
                               NAME = dr.IsNull("NAME") ? null : Convert.ToString(dr["NAME"]),
                               SHORTNM = dr.IsNull("SHORTNM") ? null : Convert.ToString(dr["SHORTNM"]),
                               STATUS = dr.IsNull("STATUS") ? null : Convert.ToBoolean(dr["STATUS"]) == true ? "Active" : "InActive",

                           }).FirstOrDefault();
                }


            }
            return obj;
        }
        public ProductTypeDTO getByType(int typeId)
        {
            ProductTypeDTO obj = new ProductTypeDTO();


            if (conn.State == 0)
            {
                conn.Open();
            }

            if (typeId != 0)
            {

                string selectData = "select * from PRODUCT_TYPE where PTYP_ID = " + typeId + "";


                SqlCommand cmd = new SqlCommand(selectData, conn);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (obj != null)
                {
                    obj = (from DataRow dr in dt.Rows
                           select new ProductTypeDTO()
                           {
                               PTYP_ID = Convert.ToInt32(dr["PTYP_ID"]),
                               DESCRIPTION = dr.IsNull("DESCRIPTION") ? null : Convert.ToString(dr["DESCRIPTION"]),
                               STATUS = dr.IsNull("STATUS") ? null : Convert.ToBoolean(dr["STATUS"]) == true ? "Active" : "InActive",

                           }).FirstOrDefault();
                }


            }
            return obj;
        }
        public ProductCategoryDTO getByCategory(int catID)
           {
            ProductCategoryDTO obj = new ProductCategoryDTO();


            if (conn.State == 0)
            {
                conn.Open();
            }

            if (catID != 0)
            {

                string selectData = "SELECT * from PRODUCT_CATEG c inner join PRODUCT_TYPE as t on c.PTYPE_ID = t.PTYP_ID where c.PRD_CAT = " + catID + "";
                /* "select * from PRODUCT_CATEG where PTYPE_ID = " + catID + "";*/
                //"SELECT  PRD_CAT, PTYPE_ID, a.CAT_NAME,a.CAT_SHNAM,a.CREATED_AT,a.CREATED_BY,a.STATUS, a.UPDATED_AT,a.CREATED_BY FROM PRODUCT_CATEG a inner join PRODUCT_TYPE " +
                //"pt on a.PTYPE_ID = PTYP_ID where PRD_CAT = "+ catID +"";



                SqlCommand cmd = new SqlCommand(selectData, conn);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (obj != null)
                {
                    obj = (from DataRow dr in dt.Rows
                           select new ProductCategoryDTO()
                           {
                               PTYPE_ID = Convert.ToInt32(dr["PTYPE_ID"]),
                               PRD_CAT = Convert.ToInt32(dr["PRD_CAT"]),
                               CAT_NAME = dr.IsNull("CAT_NAME") ? null : Convert.ToString(dr["CAT_NAME"]),
                               DESCRIPTION = dr.IsNull("DESCRIPTION") ? null : Convert.ToString(dr["DESCRIPTION"]),
                               CAT_SHNAM = dr.IsNull("CAT_SHNAM") ? null : Convert.ToString(dr["CAT_SHNAM"]),
                               STATUS = dr.IsNull("STATUS") ? null : Convert.ToBoolean(dr["STATUS"]) == true ? "Active" : "InActive",

                           }).FirstOrDefault();
                }


            }
            return obj;
        }



        //public ProProduct SaveProduct(ProProduct Obj, string _Created_By)

        //{
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        if (conn.State == 0)
        //        {
        //            conn.Open();
        //        }



        //        SqlCommand cmd = new SqlCommand("Insrt_SP_PRODUCT", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@p_typeId", Obj.PTYP_ID);
        //        cmd.Parameters.AddWithValue("@p_categoryId ", Obj.PRDCAT_ID);
        //        cmd.Parameters.AddWithValue("@P_brandId", Obj.BRND_ID);
        //        cmd.Parameters.AddWithValue("@p_name", Obj.NAME);
        //        cmd.Parameters.AddWithValue("@p_code", Obj.PROD_CODE);
        //        //cmd.Parameters.AddWithValue("@p_barcode", p_barcode);

        //        cmd.Parameters.AddWithValue("@gi_SizeId", Obj.SIZE_UOMID);
        //        cmd.Parameters.AddWithValue("@gi_WeightId", Obj.WEIGHT);
        //        cmd.Parameters.AddWithValue("@gi_ColourId", Obj.COLOR);

        //        cmd.Parameters.AddWithValue("@p_size", Obj.SIZE);
        //        cmd.Parameters.AddWithValue("@p_weight", Obj.WEIGHT);
        //        cmd.Parameters.AddWithValue("@p_colour", Obj.COLOR);
        //        cmd.Parameters.AddWithValue("@p_price", Obj.STD_PRICE);
        //        cmd.Parameters.AddWithValue("@p_quantity", Obj.QUANTITY);
        //        cmd.Parameters.AddWithValue("@p_height", Obj.D_HEIGHT);
        //        cmd.Parameters.AddWithValue("@p_width", 1);
        //        cmd.Parameters.AddWithValue("@p_length", 1);
        //        cmd.Parameters.AddWithValue("@p_uomDimension", 1);
        //        cmd.Parameters.AddWithValue("@cBy", _Created_By);
        //        cmd.Parameters.AddWithValue("@sT", 1);
        //        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //        adpt.Fill(dt);
        //        return dt;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        //public DataTable SaveProduct(string p_typeId, string p_categoryId, string P_brandId, string p_name, string p_code, string p_barcode, string gi_SizeId, string gi_WeightId, string gi_ColourId, string p_size,
        //          string p_weight, string p_colour, string p_price, string p_quantity, string p_height, string p_width, string p_length, string p_uomDimension, string _Created_By)

        //{
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        if (conn.State == 0)
        //        {
        //            conn.Open();
        //        }



        //        SqlCommand cmd = new SqlCommand("Insrt_SP_PRODUCT", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@p_typeId", p_typeId);
        //        cmd.Parameters.AddWithValue("@p_categoryId ", p_categoryId);
        //        cmd.Parameters.AddWithValue("@P_brandId", P_brandId);
        //        cmd.Parameters.AddWithValue("@p_name", p_name);
        //        cmd.Parameters.AddWithValue("@p_code", p_code);
        //        //cmd.Parameters.AddWithValue("@p_barcode", p_barcode);

        //        cmd.Parameters.AddWithValue("@gi_SizeId", gi_SizeId);
        //        cmd.Parameters.AddWithValue("@gi_WeightId", gi_WeightId);
        //        cmd.Parameters.AddWithValue("@gi_ColourId", gi_ColourId);

        //        cmd.Parameters.AddWithValue("@p_size", p_size);
        //        cmd.Parameters.AddWithValue("@p_weight", p_weight);
        //        cmd.Parameters.AddWithValue("@p_colour", p_colour);
        //        cmd.Parameters.AddWithValue("@p_price", p_price);
        //        cmd.Parameters.AddWithValue("@p_quantity", p_quantity);
        //        cmd.Parameters.AddWithValue("@p_height", p_height);
        //        cmd.Parameters.AddWithValue("@p_width", p_width);
        //        cmd.Parameters.AddWithValue("@p_length", p_length);
        //        cmd.Parameters.AddWithValue("@p_uomDimension", p_uomDimension);
        //        cmd.Parameters.AddWithValue("@cBy", _Created_By);
        //        cmd.Parameters.AddWithValue("@sT", 1);
        //        SqlDataAdapter adpt = new SqlDataAdapter(cmd);
        //        adpt.Fill(dt);
        //        return dt;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}




        public DataTable SupplierSave(string vendorList, string price, string uOMList, string min_ord_qty, string _Created_By)

        {
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == 0)
                {
                    conn.Open();
                }



                SqlCommand cmd = new SqlCommand("Insert_SP_PRODUCT_SUPPLIER", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@vendorList", vendorList);
                cmd.Parameters.AddWithValue("@price ", price);
                cmd.Parameters.AddWithValue("@uOMList", uOMList);
                cmd.Parameters.AddWithValue("@qty", min_ord_qty);
                cmd.Parameters.AddWithValue("@cBy", _Created_By);
                cmd.Parameters.AddWithValue("@sT", 1);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public DataTable GetPCategoryList()
        {
            if (conn.State == 0)
            {
                conn.Open();
            }
            //string _sql = "Select * from ARTCATEGORY";
            string _sql = "SELECT * FROM PRODUCT_CATEG";
            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable GetProductUnitList()
        {
            if (conn.State == 0)
            {
                conn.Open();
            }
            //string _sql = "Select * from ARTCATEGORY";
            string _sql = "select UNIT_ID,NAME,SHORTNM, STATUS from PRODUCT_UOM";
            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public DataTable GetProductTypeList()
        {

            if (conn.State == 0)
            {
                conn.Open();
            }
            //string _sql = "Select * from ARTCATEGORY";
            string _sql = "select * from PRODUCT_TYPE";
            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
        public DataTable GetPBrandList()
        {

            if (conn.State == 0)
            {
                conn.Open();
            }
            //string _sql = "Select * from ARTCATEGORY";
            string _sql = "select NAME,STATUS,CREATED_AT,CREATED_BY from PRODUCT_BRAND";
            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public DataTable GetUOMList()
        {

            if (conn.State == 0)
            {
                conn.Open();
            }
            //string _sql = "Select * from ARTCATEGORY";
            string _sql = "select * from PRODUCT_UOM";
            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
        public DataTable GetBrandList()
        {

            if (conn.State == 0)
            {
                conn.Open();
            }
            //string _sql = "Select * from ARTCATEGORY";
            string _sql = "select * from PRODUCT_BRAND";
            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }




        public List<ProductCategoryDTO> GetProductCategory()
        {
            List<ProductCategoryDTO> productCategoryList = new List<ProductCategoryDTO>();

            using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GET_SP_PRODUCT_CATEGORY", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                if (productCategoryList != null)
                {
                    productCategoryList = (from DataRow dr in dt.Rows
                                           select new ProductCategoryDTO()
                                           {
                                               PRD_CAT = Convert.ToInt32(dr["PRD_CAT"]),
                                               PTYPE_ID = Convert.ToInt32(dr["PTYPE_ID"]),
                                               CAT_NAME = dr.IsNull("CAT_NAME") ? null : Convert.ToString(dr["CAT_NAME"]),
                                               CAT_SHNAM = dr.IsNull("CAT_SHNAM") ? null : Convert.ToString(dr["CAT_SHNAM"]),
                                               CREATED_AT = dr.IsNull("CREATED_AT") ? null : Convert.ToString(dr["CREATED_AT"]),
                                               DESCRIPTION = dr.IsNull("DESCRIPTION") ? null : Convert.ToString(dr["DESCRIPTION"]),
                                               //CODE = dr.IsNull("CODE") ? null : Convert.ToString(dr["CODE"]),
                                               STATUS = dr.IsNull("STATUS") ? null : Convert.ToBoolean(dr["STATUS"]) == true ? "Active" : "InActive",
                                               CREATED_BY = Convert.ToBoolean(dr["CREATED_BY"])
                                           }).ToList();
                }

            }

            return productCategoryList;
        }






        public DataTable SaveBrand(string bName, string userid, bool sTatus)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == 0)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert_SP_Brand", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@bName", bName);
                cmd.Parameters.AddWithValue("@sTatus", sTatus);
                cmd.Parameters.AddWithValue("@cBy", userid);
                //cmd.Parameters.AddWithValue("@sT", 1);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable SaveType(string tDescription, bool sTatus)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == 0)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert_SP_Type", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tDescription", tDescription);
                cmd.Parameters.AddWithValue("@sTatus", sTatus);
                //cmd.Parameters.AddWithValue("@sT", 1);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public DataTable SaveUnit(string Unit, string shortUnitName, string userid, bool sTatus)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == 0)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert_SP_Unit", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@unit", Unit);
                cmd.Parameters.AddWithValue("@shortName", shortUnitName);
                cmd.Parameters.AddWithValue("@sTatus", sTatus);
                cmd.Parameters.AddWithValue("@cBy", userid);

                //cmd.Parameters.AddWithValue("@sT", 1);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable SaveCategory(string productId, string cNAME, string pSName, string productList, string userid, bool sTatus)
        {
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == 0)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("Insrt_SP_PRODUCT_CATEGORY", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@cNAME", cNAME);
                cmd.Parameters.AddWithValue("@pSName", pSName);
                cmd.Parameters.AddWithValue("@productList", productList);
                cmd.Parameters.AddWithValue("@cBy", userid);
                cmd.Parameters.AddWithValue("@sTatus", sTatus);
                cmd.Parameters.AddWithValue("@sT", 1);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public DataTable GetCategoryByp_typeId(string ptypeId)
        {
            //using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
            //{
            try
            {
                if (conn.State == 0)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand("Get_SP_CategoryByp_typeId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ptypeId", Convert.ToInt32(ptypeId));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //conn.Open();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetProductList()
        {
            try
            {
                if (conn.State == 0)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Get_SP_ProductList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@R_ID", Convert.ToInt32(R_ID));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //conn.Open();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //using (SqlConnection con = new SqlConnection(DBConnection.GetConnectionString()))
            //{
            //    SqlCommand cmd = new SqlCommand("Get_SP_ProductList", con);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    //cmd.Parameters.AddWithValue("@R_ID", Convert                                                                                                                                                                                                                                                      ToInt32(R_ID));
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    con.Open();
            //    da.Fill(dt);
            //    con.Close();
            //    return dt;
            //}
        }

       public ProductOperationCostDTO getByOperationCost(int OperationCostId)
        {
            ProductOperationCostDTO obj = new ProductOperationCostDTO();


            if (conn.State == 0)
            {
                conn.Open();
            }

            if (OperationCostId != 0)
            {

                string selectData = "select * from PROD_OP_COST where PROD_OPID = " + OperationCostId + "";


                SqlCommand cmd = new SqlCommand(selectData, conn);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (obj != null)
                {
                    obj = (from DataRow dr in dt.Rows
                           select new ProductOperationCostDTO()
                           {
                               PROD_OPID = Convert.ToInt32(dr["PROD_OPID"]),
                               DESCRIPTION = dr.IsNull("DESCRIPTION") ? null : Convert.ToString(dr["DESCRIPTION"]),
                               COST_PER_H = dr.IsNull("COST_PER_H") ? null : Convert.ToString(dr["COST_PER_H"]),
                               STATUS = dr.IsNull("STATUS") ? null : Convert.ToBoolean(dr["STATUS"]) == true ? "Active" : "InActive",

                           }).FirstOrDefault();
                }


            }
            return obj;
        }
        public ProductOperationCostDTO deleteByOperationCost(int OperationCostId)
        {
            ProductOperationCostDTO obj = new ProductOperationCostDTO();


            if (conn.State == 0)
            {
                conn.Open();
            }

            if (OperationCostId != 0)
            {

                string selectData = "Delete from PROD_OP_COST where PROD_OPID = " + OperationCostId + "";


                SqlCommand cmd = new SqlCommand(selectData, conn);



                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (obj != null)
                {
                    obj = (from DataRow dr in dt.Rows
                           select new ProductOperationCostDTO()
                           {
                               PROD_OPID = Convert.ToInt32(dr["PROD_OPID"]),
                               DESCRIPTION = dr.IsNull("DESCRIPTION") ? null : Convert.ToString(dr["DESCRIPTION"]),
                               COST_PER_H = dr.IsNull("COST_PER_H") ? null : Convert.ToString(dr["COST_PER_H"]),
                               STATUS = dr.IsNull("STATUS") ? null : Convert.ToBoolean(dr["STATUS"]) == true ? "Active" : "InActive",

                           }).FirstOrDefault();
                }


            }
            return obj;
        }
        public DataTable SaveOperationCost(string tDescription, string cost_Per_H, string _Created_By, bool sTatus)
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == 0)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert_SP_OperationCost", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tDescription", tDescription);
                cmd.Parameters.AddWithValue("@cost_Per_H", cost_Per_H);
                cmd.Parameters.AddWithValue("@cBy", _Created_By);
                cmd.Parameters.AddWithValue("@sTatus", sTatus);
                //cmd.Parameters.AddWithValue("@sT", 1);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                adpt.Fill(dt);
                return dt;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetOperationCostList()
        {

            if (conn.State == 0)
            {
                conn.Open();
            }
            //string _sql = "Select * from ARTCATEGORY";
            string _sql = "select * from PROD_OP_COST";
            SqlCommand cmd = new SqlCommand(_sql, conn);
            cmd.CommandType = System.Data.CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

    }
}