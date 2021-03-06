
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace SalesReport
{

    [FormAttribute("1470000200", "Заявка на закупку.b1f")]
    class Заявка_на_закупку : SystemFormBase
    {
        public Заявка_на_закупку()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("Item_1").Specific));
            this.Button1.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.Button1_ClickAfter);
            this.Button1.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button1_ClickBefore);
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("1470002186").Specific));
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_4").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_5").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("38").Specific));
            this.Matrix0.LostFocusAfter += new SAPbouiCOM._IMatrixEvents_LostFocusAfterEventHandler(this.Matrix0_LostFocusAfter);
            this.OnCustomInitialize();

        }
        public static int formCount = 0;
        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.ActiveForm;
            formCount = oForm.TypeCount;
            this.CloseAfter += new CloseAfterHandler(this.Form_CloseAfter);

        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {

        }

        private SAPbouiCOM.Button Button1;

        private void Button1_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (ComboBox0.Value.Trim() == "")
            {
                Application.SBO_Application.StatusBar.SetText("Заполните автора заявки", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                BubbleEvent = false;
            }
        }

        private SAPbouiCOM.ComboBox ComboBox0;
        public static string formType; 

        private void Button1_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            Form1 oForm = new Form1();
            oForm.Show();
            formType = "заявка";
        }

        private void Form_CloseAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {

            formType = null;
        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.Matrix Matrix0;
        private SAPbouiCOM.Form oForm;
        private SAPbouiCOM.UserDataSource oUserDataSourse1;
        private SAPbouiCOM.UserDataSource oUserDataSourse2;
        private static double p = 0;
        private static double pp = 0;
        private void Matrix0_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            oForm = Application.SBO_Application.Forms.GetForm("1470000200", formCount);
            oUserDataSourse1 = oForm.DataSources.UserDataSources.Item("UD_366");
            oUserDataSourse2 = oForm.DataSources.UserDataSources.Item("UD_367");
            string s = String.Empty;

            if (pVal.ColUID == "58")
            {
                foreach (char ch in ((SAPbouiCOM.EditText)Matrix0.Columns.Item("58").Cells.Item(pVal.Row).Specific).Value)
                {
                    if (char.IsDigit(ch) || char.IsPunctuation(ch)) s += ch;
                }
                if (s != "")
                    p += Double.Parse(s.Replace(".", ",").Replace("'", ""));
                oUserDataSourse1.Value = p.ToString();
                

            }
            if (pVal.ColUID == "56")
            {
                if (((SAPbouiCOM.EditText)Matrix0.Columns.Item("56").Cells.Item(pVal.Row).Specific).Value != "")
                    pp += Double.Parse(((SAPbouiCOM.EditText)Matrix0.Columns.Item("56").Cells.Item(pVal.Row).Specific).Value.Replace(".", ",").Replace("'", ""));
                oUserDataSourse2.Value = pp.ToString();
                
            }
            
        }
    }
}
