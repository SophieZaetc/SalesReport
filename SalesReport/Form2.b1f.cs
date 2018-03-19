using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace SalesReport
{
    [FormAttribute("SalesReport.Form2", "Form2.b1f")]
    class Form2 : UserFormBase
    {
        public Form2()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Grid0 = ((SAPbouiCOM.Grid)(this.GetItem("Item_0").Specific));
            this.Grid0.LostFocusAfter += new SAPbouiCOM._IGridEvents_LostFocusAfterEventHandler(this.Grid0_LostFocusAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_2").Specific));
            this.Button1.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button1_ClickAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_5").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_6").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_7").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_9").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_10").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_11").Specific));
            this.OnCustomInitialize();

        }
       
        void Grid0_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (pVal.ColUID == "Заказ")
            {
                oForm = Application.SBO_Application.Forms.ActiveForm;
                oForm.Freeze(true);
                Double.TryParse(Grid0.DataTable.GetValue("Заказ", pVal.Row).ToString().Replace(".", ","), out x);

                if (x != 0)
                {
                    double volume = Grid0.DataTable.GetValue("Обьем", pVal.Row).ToString().Replace(".", ",") == "" ? 0.00 : Double.Parse(Grid0.DataTable.GetValue("Обьем", pVal.Row).ToString().Replace(".", ","));
                    double weight = Grid0.DataTable.GetValue("Вес", pVal.Row).ToString().Replace(".", ",") == "" ? 0.00 : Double.Parse(Grid0.DataTable.GetValue("Вес", pVal.Row).ToString().Replace(".", ","));
                    if (Grid0.DataTable.GetValue(pVal.ColUID, pVal.Row).ToString() != "0.00")
                    {
                        Grid0.DataTable.SetValue("Общий вес", pVal.Row, (weight * Double.Parse(Grid0.DataTable.GetValue(pVal.ColUID, pVal.Row).ToString().Replace(".", ","))).ToString("0.00"));
                        Grid0.DataTable.SetValue("Общий обьем", pVal.Row, (volume * Double.Parse(Grid0.DataTable.GetValue(pVal.ColUID, pVal.Row).ToString().Replace(".", ","))).ToString("0.00"));
                        double s1 = 0.00;
                        double s2 = 0.00;
                        for (int i = 0; i < Grid0.Rows.Count - 1; i++)
                        {
                            s1 += Grid0.DataTable.GetValue("Общий вес", i).ToString().Replace(".", ",") == "" ? 0.00 : Double.Parse(Grid0.DataTable.GetValue("Общий вес", i).ToString().Replace(".", ","));
                            s2 += Grid0.DataTable.GetValue("Общий обьем", i).ToString().Replace(".", ",") == "" ? 0.00 : Double.Parse(Grid0.DataTable.GetValue("Общий обьем", i).ToString().Replace(".", ","));
                        }
                        Grid0.DataTable.SetValue("Общий вес", Grid0.DataTable.Rows.Count - 1, s1.ToString("0.00"));
                        Grid0.DataTable.SetValue("Общий обьем", Grid0.DataTable.Rows.Count - 1, s2.ToString("0.00"));
                        EditText3.Value = s1.ToString("0.00");
                        EditText4.Value = s2.ToString("0.00");
                    }
                }
                oForm.Freeze(false);
                oForm.Update();
            }
            
        }

       
        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {

            
        }

        private SAPbouiCOM.Grid Grid0;

        private void OnCustomInitialize()
        {
           
            string query = String.Format(@"declare @Prod1 table(
ItemCode nvarchar(250),
Dscription nvarchar(250),
КоличествоПродаж1 decimal(20));

INSERT INTO @Prod1 SELECT  INV1.ItemCode, max(INV1.Dscription), SUM(INV1.Quantity) AS КоличествоПродаж1
FROM         INV1 INNER JOIN
                      OINV ON INV1.DocEntry = OINV.DocEntry  where OINV.DocDate >= CONVERT(DATETIME, '{0}', 102) AND OINV.DocDate <= CONVERT(DATETIME, '{1}', 102)
GROUP BY  INV1.ItemCode


declare @Prod2 table(
ItemCode nvarchar(250),
Dscription nvarchar(250),
КоличествоПродаж2 decimal(20));

INSERT INTO @Prod2 SELECT  INV1.ItemCode, max(INV1.Dscription), SUM(INV1.Quantity) AS КоличествоПродаж2
FROM         INV1 INNER JOIN
                      OINV ON INV1.DocEntry = OINV.DocEntry  where OINV.DocDate >= CONVERT(DATETIME, '{2}', 102) AND OINV.DocDate <= CONVERT(DATETIME, '{3}', 102)
GROUP BY  INV1.ItemCode


declare @Prod3 table(
ItemCode nvarchar(250),
Dscription nvarchar(250),
КоличествоПродаж3 numeric(19));

INSERT INTO @Prod3 SELECT     INV1.ItemCode, max(INV1.Dscription), SUM(INV1.Quantity) AS КоличествоПродаж3
FROM         INV1 INNER JOIN
                      OINV ON INV1.DocEntry = OINV.DocEntry where OINV.DocDate >= CONVERT(DATETIME, '{4}', 102) AND OINV.DocDate <= CONVERT(DATETIME, '{5}', 102)
GROUP BY  INV1.ItemCode


Select max(OITM.FrgnName) as 'Артикул', OITM.ItemCode as 'Код товара',  max(OITM.ItemName) as 'Наименование', CONVERT(nvarchar,OITM.UserText) as 'Примечание', Format(max(OITM.IWeight1),'N2') as 'Вес',  Format(max(OITM.BVolume),'N2') as 'Обьем', Format(max(OITM.NumInBuy),'N2') as 'Кво шт. в ящ',  Format(SUM(OITW.OnHand),'N2') as 'Общее кол-во',  max(Convert(nvarchar,Format(ITM1.AddPrice2, 'N2'))) as 'Цена в ¥' ,  Format(max([@Prod1].КоличествоПродаж1),'N2') as '1', Format(max([@Prod2].КоличествоПродаж2),'N2') as '2', Format(max([@Prod3].КоличествоПродаж3),'N2') as '3', Format(SUM(OITW.OnHand) - max([@Prod1].КоличествоПродаж1),'N2') as 'Рекомендовано на заказ', Format(Try_Convert(decimal, 0),'N2') AS 'Заказ', Format(Try_Convert(decimal, 0),'N2') AS 'Общий вес', Format(Try_Convert(decimal, 0),'N2') AS 'Общий обьем'  from OITW, OITM 
LEFT JOIN @Prod1 ON OITM.ItemCode = [@Prod1].ItemCode LEFT JOIN @Prod2 ON OITM.ItemCode = [@Prod2].ItemCode LEFT JOIN @Prod3 ON OITM.ItemCode = [@Prod3].ItemCode LEFT JOIN ITM1 ON ITM1.ItemCode = OITM.ItemCode where OITM.ItemCode = OITW.ItemCode and OITM.InvntItem = 'Y' and OITM.PrchseItem = 'Y' and OITM.ItmsGrpCod = '{6}' and OITM.U_collection = '{7}'  GROUP BY OITM.ItemCode, CONVERT(nvarchar,OITM.UserText) ", Form1.dateStart1, Form1.dateEnd1, Form1.dateStart2, Form1.dateEnd2, Form1.dateStart3, Form1.dateEnd3, Form1.group, Form1.lowGroup);
            Grid0.DataTable.ExecuteQuery(query);
            oGridColumn = (SAPbouiCOM.EditTextColumn)Grid0.Columns.Item("Код товара");
            oGridColumn.LinkedObjectType = "4";
            Grid0.Columns.Item("Артикул").Editable = false;
            Grid0.Columns.Item("Код товара").Editable = false;
            Grid0.Columns.Item("Наименование").Editable = false;
            Grid0.Columns.Item("Примечание").Editable = false;
            Grid0.Columns.Item("Вес").Editable = false;
            Grid0.Columns.Item("Обьем").Editable = false;
            Grid0.Columns.Item("Кво шт. в ящ").Editable = false;
            Grid0.Columns.Item("Общее кол-во").Editable = false;
            Grid0.Columns.Item("Цена в ¥").Editable = false;
            Grid0.Columns.Item("1").Editable = false;
            Grid0.Columns.Item("2").Editable = false;
            Grid0.Columns.Item("3").Editable = false;
            Grid0.Columns.Item("Общий вес").Editable = false;
            Grid0.Columns.Item("Общий обьем").Editable = false;
            Grid0.Columns.Item("Рекомендовано на заказ").Editable = false;
            Grid0.Columns.Item("1").TitleObject.Sortable = true;
            Grid0.Columns.Item("2").TitleObject.Sortable = true;
            Grid0.Columns.Item("3").TitleObject.Sortable = true;
            Grid0.DataTable.Rows.Add();
          
            Grid0.SelectionMode = SAPbouiCOM.BoMatrixSelect.ms_Auto;
            
            double s1 = 0.00;
            double s2 = 0.00;
            double s3 = 0.00;
            for (int i = 0; i < Grid0.Rows.Count; i++)
            {
                if (Grid0.DataTable.GetValue("1", i).ToString() != "")
                    s1 += Double.Parse(Grid0.DataTable.GetValue("1", i).ToString().Replace(".", ","));
                if (Grid0.DataTable.GetValue("2", i).ToString() != "")
                    s2 += Double.Parse(Grid0.DataTable.GetValue("2", i).ToString().Replace(".", ","));
                if (Grid0.DataTable.GetValue("3", i).ToString() != "")
                {
                    s3 += Double.Parse(Grid0.DataTable.GetValue("3", i).ToString().Replace(".",","));
                        }
            }
            Grid0.DataTable.SetValue("1", Grid0.DataTable.Rows.Count - 1, s1.ToString("0.00"));
            Grid0.DataTable.SetValue("2", Grid0.DataTable.Rows.Count - 1, s2.ToString("0.00"));
            Grid0.DataTable.SetValue("3", Grid0.DataTable.Rows.Count - 1, s3.ToString("0.00"));
            EditText0.Value = s1.ToString("0.00");
            EditText1.Value = s2.ToString("0.00");
            EditText2.Value = s3.ToString("0.00");
        }

        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.EditTextColumn oGridColumn;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.Form oForm;
        double x = 0;
        private bool s = false;
        private void Button1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
           
            if(Заказ_на_закупку___разделение.formType != null)
            oForm = Application.SBO_Application.Forms.GetFormByTypeAndCount(142, Заказ_на_закупку___разделение.formCount);
            else
            oForm = Application.SBO_Application.Forms.GetFormByTypeAndCount(1470000200, Заявка_на_закупку.formCount);
            SAPbouiCOM.Matrix oMatrix = ((SAPbouiCOM.Matrix)(oForm.Items.Item("38").Specific));
            
            for (int i = 0; i < Grid0.Rows.Count-1; i++)
            {
                Double.TryParse(Grid0.DataTable.GetValue("Заказ", i).ToString().Replace(".", ","), out x);
                if (x != 0)
                {
                    s = true;
                    ((SAPbouiCOM.EditText)oMatrix.Columns.Item("1").Cells.Item(oMatrix.RowCount).Specific).Value = Grid0.DataTable.GetValue("Код товара", i).ToString();
                    ((SAPbouiCOM.EditText)oMatrix.Columns.Item("11").Cells.Item(oMatrix.RowCount -1).Specific).Value = Grid0.DataTable.GetValue("Заказ", i).ToString();
                    ((SAPbouiCOM.EditText)oMatrix.Columns.Item("56").Cells.Item(oMatrix.RowCount - 1).Specific).Value = Grid0.DataTable.GetValue("Общий обьем", i).ToString().Replace(",", ".") != "0.00" ? Grid0.DataTable.GetValue("Общий обьем", i).ToString().Replace(",", ".") : "";
                    ((SAPbouiCOM.EditText)oMatrix.Columns.Item("58").Cells.Item(oMatrix.RowCount - 1).Specific).Value = Grid0.DataTable.GetValue("Общий вес", i).ToString() != "0.00" ? Grid0.DataTable.GetValue("Общий вес", i).ToString() : "";
                }
               
                
            }
            if (!s)
                Application.SBO_Application.MessageBox("Не выбрано количество для заказа!", 1, "Ok");
            oForm.Select();
            oForm.Items.Item("Item_4").Click();
        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.EditText EditText4;
    }
}
