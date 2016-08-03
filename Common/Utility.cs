using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Reflection;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PromolinkUS.Common
{
    public class Utility
    {
        /// <summary>
        /// Get Current User IP
        /// </summary>
        public static string CurrentUserIP
        {
            //get { return HttpContext.Current.Request.UserHostAddress; }
            get {
                if (!String.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"]))
                {
                    return HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
                }
                else
                {
                    return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
            }
        }

        public static string GetPageNameByOfferTypeID(string offerTypeID)
        {
            Dictionary<string, string> dicPageName = new Dictionary<string, string>();
            dicPageName.Add("1", "BXGY");
            dicPageName.Add("2", "PercentOff");
            dicPageName.Add("3", "SpendGet");
            dicPageName.Add("4", "DollarOff");
            dicPageName.Add("5", "WasAndNow");
            dicPageName.Add("6", "Clearance");
            dicPageName.Add("7", "Consumer");
            dicPageName.Add("8", "Credit");
            dicPageName.Add("9", "Utilities");
            dicPageName.Add("10", "TaxFree");
            return dicPageName.Where(p => p.Key == offerTypeID).FirstOrDefault().Value;            
        }

        public static List<SelectListItem> MakeSelectItemList(string strValue, string strText = "",string strSelValue="")
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            string[] keys = strValue.Split(new char[] {'|'},StringSplitOptions.RemoveEmptyEntries);
            if(strText.Length >0)
            {
                string[] texts = strText.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if(texts.Length == keys.Length)
                {
                    for(int i=0;i<keys.Length;i++)
                    {
                        lst.Add(new SelectListItem() { Text = texts[i], Value = keys[i], Selected = (keys[i] == strSelValue) });
                    }
                }
            }
            else
            {
                foreach(string strKey in keys)
                {
                    lst.Add(new SelectListItem() { Text = strKey, Value = strKey, Selected = (strKey == strSelValue) });
                }
            }
            return lst;
        }

        public static Dictionary<string, string> MakeDictionaryList(string strValue, string strText = "")
        {
            Dictionary<string, string> lst = new Dictionary<string, string>();
            string[] keys = strValue.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (strText.Length > 0)
            {
                string[] texts = strText.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (texts.Length == keys.Length)
                {
                    for (int i = 0; i < keys.Length; i++)
                    {
                        //lst.Add(new SelectListItem() { Text = texts[i], Value = keys[i], Selected = (keys[i] == strSelValue) });
                        lst.Add(keys[i], texts[i]);
                    }
                }
            }
            else
            {
                foreach (string strKey in keys)
                {
                    //lst.Add(new SelectListItem() { Text = strKey, Value = strKey, Selected = (strKey == strSelValue) });
                    lst.Add(strKey, strKey);
                }
            }
            return lst;
        }

        public static string JSSerializerObject(Object obj)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;
            return jsSerializer.Serialize(obj);
        }

        public static T JSDeserializeObject<T>(string input)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;
            return jsSerializer.Deserialize<T>(input ?? string.Empty);
        }              

        public static void ConvertModel<K, T>(K Sourcmodel, T destinateModel) where T : class
        {
            foreach (PropertyInfo info in typeof(K).GetProperties())
            {
                foreach (PropertyInfo iteminfo in typeof(T).GetProperties())
                {
                    if (info.Name == iteminfo.Name)
                    {
                        iteminfo.SetValue(destinateModel, info.GetValue(Sourcmodel, null), null);
                    }
                }
            }
        }

        public static Dictionary<string, string> ConvertObjectListToDictionay<T>(List<T> sourceObject, string keyName, string valueName) where T : class
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (T item in sourceObject)
            {
                string key = null;
                string _value = null;
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (info.Name == keyName)
                    {
                        key = info.GetValue(item, null).ToString();
                    }
                    if (info.Name == valueName)
                    {
                        _value = info.GetValue(item, null).ToString();
                    }
                    if (string.IsNullOrEmpty(key) == false && string.IsNullOrEmpty(_value) == false)
                    {
                        break;
                    }
                }
                if (string.IsNullOrEmpty(key) == false && string.IsNullOrEmpty(_value) == false)
                {
                    dic.Add(key, _value);
                }
            }
            return dic;
        }

        public static List<SelectListItem> ConvertObjectListToSelectListItems<T>(List<T> sourceObject, string keyName, string valueName) where T : class
        {            
            List<SelectListItem> lstSelectListItem = new List<SelectListItem>();
            foreach (T item in sourceObject)
            {
                string key = null;
                string _value = null;
                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    if (info.Name == keyName)
                    {
                        key = info.GetValue(item, null).ToString();
                    }
                    if (info.Name == valueName)
                    {
                        _value = info.GetValue(item, null).ToString();
                    }
                    if (string.IsNullOrEmpty(key) == false && string.IsNullOrEmpty(_value) == false)
                    {
                        break;
                    }
                }
                if (string.IsNullOrEmpty(key) == false && string.IsNullOrEmpty(_value) == false)
                {
                    lstSelectListItem.Add(new SelectListItem() { Text = _value, Value = key });                    
                }
            }
            return lstSelectListItem;
        }

        public static DataTable ToDataTable<T>(IEnumerable<T> list, Action fn)
        {
            var dtReturn = new DataTable();
            PropertyInfo[] oProps = null;
            foreach (T rec in list)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;
                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }
                        if (colType.FullName == "System.DateTime")
                        {
                            colType = typeof (string);
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow();
                foreach (PropertyInfo pi in oProps)
                {
                    Type colType = pi.PropertyType;
                    if (((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>))) || colType.FullName == "System.DateTime")
                    {
                        if (pi.GetValue(rec, null) == null)
                        {
                            dr[pi.Name] = DBNull.Value;
                        }
                        else
                        {
                            if (pi.GetValue(rec, null).ToString().IndexOf(' ') > -1)
                                dr[pi.Name] = pi.GetValue(rec, null).ToString().Substring(0, pi.GetValue(rec, null).ToString().IndexOf(' '));
                            else
                                dr[pi.Name] = pi.GetValue(rec, null);
                        }
                    }
                    else
                    {
                        dr[pi.Name] = pi.GetValue(rec, null) ?? DBNull.Value;
                    }
                }
                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }

    /// <summary>
    /// This is used for when you are programmatically generating controls manually
    /// </summary>
    public static class PromolinkColoring
    {
        public const string GreenColoring = "#92D050";
        public const string BlueColoring = "#1647CA";
        public const string BackgroundGreenColoring = "#669900";

        public const string HeaderBackColoring = "#DCDCDC";
        public const string AlternatingRowBackColoring = "#E6E6E6";
        public const string RowBackColoring = "#BBCCFF";
    }

    public static class Exporting
    {
        /// <summary>
        /// Summary description for CreateWorkbook
        /// </summary>
        public class ExcelGenerator
        {
            /// <summary>
            /// This will return a stringbuilder with the XML markup for the excel file, you can save this as an excel file if you so choose.
            /// - Josh 2/10/2011
            /// </summary>
            /// <param name="source">You can give either a Dataset or DataTable</param>
            /// <returns></returns>
            public static StringBuilder exportToXML(object data)
            {
                DataSet source = new DataSet();
                if (data is DataTable)
                    source.Tables.Add((data as DataTable).Copy());
                else if (data is DataSet)
                    source = (data as DataSet);
                else
                    new Exception("ExcelGenerator.exportToExcel(object data) must be given a dataTable or DataSet!");


                //System.IO.StreamWriter excelDoc;

                //string excelDoc = string.Empty;
                StringBuilder excelDoc = new StringBuilder();

                //excelDoc = new System.IO.StreamWriter();
                const string startExcelXML = "<xml version='1.0'>\r\n<Workbook " +
                      "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                      " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                      "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                      "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                      "office:spreadsheet\">\r\n <Styles>\r\n " +
                      "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                      "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                      "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                      "\r\n <Protection/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                      "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                      "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                      " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                      "ss:Format=\"0.00\"/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                      "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                      "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
                      "</Styles>\r\n ";
                const string endExcelXML = "</Workbook>";

                int rowCount = 0;
                int sheetCount = 1;
                /*
               <xml version>
               <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"
               xmlns:o="urn:schemas-microsoft-com:office:office"
               xmlns:x="urn:schemas-microsoft-com:office:excel"
               xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">
               <Styles>
               <Style ss:ID="Default" ss:Name="Normal">
                 <Alignment ss:Vertical="Bottom"/>
                 <Borders/>
                 <Font/>
                 <Interior/>
                 <NumberFormat/>
                 <Protection/>
               </Style>
               <Style ss:ID="BoldColumn">
                 <Font x:Family="Swiss" ss:Bold="1"/>
               </Style>
               <Style ss:ID="StringLiteral">
                 <NumberFormat ss:Format="@"/>
               </Style>
               <Style ss:ID="Decimal">
                 <NumberFormat ss:Format="0.0000"/>
               </Style>
               <Style ss:ID="Integer">
                 <NumberFormat ss:Format="0"/>
               </Style>
               <Style ss:ID="DateLiteral">
                 <NumberFormat ss:Format="mm/dd/yyyy;@"/>
               </Style>
               </Styles>
               <Worksheet ss:Name="Sheet1">
               </Worksheet>
               </Workbook>
               */
                excelDoc.Append(startExcelXML);
                for (int i = 0; i < source.Tables.Count; i++)
                {
                    excelDoc.Append("<Worksheet ss:Name=\"" + source.Tables[i].TableName + "\">");
                    excelDoc.Append("<Table>");
                    // set the column width with the headers
                    for (int x = 0; x < source.Tables[i].Columns.Count; x++)
                    {
                        double width = (source.Tables[i].Columns[x].ColumnName.Length * 10);
                        excelDoc.Append("<Column ss:Width=\"" + width + "\"/>");
                    }
                    // set cells
                    excelDoc.Append("<Row>");
                    for (int x = 0; x < source.Tables[i].Columns.Count; x++)
                    {
                        excelDoc.Append("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                        excelDoc.Append(source.Tables[i].Columns[x].ColumnName);
                        excelDoc.Append("</Data></Cell>");
                    }
                    excelDoc.Append("</Row>");
                    foreach (DataRow x in source.Tables[i].Rows)
                    {
                        rowCount++;
                        //if the number of rows is > 64000 create a new page to continue output
                        if (rowCount == 64000)
                        {
                            rowCount = 0;
                            sheetCount++;
                            excelDoc.Append("</Table>");
                            excelDoc.Append(" </Worksheet>");
                            excelDoc.Append("<Worksheet ss:Name=\"" + source.Tables[i].TableName + "_" + sheetCount + "\">");
                            excelDoc.Append("<Table>");
                            for (int j = 0; j < source.Tables[i].Columns.Count; j++)
                            {
                                int width = (source.Tables[i].Columns[j].ColumnName.Length * 10);
                                excelDoc.Append("<Column ss:Width=\"24\"/>");
                            }
                            // Create new header row
                            excelDoc.Append("<Row>");
                            for (int x2 = 0; x2 < source.Tables[i].Columns.Count; x2++)
                            {
                                excelDoc.Append("<Cell ss:StyleID=\"BoldColumn\"><Data ss:Type=\"String\">");
                                excelDoc.Append(source.Tables[i].Columns[x2].ColumnName);
                                excelDoc.Append("</Data></Cell>");
                            }
                            excelDoc.Append("</Row>");

                        }
                        excelDoc.Append("<Row>"); //ID=" + rowCount + "

                        for (int y = 0; y < source.Tables[i].Columns.Count; y++)
                        {
                            System.Type rowType;
                            rowType = x[y].GetType();
                            switch (rowType.ToString())
                            {
                                case "System.String":
                                    string XMLstring = x[y].ToString();
                                    XMLstring = XMLstring.Trim();
                                    XMLstring = XMLstring.Replace("&", "&");
                                    XMLstring = XMLstring.Replace(">", ">");
                                    XMLstring = XMLstring.Replace("<", "<");
                                    excelDoc.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                                   "<Data ss:Type=\"String\">");
                                    excelDoc.Append(XMLstring);
                                    excelDoc.Append("</Data></Cell>");
                                    break;
                                case "System.DateTime":
                                    //Excel has a specific Date Format of YYYY-MM-DD followed by  

                                    //the letter 'T' then hh:mm:sss.lll Example 2005-01-31T24:01:21.000

                                    //The Following Code puts the date stored in XMLDate 

                                    //to the format above

                                    DateTime XMLDate = (DateTime)x[y];
                                    string XMLDatetoString = ""; //Excel Converted Date

                                    XMLDatetoString = XMLDate.Year.ToString() +
                                         "-" +
                                         (XMLDate.Month < 10 ? "0" +
                                         XMLDate.Month.ToString() : XMLDate.Month.ToString()) +
                                         "-" +
                                         (XMLDate.Day < 10 ? "0" +
                                         XMLDate.Day.ToString() : XMLDate.Day.ToString()) +
                                         "T" +
                                         (XMLDate.Hour < 10 ? "0" +
                                         XMLDate.Hour.ToString() : XMLDate.Hour.ToString()) +
                                         ":" +
                                         (XMLDate.Minute < 10 ? "0" +
                                         XMLDate.Minute.ToString() : XMLDate.Minute.ToString()) +
                                         ":" +
                                         (XMLDate.Second < 10 ? "0" +
                                         XMLDate.Second.ToString() : XMLDate.Second.ToString()) +
                                         ".000";
                                    excelDoc.Append("<Cell ss:StyleID=\"DateLiteral\">" +
                                                 "<Data ss:Type=\"DateTime\">");
                                    excelDoc.Append(XMLDatetoString);
                                    excelDoc.Append("</Data></Cell>");
                                    break;
                                case "System.Boolean":
                                    excelDoc.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                                "<Data ss:Type=\"String\">");
                                    excelDoc.Append(x[y].ToString());
                                    excelDoc.Append("</Data></Cell>");
                                    break;
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                case "System.Byte":
                                    excelDoc.Append("<Cell ss:StyleID=\"Integer\">" +
                                            "<Data ss:Type=\"Number\">");
                                    excelDoc.Append(x[y].ToString());
                                    excelDoc.Append("</Data></Cell>");
                                    break;
                                case "System.Decimal":
                                case "System.Double":
                                    excelDoc.Append("<Cell ss:StyleID=\"Decimal\">" +
                                          "<Data ss:Type=\"Number\">");
                                    excelDoc.Append(x[y].ToString());
                                    excelDoc.Append("</Data></Cell>");
                                    break;
                                case "System.DBNull":
                                    excelDoc.Append("<Cell ss:StyleID=\"StringLiteral\">" +
                                          "<Data ss:Type=\"String\">");
                                    excelDoc.Append("");
                                    excelDoc.Append("</Data></Cell>");
                                    break;
                                default:
                                    throw (new Exception(rowType.ToString() + " not handled."));
                            }
                        }
                        excelDoc.Append("</Row>");
                    }
                    excelDoc.Append("</Table>");
                    excelDoc.Append(" </Worksheet>");
                }

                //excelDoc.Append("</Table>");
                //excelDoc.Append(" </Worksheet>");
                excelDoc.Append(endExcelXML);
                //excelDoc.Write(endExcelXML);
                //excelDoc.Close();
                return excelDoc;
            }

            /// <summary>
            /// Clears the HTTP.Response and sends an Excel file response from a DataSet or DataTable
            /// - Josh 2/10/2011
            /// </summary>
            /// <param name="data">You can give either a Dataset or Datatable</param>
            /// <param name="Response">This is the HttpResponse object that the current page uses, just pass in -> this.Response This allows
            /// the ExcelGenerator class to write directly to the Http Response.</param>
            /// <param name="FileName">This is an optional parameter, if you so choose you can name the file of the excel document.</param>
            public static void exportExcelToClientWithXML(object data, HttpResponse Response, string FileName = "ExcelData")
            {
                Response.Clear();
                Response.ContentType = "application/ms-excel";// "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
                Response.Write(exportToXML(data).ToString());
                Response.End();
            }

            /// <summary>
            /// Export an ASP Table to an excel document and send to client
            /// </summary>
            /// <param name="tmpTable"></param>
            /// <param name="Response"></param>
            public static void ExportTable(Table tmpTable, HttpResponse Response, string filename = "ReportExport", string workSheetName = "Data")
            {
                //string excelDoc = string.Empty;
                StringBuilder excelDoc = new StringBuilder();

                //excelDoc = new System.IO.StreamWriter();
                const string startExcelXML = "<xml version='1.0'>\r\n<Workbook " +
                      "xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\r\n" +
                      " xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n " +
                      "xmlns:x=\"urn:schemas-    microsoft-com:office:" +
                      "excel\"\r\n xmlns:ss=\"urn:schemas-microsoft-com:" +
                      "office:spreadsheet\">\r\n <Styles>\r\n " +
                      "<Style ss:ID=\"Default\" ss:Name=\"Normal\">\r\n " +
                      "<Alignment ss:Vertical=\"Bottom\"/>\r\n <Borders/>" +
                      "\r\n <Font/>\r\n <Interior/>\r\n <NumberFormat/>" +
                      "\r\n <Protection/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"BoldColumn\">\r\n <Font " +
                      "x:Family=\"Swiss\" ss:Bold=\"1\"/>\r\n </Style>\r\n " +
                      "<Style     ss:ID=\"StringLiteral\">\r\n <NumberFormat" +
                      " ss:Format=\"@\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"Decimal\">\r\n <NumberFormat " +
                      "ss:Format=\"0.00\"/>\r\n </Style>\r\n " +
                      "<Style ss:ID=\"Integer\">\r\n <NumberFormat " +
                      "ss:Format=\"0\"/>\r\n </Style>\r\n <Style " +
                      "ss:ID=\"DateLiteral\">\r\n <NumberFormat " +
                      "ss:Format=\"mm/dd/yyyy;@\"/>\r\n </Style>\r\n " +
                      "</Styles>\r\n ";
                const string endExcelXML = "</Workbook>";

                excelDoc.Append(startExcelXML);
                excelDoc.Append("<ss:Worksheet ss:Name=\"" + workSheetName + "\">");
                excelDoc.Append("<ss:Table>");

                List<int> _columnIndexToExclude = new List<int>();
                List<double> _cellWidths = new List<double>();

                // By looping through the first row(header row) we can determine the blank columns that we can exclude in the following table rows
                // These blank columns are caused by one or two things, The drill down columns(that hold the collapse buttons) and the additional behind-the-scenes column that is added
                for (int i = 0; i < tmpTable.Rows[0].Cells.Count; i++)
                {
                    TableCell td = tmpTable.Rows[0].Cells[i];
                    if (string.IsNullOrEmpty(td.Text))
                        _columnIndexToExclude.Add(i);// add the excluded column
                    else
                    {
                        // find the cell widths and only for columns that shouldn't be excluded due to drill down columns!
                        //_cellWidths.Add(tmpTable.Rows[0].Cells[i].Width.Value);
                        //double width = Math.Truncate((tmpTable.Rows[0].Cells[i].Text.Length*.5) )
                        double maxDigitWidth = 6d;
                        double pixelPadding = 1;
                        //Math.Truncate((((tmpTable.Rows[0].Cells[i].Text.Length * maxDigitWidth) + pixelPadding) / (maxDigitWidth * 256)) / 256);
                        double arg1 = tmpTable.Rows[0].Cells[i].Text.Length * maxDigitWidth + pixelPadding / maxDigitWidth * 256 / 256;
                        double width = Math.Truncate(arg1);
                        _cellWidths.Add(width);
                    }
                }

                // set the column width with the headers
                foreach (double item in _cellWidths)
                {
                    excelDoc.Append("<ss:Column ss:Width=\"" + item + "\"/>");
                }

                // set HEADER cells
                excelDoc.Append("<ss:Row>");
                for (int headColIndex = 0; headColIndex < tmpTable.Rows[0].Cells.Count; headColIndex++)
                {
                    TableCell item = tmpTable.Rows[0].Cells[headColIndex];
                    if (!_columnIndexToExclude.Contains(headColIndex))
                    { // we don't want to include columns that are the drill down columns(that store the drill down buttons)
                        excelDoc.Append("<ss:Cell ss:StyleID=\"BoldColumn\"><ss:Data ss:Type=\"String\">");
                        excelDoc.Append(item.Text);
                        excelDoc.Append("</ss:Data></ss:Cell>");
                    }
                }
                excelDoc.Append("</ss:Row>");

                // set Table Row Data
                for (int rowIndex = 0; rowIndex < tmpTable.Rows.Count; rowIndex++)
                {
                    // we skip the first row because that is the header and has already been written
                    if (rowIndex != 0)
                    {
                        excelDoc.Append("<ss:Row>");
                        for (int colIndex = 0; colIndex < tmpTable.Rows[rowIndex].Cells.Count; colIndex++)
                        {
                            TableCell cell = tmpTable.Rows[rowIndex].Cells[colIndex];
                            if (!_columnIndexToExclude.Contains(colIndex))
                            { // we don't want to include columns that are the drill down columns(that store the drill down buttons)

                                // set the style ID
                                string styleID = "ss:StyleID=\"StringLiteral\"";
                                // if the css style is total then make the export style bold 
                                if (tmpTable.Rows[rowIndex].CssClass.Contains("itotal"))
                                    styleID = "ss:StyleID=\"BoldColumn\"";

                                // set the default type
                                string type = "ss:Type=\"String\"";
                                //// try to see if the value is a number so that we can set the type as number instead of string
                                //if(CBM.Utility.DataUtility.Casting.getDouble(cell.Text.Replace("%", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty)).HasValue)// if its not null then its a number!
                                //{
                                //    type = "ss:Type=\"Number\"";
                                //}
                                excelDoc.Append("<ss:Cell " + styleID + ">" +
                                                    "<ss:Data " + type + ">");
                                //excelDoc.Append(cell.Text.Replace("%", string.Empty).Replace("$", string.Empty).Replace(".", string.Empty));
                                excelDoc.Append(cell.Text);
                                excelDoc.Append("</ss:Data></ss:Cell>");
                            }
                        }
                        excelDoc.Append("</ss:Row>");
                    }
                }

                excelDoc.Append("</ss:Table>");
                excelDoc.Append(" </ss:Worksheet>");
                excelDoc.Append(endExcelXML);

                Response.Clear();
                Response.ContentType = "application/ms-excel";// "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + ".xls");
                Response.Write(excelDoc.ToString());
                Response.End();
            }


            public static void exportExcelToClientWithFormatting(DataTable dt, HttpResponse Response, string fileName, int totalColumnIndex, TableCell[] header = null)
            {
                GridView grv = new GridView();
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        grv.AllowPaging = false;
                        grv.DataSource = dt;
                        grv.DataBind();

                        if (header != null && header.Length > 0)
                        {
                            grv.HeaderRow.Cells.Clear();
                            grv.HeaderRow.Cells.AddRange(header);
                        }  

                        grv.HeaderStyle.BackColor = System.Drawing.Color.FromName(PromolinkColoring.HeaderBackColoring);

                        grv.HeaderStyle.ForeColor = System.Drawing.Color.Black;

                        grv.BorderWidth = new Unit(0);
                        for (int i = 0; i < grv.Rows.Count; i++)
                        {
                            string cellValue = grv.Rows[i].Cells[totalColumnIndex].Text;
                            if (cellValue == "&nbsp;")
                            {
                                grv.Rows[i].Cells[totalColumnIndex].Text = "";
                                grv.Rows[i].Cells[totalColumnIndex].HorizontalAlign = HorizontalAlign.Right;

                            }
                        }
                    }
                    else
                    {
                        grv.DataSource = null;
                        grv.DataBind();
                    }
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xls" + ";");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.xls";
                    StringWriter sw = new System.IO.StringWriter();
                    HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(sw);
                    grv.RenderControl(HtmlTextWriter);
                    Response.Write(sw.ToString());
                    Response.End();
                }
                catch (Exception exe)
                {
                    grv.DataSource = null;
                    grv.DataBind();
                    throw exe;
                }
            }

        }
    }
}
