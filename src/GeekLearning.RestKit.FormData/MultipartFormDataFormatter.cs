namespace GeekLearning.RestKit.FormData
{
    using GeekLearning.RestKit.Core;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Net.Http;

    public class MultipartFormDataFormatter : IMediaFormatter
    {
        public MultipartFormDataFormatter()
        {
        }

        public HttpContent Format(object body, IDictionary<string, IFormData> formData)
        {
            var containerContent = new MultipartFormDataContent();
            foreach (var item in formData)
            {
                var file = item.Value as IFile;

                if (file != null)
                {
                    var contentType = !string.IsNullOrEmpty(file.MimeType) ? file.MimeType : MimeTypes.MimeTypeMap.GetMimeType(System.IO.Path.GetExtension(file.FileName));
                    var content = file.CreateContent();
                    content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
                    content.Headers.ContentDisposition.Name = item.Key;
                    content.Headers.ContentDisposition.FileName = file.FileName;
                    content.Headers.ContentDisposition.FileNameStar = file.FileName;
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                    containerContent.Add(content);
                }
                else
                {
                    var content = new StringContent(Convert.ToString(item.Value.Data, System.Globalization.CultureInfo.InvariantCulture));
                    content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data");
                    content.Headers.ContentDisposition.Name = item.Key;
                    containerContent.Add(content);
                }
            }

            return containerContent;
        }

        public bool Supports(ParsedMediaType mediaType)
        {
            return string.Equals(mediaType.Type, "multipart", StringComparison.OrdinalIgnoreCase)
               && string.Equals(mediaType.SubType, "form-data", StringComparison.OrdinalIgnoreCase);
        }

        public Task<TTarget> TransformAsync<TTarget>(HttpContent content)
        {
            throw new NotSupportedException();
        }
    }
}
