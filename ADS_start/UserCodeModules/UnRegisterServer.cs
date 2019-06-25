/*
 * Created by Ranorex
 * User: smitar
 * Date: 25-06-2019
 * Time: 11:17 AM
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;
using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ADS_start.UserCodeModules
{
    /// <summary>
    /// Description of DeRegisterServer.
    /// </summary>
    [TestModule("B97A5C1F-236C-4E50-8ACD-19858A7BC1DC", ModuleType.UserCode, 1)]
    public class UnRegisterServer : ITestModule
    {
    	private static ADS_startRepository myRepo;
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public UnRegisterServer()
        {
    		myRepo= new ADS_startRepository();
        }
        
        [TestVariable("5afaf8dc-8488-4aca-9d19-1ea45a579e80")]
        public string unRegisterServerNameVar
        {
        	get { return myRepo.ServerNameRepo; }
        	set { myRepo.ServerNameRepo = value; }
        }
        
        private string UnRegServerPropTitleBar = "Unregister Server";
        
        

        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            myRepo.Datastudio.ServerMainEle.Click();
           // if(myRepo.Datastudio.UnRegisterServerMainEle!=null && myRepo.Datastudio.UnRegisterServerMainEleInfo.Exists())
            if(myRepo.Datastudio.ServerMainEle.Text.Contains(unRegisterServerNameVar))
            {
            	Report.Success("Validation","UnRegister Server process started!!");            	
            	myRepo.Datastudio.ServerMainEle.Click(System.Windows.Forms.MouseButtons.Right);
            	myRepo.Datastudio.UnregisterServerEle.Click();
            	
            	myRepo.Datastudio.RegrServerTitleBarFormEle.Click();            	
            	HandleUnRegistrationOfServer();
            }
            else
            {
            	Report.Failure("Validation","Server Name doesn't exists!!");
            }
            
            if(myRepo.Datastudio.ServerMainEleInfo.Exists())
            {
				Report.Failure("Validation","un register server process failed!!");	            
            }
            else
            {
            	Report.Success("Validation","Server - '"+unRegisterServerNameVar+"'  UnRegistered Successfully!!");            	
            }
        }
        private void HandleUnRegistrationOfServer()
        {
        	Thread.Sleep(1000);
    		if(myRepo.Datastudio.RegrServerTitleBarFormEle.Text.Contains(UnRegServerPropTitleBar))
        	{
        		Report.Success("Validation","Edit Server properties window opened!!");         	
        			myRepo.Datastudio.UnRegisterPopupButtonYesEle.Click();
        			
        	}
        	else
        	{
        		Report.Failure("Validation","Edit Server properties window not found!!");
        	}
        }
        	
    }
}
