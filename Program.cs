Console.Write("Enter a formula: ");
var formula = Console.ReadLine()!;

var result = Evaluate(formula);
Console.WriteLine($"The result is {result}");


int Evaluate(string formula)
{
    var result = 0;

    formula = formula.Replace(" ", "");
    if (formula == "")
    {
        return 0;
    }

    char op = '+';
    if (formula[0] is '+' or '-')
    {
        op = formula[0];
        formula = formula.Substring(1);
    }

    int indexOfNextOperator;
    do
    {
        indexOfNextOperator = FindIndexOfNextOperator(formula);
        if (indexOfNextOperator == -1)
        {
            result = Aggregate(result, op, formula);
        }
        else
        {
            var left = formula.Substring(0, indexOfNextOperator);

            result = Aggregate(result, op, left);

            op = formula[indexOfNextOperator];

            formula = formula.Substring(indexOfNextOperator + 1);
        }
    }
    while (indexOfNextOperator != -1);

    return result;
}

int FindIndexOfNextOperator(string formula)
{
    var indexOfPlus = formula.IndexOf('+');
    var indexOfMinus = formula.IndexOf('-');
    if (indexOfPlus == -1) { return indexOfMinus; }
    if (indexOfMinus == -1) { return indexOfPlus; }
    return Math.Min(indexOfPlus, indexOfMinus);
}

int Aggregate(int result, char op, string numberAsString)
{
    var number = int.Parse(numberAsString);

    switch (op)
    {
        case '-': result -= number; break;
        default: result += number; break;
    }

    return result;
}