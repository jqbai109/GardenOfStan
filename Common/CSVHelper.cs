// Type: GetSenioroiOrderData.CSVHelper
// Assembly: GetSenioroiOrderData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BAC81D38-1D18-457D-868B-5C57A9B0AF9E
// Assembly location: E:\doc\Senioroi\Senioroi\SVN\website\bak\Services\Install\Email\GetSenioroiOrderData.exe

 
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;


namespace PromolinkUS.Common
{
  public class CSVHelper 
  {
    private string _csvFile;
    public string[] ColumnsName;

    public string CSVFile
    {
      get
      {
        return this._csvFile;
      }
      set
      {
        this._csvFile = value;
      }
    }

    private char[] FormatSplit
    {
      get
      {
        return new char[1]
        {
          ','
        };
      }
    }

    public CSVHelper(string csvFile)
    {
      this._csvFile = csvFile;
    }
    #region Comment by stan

    //public DataTable CsvReaderHelper()
    //{
    //    DataTable dataTable = new DataTable();
    //    using (CsvReader csvReader = new CsvReader((TextReader)new StreamReader(this._csvFile), true))
    //    {
    //        string[] fieldHeaders = csvReader.GetFieldHeaders();
    //        int fieldCount = csvReader.FieldCount;
    //        this.ColumnsName = fieldHeaders;
    //        foreach (string columnName in fieldHeaders)
    //            dataTable.Columns.Add(columnName);
    //        while (csvReader.ReadNextRecord())
    //        {
    //            DataRow row = dataTable.NewRow();
    //            for (int index = 0; index < fieldCount; ++index)
    //                row[index] = (object)csvReader[index];
    //            dataTable.Rows.Add(row);
    //        }
    //    }
    //    return dataTable;
    //}

    //public DataTable CsvReaderHelperNoHeader(int priviewCount)
    //{
    //    DataTable dataTable = new DataTable();
    //    using (CsvReader csvReader = new CsvReader((TextReader)new StreamReader(this._csvFile), false))
    //    {
    //        int fieldCount = csvReader.FieldCount;
    //        for (int index = 0; index < fieldCount; ++index)
    //            dataTable.Columns.Add("Fields" + index.ToString());
    //        int num = 0;
    //        while (csvReader.ReadNextRecord())
    //        {
    //            ++num;
    //            DataRow row = dataTable.NewRow();
    //            for (int index = 0; index < fieldCount; ++index)
    //                row[index] = (object)csvReader[index];
    //            dataTable.Rows.Add(row);
    //            if (priviewCount > 0 && num >= priviewCount)
    //                break;
    //        }
    //    }
    //    return dataTable;
    //}

    //public DataTable CsvReaderHelperNoHeader()
    //{
    //    DataTable dataTable = new DataTable();
    //    using (CsvReader csvReader = new CsvReader((TextReader)new StreamReader(this._csvFile), false))
    //    {
    //        int fieldCount = csvReader.FieldCount;
    //        for (int index = 0; index < fieldCount; ++index)
    //            dataTable.Columns.Add("Fields" + index.ToString());
    //        int num = 0;
    //        while (csvReader.ReadNextRecord())
    //        {
    //            ++num;
    //            DataRow row = dataTable.NewRow();
    //            for (int index = 0; index < fieldCount; ++index)
    //                row[index] = (object)csvReader[index];
    //            dataTable.Rows.Add(row);
    //        }
    //    }
    //    return dataTable;
    //} 
    #endregion

  //  public DataTable Read()
  //  {
  //    FileInfo fileInfo = new FileInfo(this._csvFile);
  //    if (fileInfo == null || !fileInfo.Exists)
  //      return (DataTable) null;
  //    StreamReader streamReader = new StreamReader(this._csvFile);
  //    string str = string.Empty;
  //    int num = 0;
  //    DataTable dt = new DataTable();
  //    string line;
  //    while ((line = streamReader.ReadLine()) != null)
  //    {
  //      if (num == 0)
  //      {
  //        dt = this.CreateDataTable(line);
  //        if (dt.Columns.Count == 0)
  //          return (DataTable) null;
  //      }
  //      if (!this.CreateDataRow(ref dt, line))
  //        return (DataTable) null;
  //      ++num;
  //    }
  //    streamReader.Close();
  //    return dt;
  //  }

  //  public DataTable ReadNosign()
  //  {
  //    FileInfo fileInfo = new FileInfo(this._csvFile);
  //    if (fileInfo == null || !fileInfo.Exists)
  //      return (DataTable) null;
  //    StreamReader streamReader = new StreamReader(this._csvFile);
  //    string str = string.Empty;
  //    int num = 0;
  //    DataTable dt = new DataTable();
  //    string line;
  //    while ((line = streamReader.ReadLine()) != null)
  //    {
  //      if (num == 0)
  //      {
  //        dt = this.CreateDataTableNoSign(line);
  //        if (dt.Columns.Count == 0)
  //          return (DataTable) null;
  //      }
  //      else if (!this.CreateDataRowNoSign(ref dt, line))
  //        return (DataTable) null;
  //      ++num;
  //    }
  //    streamReader.Close();
  //    return dt;
  //  }

