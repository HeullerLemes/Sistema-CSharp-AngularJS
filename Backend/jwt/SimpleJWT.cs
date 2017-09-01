using Jose;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aulaWebApi.Authorization
{
    public class JWTSimple : IAuthProvider
    {
        private byte[] H256Secret = { 0x00, 0xcd, 0xd5, 0x1f, 0x45, 0x8a, 0x74, 0x10, 0xff, 0xcc, 0x1e, 0x70, 0xab, 0x25, 0xea, 0x4d, 0x36, 0x2e, 0xff, 0x88, 0x9c, 0x6b, 0xbe, 0xd5, 0xcc, 0x4a, 0xde, 0x9d, 0x93, 0xd1, 0xcb, 0x8e };

        public string CreateToken(string[] claims, Dictionary<string, string> fields)
        {
            var payload = new Dictionary<string, object>()
            {
                { "claims", claims }

            };

            foreach (string field in fields.Keys)
            {
                payload.Add(field, fields[field]);
            }

            return JWT.Encode(payload, H256Secret, JwsAlgorithm.HS256);

        }

        public string GetField(string token, string field)
        {
            try
            {
                string json = JWT.Decode(token, H256Secret);
                JObject obj = JObject.Parse(json);
                var tok = obj[field];
                if (tok == null)
                    return null;
                else
                    return tok.ToString();
            }
            catch (IntegrityException e) { throw new FormatException(e.Message); }
        }

        public bool HasClaim(string token, string claim)
        {
            try
            {
                string json = JWT.Decode(token, H256Secret);
                JObject obj = JObject.Parse(json);
                string[] arr = (string[])JsonConvert.DeserializeObject(obj["claims"].ToString(), typeof(string[]));
                return arr.Contains(claim);
            }
            catch (IntegrityException e) { throw new FormatException(e.Message); }

        }
    }
}
