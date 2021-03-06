﻿using System;
using System.Net;
using Lykke.Common.ApiLibrary.Contract;

namespace Lykke.Common.ApiLibrary.Exceptions
{
    /// <summary>
    ///     Class for any api error.
    ///     Throw this exception to return standardized error response.
    /// </summary>
    public class LykkeApiErrorException : Exception
    {
        /// <summary>
        ///     Http status code to return in response.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        ///     Error code identifying what kind of error happened.
        /// </summary>
        public ILykkeApiErrorCode LykkeApiErrorCode { get; }

        /// <summary>
        ///     Constructs new <see cref="LykkeApiErrorException" />.
        /// </summary>
        /// <param name="httpStatusCode">Http status code.</param>
        /// <param name="lykkeApiErrorCode">
        ///     Error code identifying what kind of error happened.
        /// </param>
        /// <param name="message">Message to include in response.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="lykkeApiErrorCode" /> is null.
        /// </exception>
        private LykkeApiErrorException(HttpStatusCode httpStatusCode, ILykkeApiErrorCode lykkeApiErrorCode,
            string message) : base(GetMessage(lykkeApiErrorCode, message))
        {
            LykkeApiErrorCode = lykkeApiErrorCode ?? throw new ArgumentNullException(nameof(lykkeApiErrorCode));

            StatusCode = httpStatusCode;
        }

        /// <summary>
        ///     Create a 400 bad request api error.
        /// </summary>
        /// <param name="lykkeApiErrorCode">
        ///     Error code identifying what kind of error happened.
        /// </param>
        /// <param name="message">Message to include in response.</param>
        /// <returns>
        ///     New <see cref="LykkeApiErrorException" /> with <see cref="StatusCode" /> set to
        ///     <see cref="HttpStatusCode.BadRequest" />.
        /// </returns>
        public static LykkeApiErrorException BadRequest(ILykkeApiErrorCode lykkeApiErrorCode,
            string message = "")
        {
            return new LykkeApiErrorException(HttpStatusCode.BadRequest, lykkeApiErrorCode, message);
        }

        /// <summary>
        ///     Create a 404 not found api error.
        /// </summary>
        /// <param name="lykkeApiErrorCode">
        ///     Error code identifying what kind of error happened.
        /// </param>
        /// <param name="message">Message to include in response.</param>
        /// <returns>
        ///     New <see cref="LykkeApiErrorException" /> with <see cref="StatusCode" /> set to
        ///     <see cref="HttpStatusCode.NotFound" />.
        /// </returns>
        public static LykkeApiErrorException NotFound(ILykkeApiErrorCode lykkeApiErrorCode,
            string message = "")
        {
            return new LykkeApiErrorException(HttpStatusCode.NotFound, lykkeApiErrorCode, message);
        }

        /// <summary>
        ///     Create a 403 forbidden api error.
        /// </summary>
        /// <param name="lykkeApiErrorCode">
        ///     Error code identifying what kind of error happened.
        /// </param>
        /// <param name="message">Message to include in response.</param>
        /// <returns>
        ///     New <see cref="LykkeApiErrorException" /> with <see cref="StatusCode" /> set to
        ///     <see cref="HttpStatusCode.Forbidden" />.
        /// </returns>
        public static LykkeApiErrorException Forbidden(ILykkeApiErrorCode lykkeApiErrorCode,
            string message = "")
        {
            return new LykkeApiErrorException(HttpStatusCode.Forbidden, lykkeApiErrorCode, message);
        }

        private static string GetMessage(ILykkeApiErrorCode lykkeApiErrorCode, string message)
        {
            if (lykkeApiErrorCode == null)
                throw new ArgumentNullException(nameof(lykkeApiErrorCode));

            if (string.IsNullOrEmpty(message)) return lykkeApiErrorCode.DefaultMessage ?? string.Empty;

            return message;
        }
    }
}