  //  public bool Write(DataTable dt)
  //  {
  //    FileInfo fileInfo = new FileInfo(this._csvFile);
  //    if (fileInfo == null || !fileInfo.Exists || (dt == null || dt.Columns.Count == 0 || dt.Rows.Count == 0))
  //      return false;
  //    StreamWriter streamWriter = new StreamWriter(this._csvFile);
  //    string str1 = string.Empty;
  //    string title = this.CreateTitle(dt);
  //    streamWriter.WriteLine(title);
  //    foreach (DataRow dr in (InternalDataCollectionBase) dt.Rows)
  //    {
  //      string str2 = this.CretreLine(dr);
  //      streamWriter.WriteLine(str2);
  //    }
  //    ((TextWriter) streamWriter).Flush();
  //    streamWriter.Close();
  //    return true;
  //  }

  //  private DataTable CreateDataTable(string line)
  //  {
  //    DataTable dataTable = new DataTable();
  //    int num = 0;
  //    foreach (string str in line.Split(this.FormatSplit, StringSplitOptions.None))
  //    {
  //      dataTable.Columns.Add("Fields" + num.ToString());
  //      ++num;
  //    }
  //    return dataTable;
  //  }

  //  private DataTable CreateDataTableNoSign(string line)
  //  {
  //    DataTable dataTable = new DataTable();
  //    int num = 0;
  //    foreach (string columnName in line.Split(this.FormatSplit, StringSplitOptions.None))
  //    {
  //      dataTable.Columns.Add(columnName);
  //      ++num;
  //    }
  //    return dataTable;
  //  }

  //  private bool CreateDataRow(ref DataTable dt, string line)
  //  {
  //    DataRow row = dt.NewRow();
  //    string[] strArray = line.Split(this.FormatSplit, StringSplitOptions.None);
  //    if (strArray.Length == 0 || strArray.Length != dt.Columns.Count)
  //      return false;
  //    for (int index = 0; index < strArray.Length; ++index)
  //      row[index] = (object) strArray[index].Replace("\"", "");
  //    dt.Rows.Add(row);
  //    return true;
  //  }

  //  private bool CreateDataRowNoSign(ref DataTable dt, string line)
  //  {
  //    DataRow row = dt.NewRow();
  //    string str = string.Empty;
  //    SortedList sl = new SortedList();
  //    if (!string.IsNullOrEmpty(line))
  //    {
  //      string src = line.Replace("\"\"", "'");
  //      string[] strArray = this.MatchCloe(sl, src).Split(new char[1]
  //      {
  //        ','
  //      });
  //      for (int index = 0; index < strArray.Length; ++index)
  //      {
  //        if (!sl.ContainsKey((object) index))
  //          sl.Add((object) index, (object) strArray[index]);
  //      }
  //      IDictionaryEnumerator enumerator = sl.GetEnumerator();
  //      int index1 = 0;
  //      while (enumerator.MoveNext())
  //      {
  //        row[index1] = (object) enumerator.Value.ToString().Replace("'", "\"");
  //        ++index1;
  //      }
  //      dt.Rows.Add(row);
  //    }
  //    return true;
  //  }

  //  private string MatchCloe(SortedList sl, string src)
  //  {
  //    foreach (object obj in Regex.Matches(src, ",\"([^\"]+)\",", RegexOptions.ExplicitCapture))
  //    {
  //      string oldValue = obj.ToString();
  //      int length = src.Substring(0, src.IndexOf(oldValue)).Split(new char[1]
  //      {
  //        ','
  //      }).Length;
  //      if (!sl.ContainsKey((object) length))
  //      {
  //        sl.Add((object) length, (object) oldValue.Trim(',', '"').Replace("'", "\""));
  //        src = src.Replace(oldValue, ",,");
  //      }
  //    }
  //    if (src.Contains(",\""))
  //      src = this.MatchCloe(sl, src);
  //    return src;
  //  }

  //  private string CreateTitle(DataTable dt)
  //  {
  //    string str = string.Empty;
  //    for (int index = 0; index < dt.Columns.Count; ++index)
  //      str = str + string.Format("{0}{1}", (object) dt.Columns[index].ColumnName, (object) this.FormatSplit[0].ToString());
  //    str.TrimEnd(new char[1]
  //    {
  //      this.FormatSplit[0]
  //    });
  //    return str;
  //  }

  //  private string CretreLine(DataRow dr)
  //  {
  //    string str = string.Empty;
  //    for (int index = 0; index < dr.ItemArray.Length; ++index)
  //      str = str + string.Format("{0}{1}", dr[index], (object) this.FormatSplit[0].ToString());
  //    str.TrimEnd(new char[1]
  //    {
  //      this.FormatSplit[0]
  //    });
  //    return str;
  //  }

  //  public DataSet ChageDataset(DataSet ds)
  //  {
  //    return (DataSet) null;
  //  }

  //  public DataSet Csv(string OrderID, string filepath)
  //  {
  //    DataSet dataSet = new DataSet();
  //    DataTable table = new CSVHelper(filepath).Read();
  //    dataSet.Tables.Add(table);
  //    return dataSet;
  //  }
  }
}
