using OfficeOpenXml;

namespace DataFaker.Domain.Gerador;

public interface IExcelFakeDataGenerator
{
    byte[] Generate(int quantidade);
}

public class ExcelFakeDataGenerator : IExcelFakeDataGenerator
{
    private ExcelWorksheet _worksheet;

    public byte[] Generate(int quantidade)
    {
        using ExcelPackage package = Iniciar();

        AdicionarCabecalho();
        PreencherDados(quantidade);

        return package.GetAsByteArray();
    }

    private void PreencherDados(int quantidade)
    {
        for (int i = 1; i <= quantidade; i++)
        {
            var fakeData = AleatorizarDados();

            _worksheet.Cells[i + 1, 1].Value = fakeData.Nome;
            _worksheet.Cells[i + 1, 2].Value = fakeData.Email;
            _worksheet.Cells[i + 1, 3].Value = fakeData.DataDeNascimento.ToString("dd/MM/yyyy");
        }

        _worksheet.Cells[_worksheet.Dimension.Address].AutoFitColumns();
    }

    private FakeData AleatorizarDados()
    {
        Random random = new Random();

        string[] nomes = [
            "Alice", "Bruno", "Carla", "Daniel", "Elena",
            "Felipe", "Gabriela", "Hugo", "Isabela", "João",
            "Karla", "Lucas", "Mariana", "Nicolas", "Olivia",
            "Paulo", "Quintina", "Rafael", "Sofia", "Thiago",
            "Umberto", "Vanessa", "Wagner", "Xuxa", "Yasmin",
            "Zoe", "André", "Bianca", "Caio", "Diana",
            "Eduardo", "Fernanda", "Gustavo", "Helena", "Igor",
            "Jéssica", "Leonardo", "Márcio", "Natália", "Otávio",
            "Priscila", "Ricardo", "Samara", "Tânia", "Ulisses",
            "Viviane", "Walter", "Xavier", "Yuri", "Zita"
        ];
        string[] dominios = ["hotmail.com", "gmail.com", "mail.com", "outlook.com"];

        string nomeAleatorio = nomes[random.Next(nomes.Length)];
        string emailAleatorio = $"{nomeAleatorio.ToLower()}@{dominios[random.Next(dominios.Length)]}";

        var dataInicio = new DateTime(1950, 1, 1);
        var dataFim = new DateTime(2000, 12, 31);
        int range = (dataFim - dataInicio).Days;
        var dataDeNascimentoAleatoria = dataInicio.AddDays(random.Next(range));

        return new FakeData
        {
            Nome = nomeAleatorio,
            Email = emailAleatorio,
            DataDeNascimento = dataDeNascimentoAleatoria
        };
    }

    private ExcelPackage Iniciar()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var package = new ExcelPackage();
        _worksheet = package.Workbook.Worksheets.Add("Gerador de dados");
        return package;
    }

    private void AdicionarCabecalho()
    {
        _worksheet.Cells[1, 1].Value = "Nome";
        _worksheet.Cells[1, 2].Value = "E-mail";
        _worksheet.Cells[1, 3].Value = "Data de nascimento";
    }

}
