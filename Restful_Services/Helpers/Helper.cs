using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace Restful_Services
{
    public class MultipartMixedFormatter : JsonMediaTypeFormatter
    {
        public MultipartMixedFormatter()
        {
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/mixed"));
            this.SerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Objects
            };
            this.MediaTypeMappings.Add(new RequestHeaderMapping("Accept", "multipart/mixed", StringComparison.InvariantCultureIgnoreCase, true, "application/json"));
        }

        public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
        {
            using (JsonTextReader jsonTextReader = new JsonTextReader(new StreamReader(readStream, effectiveEncoding)) { CloseInput = false, MaxDepth = 256 })
            {
                JsonSerializer jsonSerializer = JsonSerializer.Create(this.SerializerSettings);
                if (formatterLogger != null)
                {
                    jsonSerializer.Error += (sender, e) =>
                    {
                        Exception exception = e.ErrorContext.Error;
                        //formatterLogger.LogError(e.ErrorContext.Path, exception);
                        e.ErrorContext.Handled = true;
                    };
                }
                return jsonSerializer.Deserialize(jsonTextReader, type);
            }
        }
    }
}