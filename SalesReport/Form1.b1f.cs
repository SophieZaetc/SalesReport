using System;
using System.Collections.Generic;
using System.Xml;
using SAPbouiCOM.Framework;

namespace SalesReport
{
    [FormAttribute("SalesReport.Form1", "Form1.b1f")]
    class Form1 : UserFormBase
    {
        public Form1()
        {
        }
        private Dictionary<string, string> LowGroup = new Dictionary<string, string>() { };
        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_0").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_8").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_9").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_11").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_12").Specific));
            this.EditText9 = ((SAPbouiCOM.EditText)(this.GetItem("Item_13").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_15").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_16").Specific));
            this.Button0.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button0_ClickAfter);
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button0_ClickBefore);
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_3").Specific));
            this.ComboBox0.ComboSelectAfter += ComboBox0_ComboSelectAfter;
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_5").Specific));
            this.ComboBox1.Item.Visible = false;
            for (int i = 0; i < ComboBox1.ValidValues.Count; i++)

                LowGroup.Add(ComboBox1.ValidValues.Item(i).Value, ComboBox1.ValidValues.Item(i).Description);
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_17").Specific));
            this.StaticText8.Item.Visible = false;
            this.ComboBox2 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_18").Specific));
            this.ComboBox2.Item.Visible = false;
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_19").Specific));
            this.LinkedButton1 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_20").Specific));
            this.ComboBox3 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_21").Specific));
            this.OnCustomInitialize();

        }

        private void ComboBox0_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {

            FillLowGrop(ComboBox0, ComboBox3);
            ComboBox3.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
        }

        void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (ComboBox0.Value.Trim() == "")
            {
                Application.SBO_Application.StatusBar.SetText("Выберите группу товаров", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                BubbleEvent = false;
            }
            if (ComboBox3.Value.Trim() == "")
            {
                Application.SBO_Application.StatusBar.SetText("Выберите подгруппу товаров", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                BubbleEvent = false;
            }

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }
        private void FillLowGrop(SAPbouiCOM.ComboBox oComboBox0, SAPbouiCOM.ComboBox oComboBox2)
        {
            oForm = Application.SBO_Application.Forms.ActiveForm;
            oForm.Freeze(true);
            oRecordset = (SAPbobsCOM.Recordset)Program.oCopmany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
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
            oForm.Freeze(false);
            oForm.Update();


        }
        private SAPbouiCOM.EditText EditText0;

        private void OnCustomInitialize()
        {
            EditText0.Value = "20170101";
            EditText6.Value = "20170401";
            EditText8.Value = "20170402";
            EditText1.Value = "20170701";
            EditText7.Value = "20170702";
            EditText9.Value = "20171201";
            CombooooooooBoxNoMatrix(ComboBox0, "Select ItmsGrpCod, ItmsGrpNam from OITB", "ItmsGrpCod", "ItmsGrpNam", "DT_0");
            CombooooooooBoxNoMatrix(ComboBox2, "Select WhsCode, WhsName from OWHS", "WhsCode", "WhsName", "DT_1");

        }

        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.EditText EditText8;
        private SAPbouiCOM.EditText EditText9;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.Button Button0;
        public static string dateStart1;
        public static string dateStart2;
        public static string dateStart3;
        public static string dateEnd1;
        public static string dateEnd2;
        public static string dateEnd3;
        public static string group;
        public static string lowGroup;
        public static string warehouse;

        private void Button0_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            dateStart1 = EditText0.Value;
            dateStart2 = EditText6.Value;
            dateStart3 = EditText8.Value;
            dateEnd1 = EditText1.Value;
            dateEnd2 = EditText7.Value;
            dateEnd3 = EditText9.Value;
            group = ComboBox0.Value.Trim();
            lowGroup = ComboBox3.Value.Trim();
            warehouse = ComboBox2.Value.Trim();
            Form2 oForm = new Form2();
            oForm.Show();

        }

        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.ComboBox ComboBox1;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.ComboBox ComboBox2;
        private SAPbouiCOM.LinkedButton LinkedButton0;
        private SAPbouiCOM.LinkedButton LinkedButton1;
        SAPbouiCOM.Form oForm;
        SAPbouiCOM.DataTable oDataTable;
        private void CombooooooooBoxNoMatrix(SAPbouiCOM.ComboBox oComboBox, string query, string ValName, string DescriptionName, string DataTableID)
        {
            oForm = Application.SBO_Application.Forms.GetForm("SalesReport.Form1", 0);
            oForm.Freeze(true);

                while (oComboBox.ValidValues.Count != 0)
                    oComboBox.ValidValues.Remove(oComboBox.ValidValues.Count - 1, SAPbouiCOM.BoSearchKey.psk_Index);

            oDataTable = oForm.DataSources.DataTables.Item(DataTableID);
            oDataTable.ExecuteQuery(query);
            for (int i = 0; i < oDataTable.Rows.Count; i++)

                oComboBox.ValidValues.Add(oDataTable.GetValue(ValName, i).ToString(), oDataTable.GetValue(DescriptionName, i).ToString());


            oForm.Freeze(false);
            oForm.Update();

        }

        private SAPbouiCOM.ComboBox ComboBox3;
        private SAPbobsCOM.Recordset oRecordset;
    }
}