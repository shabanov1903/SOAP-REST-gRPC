﻿using EuropAssistance.Net.Http.Options;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Http.Utils.Logging
{
    public class HttpClientMessageHandlerBuilderFilter : IHttpMessageHandlerBuilderFilter
    {
        public HttpClientMessageHandlerBuilderFilter(ILoggerFactory loggerFactory,
           IOptions<HttpClientLoggingOptions> options)
        {
            LoggerFactory = loggerFactory;
            Options = options;
        }

        public ILoggerFactory LoggerFactory { get; }

        public IOptions<HttpClientLoggingOptions> Options { get; }

        public Action<HttpMessageHandlerBuilder> Configure(Action<HttpMessageHandlerBuilder> next)
        {
            return (HttpMessageHandlerBuilder builder) =>
            {
                // call next handler 
                next(builder);

                // create logger 
                string loggerName = !string.IsNullOrEmpty(builder.Name) ? builder.Name : "Default";
                ILogger logger = LoggerFactory.CreateLogger($"Net.Http.Utils.HttpClient.{loggerName}");

                // create and add handler 
                LoggingHttpMessageHandler loggingHttpMessageHandler = new LoggingHttpMessageHandler(logger, Options);
                builder.AdditionalHandlers.Insert(0, loggingHttpMessageHandler);
            };
        }
    }
}
