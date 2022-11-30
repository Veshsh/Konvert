namespace Konvert;

internal static class Program
{
    public static void Main()
    {

        int resume = 0;
        while (resume != 2)
        {
            // Запрос файла.
            string [] TextArray = new string[3];
            string filePath = String.Empty;
            while (!File.Exists(filePath))
            {
                Console.Write("Введите путь до файла: ");
                filePath = Console.ReadLine().Trim('"');
            }
            var filecomand = new TextStructed(filePath);
            TextArray = filecomand.FileOpen();
            var editor = new TextEditor(TextArray);// извлекаем данные из файла и разделяем их на массив строк
            resume = 0;
            while (resume == 0)
            {
                editor.DisplayMenu();
                resume = editor.ReadKey();
                Console.Clear();
                editor.AdjustString();
                editor.RedactString();   
            }
            filecomand.FileClose(TextArray);
        }
    }
}