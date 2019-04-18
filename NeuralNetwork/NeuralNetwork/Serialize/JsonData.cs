using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace NeuralNetwork
{
    [DataContract]
    class JsonData
    {
        [DataMember]
        public List<double>[] m_values;

        public JsonData(List<double>[] values)
        {
            m_values = values;
        }
    }
}
