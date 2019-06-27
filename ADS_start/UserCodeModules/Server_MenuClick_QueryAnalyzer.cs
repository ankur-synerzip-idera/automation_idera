/*
 * Created by Ranorex
 * User: smitar
 * Date: 27-06-2019
 * Time: 12:47 PM
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

namespace ADS_start.UserCodeModules
{
	
    /// <summary>
    /// Description of Server_MenuClick_QueryAnalyzer.
    /// </summary>
    [TestModule("0D2011B4-17A2-48A8-A61F-87C61130435E", ModuleType.UserCode, 1)]
    public class Server_MenuClick_QueryAnalyzer : ITestModule
    {
    	
    	string verifyTitle=string.Empty;
    	int index=0;
    	private static ADS_startRepository myRepo;
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public Server_MenuClick_QueryAnalyzer()
        {
            // Do not delete - a parameterless constructor is required!
            myRepo=new ADS_startRepository();
        }
      
        [TestVariable("855fdeac-55b1-4786-ade3-074e9a834205")]
        public string ServerNameVar
        {
        	get { return myRepo.ServerNameRepo; }
        	set { myRepo.ServerNameRepo = value; }
        }
        
        string _logNameVar = "";
        [TestVariable("5722ac5c-e8f3-4ae7-ac2a-5e864621eadc")]
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
            Mouse.DefaultMoveTime = 300;
            Keyboard.DefaultKeyPressTime = 100;
            Delay.SpeedFactor = 1.0;
            Opeartion();
            Validation_OpenQA();
            Validation_CheckQA();
            
        	        	
            
        }
    	
		private void Opeartion()
		{
			try {				
			
			// Get Query analyzer window count
            index= Convert.ToInt32(TestSuite.Current.Parameters["QueryWindowIndexGP"]);
        	
        	if(index > 1)
        	{
        		verifyTitle = logNameVar+"@"+ServerNameVar+" [Untitled "+index.ToString()+"]";
        	}
        	else
        	{
        		verifyTitle = logNameVar+"@"+ServerNameVar+" [Untitled]"; 
        	}
        	Report.Info("QueryWindowIndex",index.ToString());
        	} catch (Exception ex) {
				
				Report.Error(ex.InnerException.ToString());
			}
		}
    	
		private void Validation_OpenQA()
		{
			try {
				
			
			//Ckeck server Name
        	if(myRepo.Datastudio.ServerMainEle.Text.Contains(ServerNameVar))
            {
            	Report.Info("Validation","Open Query analyzer process starts!!");
	            //Click on server
	            myRepo.Datastudio.ServerMainEle.Click();
	            
	            //Select 'Server' Menu option
	            myRepo.Datastudio.ServerMenuEle.Click();
        
	            //Click on queryAnalyzer menu
	            myRepo.Datastudio.QueryAnalyzerEle.Click();
            }
            else
            {
            	Report.Failure("Validation","Server not found!!");
            }
            } catch (Exception ex) {
				
				Report.Error(ex.InnerException.ToString());
			}
		}
    	
		private void Validation_CheckQA()
		{
			try {
			
			myRepo.QueryWindowNameRepo=verifyTitle;
            Report.Info("verifyTitle- " +verifyTitle);
            Report.Info("myRepo.Datastudio.QueryAnalyzerWindowTitleEle.Title - " + myRepo.Datastudio.QueryAnalyzerWindowTitleEle.Title);
            myRepo.Datastudio.QueryAnalyzerWindowTitleEle.Click();
             //Validating if queryanalyzer window is open
             if(myRepo.Datastudio.QueryAnalyzerWindowTitleEle.Title.Contains(verifyTitle))
             {
             	Report.Success("Validation","By Menu, Query analyzer Opened!!");
             }
             else
             {
            	Report.Failure("Validation","By Menu, Query analyzer not found!!");
             }
             TestSuite.Current.Parameters["QueryWindowIndexGP"]=Convert.ToString(index+1);
             Report.Info("QueryWindowIndex",TestSuite.Current.Parameters["QueryWindowIndexGP"]);
             	
			} catch (Exception ex) {
				
				Report.Error(ex.InnerException.ToString());
			}
		}
    }
}
