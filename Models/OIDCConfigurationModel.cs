using System.Text.Json.Serialization;

namespace LoginProductMinimalApi.Models
{
    public class OIDCConfigurationModel
    {
        [JsonPropertyName("issuer")]
        public string Issuer { get; set; } = default!;

        [JsonPropertyName("jwks_uri")]
        public string JwksUri { get; set; } = default!;

        [JsonPropertyName("token_endpoint")]
        public string TokenEndpoint { get; set; } = default!;

        [JsonPropertyName("id_token_signing_alg_values_supported")]
        public string[] IdTokenSigningAlgValuesSupported { get; set; } = default!;
    }
}
