using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using DocumentFormat.OpenXml.Spreadsheet;

namespace AnalysLogs
{
    class ExcelFile
    {
    }
    public class CreateExcelFile
    {
        public static bool CreateExcelDocument(ReportCondition dtCondition, DataTable dtDetail, string excelFilename)
        {
            try
            {
                //When don't add reference WindowsBase, error appear at here
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(excelFilename, SpreadsheetDocumentType.Workbook))
                {
                    WriteExcelFile(dtCondition, dtDetail, document);
                }
                //Trace.WriteLine("Successfully created: " + excelFilename);
                //EventLog EventLogger.WriteLog("Successfully created: Detail_Utilization_Report.xslx", System.Diagnostics.EventLogEntryType.Information);
                return true;
            }
            catch (Exception ex)
            {
                //Trace.WriteLine("Failed, exception thrown: " + ex.Message);
                //EventLogger.LogException(ex);
                return false;
            }
        }

        private static void WriteExcelFile(ReportCondition dtCondtion, DataTable dtDetail, SpreadsheetDocument spreadsheet)
        {
            //  Create the Excel file contents.  This function is used when creating an Excel file either writing 
            //  to a file, or writing to a MemoryStream.
            spreadsheet.AddWorkbookPart();
            spreadsheet.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();

            //  My thanks to James Miera for the following line of code (which prevents crashes in Excel 2010)
            spreadsheet.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));

            //  If we don't add a "WorkbookStylesPart", OLEDB will refuse to connect to this .xlsx file !
            WorkbookStylesPart workbookStylesPart = spreadsheet.WorkbookPart.AddNewPart<WorkbookStylesPart>("rIdStyles");

            //Stylesheet stylesheet = new Stylesheet();
            //workbookStylesPart.Stylesheet = stylesheet;
            CustomStylesheet customStyleSheet = new CustomStylesheet();
            Stylesheet styles = customStyleSheet;//CustomStylesheet();
            styles.Save(workbookStylesPart);

            //  Loop through each of the DataTables in our DataSet, and create a new Excel Worksheet for each.
            uint worksheetNumber = 1;
            Sheets sheets = spreadsheet.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

            //  For each worksheet you want to create
            string worksheetName = "BOC_DetailedUtilization";

            //  Create worksheet part, and add it to the sheets collection in workbook
            WorksheetPart newWorksheetPart = spreadsheet.WorkbookPart.AddNewPart<WorksheetPart>();
            Sheet sheet = new Sheet() { Id = spreadsheet.WorkbookPart.GetIdOfPart(newWorksheetPart), SheetId = worksheetNumber, Name = worksheetName };

            // If you want to define the Column Widths for a Worksheet, you need to do this *before* appending the SheetData
            // http://social.msdn.microsoft.com/Forums/en-US/oxmlsdk/thread/1d93eca8-2949-4d12-8dd9-15cc24128b10/

            sheets.Append(sheet);

            //  Append this worksheet's data to our Workbook, using OpenXmlWriter, to prevent memory problems
            WriteDataTableToExcelWorksheet(dtCondtion, dtDetail, newWorksheetPart, workbookStylesPart);

            worksheetNumber++;

            spreadsheet.WorkbookPart.Workbook.Save();
        }

        private static void WriteDataTableToExcelWorksheet(ReportCondition dtCondition, DataTable dtDetail,
            WorksheetPart worksheetPart, WorkbookStylesPart stylesPart)
        {
            OpenXmlWriter writer = OpenXmlWriter.Create(worksheetPart, Encoding.Unicode); //ASCII
            writer.WriteStartElement(new Worksheet());
            writer.WriteStartElement(new SheetData());

            string cellValue = string.Empty;

            uint rowIndexHeader = 1;
            //  Create the Conditon Report
            writer.WriteStartElement(new Row { RowIndex = rowIndexHeader++ });
            AppendTextCellFormat("A", "BOC Detailed Utilization", 1, stylesPart.Stylesheet, System.Drawing.Color.White, false, 20, true, 9, ref writer);
            writer.WriteEndElement();

            writer.WriteStartElement(new Row { RowIndex = rowIndexHeader++ });
            AppendTextCellFormat("A", "Date:", 2, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            AppendTextCellFormat(
                "B"
                , string.Format("{0} - {1}", dtCondition.FromDate.ToString("dd.MM.yyyy"), dtCondition.ToDate.AddDays(-1).ToString("dd.MM.yyyy"))
                , 2, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            writer.WriteEndElement();
            writer.WriteStartElement(new Row { RowIndex = rowIndexHeader++ });
            AppendTextCellFormat("A", "Location:", 3, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            AppendTextCellFormat("B", dtCondition.LocationPath.ToString(), 3, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            writer.WriteEndElement();
            writer.WriteStartElement(new Row { RowIndex = rowIndexHeader++ });
            AppendTextCellFormat("A", "Group:", 4, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            AppendTextCellFormat("B", dtCondition.GroupName.ToString(), 4, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            writer.WriteEndElement();
            writer.WriteStartElement(new Row { RowIndex = rowIndexHeader++ });
            AppendTextCellFormat("A", "Resource:", 5, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            AppendTextCellFormat("B", dtCondition.ResourceName.ToString(), 5, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            writer.WriteEndElement();
            writer.WriteStartElement(new Row { RowIndex = rowIndexHeader++ });
            AppendTextCellFormat("A", "Method:", 6, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            AppendTextCellFormat("B", dtCondition.Method.ToString(), 6, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            writer.WriteEndElement();
            writer.WriteStartElement(new Row { RowIndex = rowIndexHeader++ });
            AppendTextCellFormat("A", "Include weekends:", 7, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            AppendTextCellFormat("B", SetWeekendTitle(dtCondition), 7, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            writer.WriteEndElement();
            writer.WriteStartElement(new Row { RowIndex = rowIndexHeader++ });
            AppendTextCellFormat("A", "Upper threshold (%):", 8, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            AppendTextCellFormat("B", dtCondition.UpperThreshold.ToString(), 8, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            writer.WriteEndElement();
            writer.WriteStartElement(new Row { RowIndex = rowIndexHeader++ });
            AppendTextCellFormat("A", "Lower threshold (%):", 9, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            AppendTextCellFormat("B", dtCondition.LowerThreshold.ToString(), 9, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
            writer.WriteEndElement();
            //  End of Conditon Report

            //  Create a Header Row in our Excel file, containing one header for each Column of data in our DataTable.
            int numberOfColumns = dtDetail.Columns.Count;
            bool[] IsNumericColumn = new bool[numberOfColumns];
            bool[] IsDateColumn = new bool[numberOfColumns];

            string[] excelColumnNames = new string[numberOfColumns];
            for (int n = 0; n < numberOfColumns; n++)
                excelColumnNames[n] = GetExcelColumnName(n);

            //  Create the Header row in our Excel Worksheet
            writer.WriteStartElement(new Row { RowIndex = 11 });
            AppendTextCellFormat("A", "Resource", 11, stylesPart.Stylesheet, System.Drawing.Color.White, true, 11, true, 10, ref writer);
            AppendTextCellFormat("B", "Hours Used", 11, stylesPart.Stylesheet, System.Drawing.Color.White, true, 11, true, 10, ref writer);
            AppendTextCellFormat("C", "% Used", 11, stylesPart.Stylesheet, System.Drawing.Color.White, true, 11, true, 10, ref writer);
            AppendTextCellFormat("D", "Flag", 11, stylesPart.Stylesheet, System.Drawing.Color.White, true, 11, true, 9, ref writer);
            AppendTextCellFormat("E", "Group", 11, stylesPart.Stylesheet, System.Drawing.Color.White, true, 11, true, 9, ref writer);
            AppendTextCellFormat("F", "Hours Used", 11, stylesPart.Stylesheet, System.Drawing.Color.White, true, 11, true, 9, ref writer);
            AppendTextCellFormat("G", "% Used", 11, stylesPart.Stylesheet, System.Drawing.Color.White, true, 11, true, 9, ref writer);
            for (int colInx = 0; colInx < numberOfColumns; colInx++)
            {
                DataColumn col = dtDetail.Columns[colInx];
                //AppendTextCell(excelColumnNames[colInx] + "1", col.ColumnName, ref writer);

                IsNumericColumn[colInx] = (col.DataType.FullName == "System.Decimal") || (col.DataType.FullName == "System.Int32") || (col.DataType.FullName == "System.Double") || (col.DataType.FullName == "System.Single");
                IsDateColumn[colInx] = (col.DataType.FullName == "System.DateTime");
            }
            writer.WriteEndElement();
            //  End of header row

            //  Now, step through each row of data in our DataTable...
            uint rowIndex = 11;
            double cellNumericValue = 0;
            foreach (DataRow dr in dtDetail.Rows)
            {
                // ...create a new row, and append a set of this row's data to it.
                ++rowIndex;

                writer.WriteStartElement(new Row { RowIndex = rowIndex });

                for (int colInx = 0; colInx < numberOfColumns; colInx++)
                {
                    cellValue = dr.ItemArray[colInx].ToString();

                    // Create cell with data
                    if (IsNumericColumn[colInx])
                    {
                        cellNumericValue = 0;

                        if (double.TryParse(cellValue, out cellNumericValue))
                        {
                            if (cellNumericValue >= 0)
                            {
                                cellValue = ((float)cellNumericValue).ToString("0.00");
                                AppendNumericCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
                            }
                            else
                            {
                                cellValue = string.Empty;
                                //AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
                                AppendTextCellFormat(excelColumnNames[colInx], cellValue, (int)rowIndex, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, false, 9, ref writer);
                            }
                        }
                    }
                    else if (IsDateColumn[colInx])
                    {
                        DateTime dtValue = DateTime.Parse(cellValue);
                        string strValue = dtValue.ToShortDateString();
                        AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), strValue, ref writer);
                    }
                    else if (colInx == 0)
                    {
                        AppendTextCellFormat(excelColumnNames[colInx], cellValue, (int)rowIndex, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, false, 9, ref writer);
                    }
                    else
                    {
                        //AppendTextCell(excelColumnNames[colInx] + rowIndex.ToString(), cellValue, ref writer);
                        AppendTextCellFormat(excelColumnNames[colInx], cellValue, (int)rowIndex, stylesPart.Stylesheet, System.Drawing.Color.White, false, 11, true, 9, ref writer);
                    }
                }
                writer.WriteEndElement(); //  End of Row
            }
            writer.WriteEndElement(); //  End of SheetData
            writer.WriteEndElement(); //  End of worksheet

            writer.Close();
        }

        private static void AppendTextCellFormat(string header, string text, int index, Stylesheet styles,
            System.Drawing.Color fillColour, bool isFill, double? fontSize, bool isBold, UInt32 styleIndex, ref OpenXmlWriter writer)
        {
            TextCellHasFormat a = new TextCellHasFormat(header, text, index, styles, fillColour, fontSize, isBold);
            if (isFill)
            {
                a.StyleIndex = 10;
            }
            writer.WriteElement(a);
        }

        private static void AppendTextCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
        {
            writer.WriteElement(new Cell
            {
                CellValue = new CellValue(cellStringValue),
                CellReference = cellReference,
                DataType = CellValues.String,
                StyleIndex = 10
            });
        }

        //thaond11
        private void UpdateCellTextInline(Cell cell, string cellValue)
        {
            InlineString inlineString = new InlineString();
            Text cellValueText = new Text { Text = cellValue };
            inlineString.AppendChild(cellValueText);

            cell.DataType = CellValues.InlineString;
            cell.AppendChild(inlineString);
        }

        private void UpdateCellTextValue(Cell cell, string cellValue)
        {
            cell.CellValue = new CellValue(cellValue);
            cell.DataType = new EnumValue<CellValues>(CellValues.String);
        }

        //thaond11
        public static Cell AddValueToCell(Cell cell, string text)
        {
            cell.DataType = CellValues.InlineString;
            cell.RemoveAllChildren();
            InlineString inlineString = new InlineString();
            Text t = new Text();
            t.Text = text;
            inlineString.AppendChild(t);
            cell.AppendChild(inlineString);
            return cell;
        }

        //thaond11
        public static Cell CreateTextCell(string header, string text, int index)
        {
            //Create new inline string cell
            Cell c = new Cell();
            c.DataType = CellValues.InlineString;
            //Add text to text cell
            InlineString inlineString = new InlineString();
            Text t = new Text();
            t.Text = text;
            inlineString.AppendChild(t);
            c.AppendChild(inlineString);
            return c;
        }

        private static void AppendDateCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
        {
            //  Add a new Excel Cell to our Row 
            writer.WriteElement(new Cell
            {
                CellValue = new CellValue(cellStringValue),
                CellReference = cellReference,
                DataType = new EnumValue<CellValues>(CellValues.Number), // CellValues.Date
                StyleIndex = 0 // DATE_FORMAT_ID         //  m/d/yyyy H:mm
            });
        }

        //private static void AppendNumericCellFormat(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
        //{
        //    //  Add a new Excel Cell to our Row 
        //    writer.WriteElement(new Cell
        //    {
        //        CellValue = new CellValue(cellStringValue),
        //        CellReference = cellReference,
        //        DataType = CellValues.Number
        //    });
        //}

        private static void AppendNumericCell(string cellReference, string cellStringValue, ref OpenXmlWriter writer)
        {
            //  Add a new Excel Cell to our Row 
            writer.WriteElement(new Cell
            {
                CellValue = new CellValue(cellStringValue),
                CellReference = cellReference,
                DataType = CellValues.Number
            });
        }

        private static string SetWeekendTitle(ReportCondition reportCondition)
        {
            string result = string.Empty;
            if (reportCondition.IncludeSaturday && reportCondition.IncludeSunday)
            {
                result = "Yes";
            }
            if (!reportCondition.IncludeSaturday && reportCondition.IncludeSunday)
            {
                result = @"Sunday";
            }
            if (reportCondition.IncludeSaturday && !reportCondition.IncludeSunday)
            {
                result = @"Saturday";
            }
            if (!reportCondition.IncludeSaturday && !reportCondition.IncludeSunday)
            {
                result = "No";
            }
            return result;
        }

        private static UInt32Value CreateCellFormat(Stylesheet styleSheet, UInt32Value fontIndex, UInt32Value fillIndex, UInt32Value numberFormatId)
        {
            CellFormat cellFormat = new CellFormat();

            if (fontIndex != null)
                cellFormat.FontId = fontIndex;

            if (fillIndex != null)
                cellFormat.FillId = fillIndex;

            if (numberFormatId != null)
            {
                cellFormat.NumberFormatId = numberFormatId;
                cellFormat.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            }

            styleSheet.CellFormats.Append(cellFormat);

            UInt32Value result = styleSheet.CellFormats.Count;
            styleSheet.CellFormats.Count++;
            return result;
        }

        private static UInt32Value CreateFill(Stylesheet styleSheet, System.Drawing.Color fillColor)
        {
            PatternFill patternFill =
                new PatternFill(
                    new ForegroundColor()
                    {
                        Rgb = new HexBinaryValue()
                        {
                            Value =
                            System.Drawing.ColorTranslator.ToHtml(
                                System.Drawing.Color.FromArgb(
                                    fillColor.A,
                                    fillColor.R,
                                    fillColor.G,
                                    fillColor.B)).Replace("#", "")
                        }
                    });

            patternFill.PatternType = fillColor ==
                        System.Drawing.Color.White ? PatternValues.None : PatternValues.LightDown;

            Fill fill = new Fill(patternFill);
            styleSheet.Fills.Append(fill);
            UInt32Value result = styleSheet.Fills.Count;
            styleSheet.Fills.Count++;
            return result;
        }

        private static UInt32Value CreateFont(Stylesheet styleSheet, string fontName, double? fontSize, bool isBold, System.Drawing.Color foreColor)
        {
            DocumentFormat.OpenXml.Spreadsheet.Font font = new DocumentFormat.OpenXml.Spreadsheet.Font();

            if (!string.IsNullOrEmpty(fontName))
            {
                FontName name = new FontName()
                {
                    Val = fontName
                };
                font.Append(name);
            }

            if (fontSize.HasValue)
            {
                DocumentFormat.OpenXml.Spreadsheet.FontSize size = new DocumentFormat.OpenXml.Spreadsheet.FontSize()
                {
                    Val = fontSize.Value
                };
                font.Append(size);
            }

            if (isBold == true)
            {
                Bold bold = new Bold();
                font.Append(bold);
            }

            DocumentFormat.OpenXml.Spreadsheet.Color color = new DocumentFormat.OpenXml.Spreadsheet.Color()
            {
                Rgb = new HexBinaryValue()
                {
                    Value =
                        System.Drawing.ColorTranslator.ToHtml(
                            System.Drawing.Color.FromArgb(
                                foreColor.A,
                                foreColor.R,
                                foreColor.G,
                                foreColor.B)).Replace("#", "")
                }
            };
            font.Append(color);

            styleSheet.Fonts.Append(font);
            UInt32Value result = styleSheet.Fonts.Count;
            styleSheet.Fonts.Count++;
            return result;
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));

            return string.Format("{0}{1}", firstChar, secondChar);
        }
    }
}
