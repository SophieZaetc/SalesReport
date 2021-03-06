
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace SalesReport
{

    [FormAttribute("1250000940", "Запрос на перемещение запасов.b1f")]
    class Запрос_на_перемещение_запасов : SystemFormBase
    {
        public Запрос_на_перемещение_запасов()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.ClickBefore += this.Button0_ClickBefore;
            this.Button0.ClickAfter += this.Button0_ClickAfter;
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("1470000101").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("18").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("23").Specific));
            this.OnCustomInitialize();

        }

        private void Button0_ClickAfter(object sboObject, SBOItemEventArg pVal)
        {
            SAPbobsCOM.Recordset oReordset = (SAPbobsCOM.Recordset)Program.oCopmany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            string query = String.Format("Select ItemCode, OnHand, MinStock, MaxStock from OITW where WhsCode = '{0}'", EditText0.Value);
            SAPbobsCOM.Recordset oReordSet = (SAPbobsCOM.Recordset)Program.oCopmany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            SAPbobsCOM.Recordset oRecordset = (SAPbobsCOM.Recordset)Program.oCopmany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            query = String.Format("Select ItemCode, OnHand, MinStock, MaxStock from OITW where WhsCode = '{0}'", EditText0.Value);
            oReordset.DoQuery(query);
            oReordset.MoveFirst();
            double quantity = 0;
            for (int i = 0; i < oReordset.RecordCount; i++)
            {
                if(Double.Parse(oReordset.Fields.Item(1).Value.ToString()) < Double.Parse(oReordset.Fields.Item(2).Value.ToString())  && Double.Parse(oReordset.Fields.Item(1).Value.ToString()) < Double.Parse(oReordset.Fields.Item(3).Value.ToString()))
                {
                    ((SAPbouiCOM.EditText)Matrix0.Columns.Item("1").Cells.Item(Matrix0.RowCount).Specific).Value = oReordset.Fields.Item(0).Value.ToString();
                    
                    query = String.Format("Select NumInCnt, CntUnitMsr, BuyUnitMsr, NumInBuy from OITM where ItemCode = N'{0}'", oReordset.Fields.Item(0).Value.ToString());
                    oReordSet.DoQuery(query);
                    string mesure = "";
                    if (oReordSet.Fields.Item(1).Value.ToString() != "")
                    {
                        query = String.Format("Select UomCode from OUOM where UomName = N'{0}'", oReordSet.Fields.Item(1).Value.ToString());
                        quantity = Math.Round(((Double.Parse(oReordset.Fields.Item(3).Value.ToString().Replace("'", "")) - Double.Parse(oReordset.Fields.Item(1).Value.ToString().Replace("'", ""))) / Double.Parse(oReordSet.Fields.Item(0).Value.ToString().Replace("'", ""))));
                        oRecordset.DoQuery(query);
                        mesure = oRecordset.Fields.Item(0).Value.ToString();
                    }
                    else
                    {
                        query = String.Format("Select UomCode from OUOM where UomName = N'{0}'", oReordSet.Fields.Item(2).Value.ToString());                    
                        quantity = Math.Ceiling(((Double.Parse(oReordset.Fields.Item(3).Value.ToString().Replace("'","")) - Double.Parse(oReordset.Fields.Item(1).Value.ToString().Replace("'", ""))) / Double.Parse(oReordSet.Fields.Item(3).Value.ToString().Replace("'", ""))));
                        oRecordset.DoQuery(query);
                        mesure = oRecordset.Fields.Item(0).Value.ToString();
                    }
                     ((SAPbouiCOM.EditText)Matrix0.Columns.Item("10").Cells.Item(Matrix0.RowCount-1).Specific).Value = quantity.ToString();
                    ((SAPbouiCOM.ComboBox)Matrix0.Columns.Item("1001").Cells.Item(Matrix0.RowCount - 1).Specific).Select("N", BoSearchKey.psk_ByValue);
                    ((SAPbouiCOM.EditText)Matrix0.Columns.Item("1470001043").Cells.Item(Matrix0.RowCount - 1).Specific).Value = mesure;
                }
                oReordset.MoveNext();
                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Подождите, документ заполняется ...", BoMessageTime.bmt_Long, BoStatusBarMessageType.smt_Warning);
            }
            SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Документ заполнен", BoMessageTime.bmt_Short, BoStatusBarMessageType.smt_Success);
        }

        private void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if(EditText0.Value == "")
            {
                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Заполните поле \"На склад\"", BoMessageTime.bmt_Medium, BoStatusBarMessageType.smt_Error);
                BubbleEvent = false;
            }
            if (EditText1.Value == "")
            {
                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Заполните поле \"Со склада\"", BoMessageTime.bmt_Medium, BoStatusBarMessageType.smt_Error);
                BubbleEvent = false;
            }
            if (EditText0.Value == EditText1.Value)
            {
                SAPbouiCOM.Framework.Application.SBO_Application.StatusBar.SetText("Выберите разные склады для перемещения", BoMessageTime.bmt_Medium, BoStatusBarMessageType.smt_Error);
                BubbleEvent = false;
            }
        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.EditText EditText0;
        private EditText EditText1;
        private Matrix Matrix0;

       
    }
}
