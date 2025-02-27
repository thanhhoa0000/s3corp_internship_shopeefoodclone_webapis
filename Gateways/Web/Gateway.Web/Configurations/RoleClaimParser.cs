namespace ShopeeFoodClone.WebApi.Gateway.Web.Configurations;

public class RoleClaimParser : IClaimsParser
{
    private static readonly Dictionary<string, string> DefaultClaims = GetClaimTypesConstantValues();
    
    public Response<string> GetValue(IEnumerable<Claim> claims, string key, string delimiter, int index)
    {
        Response<string> value = GetValue(claims, key);
        if (value.IsError)
        {
            return value;
        }

        if (string.IsNullOrEmpty(delimiter))
        {
            return value;
        }

        string[] array = value.Data.Split(delimiter.ToCharArray());
        if (array.Length <= index || index < 0)
        {
            return new ErrorResponse<string>(new CannotFindClaimError($"Cannot find claim for key: {key}, delimiter: {delimiter}, index: {index}"));
        }

        return new OkResponse<string>(array[index]);
    }
    
    public Response<List<string>> GetValuesByClaimType(IEnumerable<Claim> claims, string claimType)
    {
        return new OkResponse<List<string>>((from x in claims
            where GetClaimTypeValue(x.Type) == claimType.ToLower()
            select x.Value).ToList());
    }

    private static Response<string> GetValue(IEnumerable<Claim> claims, string key)
    {
        string[] array = (from c in claims
            where GetClaimTypeValue(c.Type) == key.ToLower()
            select c.Value).ToArray();
        if (array.Length != 0)
        {
            return new OkResponse<string>(new StringValues(array).ToString());
        }

        return new ErrorResponse<string>(new CannotFindClaimError("Cannot find claim for key: " + key));
    }

    private static string GetClaimTypeValue(string claim)
    {
        string claimType = claim;
        if (DefaultClaims.TryGetValue(claimType, out string? claimName))
        {
            claimType = claimName;
        }

        return claimType.ToLower();
    }
    
    private static Dictionary<string, string> GetClaimTypesConstantValues()
    {
        Type type = typeof(ClaimTypes);
        FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static);
        return fieldInfos.Where(fi => fi.IsLiteral && !fi.IsInitOnly)
            .ToDictionary(fi => fi.GetValue(null)!.ToString()!, fi => fi.Name);
    }
}
