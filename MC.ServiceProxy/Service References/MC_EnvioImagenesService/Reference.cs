﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MC.ServiceProxy.MC_EnvioImagenesService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RequestBase", Namespace="http://www.mc.com.co/types/")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Request))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Request))]
    public partial class RequestBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid RequestIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsuarioField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid RequestId {
            get {
                return this.RequestIdField;
            }
            set {
                if ((this.RequestIdField.Equals(value) != true)) {
                    this.RequestIdField = value;
                    this.RaisePropertyChanged("RequestId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Usuario {
            get {
                return this.UsuarioField;
            }
            set {
                if ((object.ReferenceEquals(this.UsuarioField, value) != true)) {
                    this.UsuarioField = value;
                    this.RaisePropertyChanged("Usuario");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="setAlmacenaImagenesServidorCloud_Request", Namespace="http://www.MillensCorp.com/types/")]
    [System.SerializableAttribute()]
    public partial class setAlmacenaImagenesServidorCloud_Request : MC.ServiceProxy.MC_EnvioImagenesService.RequestBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MC.ServiceProxy.MC_EnvioImagenesService.ServiceImagen[] oImagenesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MC.ServiceProxy.MC_EnvioImagenesService.ServiceTransaccion oTransaccionField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MC.ServiceProxy.MC_EnvioImagenesService.ServiceImagen[] oImagenes {
            get {
                return this.oImagenesField;
            }
            set {
                if ((object.ReferenceEquals(this.oImagenesField, value) != true)) {
                    this.oImagenesField = value;
                    this.RaisePropertyChanged("oImagenes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MC.ServiceProxy.MC_EnvioImagenesService.ServiceTransaccion oTransaccion {
            get {
                return this.oTransaccionField;
            }
            set {
                if ((object.ReferenceEquals(this.oTransaccionField, value) != true)) {
                    this.oTransaccionField = value;
                    this.RaisePropertyChanged("oTransaccion");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="setAlmacenaImagenesServidor_Request", Namespace="http://www.MillensCorp.com/types/")]
    [System.SerializableAttribute()]
    public partial class setAlmacenaImagenesServidor_Request : MC.ServiceProxy.MC_EnvioImagenesService.RequestBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MC.ServiceProxy.MC_EnvioImagenesService.ServiceImagen[] oImagenesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MC.ServiceProxy.MC_EnvioImagenesService.ServiceTransaccion oTransaccionField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MC.ServiceProxy.MC_EnvioImagenesService.ServiceImagen[] oImagenes {
            get {
                return this.oImagenesField;
            }
            set {
                if ((object.ReferenceEquals(this.oImagenesField, value) != true)) {
                    this.oImagenesField = value;
                    this.RaisePropertyChanged("oImagenes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MC.ServiceProxy.MC_EnvioImagenesService.ServiceTransaccion oTransaccion {
            get {
                return this.oTransaccionField;
            }
            set {
                if ((object.ReferenceEquals(this.oTransaccionField, value) != true)) {
                    this.oTransaccionField = value;
                    this.RaisePropertyChanged("oTransaccion");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceTransaccion", Namespace="http://www.MillensCorp.com/types/")]
    [System.SerializableAttribute()]
    public partial class ServiceTransaccion : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CarrilEntradaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CarrilSalidaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IdAutorizadoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IdEstacionamientoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdTarjetaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IdTransaccionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ModuloEntradaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ModuloSalidaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlacaEntradaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PlacaSalidaField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CarrilEntrada {
            get {
                return this.CarrilEntradaField;
            }
            set {
                if ((this.CarrilEntradaField.Equals(value) != true)) {
                    this.CarrilEntradaField = value;
                    this.RaisePropertyChanged("CarrilEntrada");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CarrilSalida {
            get {
                return this.CarrilSalidaField;
            }
            set {
                if ((this.CarrilSalidaField.Equals(value) != true)) {
                    this.CarrilSalidaField = value;
                    this.RaisePropertyChanged("CarrilSalida");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long IdAutorizado {
            get {
                return this.IdAutorizadoField;
            }
            set {
                if ((this.IdAutorizadoField.Equals(value) != true)) {
                    this.IdAutorizadoField = value;
                    this.RaisePropertyChanged("IdAutorizado");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long IdEstacionamiento {
            get {
                return this.IdEstacionamientoField;
            }
            set {
                if ((this.IdEstacionamientoField.Equals(value) != true)) {
                    this.IdEstacionamientoField = value;
                    this.RaisePropertyChanged("IdEstacionamiento");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IdTarjeta {
            get {
                return this.IdTarjetaField;
            }
            set {
                if ((object.ReferenceEquals(this.IdTarjetaField, value) != true)) {
                    this.IdTarjetaField = value;
                    this.RaisePropertyChanged("IdTarjeta");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long IdTransaccion {
            get {
                return this.IdTransaccionField;
            }
            set {
                if ((this.IdTransaccionField.Equals(value) != true)) {
                    this.IdTransaccionField = value;
                    this.RaisePropertyChanged("IdTransaccion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ModuloEntrada {
            get {
                return this.ModuloEntradaField;
            }
            set {
                if ((object.ReferenceEquals(this.ModuloEntradaField, value) != true)) {
                    this.ModuloEntradaField = value;
                    this.RaisePropertyChanged("ModuloEntrada");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ModuloSalida {
            get {
                return this.ModuloSalidaField;
            }
            set {
                if ((object.ReferenceEquals(this.ModuloSalidaField, value) != true)) {
                    this.ModuloSalidaField = value;
                    this.RaisePropertyChanged("ModuloSalida");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlacaEntrada {
            get {
                return this.PlacaEntradaField;
            }
            set {
                if ((object.ReferenceEquals(this.PlacaEntradaField, value) != true)) {
                    this.PlacaEntradaField = value;
                    this.RaisePropertyChanged("PlacaEntrada");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PlacaSalida {
            get {
                return this.PlacaSalidaField;
            }
            set {
                if ((object.ReferenceEquals(this.PlacaSalidaField, value) != true)) {
                    this.PlacaSalidaField = value;
                    this.RaisePropertyChanged("PlacaSalida");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceImagen", Namespace="http://www.MillensCorp.com/types/")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(System.IO.MemoryStream))]
    public partial class ServiceImagen : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.IO.Stream ContenidoImagenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreImagenField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.IO.Stream ContenidoImagen {
            get {
                return this.ContenidoImagenField;
            }
            set {
                if ((object.ReferenceEquals(this.ContenidoImagenField, value) != true)) {
                    this.ContenidoImagenField = value;
                    this.RaisePropertyChanged("ContenidoImagen");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NombreImagen {
            get {
                return this.NombreImagenField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreImagenField, value) != true)) {
                    this.NombreImagenField = value;
                    this.RaisePropertyChanged("NombreImagen");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResponseBase", Namespace="http://www.mc.com.co/types/")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Response))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Response))]
    public partial class ResponseBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MC.ServiceProxy.MC_EnvioImagenesService.AcknowledgeType AcknowledgeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid CorrelationIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double IdInsertField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReservationIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int RowsAffectedField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MC.ServiceProxy.MC_EnvioImagenesService.AcknowledgeType Acknowledge {
            get {
                return this.AcknowledgeField;
            }
            set {
                if ((this.AcknowledgeField.Equals(value) != true)) {
                    this.AcknowledgeField = value;
                    this.RaisePropertyChanged("Acknowledge");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid CorrelationId {
            get {
                return this.CorrelationIdField;
            }
            set {
                if ((this.CorrelationIdField.Equals(value) != true)) {
                    this.CorrelationIdField = value;
                    this.RaisePropertyChanged("CorrelationId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double IdInsert {
            get {
                return this.IdInsertField;
            }
            set {
                if ((this.IdInsertField.Equals(value) != true)) {
                    this.IdInsertField = value;
                    this.RaisePropertyChanged("IdInsert");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReservationId {
            get {
                return this.ReservationIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ReservationIdField, value) != true)) {
                    this.ReservationIdField = value;
                    this.RaisePropertyChanged("ReservationId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int RowsAffected {
            get {
                return this.RowsAffectedField;
            }
            set {
                if ((this.RowsAffectedField.Equals(value) != true)) {
                    this.RowsAffectedField = value;
                    this.RaisePropertyChanged("RowsAffected");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="setAlmacenaImagenesServidor_Response", Namespace="http://www.MillensCorp.com/types/")]
    [System.SerializableAttribute()]
    public partial class setAlmacenaImagenesServidor_Response : MC.ServiceProxy.MC_EnvioImagenesService.ResponseBase {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="setAlmacenaImagenesServidorCloud_Response", Namespace="http://www.MillensCorp.com/types/")]
    [System.SerializableAttribute()]
    public partial class setAlmacenaImagenesServidorCloud_Response : MC.ServiceProxy.MC_EnvioImagenesService.ResponseBase {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AcknowledgeType", Namespace="http://www.mc.com.co/types/")]
    public enum AcknowledgeType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Failure = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Success = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MC_EnvioImagenesService.IEnvioImagenesService")]
    public interface IEnvioImagenesService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEnvioImagenesService/setAlmacenaImagenesServidor", ReplyAction="http://tempuri.org/IEnvioImagenesService/setAlmacenaImagenesServidorResponse")]
        MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Response setAlmacenaImagenesServidor(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEnvioImagenesService/setAlmacenaImagenesServidor", ReplyAction="http://tempuri.org/IEnvioImagenesService/setAlmacenaImagenesServidorResponse")]
        System.Threading.Tasks.Task<MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Response> setAlmacenaImagenesServidorAsync(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEnvioImagenesService/setAlmacenaImagenesServidorCloud", ReplyAction="http://tempuri.org/IEnvioImagenesService/setAlmacenaImagenesServidorCloudResponse" +
            "")]
        MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Response setAlmacenaImagenesServidorCloud(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IEnvioImagenesService/setAlmacenaImagenesServidorCloud", ReplyAction="http://tempuri.org/IEnvioImagenesService/setAlmacenaImagenesServidorCloudResponse" +
            "")]
        System.Threading.Tasks.Task<MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Response> setAlmacenaImagenesServidorCloudAsync(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Request request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IEnvioImagenesServiceChannel : MC.ServiceProxy.MC_EnvioImagenesService.IEnvioImagenesService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class EnvioImagenesServiceClient : System.ServiceModel.ClientBase<MC.ServiceProxy.MC_EnvioImagenesService.IEnvioImagenesService>, MC.ServiceProxy.MC_EnvioImagenesService.IEnvioImagenesService {
        
        public EnvioImagenesServiceClient() {
        }
        
        public EnvioImagenesServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public EnvioImagenesServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EnvioImagenesServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public EnvioImagenesServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Response setAlmacenaImagenesServidor(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Request request) {
            return base.Channel.setAlmacenaImagenesServidor(request);
        }
        
        public System.Threading.Tasks.Task<MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Response> setAlmacenaImagenesServidorAsync(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidor_Request request) {
            return base.Channel.setAlmacenaImagenesServidorAsync(request);
        }
        
        public MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Response setAlmacenaImagenesServidorCloud(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Request request) {
            return base.Channel.setAlmacenaImagenesServidorCloud(request);
        }
        
        public System.Threading.Tasks.Task<MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Response> setAlmacenaImagenesServidorCloudAsync(MC.ServiceProxy.MC_EnvioImagenesService.setAlmacenaImagenesServidorCloud_Request request) {
            return base.Channel.setAlmacenaImagenesServidorCloudAsync(request);
        }
    }
}
