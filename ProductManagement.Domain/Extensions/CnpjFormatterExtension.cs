namespace ProductManagement.Domain.Extensions;

public static class CnpjFormatterExtension
{
    public static string FormatCnpjToDatabase(this string cnpjCpfRg)
    {
        return new string(cnpjCpfRg.Where(char.IsDigit).ToArray());
    }
}
