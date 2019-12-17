using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace IntelligentCheckout.Backend.Domain.Services
{
    public interface IBlockchain
    {
        Task InserirContrato(Models.Compra compra);
    }

    public class Blockchain : IBlockchain
    {

        private readonly Configuracoes _configuracoes;
        public Blockchain(Configuracoes configuracoes)
            => this._configuracoes = configuracoes;

        public Task InserirContrato(Models.Compra compra)
        {
            //var accessToken = await this.ObterAccessToken();
            //using var client = this.CriarHttpClient();
            ////client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //client.DefaultRequestHeaders.Remove("Authorization");
            //client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");


            //var contract = new Contract(compra);
            //var content = this.GetHttpContent(contract, accessToken);
            //var response = await client.PostAsync(this._configuracoes.ContractEndpoint, content);
            //var json = await response.Content.ReadAsStringAsync();
            return Task.CompletedTask;
        }

        private string _accessToken;
        private async Task<string> ObterAccessToken()
        {
            if (!string.IsNullOrEmpty(this._accessToken))
                return this._accessToken;

            var parametros = this._configuracoes.ObterParametros();
            using var client = this.CriarHttpClient();
            using var response = await client.PostAsync(this._configuracoes.TokenEndpoint, new FormUrlEncodedContent(parametros));
            var json = await response.Content.ReadAsStringAsync();
            var @object = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            this._accessToken = @object.access_token;
            return this._accessToken;
        }

        private HttpClient CriarHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private HttpContent GetHttpContent<T>(T content, string accessToken)
        {
            var serializerSettings = new JsonSerializerSettings();
            var json = JsonConvert.SerializeObject(content, serializerSettings);
            var stringContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            return stringContent;
        }

        private class Contract
        {
            [JsonProperty("workflowActionParameters")]
            public IList<WorkflowActionParameter> WorkflowActionParameters { get; set; } = new List<WorkflowActionParameter>();

            [JsonProperty("workflowFunctionID")]
            public int WorkflowFunctionID { get; set; }

            public Contract(Models.Compra compra)
            {
                var json = JsonConvert.SerializeObject(compra);
                this.WorkflowFunctionID = 6;
                this.WorkflowActionParameters.Add(new WorkflowActionParameter("message", json));
            }
        }

        private class WorkflowActionParameter
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }

            public WorkflowActionParameter(string name, string value)
            {
                this.Name = name;
                this.Value = value;
            }
        }

        public class Configuracoes
        {
            public string TokenEndpoint { get; }
            public string TokenGrantType { get; }
            public string TokenClientId { get; }
            public string TokenClientSecret { get; }
            public string TokenResource { get; }
            public string ContractEndpoint { get; set; }

            public Configuracoes(string tokenEndpoint,
                                 string tokenGrantType,
                                 string tokenClientId,
                                 string tokenClientSecret,
                                 string tokenResource,
                                 string contractEndpoint)
            {
                this.TokenEndpoint = tokenEndpoint;
                this.TokenGrantType = tokenGrantType;
                this.TokenClientId = tokenClientId;
                this.TokenClientSecret = tokenClientSecret;
                this.TokenResource = tokenResource;
                this.ContractEndpoint = contractEndpoint;
            }

            internal IDictionary<string, string> ObterParametros()
            {
                var parametros = new Dictionary<string, string>();
                parametros.Add("grant_type", this.TokenGrantType);
                parametros.Add("client_id", this.TokenClientId);
                parametros.Add("client_secret", this.TokenClientSecret);
                parametros.Add("resource", this.TokenResource);
                return parametros;
            }
        }
    }
}
