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
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(Button0_ClickBefore);
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_3").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_5").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_17").Specific));
            this.StaticText8.Item.Visible = false;
            this.ComboBox2 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_18").Specific));
            this.ComboBox2.Item.Visible = false;
            this.LinkedButton0 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_19").Specific));
            this.LinkedButton1 = ((SAPbouiCOM.LinkedButton)(this.GetItem("Item_20").Specific));
            this.OnCustomInitialize();

        }

        void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (ComboBox0.Value.Trim() == "")
            {
                Application.SBO_Application.StatusBar.SetText("Выберите группу товаров", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                BubbleEvent = false;
            }
            if (ComboBox1.Value.Trim() == "")
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
            lowGroup = ComboBox1.Value.Trim();
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

            if (oComboBox.ValidValues.Count != 0)
            {
                while (oComboBox.ValidValues.Count != 0)
                {
                    oComboBox.ValidValues.Remove(oComboBox.ValidValues.Count - 1, SAPbouiCOM.BoSearchKey.psk_Index);
                }
            }


            oDataTable = oForm.DataSources.DataTables.Item(DataTableID);
            oDataTable.ExecuteQuery(query);
            for (int i = 0; i < oDataTable.Rows.Count; i++)

                oComboBox.ValidValues.Add(oDataTable.GetValue(ValName, i).ToString(), oDataTable.GetValue(DescriptionName, i).ToString());


            oForm.Freeze(false);
            oForm.Update();

        }
    }
}