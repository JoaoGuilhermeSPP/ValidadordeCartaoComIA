using System;
using System.Text.RegularExpressions;

public class CartaoValidator
{
    public static string IdentificarBandeira(string numeroCartao)
    {
        numeroCartao = numeroCartao.Replace(" ", "").Replace("-", "");

        if (Regex.IsMatch(numeroCartao, @"^4[0-9]{12}(?:[0-9]{3})?$"))
            return "VISA";

        if (Regex.IsMatch(numeroCartao, @"^(5[1-5][0-9]{14}|2(2[2-9][0-9]{12}|[3-6][0-9]{13}|7[01][0-9]{12}|720[0-9]{12}))$"))
            return "MASTERCARD";

        if (Regex.IsMatch(numeroCartao, @"^3[47][0-9]{13}$"))
            return "AMERICAN EXPRESS";

        if (Regex.IsMatch(numeroCartao, @"^3(?:0[0-5]|[68][0-9])[0-9]{11}$"))
            return "DINERS CLUB";

        return "DESCONHECIDA";
    }

    public static bool ValidarDataValidade(string mesAno)
    {
        if (!Regex.IsMatch(mesAno, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            return false;

        int mes = int.Parse(mesAno.Substring(0, 2));
        int ano = int.Parse("20" + mesAno.Substring(3, 2));

        DateTime validade = new DateTime(ano, mes, DateTime.DaysInMonth(ano, mes));
        return validade >= DateTime.Now;
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Digite o número do cartão:");
        string numero = Console.ReadLine();

        Console.WriteLine("Digite a validade (MM/AA):");
        string validade = Console.ReadLine();

        string bandeira = CartaoValidator.IdentificarBandeira(numero);
        bool dataValida = CartaoValidator.ValidarDataValidade(validade);

        Console.WriteLine($"Bandeira: {bandeira}");
        Console.WriteLine($"Validade: {(dataValida ? "Válida" : "Inválida")}");

        if (bandeira != "DESCONHECIDA" && dataValida)
            Console.WriteLine("Cartão Válido");
        else
            Console.WriteLine("Cartão Inválido");
    }
}
