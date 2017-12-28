using System;
using System.Collections.Generic;
using System.Text;
using SAPbouiCOM.Framework;

namespace SalesReport
{
    class Menu
    {
        public void AddMenuItems()
        {
            

            try
            {
                //  If the manu already exists this code will fail
               
            }
            catch (Exception e)
            {

            }

            try
            {
               
            }
            catch (Exception er)
            { //  Menu already exists
                Application.SBO_Application.SetStatusBarMessage("Menu Already Exists", SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (pVal.BeforeAction && pVal.MenuUID == "SalesReport.Form1")
                {
                    Form1 activeForm = new Form1();
                    activeForm.Show();
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

    }
}
