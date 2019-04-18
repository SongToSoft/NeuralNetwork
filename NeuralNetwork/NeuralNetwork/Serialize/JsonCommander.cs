using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;

namespace NeuralNetwork
{
    class JsonCommander
    {
        public void Serialize(string name, List<double>[] inputSynapses, List<double>[] outputSynapses)
        {
            JsonData jsonInputData = new JsonData(inputSynapses);
            JsonData jsonOutputData = new JsonData(outputSynapses);
            JsonData[] jsonNeuronsDatas = new JsonData[] { jsonInputData, jsonOutputData };
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(JsonData[]));
            using (FileStream fs = new FileStream(name + ".json", FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, jsonNeuronsDatas);
            }
        }

        public JsonData[] DeSerialize(string name)
        {
            using (FileStream fs = new FileStream(name + ".json", FileMode.Open))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(JsonData[]));
                JsonData[] jsonNeuronsDatas = (JsonData[])jsonFormatter.ReadObject(fs);
                return jsonNeuronsDatas;
            }
        }
    }
}
