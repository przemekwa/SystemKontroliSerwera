﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PingIVR.Poczta {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://xml.apache.org/axis/wsdd/", ConfigurationName="Poczta.SendMailServiceImpl")]
    public interface SendMailServiceImpl {
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.DataContractFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="SendMailReturn")]
        bool SendMail(string from, string recipent, string subject, string message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SendMailServiceImplChannel : PingIVR.Poczta.SendMailServiceImpl, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SendMailServiceImplClient : System.ServiceModel.ClientBase<PingIVR.Poczta.SendMailServiceImpl>, PingIVR.Poczta.SendMailServiceImpl {
        
        public SendMailServiceImplClient() {
        }
        
        public SendMailServiceImplClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SendMailServiceImplClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SendMailServiceImplClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SendMailServiceImplClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool SendMail(string from, string recipent, string subject, string message) {
            return base.Channel.SendMail(from, recipent, subject, message);
        }
    }
}
