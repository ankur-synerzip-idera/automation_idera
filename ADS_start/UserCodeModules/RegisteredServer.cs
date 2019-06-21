/*
 * Created by Ranorex
 * User: smitar
 * Date: 20-06-2019
 * Time: 04:10 PM
 * 
 * To change this template use Tools > Options > Coding > Edit standard headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Description of RegisteredServer.
    /// </summary>
    [TestModule("1C501CB7-AF18-4F5F-89C3-C281529104F9", ModuleType.UserCode, 1)]
    public class RegisteredServer : ITestModule
    {
    	private static ADS_startRepository myRepo;
        /// <summary>
        /// Constructs a new instance.
        /// </summary>
        public RegisteredServer()
        {
            // Do not delete - a parameterless constructor is required!
            myRepo = new ADS_startRepository();
        }
        string _regWindowTitleVar = "Register Server";
        [TestVariable("530e84d3-e2d6-43bd-98e2-2959191840b8")]
        public string regWindowTitleVar
        {
        	get { return _regWindowTitleVar; }
        	set { _regWindowTitleVar = value; }
        }
        
        
        string _serverNameVar = "";
        [TestVariable("4ab6b51c-db37-4639-ba7e-4471493cb9c5")]
        public string serverNameVar
        {
        	get { return _serverNameVar; }
        	set { _serverNameVar = value; }
        }
        
        
        string _serverTypeVar = "";
        [TestVariable("30de2f7f-f4d9-4fcb-8fcd-cfabbfeda827")]
        public string serverTypeVar
        {
        	get { return _serverTypeVar; }
        	set { _serverTypeVar = value; }
        }
        
        
        string _driverTypeVar = "";
        [TestVariable("ecd0e6f5-938b-40ce-928b-b888ec626476")]
        public string driverTypeVar
        {
        	get { return _driverTypeVar; }
        	set { _driverTypeVar = value; }
        }
        
        string _AuthTypeVar = "";
        [TestVariable("6b81950f-a362-4373-9d7b-4f244037a1aa")]
        public string AuthTypeVar
        {
        	get { return myRepo.WindowsAuthTypeRepo; }
        	set { myRepo.WindowsAuthTypeRepo = value; }
        }
        
        string _loginTxtVar = "";
        [TestVariable("18fd9fa3-5632-4574-8d1a-139a5fc6701d")]
        public string loginTxtVar
        {
        	get { return _loginTxtVar; }
        	set { _loginTxtVar = value; }
        }
        
        
        string _loginPwdVar = "";
        [TestVariable("54e78504-5525-4c28-ad34-412beb71a133")]
        public string loginPwdVar
        {
        	get { return _loginPwdVar; }
        	set { _loginPwdVar = value; }
        }
        
        string _hostNameVar = "";
        [TestVariable("e8b1f3c3-9e85-4214-94fc-dad608b89ace")]
        public string hostNameVar
        {
        	get { return _hostNameVar; }
        	set { _hostNameVar = value; }
        }
        
        
        
        string _SuccessConnToDbVar = "";
        [TestVariable("5e643ca3-957b-41d7-90af-014ffa0d615a")]
        public string SuccessConnToDbVar
        {
        	get { return _SuccessConnToDbVar; }
        	set { _SuccessConnToDbVar = value; }
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
            
            // Click on  - Local Database Servers                      
             var LocalDBServerlink = myRepo.AquaDataStudioEvaluation.DatabaseServersEle;
             LocalDBServerlink.Click(System.Windows.Forms.MouseButtons.Right);
             
             
             // Click register server link
             var registerServerLink = myRepo.Datastudio.RegisterServerEle;
             registerServerLink.Click();
             
             
             // Select server Type
			ListItem elem="/form[@processname='datastudio']/?/?/container[@name='fXv']/?/?/container[@name='fXG']/?/?/list[@name='fXF']/listitem[@text='"+serverTypeVar+"']";
			elem.Click();             
           
             
             Delay.Duration(1000);             
             //validation
             if ( myRepo.Datastudio.RegisterServerTitleBarFormEle.Text.Contains(regWindowTitleVar))
             {
            	Report.Success("Validation","Register Server process statrted successfully!!!");
            }
            else
            {
            	Report.Failure("Validation","Invalid window!!!");
            }
            
            var txt="/form[@title>'"+regWindowTitleVar+"']/container[@name='fSs']/container[@name='fXv']/?/?/container[@name='fYs']//container[@name='fYq']/?/?/text[@name='ec']";
            ((Ranorex.Text)txt).Click();
            ((Ranorex.Text)txt).TextValue=serverNameVar;

         

			myRepo.Datastudio.ContainerFYs.comboDriverEle.Click();
			myRepo.Datastudio.ComboBoxList.JTDSDriverEle.Click();
			
		    myRepo.Datastudio.ContainerFYs.ComboAuthTypeEle.Click();
			myRepo.Datastudio.ComboBoxList.AuthTypeEle.Click();		
			      
				
			 myRepo.Datastudio.ContainerFYs.LoginTxtEle.Click();
			  myRepo.Datastudio.ContainerFYs.LoginTxtEle.TextValue = loginTxtVar;
	
			  myRepo.Datastudio.ContainerFYs.LoginPwdEle.Click();
			  myRepo.Datastudio.ContainerFYs.LoginPwdEle.TextValue = loginPwdVar;
			  
			  myRepo.Datastudio.ContainerFYs.hostNameEle.Click();			  
			  myRepo.Datastudio.ContainerFYs.hostNameEle.TextValue = hostNameVar;
			 
			 myRepo.Datastudio.TestConnBtnEle.Click();
			 
			 if ( myRepo.Datastudio.JPanel.SuccessConnToDbEle.TextValue.Contains(SuccessConnToDbVar))
             {
            	Report.Success("Validation","Test connection successfully!!!");
            }
            else
            {
            	Report.Failure("Validation","Invalid connection!!!");
            }
            
            
            myRepo.Datastudio.CloseTestConnEle.Click();
            
            myRepo.Datastudio.SaveServerRegEle.Click();


        }
    }
}
