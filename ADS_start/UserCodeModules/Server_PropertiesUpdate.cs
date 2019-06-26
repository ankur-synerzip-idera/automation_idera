/*
 * Created by Ranorex
 * User: smitar
 * Date: 25-06-2019
 * Time: 02:11 PM
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Threading;
using WinForms = System.Windows.Forms;

using Ranorex;
using Ranorex.Core;
using Ranorex.Core.Testing;

namespace ADS_start
{
    /// <summary>
    /// Description of Server_PropertiesUpdate.
    /// </summary>
    [TestModule("A148252E-5F2C-40C6-81B1-8AA1F107A160", ModuleType.UserCode, 1)]
    public class Server_PropertiesUpdate : ITestModule
    {
    	
    	private static ADS_startRepository myRepo;
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Server_PropertiesUpdate()
        {
        	myRepo= new ADS_startRepository();
            // Do not delete - a parameterless constructor is required!
        }
        
        
        [TestVariable("124963b7-41af-4506-9cf9-dd17803084ec")]
        public string serverNameVar
        {
        	get { return myRepo.ServerNameRepo; }
        	set { myRepo.ServerNameRepo = value; }
        }
        
        
        string _editServerPropTitleVar = "Edit Server Properties";
        [TestVariable("a0210479-4cb0-473d-bf76-e272673602e5")]
        public string editServerPropTitleVar
        {
        	get { return _editServerPropTitleVar; }
        	set { _editServerPropTitleVar = value; }
        }
        
        string _addScriptVar = "";
        [TestVariable("57579758-b2b4-4526-9c32-e9204596b91e")]
        public string addScriptVar
        {
        	get { return _addScriptVar; }
        	set { _addScriptVar = value; }
        }
        
        

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
            
            if(myRepo.Datastudio.ServerMainEle.Text.Contains(serverNameVar))
            {
            	Report.Info("Validation","Update Server properties in progress!!");            	
            	myRepo.Datastudio.ServerMainEle.Click(System.Windows.Forms.MouseButtons.Right);            	
            	myRepo.Datastudio.ServerPropertiesEle.Click();
            	Thread.Sleep(1000);
            	HandleUpdateServerProp();
            	
            }
            else
            {
            	Report.Info("Validation","Server Name doesn't exists!!");
            }
            
        }
        
        private void HandleUpdateServerProp()
        {
    		if(myRepo.Datastudio.RegrServerTitleBarFormEle.Text.Contains(editServerPropTitleVar))
        	{
    			
        		Report.Success("Validation","Edit Server Property window opened!!");  
        		myRepo.Datastudio.ScriptMenuEle.Click();
        		Thread.Sleep(400);
        		myRepo.Datastudio.scriptEditEle.Click();
        		myRepo.Datastudio.scriptEditEle.PressKeys("{END}{SHIFT DOWN}{HOME}{SHIFT UP}{DELETE}"); 
        		myRepo.Datastudio.scriptEditEle.PressKeys(addScriptVar);
        		myRepo.Datastudio.SaveServerPropEle.Click(); 
        		Thread.Sleep(400);
//        		if(myRepo.Datastudio.SchemaChangePopupEle !=null)
//        		{
//        			myRepo.Datastudio.UnRegisterPopupButtonYesEle.Click();
//        		}
        			
        		Report.Success("Validation","Server Property Updated!!");  
        			
        	}
        	else
        	{
        		Report.Error("Validation","Edit Server Property window not found!!");
        	}
    }
}
}
