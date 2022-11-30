namespace Konvert;

internal class TextEditor
{
    const string _menuText = @"Сохранить файл в одном из форматов (txt,json,xml). Закрыть программу: Esc Ввести новый адрес файла: F1
------------------------------------------------------------------------------------------------------";
    const int _menuTextLinesCount = 2;
    ConsoleKeyInfo key;

    // Редактируемая строка.
    private string _text;
    private int _xCursorPos;
    private int _yCursorPos;

    public string[] TextArray;

    public TextEditor(string[] receivedText)
    {
        _text = String.Join("\r\n", receivedText);
        TextArray = receivedText;
        _xCursorPos = 0;
        _yCursorPos = 0;
    }

    // Управление редактором (считывание и реагирование на клавиши).
    public int ReadKey()
    {
        key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                if (_yCursorPos > 0)
                    _yCursorPos--;
                break;
            case ConsoleKey.DownArrow:
                if (_yCursorPos < TextArray.Length - 1)
                    _yCursorPos++;
                break;
            case ConsoleKey.LeftArrow:
                if (_xCursorPos > 0)
                    _xCursorPos--;
                break;
            case ConsoleKey.RightArrow:
                if (_xCursorPos <= 100)
                    _xCursorPos++;
                break;
        }
        _text = TextArray[_yCursorPos];
        if (key.Key == ConsoleKey.F1)
            return 1;
        else if (key.Key == ConsoleKey.Escape)
            return 2;
        else
            return 0;
    }
    // Дабовление и удаление символов в строке при её уменьшении и увеличении.
    public void AdjustString()
    {
        // Добавить такое же кол-во пробелов при переходе на другую строчку.
        for (int i = 0; i < TextArray.Length; i++)
        {
            if (_text.Length >= _xCursorPos && _xCursorPos <= 100)
                TextArray[i] = TextArray[i] + " ";
        }
        _text = TextArray[_yCursorPos];
    }
    public void RedactString()
    {
        if (!Char.IsControl(key.KeyChar) && _xCursorPos <= 100)
        {
            _text = _text.Insert(_xCursorPos, key.KeyChar.ToString());
            _xCursorPos++;
        }
        else if (key.Key == ConsoleKey.Backspace && _xCursorPos > 0)
        {
            _text = _text.Remove(_xCursorPos - 1, 1);
            _xCursorPos--;
        }
        TextArray[_yCursorPos] = _text;
    }
    public void DisplayMenu()
    {
        Console.SetCursorPosition(0, 0);
        Console.WriteLine(_menuText);
        for (int i = 0; i < TextArray.Length; i++)
            Console.WriteLine(TextArray[i]);
        Console.SetCursorPosition(_xCursorPos, _yCursorPos+ _menuTextLinesCount);
    }
}