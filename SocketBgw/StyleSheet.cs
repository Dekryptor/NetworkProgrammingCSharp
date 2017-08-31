using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalysLogs
{
    class StyleSheet
    {
    }
    public class TextCellHasFormat : Cell
    {
        public TextCellHasFormat(string header, string text, int index, Stylesheet styles,
            System.Drawing.Color fillColour, double? fontSize, bool isBold)//System.Drawing.Color fontColour,
        {
            this.DataType = CellValues.InlineString;
            this.CellReference = header + index;
            //Add text to the text cell.

            this.InlineString = new InlineString { Text = new Text { Text = text } };
            UInt32Value fontId = CreateFont(styles, "", fontSize, isBold, System.Drawing.Color.Black);//Text color in header
            UInt32Value fillId = CreateFill(styles, fillColour); //Fill color in header
            UInt32Value formatId = CreateCellFormat(styles, fontId, fillId, 0);
            this.StyleIndex = formatId;
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

        private static UInt32Value CreateFill(
            Stylesheet styleSheet,
            System.Drawing.Color fillColor)
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

        private static UInt32Value CreateFont(
            Stylesheet styleSheet,
            string fontName,
            double? fontSize,
            bool isBold,
            System.Drawing.Color foreColor)
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
    }

    public class CustomStylesheet : Stylesheet
    {
        public CustomStylesheet()
        {
            Fonts fts = new Fonts();
            DocumentFormat.OpenXml.Spreadsheet.Font ft = new DocumentFormat.OpenXml.Spreadsheet.Font();
            FontName ftn = new FontName();
            ftn.Val = StringValue.FromString("Calibri");
            DocumentFormat.OpenXml.Spreadsheet.FontSize ftsz = new DocumentFormat.OpenXml.Spreadsheet.FontSize();
            ftsz.Val = DoubleValue.FromDouble(11);
            ft.FontName = ftn;
            ft.FontSize = ftsz;
            fts.Append(ft);

            ft = new DocumentFormat.OpenXml.Spreadsheet.Font();
            ftn = new FontName();
            ftn.Val = StringValue.FromString("Palatino Linotype");
            ftsz = new DocumentFormat.OpenXml.Spreadsheet.FontSize();
            ftsz.Val = DoubleValue.FromDouble(18);
            ft.FontName = ftn;
            ft.FontSize = ftsz;
            fts.Append(ft);

            fts.Count = UInt32Value.FromUInt32((uint)fts.ChildElements.Count);

            Fills fills = new Fills();
            Fill fill;
            PatternFill patternFill;
            fill = new Fill();
            patternFill = new PatternFill();
            patternFill.PatternType = PatternValues.None;
            fill.PatternFill = patternFill;
            fills.Append(fill);

            fill = new Fill();
            patternFill = new PatternFill();
            patternFill.PatternType = PatternValues.Gray125; //Gray125;LightDown
            fill.PatternFill = patternFill;
            fills.Append(fill);

            fill = new Fill();
            patternFill = new PatternFill();
            //patternFill.PatternType = PatternValues.Solid;
            patternFill.PatternType = PatternValues.LightDown;
            patternFill.ForegroundColor = new ForegroundColor();
            //patternFill.ForegroundColor.Rgb = HexBinaryValue.FromString("00ff9728");//00837e8a
            //thaond1
            patternFill.ForegroundColor.Rgb = HexBinaryValue.FromString("00a6a6a6");
            patternFill.BackgroundColor = new BackgroundColor();
            patternFill.BackgroundColor.Rgb = patternFill.ForegroundColor.Rgb;
            fill.PatternFill = patternFill;
            fills.Append(fill);

            fills.Count = UInt32Value.FromUInt32((uint)fills.ChildElements.Count);

            Borders borders = new Borders();
            Border border = new Border();
            border.LeftBorder = new LeftBorder();
            border.RightBorder = new RightBorder();
            border.TopBorder = new TopBorder();
            border.BottomBorder = new BottomBorder();
            border.DiagonalBorder = new DiagonalBorder();
            borders.Append(border);

            //Boarder Index 1
            border = new Border();
            border.LeftBorder = new LeftBorder();
            border.LeftBorder.Style = BorderStyleValues.Thin;
            border.RightBorder = new RightBorder();
            border.RightBorder.Style = BorderStyleValues.Thin;
            border.TopBorder = new TopBorder();
            border.TopBorder.Style = BorderStyleValues.Thin;
            border.BottomBorder = new BottomBorder();
            border.BottomBorder.Style = BorderStyleValues.Thin;
            border.DiagonalBorder = new DiagonalBorder();
            borders.Append(border);

            //Boarder Index 2
            border = new Border();
            border.LeftBorder = new LeftBorder();
            border.RightBorder = new RightBorder();
            border.TopBorder = new TopBorder();
            border.TopBorder.Style = BorderStyleValues.Thin;
            border.BottomBorder = new BottomBorder();
            border.BottomBorder.Style = BorderStyleValues.Thin;
            border.DiagonalBorder = new DiagonalBorder();
            borders.Append(border);
            borders.Count = UInt32Value.FromUInt32((uint)borders.ChildElements.Count);

            CellStyleFormats csfs = new CellStyleFormats();
            CellFormat cf = new CellFormat();
            cf.NumberFormatId = 0;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            csfs.Append(cf);
            csfs.Count = UInt32Value.FromUInt32((uint)csfs.ChildElements.Count);

            uint iExcelIndex = 164;
            NumberingFormats nfs = new NumberingFormats();
            CellFormats cfs = new CellFormats();

            cf = new CellFormat();
            cf.NumberFormatId = 0;
            cf.FontId = 0;
            cf.FillId = 0;
            cf.BorderId = 0;
            cf.FormatId = 0;
            cfs.Append(cf);

            NumberingFormat nfDateTime = new NumberingFormat();
            nfDateTime.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfDateTime.FormatCode = StringValue.FromString("dd/mm/yyyy hh:mm:ss");
            nfs.Append(nfDateTime);

            NumberingFormat nf4decimal = new NumberingFormat();
            nf4decimal.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nf4decimal.FormatCode = StringValue.FromString("#,##0.0000");
            nfs.Append(nf4decimal);

            // #,##0.00 is also Excel style index 4
            NumberingFormat nf2decimal = new NumberingFormat();
            nf2decimal.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nf2decimal.FormatCode = StringValue.FromString("#,##0.00");
            nfs.Append(nf2decimal);

            // @ is also Excel style index 49
            NumberingFormat nfForcedText = new NumberingFormat();
            nfForcedText.NumberFormatId = UInt32Value.FromUInt32(iExcelIndex++);
            nfForcedText.FormatCode = StringValue.FromString("@");
            nfs.Append(nfForcedText);

            // index 1
            // Format dd/mm/yyyy
            cf = new CellFormat();
            cf.NumberFormatId = 14;
            cf.FontId = 0;//0
            cf.FillId = 0;//0
            cf.BorderId = 0;//0
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 2
            // Format #,##0.00
            cf = new CellFormat();
            cf.NumberFormatId = 4;
            cf.FontId = 0;//0
            cf.FillId = 0;//0
            cf.BorderId = 0;//0
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 3
            cf = new CellFormat();
            cf.NumberFormatId = nfDateTime.NumberFormatId;
            cf.FontId = 0;//0
            cf.FillId = 0;//0
            cf.BorderId = 0;//0
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 4
            cf = new CellFormat();
            cf.NumberFormatId = nf4decimal.NumberFormatId;
            cf.FontId = 0;//0
            cf.FillId = 0;//0
            cf.BorderId = 0;//0
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 5
            cf = new CellFormat();
            cf.NumberFormatId = nf2decimal.NumberFormatId;
            cf.FontId = 0;//0
            cf.FillId = 0;//0
            cf.BorderId = 0;//0
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 6
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 0;//0
            cf.FillId = 0;//0
            cf.BorderId = 0;//0
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 7
            // Header text
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 1;//1
            cf.FillId = 0;//0
            cf.BorderId = 0;//0
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 8
            // column text
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 0;//0
            cf.FillId = 0;//0
            cf.BorderId = 1;//1
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 9
            // coloured 2 decimal text
            cf = new CellFormat();
            cf.NumberFormatId = nf2decimal.NumberFormatId;
            cf.FontId = 0;//0
            cf.FillId = 2;//2
            cf.BorderId = 2;//2
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            // index 10
            // coloured column text
            cf = new CellFormat();
            cf.NumberFormatId = nfForcedText.NumberFormatId;
            cf.FontId = 0;//0
            cf.FillId = 2;//2
            cf.BorderId = 1;//2
            cf.FormatId = 0;//0
            cf.ApplyNumberFormat = BooleanValue.FromBoolean(true);
            cfs.Append(cf);

            nfs.Count = UInt32Value.FromUInt32((uint)nfs.ChildElements.Count);
            cfs.Count = UInt32Value.FromUInt32((uint)cfs.ChildElements.Count);

            this.Append(nfs);
            this.Append(fts);
            this.Append(fills);
            this.Append(borders);
            this.Append(csfs);
            this.Append(cfs);

            CellStyles css = new CellStyles();
            CellStyle cs = new CellStyle();
            cs.Name = StringValue.FromString("Normal");
            cs.FormatId = 0;
            cs.BuiltinId = 0;
            css.Append(cs);
            css.Count = UInt32Value.FromUInt32((uint)css.ChildElements.Count);
            this.Append(css);

            DifferentialFormats dfs = new DifferentialFormats();
            dfs.Count = 0;
            this.Append(dfs);

            TableStyles tss = new TableStyles();
            tss.Count = 0;
            tss.DefaultTableStyle = StringValue.FromString("TableStyleMedium9");
            tss.DefaultPivotStyle = StringValue.FromString("PivotStyleLight16");
            this.Append(tss);
        }
    }
}
