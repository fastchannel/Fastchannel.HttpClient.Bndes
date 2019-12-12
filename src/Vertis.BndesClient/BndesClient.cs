using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Vertis.BndesClient.Models.Operations.Request;
using Vertis.BndesClient.Models.Operations.Response;
using Vertis.BndesClient.Models.Types;
using OrderPayment = Vertis.BndesClient.Models.Operations.Request.OrderPayment;
// ReSharper disable UnusedMember.Global

namespace Vertis.BndesClient
{
    public class BndesClient : IDisposable
    {
        #region Constructor

        public BndesClient(BndesSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings), "Settings is null or invalid.");
            Client = new RestClient(settings.BaseEndpoint, settings.ClientCertificateThumbprint, settings.DefaultTimeoutInSeconds);

        }

        #endregion

        #region Fields

        private readonly BndesSettings _settings;

        public RestClient Client { get; }

        #endregion

        #region Methods

        #region HttpClient Context Methods

        public void Dispose()
        {
            Client?.Dispose();
        }

        
        #endregion

        #region Session Methods
        public async Task<SessionResponse> CreateSessionAsync(CreateSession request)
        {
            var httpRequest = new Request(HttpMethod.Post, _settings.SessionEndpoint, request.TimeoutInSeconds);
            httpRequest.AddJsonBody(request.Data);

            using (var response = await Client.Execute(httpRequest))
            {
                var sessionResponse =
                    new SessionResponse {HttpStatusCode = (int) response.StatusCode};
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Created:
                        sessionResponse.Id = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        return sessionResponse;

                    case HttpStatusCode.Unauthorized:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                        if (content == null)
                            return sessionResponse;

                        sessionResponse = BaseResponse.FromJsonString<SessionResponse>(sessionResponse.HttpStatusCode, content);
                        return sessionResponse;

                    default:
                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            sessionResponse.Messages.Add(new ResponseMessage
                            {
                                Code = sessionResponse.HttpStatusCode,
                                Message = msg
                            });
                        return sessionResponse;
                }
            }
        }

        public async Task<GenericResponse> RemoveSessionAsync(string sessionId, int? requestTimeoutInSeconds = null)
        {
            var httpRequest = new Request(HttpMethod.Delete, _settings.SessionEndpoint, requestTimeoutInSeconds);
            httpRequest.AddCookie("Cookie", $"CTRL={sessionId}");
            using (var response = await Client.Execute(httpRequest))
            {
                var genericResponse = new GenericResponse {HttpStatusCode = (int) response.StatusCode};
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return new GenericResponse
                        {
                            HttpStatusCode = Convert.ToInt32(response.StatusCode)
                        };
                    case HttpStatusCode.Unauthorized:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (content == null)
                            return genericResponse;

                        genericResponse = BaseResponse.FromJsonString<GenericResponse>(genericResponse.HttpStatusCode, content);
                        return genericResponse;
                    default:

                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            genericResponse.Messages.Add(new ResponseMessage
                            {
                                Code = genericResponse.HttpStatusCode,
                                Message = msg
                            });
                        return genericResponse;
                }
            }

        }
        #endregion

        #region Financing Methods
        public async Task<FinancingSimulationResponse> SimulateTaxAsync(string sessionId, int? requestTimeoutInSeconds = null)
        {
            var httpRequest = new Request(HttpMethod.Get, _settings.TaxSimulationEndpoint, requestTimeoutInSeconds);
            httpRequest.AddCookie("Cookie", $"CTRL={sessionId}");
            using (var response = await Client.Execute(httpRequest))
            {
                var financingSimulationResponse =
                    new FinancingSimulationResponse { HttpStatusCode = (int)response.StatusCode };
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        financingSimulationResponse.Tax =
                            await response.Content.ReadAsDoubleAsync().ConfigureAwait(false);
                        return financingSimulationResponse;
                    case HttpStatusCode.BadRequest:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (content == null)
                            return financingSimulationResponse;

                        financingSimulationResponse = BaseResponse.FromJsonString<FinancingSimulationResponse>(financingSimulationResponse.HttpStatusCode, content);
                        return financingSimulationResponse;
                    default:
                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            financingSimulationResponse.Messages.Add(new ResponseMessage
                            {
                                Code = financingSimulationResponse.HttpStatusCode,
                                Message = msg
                            });
                        return financingSimulationResponse;
                }
            }
        }
        public async Task<FinancingSimulationResponse> SimulateAsync(string sessionId, int value, int? requestTimeoutInSeconds = null)
        {
            var financingSimulationResponse = await SimulateTaxAsync(sessionId).ConfigureAwait(false);
            if (!financingSimulationResponse.IsHttpStatusCodeOk)
                return financingSimulationResponse;

            var taxRate = financingSimulationResponse.Tax;
            var httpRequest = new Request(HttpMethod.Get, _settings.FinancingSimulationEndpoint, requestTimeoutInSeconds);
            httpRequest.AddCookie("Cookie", $"CTRL={sessionId}");
            httpRequest.AddQueryString("valor", value.ToFormattedString());
            using (var response = await Client.Execute(httpRequest))
            {
                financingSimulationResponse =
                    new FinancingSimulationResponse { HttpStatusCode = (int)response.StatusCode };
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var s = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        financingSimulationResponse =
                            BaseResponse.FromJsonString<FinancingSimulationResponse>(
                                financingSimulationResponse.HttpStatusCode, s);
                        financingSimulationResponse.Tax = taxRate;

                        return financingSimulationResponse;
                    case HttpStatusCode.BadRequest:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (content == null)
                            return financingSimulationResponse;

                        financingSimulationResponse =
                            BaseResponse.FromJsonString<FinancingSimulationResponse>(
                                financingSimulationResponse.HttpStatusCode, content);

                        return financingSimulationResponse;
                    default:
                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            financingSimulationResponse.Messages.Add(new ResponseMessage
                            {
                                Code = financingSimulationResponse.HttpStatusCode,
                                Message = msg
                            });
                        return financingSimulationResponse;
                }
            }

        }
        #endregion

        #region Order Methods
        public async Task<CreateOrderResponse> CreateOrderAsync(string sessionId, CreateOrder request)
        {
            var httpRequest = new Request(HttpMethod.Post, _settings.OrderEndpoint, request.TimeoutInSeconds);
            httpRequest.AddCookie("Cookie", $"CTRL={sessionId}");
            httpRequest.AddJsonBody(request.Data);
            using (var response = await Client.Execute(httpRequest))
            {
                var createOrderResponse =
                    new CreateOrderResponse { HttpStatusCode = (int)response.StatusCode };
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Created:
                        var res = await response.Content.ReadAsIntAsync().ConfigureAwait(false);
                        createOrderResponse.OrderId = res;
                        return createOrderResponse;
                    case HttpStatusCode.BadRequest:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (content == null)
                            return createOrderResponse;

                        createOrderResponse = BaseResponse.FromJsonString<CreateOrderResponse>(createOrderResponse.HttpStatusCode, content);
                        return createOrderResponse;
                    default:
                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            createOrderResponse.Messages.Add(new ResponseMessage
                            {
                                Code = createOrderResponse.HttpStatusCode,
                                Message = msg
                            });
                        return createOrderResponse;
                }
            }
        }

        public async Task<FinalizeOrderResponse> FinalizeOrderAsync(string sessionId, FinalizeOrder request)
        {
            var httpRequest = new Request(HttpMethod.Put, _settings.OrderEndpoint + "/{0}", request.TimeoutInSeconds);
            httpRequest.AddCookie("Cookie", $"CTRL={sessionId}");
            httpRequest.AddUrlSegment("id", request.Data.OrderId);
            httpRequest.AddJsonBody(request.Data);
            using (var response = await Client.Execute(httpRequest))
            {
                var finalizeOrderResponse =
                    new FinalizeOrderResponse { HttpStatusCode = (int)response.StatusCode };
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var res = await response.Content.ReadAsIntAsync().ConfigureAwait(false);
                        finalizeOrderResponse.OrderId = res;
                        return finalizeOrderResponse;
                    case HttpStatusCode.BadRequest:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (content == null)
                            return finalizeOrderResponse;

                        finalizeOrderResponse = BaseResponse.FromJsonString<FinalizeOrderResponse>(finalizeOrderResponse.HttpStatusCode, content);
                        return finalizeOrderResponse;
                    default:
                        
                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            finalizeOrderResponse.Messages.Add(new ResponseMessage
                            {
                                Code = finalizeOrderResponse.HttpStatusCode,
                                Message = msg
                            });
                        return finalizeOrderResponse;
                }
            }
            
        }

        public async Task<OrderPaymentResponse> OrderPaymentExecuteAsync(string sessionId, OrderPayment request)
        {
            var httpRequest = new Request(HttpMethod.Post, _settings.OrderEndpoint + "/{0}/pagamento", request.TimeoutInSeconds);
            httpRequest.AddCookie("Cookie", $"CTRL={sessionId}");
            httpRequest.AddUrlSegment("id", request.Data.OrderId);
            httpRequest.AddJsonBody(request.Data);
            using (var response = await Client.Execute(httpRequest))
            {
                var orderPaymentResponse =
                    new OrderPaymentResponse { HttpStatusCode = (int)response.StatusCode };
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var res = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        orderPaymentResponse = BaseResponse.FromJsonString<OrderPaymentResponse>(orderPaymentResponse.HttpStatusCode, res);

                        return orderPaymentResponse;
                    case HttpStatusCode.BadRequest:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (content == null)
                            return orderPaymentResponse;

                        orderPaymentResponse = BaseResponse.FromJsonString<OrderPaymentResponse>(orderPaymentResponse.HttpStatusCode, content);
                        return orderPaymentResponse;
                    default:
                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            orderPaymentResponse.Messages.Add(new ResponseMessage
                            {
                                Code = orderPaymentResponse.HttpStatusCode,
                                Message = msg
                            });
                        return orderPaymentResponse;
                }
            }

        }

        public async Task<CaptureOrderResponse> CaptureOrderAsync(string sessionId, Models.Operations.Request.CaptureOrder request)
        {
            var httpRequest = new Request(HttpMethod.Post, _settings.OrderEndpoint + "/{0}/captura", request.TimeoutInSeconds);
            httpRequest.AddCookie("Cookie", $"CTRL={sessionId}");
            httpRequest.AddUrlSegment("id", request.Data.OrderId);
            httpRequest.AddJsonBody(request.Data);
            using (var response = await Client.Execute(httpRequest))
            {
                var captureOrderResponse =
                    new CaptureOrderResponse { HttpStatusCode = (int)response.StatusCode };

                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var res = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        captureOrderResponse = BaseResponse.FromJsonString<CaptureOrderResponse>(captureOrderResponse.HttpStatusCode, res);
                        return captureOrderResponse;
                        
                    case HttpStatusCode.BadRequest:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (content == null)
                            return captureOrderResponse;

                        captureOrderResponse = BaseResponse.FromJsonString<CaptureOrderResponse>(captureOrderResponse.HttpStatusCode, content);
                        return captureOrderResponse;
                    default:
                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            captureOrderResponse.Messages.Add(new ResponseMessage
                            {
                                Code = captureOrderResponse.HttpStatusCode,
                                Message = msg
                            });
                        return captureOrderResponse;
                }
            }
        }

        public async Task<OrderCancellingResponse> CancelOrderAsync(string sessionId, int orderId, Models.Operations.Request.OrderCancelling request)
        {
            var httpRequest = new Request(HttpMethod.Put, _settings.OrderEndpoint + "/{0}/cancelamento", request.TimeoutInSeconds);
            httpRequest.AddCookie("Cookie", $"CTRL={sessionId}");
            httpRequest.AddUrlSegment("id", request.Data.OrderId);
            httpRequest.AddJsonBody(request.Data);
            using (var response = await Client.Execute(httpRequest))
            {
                var orderCancellingResponse =
                    new OrderCancellingResponse { HttpStatusCode = (int)response.StatusCode };

                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var res = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        orderCancellingResponse = BaseResponse.FromJsonString<OrderCancellingResponse>(orderCancellingResponse.HttpStatusCode, res);
                        return orderCancellingResponse;
                    case HttpStatusCode.BadRequest:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (content == null)
                            return orderCancellingResponse;

                        orderCancellingResponse = BaseResponse.FromJsonString<OrderCancellingResponse>(orderCancellingResponse.HttpStatusCode, content);
                        return orderCancellingResponse;
                    default:
                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            orderCancellingResponse.Messages.Add(new ResponseMessage
                            {
                                Code = orderCancellingResponse.HttpStatusCode,
                                Message = msg
                            });
                        return orderCancellingResponse;
                }
            }
            
        }
        #endregion

        #region Catalog Methods
        #endregion

        public async Task<CatalogResponse> GetCatalogAsync(string sessionId, int? requestTimeoutInSeconds = null)
        {
            var httpRequest = new Request(HttpMethod.Get, _settings.CatalogEndpoint, requestTimeoutInSeconds);
            httpRequest.AddCookie("Cookie", $"CTRL={sessionId}");
            
            using (var response = await Client.Execute(httpRequest))
            {
                var catalogResponse =
                    new CatalogResponse { HttpStatusCode = (int)response.StatusCode };
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        
                        var s = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var c = Helper.FromJsonString<List<Product>>(s);
                        catalogResponse.Products = c;
                        
                        return catalogResponse;
                    case HttpStatusCode.BadRequest:
                        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (content == null)
                            return catalogResponse;

                        catalogResponse =
                            BaseResponse.FromJsonString<CatalogResponse>(
                                catalogResponse.HttpStatusCode, content);

                        return catalogResponse;
                    default:
                        var msg = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        if (!string.IsNullOrEmpty(msg))
                            catalogResponse.Messages.Add(new ResponseMessage
                            {
                                Code = catalogResponse.HttpStatusCode,
                                Message = msg
                            });
                        return catalogResponse;
                }
            }
        }
        #endregion
    }
}