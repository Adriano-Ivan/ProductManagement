namespace ProductManagement.Domain.Extensions;

public static class CnpjValidationExtension
{
    public static bool CnpjIsValid(this string cnpj)
    {
        // Remove caracteres não numéricos
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        // Verifica se o CNPJ possui 14 dígitos
        if (cnpj.Length != 14)
        {
            return false;
        }

        // Verifica se todos os dígitos são iguais (caso contrário, o cálculo abaixo falhará)
        if (cnpj.All(d => d == cnpj[0]))
        {
            return false;
        }

        // Calcula o primeiro dígito verificador
        int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int digito1 = LastVerifierDigit(cnpj, multiplicadores1);

        // Calcula o segundo dígito verificador
        int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int digito2 = LastVerifierDigit(cnpj + digito1, multiplicadores2);

        // Verifica se os dígitos calculados são iguais aos dígitos informados
        return cnpj.EndsWith($"{digito1}{digito2}");
    }

    private static int LastVerifierDigit(string cnpjParcial, int[] multiplicadores)
    {
        int total = 0;
        for (int i = 0; i < multiplicadores.Length; i++)
        {
            total += int.Parse(cnpjParcial[i].ToString()) * multiplicadores[i];
        }

        int resto = total % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}
