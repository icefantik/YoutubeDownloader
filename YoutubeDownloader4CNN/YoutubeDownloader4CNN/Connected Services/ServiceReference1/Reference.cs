﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace YoutubeDownloader4CNN.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IGetAudioWCF", CallbackContract=typeof(YoutubeDownloader4CNN.ServiceReference1.IGetAudioWCFCallback))]
    public interface IGetAudioWCF {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/messageForClient")]
        void messageForClient(List<Functional.MyTableAudioData> audionDatas);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/messageForClient")]
        System.Threading.Tasks.Task messageForClientAsync(List<Functional.MyTableAudioData> audionDatas);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetAudioWCF/GetAudioData", ReplyAction="http://tempuri.org/IGetAudioWCF/GetAudioDataResponse")]
        List<Functional.MyTableAudioData> GetAudioData();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetAudioWCF/GetAudioData", ReplyAction="http://tempuri.org/IGetAudioWCF/GetAudioDataResponse")]
        System.Threading.Tasks.Task<List<Functional.MyTableAudioData>> GetAudioDataAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetAudioWCF/GetUserList", ReplyAction="http://tempuri.org/IGetAudioWCF/GetUserListResponse")]
        List<Functional.User> GetUserList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetAudioWCF/GetUserList", ReplyAction="http://tempuri.org/IGetAudioWCF/GetUserListResponse")]
        System.Threading.Tasks.Task<List<Functional.User>> GetUserListAsync();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/Join")]
        void Join(string UserName, string Ip);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/Join")]
        System.Threading.Tasks.Task JoinAsync(string UserName, string Ip);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/Disconnect")]
        void Disconnect(string UserName, string Ip, string clientStatus);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/Disconnect")]
        System.Threading.Tasks.Task DisconnectAsync(string UserName, string Ip, string clientStatus);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/SendCountDownload")]
        void SendCountDownload(int countDownload);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/SendCountDownload")]
        System.Threading.Tasks.Task SendCountDownloadAsync(int countDownload);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetAudioWCF/getIpPattern", ReplyAction="http://tempuri.org/IGetAudioWCF/getIpPatternResponse")]
        string getIpPattern();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGetAudioWCF/getIpPattern", ReplyAction="http://tempuri.org/IGetAudioWCF/getIpPatternResponse")]
        System.Threading.Tasks.Task<string> getIpPatternAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGetAudioWCFCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/MessageResult")]
        void MessageResult(List<Functional.MyTableAudioData> audionDatas);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IGetAudioWCF/ListUpdated")]
        void ListUpdated(List<Functional.User> userList);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGetAudioWCFChannel : YoutubeDownloader4CNN.ServiceReference1.IGetAudioWCF, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetAudioWCFClient : System.ServiceModel.DuplexClientBase<YoutubeDownloader4CNN.ServiceReference1.IGetAudioWCF>, YoutubeDownloader4CNN.ServiceReference1.IGetAudioWCF {
        
        public GetAudioWCFClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public GetAudioWCFClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public GetAudioWCFClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GetAudioWCFClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public GetAudioWCFClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void messageForClient(List<Functional.MyTableAudioData> audionDatas) {
            base.Channel.messageForClient(audionDatas);
        }
        
        public System.Threading.Tasks.Task messageForClientAsync(List<Functional.MyTableAudioData> audionDatas) {
            return base.Channel.messageForClientAsync(audionDatas);
        }
        
        public List<Functional.MyTableAudioData> GetAudioData() {
            return base.Channel.GetAudioData();
        }
        
        public System.Threading.Tasks.Task<List<Functional.MyTableAudioData>> GetAudioDataAsync() {
            return base.Channel.GetAudioDataAsync();
        }
        
        public List<Functional.User> GetUserList() {
            return base.Channel.GetUserList();
        }
        
        public System.Threading.Tasks.Task<List<Functional.User>> GetUserListAsync() {
            return base.Channel.GetUserListAsync();
        }
        
        public void Join(string UserName, string Ip) {
            base.Channel.Join(UserName, Ip);
        }
        
        public System.Threading.Tasks.Task JoinAsync(string UserName, string Ip) {
            return base.Channel.JoinAsync(UserName, Ip);
        }
        
        public void Disconnect(string UserName, string Ip, string clientStatus) {
            base.Channel.Disconnect(UserName, Ip, clientStatus);
        }
        
        public System.Threading.Tasks.Task DisconnectAsync(string UserName, string Ip, string clientStatus) {
            return base.Channel.DisconnectAsync(UserName, Ip, clientStatus);
        }
        
        public void SendCountDownload(int countDownload) {
            base.Channel.SendCountDownload(countDownload);
        }
        
        public System.Threading.Tasks.Task SendCountDownloadAsync(int countDownload) {
            return base.Channel.SendCountDownloadAsync(countDownload);
        }
        
        public string getIpPattern() {
            return base.Channel.getIpPattern();
        }
        
        public System.Threading.Tasks.Task<string> getIpPatternAsync() {
            return base.Channel.getIpPatternAsync();
        }
    }
}
