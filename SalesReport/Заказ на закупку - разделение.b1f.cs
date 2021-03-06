
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace SalesReport
{

    [FormAttribute("142", "Заказ на закупку - разделение.b1f")]
    class Заказ_на_закупку___разделение : SystemFormBase
    {
        public static int formCount = 0;
        public Заказ_на_закупку___разделение()
        {
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("Item_0").Specific));
            this.Button0.ClickBefore += new SAPbouiCOM._IButtonEvents_ClickBeforeEventHandler(this.Button0_ClickBefore);
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("4").Specific));
            this.EditText0.LostFocusAfter += new SAPbouiCOM._IEditTextEvents_LostFocusAfterEventHandler(this.EditText0_LostFocusAfter);
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_2").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_3").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_4").Specific));
            this.Matrix0 = ((SAPbouiCOM.Matrix)(this.GetItem("38").Specific));
            this.Matrix0.LostFocusAfter += new SAPbouiCOM._IMatrixEvents_LostFocusAfterEventHandler(this.Matrix0_LostFocusAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            SAPbouiCOM.Form oForm = Application.SBO_Application.Forms.ActiveForm;
            formCount = oForm.TypeCount;
            this.CloseAfter += Заказ_на_закупку___разделение_CloseAfter;
        }

        private void Заказ_на_закупку___разделение_CloseAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            formType = null;
        }

        private SAPbouiCOM.Button Button0;

        private void OnCustomInitialize()
        {

        }

        private void Button0_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (EditText0.Value != "" && EditText0.Item.Enabled == true)
            {
                Form1 oForm = new Form1();
                oForm.Show();
                formType = "заказ";
            }
            else
            {
                BubbleEvent = false;
                Application.SBO_Application.StatusBar.SetText("Введите Бизнес партнера", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
            }

        }
        public static string formType;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.Form oForm;

        private void EditText0_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (pVal.ActionSuccess == true && EditText0.Value != "")

                Button0.Item.Enabled = true;

        }

        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.Matrix Matrix0;
        private SAPbouiCOM.UserDataSource oUserDataSourse1;
        private SAPbouiCOM.UserDataSource oUserDataSourse2;
        private static double p = 0;
        private static double pp = 0;
        private void Matrix0_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            oForm = Application.SBO_Application.Forms.GetForm("142", formCount);
            oUserDataSourse1 = oForm.DataSources.UserDataSources.Item("UD_366");
            oUserDataSourse2 = oForm.DataSources.UserDataSources.Item("UD_367");
            string s = String.Empty;
            
            if (pVal.ColUID == "58")
            {
                foreach (char ch in ((SAPbouiCOM.EditText)Matrix0.Columns.Item("58").Cells.Item(pVal.Row).Specific).Value)
                {
                    if (char.IsDigit(ch) || char.IsPunctuation(ch)) s += ch;
                }
                if(s != "")
                    p += Double.Parse(s.Replace(".", ",").Replace("'", ""));
                oUserDataSourse1.Value = p.ToString();
               
                
            }
            if (pVal.ColUID == "56" )
            {
                if(((SAPbouiCOM.EditText)Matrix0.Columns.Item("56").Cells.Item(pVal.Row).Specific).Value != "")
                    pp += Double.Parse(((SAPbouiCOM.EditText)Matrix0.Columns.Item("56").Cells.Item(pVal.Row).Specific).Value.Replace(".", ",").Replace("'", ""));
                oUserDataSourse2.Value = pp.ToString();
                
            }

            
        }

      
    }
}
