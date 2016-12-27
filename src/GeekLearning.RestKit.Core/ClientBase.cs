namespace GeekLearning.RestKit.Core
{
    using Microsoft.Extensions.Options;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class ClientBase<TOptions>
       where TOptions : class, IProvideRequestFilters, new()
    {
        private IMediaFormatterProvider mediaFormatterProvider;
        private IServiceProvider serviceProvider;
        private Lazy<IRequestFilter[]> requestFilters;
        private IHttpClientFactory httpClientFactory;

        public ClientBase(IOptions<TOptions> options,
            IHttpClientFactory httpClientFactory,
            IMediaFormatterProvider mediaFormatterProvider,
            IServiceProvider serviceProvider)
        {
            this.Options = options.Value;
            this.mediaFormatterProvider = mediaFormatterProvider;
            this.serviceProvider = serviceProvider;
            this.httpClientFactory = httpClientFactory;
            this.requestFilters = new Lazy<IRequestFilter[]>(() =>
                this.Options.RequestFilters.Select(x => ActivatorUtilities.CreateInstance(this.serviceProvider, x.Type, x.Arguments)).Cast<IRequestFilter>().ToArray()
            );
        }

        protected HttpClient GetClient()
        {
            return this.httpClientFactory.CreateClient();
        }

        protected TOptions Options { get; private set; }

        protected Task<TTarget> TransformResponseAsync<TTarget>(HttpResponseMessage message)
        {
            IMediaFormatter mediaFormatter = this.mediaFormatterProvider.GetMediaFormatter(message.Content.Headers.ContentType);
            if (mediaFormatter == null)
            {
                throw new UnsupportedMediaTypeApiException(message);
            }
            return mediaFormatter.TransformAsync<TTarget>(message.Content);
        }

        protected HttpRequestMessage ApplyFilters(HttpRequestMessage requestMessage, params string[] securityDefinitions)
        {
            HttpRequestMessage finalMessage = requestMessage;
            foreach (var filter in this.requestFilters.Value)
            {
                finalMessage = filter.Apply(requestMessage, securityDefinitions) ?? finalMessage;
            }

            return finalMessage;
        }

        protected HttpContent TransformRequestBody(object body, IDictionary<string, IFormData> formData, string mediaType)
        {
            IMediaFormatter mediaFormatter = this.mediaFormatterProvider.GetMediaFormatter(mediaType);
            if (mediaFormatter == null)
            {
                throw new UnsupportedMediaTypeApiException(mediaType);
            }
            return mediaFormatter.Format(body, formData);
        }

        protected ApiException MapToException(HttpResponseMessage message)
        {
            switch (message.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    return new BadRequestApiException(message);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedApiException(message);
                case HttpStatusCode.Forbidden:
                    return new ForbiddenApiException(message);
                case HttpStatusCode.NotFound:
                    return new NotFoundApiException(message);
                case HttpStatusCode.Conflict:
                    return new ConflictApiException(message);
                case HttpStatusCode.InternalServerError:
                    return new InternalServerErrorApiException(message);
                case HttpStatusCode.ServiceUnavailable:
                    return new ServiceUnavailableApiException(message);
                default:
                    return new UnhandledApiException(message);
            }
        }

        protected async Task<ApiException> MapToException<TTarget>(HttpResponseMessage message)
        {
            if (message.Content == null)
            {
                return this.MapToException(message);
            }
            else
            {
                var result = await this.TransformResponseAsync<TTarget>(message);

                switch (message.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        return new BadRequestApiException<TTarget>(message, result);
                    case HttpStatusCode.Unauthorized:
                        return new UnauthorizedApiException<TTarget>(message, result);
                    case HttpStatusCode.Forbidden:
                        return new ForbiddenApiException<TTarget>(message, result);
                    case HttpStatusCode.NotFound:
                        return new NotFoundApiException<TTarget>(message, result);
                    case HttpStatusCode.Conflict:
                        return new ConflictApiException<TTarget>(message, result);
                    case HttpStatusCode.InternalServerError:
                        return new ConflictApiException<TTarget>(message, result);
                    case HttpStatusCode.ServiceUnavailable:
                        return new ServiceUnavailableApiException<TTarget>(message, result);
                    default:
                        return new UnhandledApiException<TTarget>(message, result);
                }
            }
        }

        protected bool MatchStatus(HttpResponseMessage message, int status)
        {
            return status == (int)message.StatusCode;
        }

        protected bool MatchStatus(HttpResponseMessage message, string status)
        {
            return message.StatusCode.ToString().Equals(status, StringComparison.OrdinalIgnoreCase)
                || message.ReasonPhrase == status;
        }

        protected IFormData GetFormData(IFormData data)
        {
            return data;
        }
        protected IFormData GetFormData(object data)
        {
            return new FormData(data);
        }

        /// <summary>
        /// Append the given query keys and values to the uri.
        /// </summary>
        /// <param name="uri">The base uri.</param>
        /// <param name="queryString">A collection of name value query pairs to append.</param>
        /// <returns>The combined result.</returns>
        protected static string AddQueryString(
            string uri,
            IEnumerable<KeyValuePair<string, object>> queryString)
        {
            var anchorIndex = uri.IndexOf('#');
            var uriToBeAppended = uri;
            var anchorText = "";
            // If there is an anchor, then the query string must be inserted before its first occurance.
            if (anchorIndex != -1)
            {
                anchorText = uri.Substring(anchorIndex);
                uriToBeAppended = uri.Substring(0, anchorIndex);
            }

            var queryIndex = uriToBeAppended.IndexOf('?');
            var hasQuery = queryIndex != -1;

            var sb = new StringBuilder();
            sb.Append(uriToBeAppended);
            foreach (var parameter in queryString)
            {
                if (parameter.Value == null)
                    continue;
                sb.Append(hasQuery ? '&' : '?');
                sb.Append(WebUtility.UrlEncode(parameter.Key));
                sb.Append('=');
                sb.Append(WebUtility.UrlEncode(parameter.Value.ToString()));
                hasQuery = true;
            }

            sb.Append(anchorText);
            return sb.ToString();
        }
    }
}
