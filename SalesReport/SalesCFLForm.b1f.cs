using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;
using SAPbobsCOM;
using System.Drawing;
using System.Diagnostics;

namespace SalesReport
{
    [FormAttribute("BDO_Tax_and_Accounting_for_Ukraine.SalesCFLForm", "SalesCFLForm.b1f")]
    class SalesCFLForm : UserFormBase
    {
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Form SalesForm;
        private string EditText6 = "";
        private SAPbobsCOM.Recordset oRecordset;
        private int selectedRow = -1;
        private List<string> columnNames;
        private Dictionary<string, double> CurrectMoneyCourse = new Dictionary<string, double>() { { "UAH", 1 } };
        private Dictionary<string, string> LowGroup = new Dictionary<string, string>() { };
        private bool IsOpen = false;

        public SalesCFLForm(SAPbouiCOM.Form af, string date)
        {
            SalesForm = af;
            //this.EditText6 = date;
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.ResizeAfter += new ResizeAfterHandler(this.Form_ResizeAfter);
            
        }

       
        private void OnCustomInitialize()
        {
            oRecordset = (SAPbobsCOM.Recordset)Program.oCopmany.GetBusinessObject(BoObjectTypes.BoRecordset);

            this.Button2 = ((SAPbouiCOM.Button)(this.GetItem("Item_5").Specific));
            this.Button2.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button2_ClickBefore);
            this.Button2.Item.Visible = false;
            this.edit0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_8").Specific));
            this.edit1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_9").Specific));
            this.Grid1 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.Grid1.DoubleClickAfter += Grid1_DoubleClickAfter;
            this.Grid1.LostFocusAfter += Grid1_LostFocusAfter;
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button1.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button1_ClickAfter);
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_2").Specific));
            this.Grid0.SelectionMode = SAPbouiCOM.BoMatrixSelect.ms_Auto;
            this.Grid0.DoubleClickBefore += Grid0_DoubleClickBefore;
            this.Grid0.GotFocusAfter += new SAPbouiCOM._IGridEvents_GotFocusAfterEventHandler(this.Grid0_GotFocusAfter);
            this.Grid0.LostFocusAfter += Grid0_LostFocusAfter;
            this.ComboBox2 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_10").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_3").Specific));
            this.ComboBox0.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.ComboBox0_ComboSelectAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));

            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_6").Specific));
            ComboBox1.Item.Visible = false;

            for (int i = 0; i < ComboBox1.ValidValues.Count; i++)

                LowGroup.Add(ComboBox1.ValidValues.Item(i).Value, ComboBox1.ValidValues.Item(i).Description);

            Application.SBO_Application.Forms.ActiveForm.Menu.Add("DeleteRow", "Удалить строку", SAPbouiCOM.BoMenuType.mt_STRING, 0);
            this.ComboBox2.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.ComboBox0_ComboSelectAfter);
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));

            oRecordset.DoQuery("SELECT ItmsGrpCod, ItmsGrpNam FROM OITB");
            string selected = oRecordset.Fields.Item("ItmsGrpCod").Value.ToString();
            while (!oRecordset.EoF)
            {
                ComboBox0.ValidValues.Add(oRecordset.Fields.Item("ItmsGrpCod").Value.ToString(), oRecordset.Fields.Item("ItmsGrpNam").Value.ToString());
                oRecordset.MoveNext();
            }
            columnNames = new List<string>();
            ComboBox0.Select(selected);
            FillLowGrop(ComboBox0, ComboBox2);

            oRecordset.DoQuery("SELECT DISTINCT U_collection FROM OITM");

            while (!oRecordset.EoF)
            {
                //ComboBox1.ValidValues.Add(oRecordset.Fields.Item("U_collection").Value.ToString(), "");
                oRecordset.MoveNext();
            }
            ComboBox2.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);

            oRecordset.DoQuery(string.Format("SELECT * FROM ORTT where RateDate = '{0}'", Продажа.EditTextData.Value));
            oRecordset.MoveFirst();
            for (int i = 0; i < oRecordset.RecordCount; i++)
            {
                CurrectMoneyCourse.Add(oRecordset.Fields.Item("Currency").Value.ToString(), double.Parse(oRecordset.Fields.Item("Rate").Value.ToString()));
                oRecordset.MoveNext();
            }



        }

        private void Grid1_DoubleClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Grid1.DataTable.Rows.Remove(pVal.Row);
           
        }

        private void Grid0_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (pVal.ColUID == "Скидка, %" || pVal.ColUID == "Цена")
            {

                double val1, val2;
                Double.TryParse(Grid0.DataTable.GetValue(columnNames[2], pVal.Row).ToString(), out val1);//price
                Double.TryParse(Grid0.DataTable.GetValue(columnNames[4], pVal.Row).ToString(), out val2);//skidka

                double sum = val1 - (val2 / 100) * (val1);
                Grid0.DataTable.SetValue("Цена со скидкой", pVal.Row, sum.ToString());
            }
        }

        private void Grid1_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)

        {
            if (pVal.ColUID == "Скидка, %" || pVal.ColUID == "Цена")
            {

                double val1, val2;
                Double.TryParse(Grid1.DataTable.GetValue(columnNames[2], pVal.Row).ToString(), out val1);//price
                Double.TryParse(Grid1.DataTable.GetValue(columnNames[4], pVal.Row).ToString(), out val2);//skidka

                double sum = val1 - (val2 / 100) * (val1);
                Grid1.DataTable.SetValue("Цена со скидкой", pVal.Row, sum.ToString());
            }
        }

        private void Grid0_DoubleClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            if (pVal.Row != -1)
            {
                Application.SBO_Application.Forms.ActiveForm.Freeze(true);
                for (int j = 0; j < columnNames.Count; j++)
                {
                    Grid1.DataTable.SetValue(columnNames[j], Grid1.Rows.Count - 1, Grid0.DataTable.GetValue(columnNames[j], pVal.Row));
                }
                Grid1.DataTable.Rows.Add();

                SAPbouiCOM.EditTextColumn ItemCodeLinkedButt = (SAPbouiCOM.EditTextColumn)(Grid1.Columns.Item("Код товара"));
                ItemCodeLinkedButt.LinkedObjectType = "4";

                Application.SBO_Application.Forms.ActiveForm.Freeze(false);
                Application.SBO_Application.Forms.ActiveForm.Update();
            }
        }



        private void ComboBox0_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (ComboBox0.Selected == null || ComboBox2.Selected == null) return;
            oRecordset.DoQuery(string.Format("SELECT ItmsGrpNam FROM OITB WHERE ItmsGrpCod = '{0}'", ComboBox0.Selected.Value.ToString()));
            edit0.Value = oRecordset.Fields.Item(0).Value.ToString();
            edit1.Value = ComboBox2.Selected.Description;

            if (pVal.ItemUID == "Item_3")
            {
                FillLowGrop(ComboBox0, ComboBox2);
                ComboBox2.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            }

            if(pVal.ItemUID == "Item_10") FillMatrix();

            
            setGridWidth(Grid1);
        }

        private void setGridWidth(SAPbouiCOM.Grid grid)
        {
            Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            //grid.Columns.Item(0).Width = 80;
            grid.Columns.Item(1).Width = 220;
            for (int i = 2; i < grid.Columns.Count; i++)
            {
                grid.Columns.Item(i).Width = 100;
            }
            Application.SBO_Application.Forms.ActiveForm.Freeze(false);
            Application.SBO_Application.Forms.ActiveForm.Update();
            Application.SBO_Application.Forms.ActiveForm.Visible = true;
        }

        private void Grid0_GotFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            selectedRow = pVal.Row;
        }
        private void FillLowGrop(SAPbouiCOM.ComboBox oComboBox0, SAPbouiCOM.ComboBox oComboBox2)
        {
            Application.SBO_Application.Forms.ActiveForm.Freeze(true);
            while (oComboBox2.ValidValues.Count != 0)

                //oComboBox2.ValidValues.Remove("1", SAPbouiCOM.BoSearchKey.psk_Index);
                oComboBox2.ValidValues.Remove(oComboBox2.ValidValues.Count - 1, SAPbouiCOM.BoSearchKey.psk_Index);
            oRecordset.DoQuery(String.Format("Select distinct Cast(U_collection as decimal(18)) from OITM where ItmsGrpCod = '{0}' and U_collection IS NOT NULL order by Cast(U_collection as decimal(18)) asc", oComboBox0.Value.Trim()));
            oRecordset.MoveFirst();
            for (int i = 1; i <= oRecordset.RecordCount; i++)
            {
                var s = oRecordset.Fields.Item(0).Value.ToString();
                oComboBox2.ValidValues.Add(oRecordset.Fields.Item(0).Value.ToString(), LowGroup[oRecordset.Fields.Item(0).Value.ToString()]);
                oRecordset.MoveNext();
            }
            Application.SBO_Application.Forms.ActiveForm.Freeze(false);
            Application.SBO_Application.Forms.ActiveForm.Update();

        }

        private void FillMatrix()
        {
            StringBuilder ourQuery = new StringBuilder(
                @"SELECT ItemCode as 'Код товара', ItemName as 'Название товара',
(SELECT Format(Price,'N2') FROM ITM1 WHERE ItemCode = T0.ItemCode AND PriceList = '2') AS ЦенаМелк,
(SELECT Format(Amount,'N2') FROM SPP2 WHERE ItemCode = T0.ItemCode) AS 'Количество',
(SELECT Format(Discount,'N2') FROM SPP2 WHERE ItemCode = T0.ItemCode) AS 'Скидка, %',
((SELECT Price FROM ITM1 WHERE ItemCode = T0.ItemCode AND PriceList = '2')
- ((SELECT Price FROM ITM1 WHERE ItemCode = T0.ItemCode AND PriceList = '2') / 100)
* (SELECT Discount FROM SPP2 WHERE ItemCode = T0.ItemCode)) AS 'ЦенаОпт',
(SELECT Format(sum(OnHand),'N2') from OITW where ItemCode = T0.ItemCode) AS 'На складе', (SELECT WhsName from OWHS where T0.DfltWH = WhsCode) as 'Склад по умолчанию'"
);
            columnNames = new List<string>();
            columnNames.Add("Код товара");
            columnNames.Add("Название товара");
            columnNames.Add("ЦенаМелк");
            columnNames.Add("Количество");
            columnNames.Add("Скидка, %");
            columnNames.Add("ЦенаОпт");
            columnNames.Add("На складе");
            columnNames.Add("Склад по умолчанию");
            oRecordset.DoQuery("SELECT WhsCode, WhsName FROM OWHS where WhsName =N'4001-нижний' or WhsName =N'4001-02 верх' or WhsName =N'4002-нижний'");


            while (!oRecordset.EoF)
            {
                ourQuery.Append(string.Format(", (SELECT Format(OnHand,'N2')  FROM OITW WHERE ItemCode = T0.ItemCode AND WhsCode = '{0}' AND OnHand <> 0) AS '{1}'", oRecordset.Fields.Item("WhsCode").Value.ToString(), oRecordset.Fields.Item("WhsName").Value.ToString().Replace(' ', '_')));
                columnNames.Add(oRecordset.Fields.Item("WhsName").Value.ToString().Replace(' ', '_'));
                oRecordset.MoveNext();
            }
            ourQuery.Append(string.Format(" FROM OITM T0 WHERE ItmsGrpCod = {0} AND U_collection = {1}", ComboBox0.Value.ToString(), ComboBox2.Value.ToString()));

            string ggggg = ourQuery.ToString();
            try
            {
                Grid0.DataTable.ExecuteQuery(ourQuery.ToString());
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            //setGridWidth(Grid0);
            this.Grid0.Columns.Item("Название товара").Width = 210;
            SAPbouiCOM.EditTextColumn ItemCodeLinkedButt = (SAPbouiCOM.EditTextColumn)(Grid0.Columns.Item("Код товара"));


            if (!IsOpen)
            {
                initGrid1(ourQuery);
                IsOpen = true;
            }

            for (int i = 0; i < Grid1.Columns.Count; i++)
            {
                if (i != 3) Grid1.Columns.Item(i).Editable = false;
            }
        }

        private void initGrid1(StringBuilder ourQuery)
        {
            ourQuery.Append(" and 'Код товара' = 'марсик'");
            Grid1.DataTable.ExecuteQuery(ourQuery.ToString());
            Grid1.Columns.Item("Название товара").Width = 210;
            Grid1.Columns.Item("Количество").Editable = true;
            setGridWidth(Grid0);
        }

        private SAPbouiCOM.Grid Grid0;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.EditText edit0;
        private SAPbouiCOM.EditText edit1;

        private void Button1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var thisForm = Application.SBO_Application.Forms.ActiveForm;
            SAPbouiCOM.Matrix oMatrix = ((SAPbouiCOM.Matrix)SalesForm.Items.Item("38").Specific);// Matrix Sell Item
            SalesForm.Mode = SAPbouiCOM.BoFormMode.fm_ADD_MODE;
            int StratRow = oMatrix.RowCount;
            thisForm.Visible = false;
            for (int i = 0; i < Grid1.DataTable.Rows.Count; i++)
            {
                SalesForm.Freeze(true);
                if (Grid1.DataTable.GetValue(columnNames[0], i).ToString() != "")
                {
                    try
                    {
                        ((SAPbouiCOM.EditText)oMatrix.Columns.Item("1").Cells.Item(StratRow + i).Specific).Value = Grid1.DataTable.GetValue(columnNames[0], i).ToString();//number item
                    }
                    catch
                    {
                        Application.SBO_Application.MessageBox("Товар не соответствует общему соглашению или не существует");
                    }

                    ((SAPbouiCOM.EditText)oMatrix.Columns.Item("11").Cells.Item(StratRow + i).Specific).Value = Grid1.DataTable.GetValue(columnNames[3], i).ToString();//count item
                    var itmcode = Grid1.DataTable.GetValue(columnNames[0], i).ToString().Replace(',', '.');
                    var price = Grid1.DataTable.GetValue(columnNames[2], i).ToString().Replace(',', '.');
                    oRecordset.DoQuery(string.Format("Select Currency from ITM1 where ItemCode ='{0}' and Price like '{1}%'", itmcode, price.Remove(price.Length - 1)));
                    if (oRecordset.Fields.Item(0).Value.ToString() == Продажа.EditTextMoneyCurr.Value)
                        ((SAPbouiCOM.EditText)oMatrix.Columns.Item("14").Cells.Item(StratRow + i).Specific).Value = Grid1.DataTable.GetValue(columnNames[2], i).ToString();//price item
                    else
                    {
                        var cursValDoc = CurrectMoneyCourse.Single(x => x.Key == Продажа.EditTextMoneyCurr.Value).Value;//curr vall for currect
                        double cursVal = 0; //national vall doc
                        try
                        {
                            cursVal = (CurrectMoneyCourse.Single(x => x.Key == oRecordset.Fields.Item(0).Value.ToString()).Value);
                        }
                        catch (Exception)
                        {
                            Application.SBO_Application.MessageBox("Hasn't Currency course into ITM1 with itemcode=" + itmcode + " and price =" + price);
                        }
                        double curPrice;
                        double.TryParse(Grid1.DataTable.GetValue(columnNames[2], i).ToString().Replace('.', ','), out curPrice);
                        var a = (curPrice * cursVal) / cursValDoc;
                        Debug.Print(a.ToString());
                        ((SAPbouiCOM.EditText)oMatrix.Columns.Item("14").Cells.Item(StratRow + i).Specific).Value = a.ToString("0,00");//price item

                    }
                }
                else
                {
                    SalesForm.Freeze(false);
                    Application.SBO_Application.Forms.ActiveForm.Update();
                    continue;

                }
                SalesForm.Freeze(false);
                Application.SBO_Application.Forms.ActiveForm.Update();
            }
            //Grid1.DataTable.Rows.Add();
            thisForm.Close();

        }//copy to sales
        private SAPbouiCOM.Grid Grid1;
        private SAPbouiCOM.Button Button2;

        private void Button2_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)//add to grid1
        {
            BubbleEvent = true;

            if (pVal.Row != -1)
            {
                Application.SBO_Application.Forms.ActiveForm.Freeze(true);
                for (int j = 0; j < columnNames.Count; j++)
                {


                    Grid1.DataTable.SetValue(columnNames[j], Grid1.Rows.Count - 1, Grid0.DataTable.GetValue(columnNames[j], pVal.Row));
                }
                Grid1.DataTable.Rows.Add();

                SAPbouiCOM.EditTextColumn ItemCodeLinkedButt = (SAPbouiCOM.EditTextColumn)(Grid1.Columns.Item("Код товара"));
                ItemCodeLinkedButt.LinkedObjectType = "4";

                Application.SBO_Application.Forms.ActiveForm.Freeze(false);
                Application.SBO_Application.Forms.ActiveForm.Update();
            }


        }

        private void Form_ResizeAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            //this.Grid0.Item.Height = 144;
            this.Grid1.Item.Height = 140;
            this.Grid1.Columns.Item("Название товара").Width = 220; //Grid0.Columns.Item("Код товара").Width;
            this.Grid0.Columns.Item("Название товара").Width = 220;

            Grid1.Item.Top = Grid0.Item.Height + Grid0.Item.Top + 22;


        }

        private SAPbouiCOM.ComboBox ComboBox1;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.ComboBox ComboBox2;
    }
}
