using System.Net;
using k8s;
using k8s.Models;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;

namespace Kubernetes.Core.Client;

public class AnyResourceKind : IServiceOperations<k8s.Kubernetes>, IAnyResourceKind
{
    public AnyResourceKind(IKubernetes kubernetes)
    {
        Client = (k8s.Kubernetes)kubernetes;
    }

    public k8s.Kubernetes Client { get; }

    private static string Pattern(string group, string ns)
    {
        if (string.IsNullOrEmpty(group))
        {
            if (string.IsNullOrEmpty(ns))
            {
                return "api/{version}/{plural}";
            }
            else
            {
                return "api/{version}/namespaces/{namespace}/{plural}";
            }
        }
        else
        {
            if (string.IsNullOrEmpty(ns))
            {
                return "apis/{group}/{version}/{plural}";
            }
            else
            {
                return "apis/{group}/{version}/namespaces/{namespace}/{plural}";
            }

        }
    }

    /// <inheritdoc/>
    public async Task<HttpOperationResponse<KubernetesList<TResource>>>
        ListClusterAnyResourceKindWithHttpMessagesAsync<TResource>(string group, string version, string plural,
            string continueParameter = null, string fieldSelector = null, string labelSelector = null,
            int? limit = null, string resourceVersion = null, int? timeoutSeconds = null, bool? watch = null,
            string pretty = null, Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default) where TResource : IKubernetesObject
    {
        if (group == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "group");
        }

