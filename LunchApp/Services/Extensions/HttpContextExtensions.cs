﻿using Microsoft.AspNetCore.Http;
using System.Net;

namespace Services.Extensions
{
    public static class HttpContextExtensions
    {
        private const string NullIpAddress = "::1";

        public static bool IsLocal(this HttpRequest req)
        {
            var connection = req.HttpContext.Connection;
            if (connection.RemoteIpAddress.IsSet())
            {
                return connection.LocalIpAddress.IsSet()
                    ? connection.RemoteIpAddress.Equals(connection.LocalIpAddress)
                    : IPAddress.IsLoopback(connection.RemoteIpAddress);
            }
            return true;
        }

        private static bool IsSet(this IPAddress address)
        {
            return address != null && address.ToString() != NullIpAddress;
        }
    }
}
