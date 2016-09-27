﻿namespace GeekLearning.RestKit.Core
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

        public ClientBase(IOptions<TOptions> options,
            IMediaFormatterProvider mediaFormatterProvider,
            IServiceProvider serviceProvider)
        {
            this.Options = options.Value;
            this.mediaFormatterProvider = mediaFormatterProvider;
            this.serviceProvider = serviceProvider;
            this.requestFilters = new Lazy<IRequestFilter[]>(() =>
                this.Options.RequestFilters.Select(x=> ActivatorUtilities.CreateInstance(this.serviceProvider, x.Type, x.Arguments)).Cast<IRequestFilter>().ToArray()
            );
        }

        protected TOptions Options { get; private set; }

        protected Task<TTarget> TransformResponseAsync<TTarget>(HttpResponseMessage message)
        {
            IMediaFormatter mediaFormatter = this.mediaFormatterProvider.GetMediaFormatter(message.Content.Headers.ContentType);
            if (mediaFormatter == null)
            {
                throw new UnsupportedMediaTypeApiException(message.Content.Headers.ContentType);
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

        protected HttpContent TransformRequestBody(object data, string mediaType)
        {
            IMediaFormatter mediaFormatter = this.mediaFormatterProvider.GetMediaFormatter(mediaType);
            if (mediaFormatter == null)
            {
                throw new UnsupportedMediaTypeApiException(mediaType);
            }
            return mediaFormatter.Format(data);
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
