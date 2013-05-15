using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.ServiceModel.Description;
using System.Runtime.Serialization;
using System.Xml;

namespace AppCommon
{
    // pulled from site: http://blogs.msdn.com/sowmy/archive/2006/03/26/561188.aspx
    //
    //  And then: https://connect.microsoft.com/wcf/feedback/ViewFeedback.aspx?FeedbackID=307497&wa=wsignin1.0

    public class ReferencePreservingDataContractSerializerOperationBehavior
      : DataContractSerializerOperationBehavior
    {
        public ReferencePreservingDataContractSerializerOperationBehavior(
          OperationDescription operationDescription)
            : base(operationDescription) { }

        public override XmlObjectSerializer CreateSerializer(
          Type type, string name, string ns, IList<Type> knownTypes)
        {
            return CreateDataContractSerializer(type, name, ns, knownTypes);
        }

        private static XmlObjectSerializer CreateDataContractSerializer(
          Type type, string name, string ns, IList<Type> knownTypes)
        {
            return CreateDataContractSerializer(type, name, ns, knownTypes);
        }

        public override XmlObjectSerializer CreateSerializer(
          Type type, XmlDictionaryString name, XmlDictionaryString ns,
          IList<Type> knownTypes)
        {
            return new DataContractSerializer(type, name, ns, knownTypes,
                2147483647 /* 0x7FFF maxItemsInObjectGraph*/,
                false/*ignoreExtensionDataObject*/,
                true/*preserveObjectReferences*/,
                null/*dataContractSurrogate*/);
        }

    }

}
