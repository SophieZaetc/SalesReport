
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM.Framework;

namespace SalesReport
{

    [FormAttribute("133", "Продажа.b1f")]
    class Продажа : SystemFormBase
    {
        public Продажа()
        {
        }

        private SAPbouiCOM.Button ButtonCFL;
        private SAPbouiCOM.EditText EditText6;
        public static SAPbouiCOM.EditText EditTextData;
        public static SAPbouiCOM.ComboBox EditTextMoneyCurr;
        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {

            this.OnCustomInitialize();

        }

        private void ButtonCFL_ClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
           
                if (EditText6.Value != "" && EditTextData.Value != "")
                {

                    //Продажа.EditTextMoneyCurr = "UAH";

                    SalesCFLForm f = new SalesCFLForm(Application.SBO_Application.Forms.ActiveForm, EditTextData.Value);
                    f.Show();
                }
                else Application.SBO_Application.MessageBox("Не введен бизнес партнер или дата документа");
            
           
        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }


        private void OnCustomInitialize()
        {
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("4").Specific));

            Продажа.EditTextData = ((SAPbouiCOM.EditText)(this.GetItem("46").Specific));

            this.ButtonCFL = ((SAPbouiCOM.Button)(this.GetItem("btn_CFL").Specific));
            this.ButtonCFL.ClickAfter += new SAPbouiCOM._IButtonEvents_ClickAfterEventHandler(this.ButtonCFL_ClickAfter);
            this.ButtonCFL.ClickBefore += ButtonCFL_ClickBefore;
        }

        private void ButtonCFL_ClickBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            EditTextMoneyCurr = ((SAPbouiCOM.ComboBox)(this.GetItem("63").Specific));
            //Продажа.EditTextMoneyCurr = ((SAPbouiCOM.EditText)(this.GetItem("63").Specific)).Value;
        }
    }
}