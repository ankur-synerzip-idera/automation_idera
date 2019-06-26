/*
 * Created by Ranorex
 * User: smitar
 * Date: 26-06-2019
 * Time: 11:24 AM
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
    /// Description of StartQueryAnalyzer.
    /// </summary>
    [TestModule("EB21E499-4BC5-42D1-9E5D-D4927A656B01", ModuleType.UserCode, 1)]
    public class StartQueryAnalyzer : ITestModule
    {
    	private static ADS_startRepository myRepo;
		
    	/// <summary>
        /// Constructs a new instance.
        /// </summary>
        public StartQueryAnalyzer()
        {
        	myRepo=new ADS_startRepository();
        }

        
        [TestVariable("c3fccf8d-7c20-4094-8694-07e9e441e376")]
        public string ServerNameVar
        {
        	get { return myRepo.ServerNameRepo; }
        	set { myRepo.ServerNameRepo = value; }
        }
        
        string _logNameVar = "";
        [TestVariable("a521ba9f-f6d5-4acb-8290-7c4f88740036")]
        public string logNameVar
        {
        	get { return _logNameVar; }
        	set { _logNameVar = value; }
        }
        
        /// <summary>
        /// Performs the playback of actions in this module.
        /// </summary>
        /// <remarks>You should not call this method directly, instead pass the module
        /// instance to the <see cref="TestModuleRunner.Run(ITestModule)"/> method
        /// that will in turn invoke this method.</remarks>
        void ITestModule.Run()
        {
        	int index=Convert.ToInt32(TestSuite.Current.Parameters["QueryWindowIndexGP"]);
        	string verifyTitle=string.Empty;
        	if(index > 1)
        	{
        		verifyTitle = logNameVar+"@"+ServerNameVar+" [Untitled"+index.ToString()+"]";
        	}
        	else
        	{
        		verifyTitle = logNameVar+"@"+ServerNameVar+" [Untitled]";
        	}
        	Report.Info("QueryWindowIndex",index.ToString());
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            if(myRepo.Datastudio.ServerMainEle.Text.Contains(ServerNameVar))
            {
            	Report.Info("Validation","Open Query analyzer process starts!!");
	            //Click on server
	            myRepo.Datastudio.ServerMainEle.Click();
	            
	            //Right click on same element
	            myRepo.Datastudio.ServerMainEle.Click(System.Windows.Forms.MouseButtons.Right);
        
	            //Click on queryAnalyzer menu
	            myRepo.Datastudio.QueryAnalyzerEle.Click();
            }
            else
            {
            	Report.Failure("Validation","Server not found!!");
            }
             
             //Validating if queryanalyzer window is open
             if(myRepo.Datastudio.QueryAnalyzerWindowTitleEle.Title.Contains(verifyTitle))
             {
             	Report.Success("Validation","Query analyzer Opened!!");
             }
             else
             {
            	Report.Failure("Validation","Query analyzer not found!!");
             }
             TestSuite.Current.Parameters["QueryWindowIndexGP"]=Convert.ToString(index+1);
             Report.Info("QueryWindowIndex",TestSuite.Current.Parameters["QueryWindowIndexGP"]);
        }
    }
}
