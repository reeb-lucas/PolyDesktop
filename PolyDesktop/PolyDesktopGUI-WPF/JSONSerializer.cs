using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace NetworkCommsDotNet.DPSBase
{
    [DataSerializerProcessor(4)]
    public class JSONSerializer : DataSerializer
    {

        private JSONSerializer() { }

        #region ISerialize Members

        /// <inheritdoc />
        protected override void SerialiseDataObjectInt(Stream outputStream, object objectToSerialise, Dictionary<string, string> options)
        {
            if (outputStream == null)
                throw new ArgumentNullException("outputStream");

            if (objectToSerialise == null)
                throw new ArgumentNullException("objectToSerialize");

            outputStream.Seek(0, 0);
            var data = Encoding.Unicode.GetBytes(JsonConvert.SerializeObject(objectToSerialise));
            outputStream.Write(data, 0, data.Length);
            outputStream.Seek(0, 0);
        }

        /// <inheritdoc />
        protected override object DeserialiseDataObjectInt(Stream inputStream, Type resultType, Dictionary<string, string> options)
        {
            var data = new byte[inputStream.Length];
            inputStream.Read(data, 0, data.Length);
            return JsonConvert.DeserializeObject(new String(Encoding.Unicode.GetChars(data)), resultType);
        }

        #endregion
    }
}