        if (version == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "version");
        }

        if (plural == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "plural");
        }

        var namespaces = Client.ListNamespace();

        // Tracing
        bool _shouldTrace = ServiceClientTracing.IsEnabled;
        string _invocationId = null;
        if (_shouldTrace)
        {
            _invocationId = ServiceClientTracing.NextInvocationId.ToString();
            Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
            tracingParameters.Add("continueParameter", continueParameter);
            tracingParameters.Add("fieldSelector", fieldSelector);
            tracingParameters.Add("labelSelector", labelSelector);
            tracingParameters.Add("limit", limit);
            tracingParameters.Add("resourceVersion", resourceVersion);
            tracingParameters.Add("timeoutSeconds", timeoutSeconds);
            tracingParameters.Add("watch", watch);
            tracingParameters.Add("pretty", pretty);
            tracingParameters.Add("group", group);
            tracingParameters.Add("version", version);
            tracingParameters.Add("plural", plural);
            tracingParameters.Add("cancellationToken", cancellationToken);
            ServiceClientTracing.Enter(_invocationId, this, "ListClusterAnyResourceKind", tracingParameters);
        }

        // Construct URL
        var _baseUrl = Client.BaseUri.AbsoluteUri;
        var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")),
            "apis/{group}/{version}/{plural}").ToString();
        if (string.IsNullOrEmpty(group))
        {
            _url = _url.Replace("apis/{group}", "api");
        }
        else
        {
            _url = _url.Replace("{group}", System.Uri.EscapeDataString(group));
        }

        _url = _url.Replace("{version}", System.Uri.EscapeDataString(version));
        _url = _url.Replace("{plural}", System.Uri.EscapeDataString(plural));
        List<string> _queryParameters = new List<string>();
        if (continueParameter != null)
        {
            _queryParameters.Add(string.Format("continue={0}", System.Uri.EscapeDataString(continueParameter)));
        }

        if (fieldSelector != null)
        {
            _queryParameters.Add(string.Format("fieldSelector={0}", System.Uri.EscapeDataString(fieldSelector)));
        }

        if (labelSelector != null)
        {
            _queryParameters.Add(string.Format("labelSelector={0}", System.Uri.EscapeDataString(labelSelector)));
        }

        if (limit != null)
        {
            _queryParameters.Add(string.Format("limit={0}",
                System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(limit, Client.SerializationSettings)
                    .Trim('"'))));
        }

        if (resourceVersion != null)
        {
            _queryParameters.Add(string.Format("resourceVersion={0}", System.Uri.EscapeDataString(resourceVersion)));
        }

        if (timeoutSeconds != null)
        {
            _queryParameters.Add(string.Format("timeoutSeconds={0}",
                System.Uri.EscapeDataString(SafeJsonConvert
                    .SerializeObject(timeoutSeconds, Client.SerializationSettings).Trim('"'))));
        }

        if (watch != null)
        {
            _queryParameters.Add(string.Format("watch={0}",
                System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(watch, Client.SerializationSettings)
                    .Trim('"'))));
        }

        if (pretty != null)
        {
            _queryParameters.Add(string.Format("pretty={0}", System.Uri.EscapeDataString(pretty)));
        }

        if (_queryParameters.Count > 0)
        {
            _url += "?" + string.Join("&", _queryParameters);
        }

        // Create HTTP transport objects
        var _httpRequest = new HttpRequestMessage();
        HttpResponseMessage _httpResponse = null;
        _httpRequest.Method = new HttpMethod("GET");
        _httpRequest.RequestUri = new System.Uri(_url);
        // Set Headers


        if (customHeaders != null)
        {
            foreach (var _header in customHeaders)
            {
                if (_httpRequest.Headers.Contains(_header.Key))
                {
                    _httpRequest.Headers.Remove(_header.Key);
                }

                _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
            }
        }

        // Serialize Request
        string _requestContent = null;
        // Set Credentials
        if (Client.Credentials != null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Client.Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
        }

        // Send Request
        if (_shouldTrace)
        {
            ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
        }

        cancellationToken.ThrowIfCancellationRequested();
        _httpResponse = await Client.HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
        if (_shouldTrace)
        {
            ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
        }

        HttpStatusCode _statusCode = _httpResponse.StatusCode;
        cancellationToken.ThrowIfCancellationRequested();
        string _responseContent = null;
        if ((int)_statusCode != 200)
        {
            var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'",
                _statusCode));
            if (_httpResponse.Content != null)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                _responseContent = string.Empty;
            }

            ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
            ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
            if (_shouldTrace)
            {
                ServiceClientTracing.Error(_invocationId, ex);
            }

            _httpRequest.Dispose();
            if (_httpResponse != null)
            {
                _httpResponse.Dispose();
            }

            throw ex;
        }

        // Create Result
        var _result = new HttpOperationResponse<KubernetesList<TResource>>();
        _result.Request = _httpRequest;
        _result.Response = _httpResponse;
        // Deserialize Response
        if ((int)_statusCode == 200)
        {
            _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                _result.Body =
                    SafeJsonConvert.DeserializeObject<KubernetesList<TResource>>(_responseContent,
                        Client.DeserializationSettings);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }

                throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
            }
        }

        if (_shouldTrace)
        {
            ServiceClientTracing.Exit(_invocationId, _result);
        }

        return _result;
    }

    /// <inheritdoc/>
    public async Task<HttpOperationResponse<object>> CreateAnyResourceKindWithHttpMessagesAsync<TResource>(
        TResource body, string group, string version, string namespaceParameter, string plural,
        string dryRun = default(string), string fieldManager = default(string), string pretty = default(string),
        Dictionary<string, List<string>> customHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken)) where TResource : IKubernetesObject
    {
        if (body == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "body");
        }

        if (group == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "group");
        }

        if (version == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "version");
        }

        if (namespaceParameter == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "namespaceParameter");
        }

        if (plural == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "plural");
        }

        // Tracing
        bool _shouldTrace = ServiceClientTracing.IsEnabled;
        string _invocationId = null;
        if (_shouldTrace)
        {
            _invocationId = ServiceClientTracing.NextInvocationId.ToString();
            Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
            tracingParameters.Add("body", body);
            tracingParameters.Add("dryRun", dryRun);
            tracingParameters.Add("fieldManager", fieldManager);
            tracingParameters.Add("pretty", pretty);
            tracingParameters.Add("group", group);
            tracingParameters.Add("version", version);
            tracingParameters.Add("namespaceParameter", namespaceParameter);
            tracingParameters.Add("plural", plural);
            tracingParameters.Add("cancellationToken", cancellationToken);
            ServiceClientTracing.Enter(_invocationId, this, "CreateNamespacedCustomObject", tracingParameters);
        }

        // Construct URL
        var _baseUrl = Client.BaseUri.AbsoluteUri;
        var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")),
            Pattern(group, namespaceParameter)).ToString();
        _url = _url.Replace("{group}", System.Uri.EscapeDataString(group));
        _url = _url.Replace("{version}", System.Uri.EscapeDataString(version));
        _url = _url.Replace("{namespace}", System.Uri.EscapeDataString(namespaceParameter));
        _url = _url.Replace("{plural}", System.Uri.EscapeDataString(plural));
        List<string> _queryParameters = new List<string>();
        if (dryRun != null)
        {
            _queryParameters.Add(string.Format("dryRun={0}", System.Uri.EscapeDataString(dryRun)));
        }

        if (fieldManager != null)
        {
            _queryParameters.Add(string.Format("fieldManager={0}", System.Uri.EscapeDataString(fieldManager)));
        }

        if (pretty != null)
        {
            _queryParameters.Add(string.Format("pretty={0}", System.Uri.EscapeDataString(pretty)));
        }

        if (_queryParameters.Count > 0)
        {
            _url += "?" + string.Join("&", _queryParameters);
        }

        // Create HTTP transport objects
        var _httpRequest = new HttpRequestMessage();
        HttpResponseMessage _httpResponse = null;
        _httpRequest.Method = new HttpMethod("POST");
        _httpRequest.RequestUri = new System.Uri(_url);
        // Set Headers


        if (customHeaders != null)
        {
            foreach (var _header in customHeaders)
            {
                if (_httpRequest.Headers.Contains(_header.Key))
                {
                    _httpRequest.Headers.Remove(_header.Key);
                }

                _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
            }
        }

        // Serialize Request
        string _requestContent = null;
        if (body != null)
        {
            _requestContent = SafeJsonConvert.SerializeObject(body, Client.SerializationSettings);
            _httpRequest.Content = new StringContent(_requestContent, System.Text.Encoding.UTF8);
            _httpRequest.Content.Headers.ContentType = Client.GetHeader(body);
        }

        // Set Credentials
        if (Client.Credentials != null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Client.Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
        }

        // Send Request
        if (_shouldTrace)
        {
            ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
        }

        cancellationToken.ThrowIfCancellationRequested();
        _httpResponse = await Client.HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
        if (_shouldTrace)
        {
            ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
        }

        HttpStatusCode _statusCode = _httpResponse.StatusCode;
        cancellationToken.ThrowIfCancellationRequested();
        string _responseContent = null;
        if ((int)_statusCode != 201)
        {
            var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'",
                _statusCode));
            if (_httpResponse.Content != null)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                _responseContent = string.Empty;
            }

            ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
            ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
            if (_shouldTrace)
            {
                ServiceClientTracing.Error(_invocationId, ex);
            }

            _httpRequest.Dispose();
            if (_httpResponse != null)
            {
                _httpResponse.Dispose();
            }

            throw ex;
        }

        // Create Result
        var _result = new HttpOperationResponse<object>();
        _result.Request = _httpRequest;
        _result.Response = _httpResponse;
        // Deserialize Response
        if ((int)_statusCode == 201)
        {
            _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                _result.Body =
                    SafeJsonConvert.DeserializeObject<object>(_responseContent, Client.DeserializationSettings);
            }
            catch (JsonException ex)
            {
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }

                throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
            }
        }

        if (_shouldTrace)
        {
            ServiceClientTracing.Exit(_invocationId, _result);
        }

        return _result;
    }

    /// <inheritdoc/>
    public async Task<HttpOperationResponse<object>> PatchAnyResourceKindWithHttpMessagesAsync(V1Patch body,
        string group, string version, string namespaceParameter, string plural, string name,
        string dryRun = default(string), string fieldManager = default(string), bool? force = default(bool?),
        Dictionary<string, List<string>> customHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        if (body == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "body");
        }

        if (group == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "group");
        }

        if (version == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "version");
        }

        if (namespaceParameter == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "namespaceParameter");
        }

        if (plural == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "plural");
        }

        if (name == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "name");
        }

        // Tracing
        bool _shouldTrace = ServiceClientTracing.IsEnabled;
        string _invocationId = null;
        if (_shouldTrace)
        {
            _invocationId = ServiceClientTracing.NextInvocationId.ToString();
            Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
            tracingParameters.Add("body", body);
            tracingParameters.Add("dryRun", dryRun);
            tracingParameters.Add("fieldManager", fieldManager);
            tracingParameters.Add("force", force);
            tracingParameters.Add("group", group);
            tracingParameters.Add("version", version);
            tracingParameters.Add("namespaceParameter", namespaceParameter);
            tracingParameters.Add("plural", plural);
            tracingParameters.Add("name", name);
            tracingParameters.Add("cancellationToken", cancellationToken);
            ServiceClientTracing.Enter(_invocationId, this, "PatchNamespacedCustomObject", tracingParameters);
        }

        // Construct URL
        var _baseUrl = Client.BaseUri.AbsoluteUri;
        var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")),
            Pattern(group, namespaceParameter) + "/{name}").ToString();
        _url = _url.Replace("{group}", System.Uri.EscapeDataString(group));
        _url = _url.Replace("{version}", System.Uri.EscapeDataString(version));
        _url = _url.Replace("{namespace}", System.Uri.EscapeDataString(namespaceParameter));
        _url = _url.Replace("{plural}", System.Uri.EscapeDataString(plural));
        _url = _url.Replace("{name}", System.Uri.EscapeDataString(name));
        List<string> _queryParameters = new List<string>();
        if (dryRun != null)
        {
            _queryParameters.Add(string.Format("dryRun={0}", System.Uri.EscapeDataString(dryRun)));
        }

        if (fieldManager != null)
        {
            _queryParameters.Add(string.Format("fieldManager={0}", System.Uri.EscapeDataString(fieldManager)));
        }

        if (force != null)
        {
            _queryParameters.Add(string.Format("force={0}",
                System.Uri.EscapeDataString(SafeJsonConvert.SerializeObject(force, Client.SerializationSettings)
                    .Trim('"'))));
        }

        if (_queryParameters.Count > 0)
        {
            _url += "?" + string.Join("&", _queryParameters);
        }

        // Create HTTP transport objects
        var _httpRequest = new HttpRequestMessage();
        HttpResponseMessage _httpResponse = null;
        _httpRequest.Method = new HttpMethod("PATCH");
        _httpRequest.RequestUri = new System.Uri(_url);
        // Set Headers


        if (customHeaders != null)
        {
            foreach (var _header in customHeaders)
            {
                if (_httpRequest.Headers.Contains(_header.Key))
                {
                    _httpRequest.Headers.Remove(_header.Key);
                }

                _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
            }
        }

        // Serialize Request
        string _requestContent = null;
        if (body != null)
        {
            _requestContent = SafeJsonConvert.SerializeObject(body, Client.SerializationSettings);
            _httpRequest.Content = new StringContent(_requestContent, System.Text.Encoding.UTF8);
            _httpRequest.Content.Headers.ContentType = Client.GetHeader(body);
        }

        // Set Credentials
        if (Client.Credentials != null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Client.Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
        }

        // Send Request
        if (_shouldTrace)
        {
            ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
        }

        cancellationToken.ThrowIfCancellationRequested();
        _httpResponse = await Client.HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
        if (_shouldTrace)
        {
            ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
        }

        HttpStatusCode _statusCode = _httpResponse.StatusCode;
        cancellationToken.ThrowIfCancellationRequested();
        string _responseContent = null;
        if ((int)_statusCode != 200)
        {
            var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'",
                _statusCode));
            if (_httpResponse.Content != null)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                _responseContent = string.Empty;
            }

            ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
            ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
            if (_shouldTrace)
            {
                ServiceClientTracing.Error(_invocationId, ex);
            }

            _httpRequest.Dispose();
            if (_httpResponse != null)
            {
                _httpResponse.Dispose();
            }

            throw ex;
        }

        // Create Result
        var _result = new HttpOperationResponse<object>();
        _result.Request = _httpRequest;
        _result.Response = _httpResponse;
        // Deserialize Response
        if ((int)_statusCode == 200)
        {
            _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                _result.Body =
                    SafeJsonConvert.DeserializeObject<object>(_responseContent, Client.DeserializationSettings);
            }
            catch (JsonException ex)
            {
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }

                throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
            }
        }

        if (_shouldTrace)
        {
            ServiceClientTracing.Exit(_invocationId, _result);
        }

        return _result;
    }

    /// <inheritdoc/>
    public async Task<HttpOperationResponse<object>> DeleteAnyResourceKindWithHttpMessagesAsync(string group,
        string version, string namespaceParameter, string plural, string name,
        V1DeleteOptions body = default(V1DeleteOptions), int? gracePeriodSeconds = default(int?),
        bool? orphanDependents = default(bool?), string propagationPolicy = default(string),
        string dryRun = default(string), Dictionary<string, List<string>> customHeaders = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        if (group == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "group");
        }

        if (version == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "version");
        }

        if (namespaceParameter == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "namespaceParameter");
        }

        if (plural == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "plural");
        }

        if (name == null)
        {
            throw new ValidationException(ValidationRules.CannotBeNull, "name");
        }

        // Tracing
        bool _shouldTrace = ServiceClientTracing.IsEnabled;
        string _invocationId = null;
        if (_shouldTrace)
        {
            _invocationId = ServiceClientTracing.NextInvocationId.ToString();
            Dictionary<string, object> tracingParameters = new Dictionary<string, object>();
            tracingParameters.Add("body", body);
            tracingParameters.Add("gracePeriodSeconds", gracePeriodSeconds);
            tracingParameters.Add("orphanDependents", orphanDependents);
            tracingParameters.Add("propagationPolicy", propagationPolicy);
            tracingParameters.Add("dryRun", dryRun);
            tracingParameters.Add("group", group);
            tracingParameters.Add("version", version);
            tracingParameters.Add("namespaceParameter", namespaceParameter);
            tracingParameters.Add("plural", plural);
            tracingParameters.Add("name", name);
            tracingParameters.Add("cancellationToken", cancellationToken);
            ServiceClientTracing.Enter(_invocationId, this, "DeleteNamespacedCustomObject", tracingParameters);
        }

        // Construct URL
        var _baseUrl = Client.BaseUri.AbsoluteUri;
        var _url = new System.Uri(new System.Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")),
            Pattern(group, namespaceParameter) + "/{name}").ToString();
        _url = _url.Replace("{group}", System.Uri.EscapeDataString(group));
        _url = _url.Replace("{version}", System.Uri.EscapeDataString(version));
        _url = _url.Replace("{namespace}", System.Uri.EscapeDataString(namespaceParameter));
        _url = _url.Replace("{plural}", System.Uri.EscapeDataString(plural));
        _url = _url.Replace("{name}", System.Uri.EscapeDataString(name));
        List<string> _queryParameters = new List<string>();
        if (gracePeriodSeconds != null)
        {
            _queryParameters.Add(string.Format("gracePeriodSeconds={0}",
                System.Uri.EscapeDataString(SafeJsonConvert
                    .SerializeObject(gracePeriodSeconds, Client.SerializationSettings).Trim('"'))));
        }

        if (orphanDependents != null)
        {
            _queryParameters.Add(string.Format("orphanDependents={0}",
                System.Uri.EscapeDataString(SafeJsonConvert
                    .SerializeObject(orphanDependents, Client.SerializationSettings).Trim('"'))));
        }

        if (propagationPolicy != null)
        {
            _queryParameters.Add(string.Format("propagationPolicy={0}",
                System.Uri.EscapeDataString(propagationPolicy)));
        }

        if (dryRun != null)
        {
            _queryParameters.Add(string.Format("dryRun={0}", System.Uri.EscapeDataString(dryRun)));
        }

        if (_queryParameters.Count > 0)
        {
            _url += "?" + string.Join("&", _queryParameters);
        }

        // Create HTTP transport objects
        var _httpRequest = new HttpRequestMessage();
        HttpResponseMessage _httpResponse = null;
        _httpRequest.Method = new HttpMethod("DELETE");
        _httpRequest.RequestUri = new System.Uri(_url);
        // Set Headers


        if (customHeaders != null)
        {
            foreach (var _header in customHeaders)
            {
                if (_httpRequest.Headers.Contains(_header.Key))
                {
                    _httpRequest.Headers.Remove(_header.Key);
                }

                _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
            }
        }

        // Serialize Request
        string _requestContent = null;
        if (body != null)
        {
            _requestContent = SafeJsonConvert.SerializeObject(body, Client.SerializationSettings);
            _httpRequest.Content = new StringContent(_requestContent, System.Text.Encoding.UTF8);
            _httpRequest.Content.Headers.ContentType = Client.GetHeader(body);
        }

        // Set Credentials
        if (Client.Credentials != null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await Client.Credentials.ProcessHttpRequestAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
        }

        // Send Request
        if (_shouldTrace)
        {
            ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
        }

        cancellationToken.ThrowIfCancellationRequested();
        _httpResponse = await Client.HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
        if (_shouldTrace)
        {
            ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
        }

        HttpStatusCode _statusCode = _httpResponse.StatusCode;
        cancellationToken.ThrowIfCancellationRequested();
        string _responseContent = null;
        if ((int)_statusCode != 200)
        {
            var ex = new HttpOperationException(string.Format("Operation returned an invalid status code '{0}'",
                _statusCode));
            if (_httpResponse.Content != null)
            {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                _responseContent = string.Empty;
            }

            ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
            ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
            if (_shouldTrace)
            {
                ServiceClientTracing.Error(_invocationId, ex);
            }

            _httpRequest.Dispose();
            if (_httpResponse != null)
            {
                _httpResponse.Dispose();
            }

            throw ex;
        }

        // Create Result
        var _result = new HttpOperationResponse<object>();
        _result.Request = _httpRequest;
        _result.Response = _httpResponse;
        // Deserialize Response
        if ((int)_statusCode == 200)
        {
            _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            try
            {
                _result.Body =
                    SafeJsonConvert.DeserializeObject<object>(_responseContent, Client.DeserializationSettings);
            }
            catch (JsonException ex)
            {
                _httpRequest.Dispose();
                if (_httpResponse != null)
                {
                    _httpResponse.Dispose();
                }

                throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
            }
        }

        if (_shouldTrace)
        {
            ServiceClientTracing.Exit(_invocationId, _result);
        }

        return _result;
    }

}