using Microsoft.AspNetCore.Mvc;

namespace RestWithASP.Controllers;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    
    private readonly ILogger<CalculatorController> _logger;

    public CalculatorController(ILogger<CalculatorController> logger)
    {
        _logger = logger;
    }

    [HttpGet("sum/{firstNumber}/{secondNumber}")]
    public IActionResult Sum(string firstNumber, string secondNumber)
    {
        if(IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
            return Ok(sum.ToString());
        }

        return BadRequest("Invalid Input");
    }

    [HttpGet("sub/{firstNumber}/{secondNumber}")]
    public IActionResult Sub(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
            return Ok(sub.ToString());
        }

        return BadRequest("Inavlid Input");
    }

    [HttpGet("mul/{firstNumber}/{secondNumber}")]
    public IActionResult Mul(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var mul = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
            return Ok(mul.ToString());
        }

        return BadRequest("Inavlid Input");
    }

    [HttpGet("div/{firstNumber}/{secondNumber}")]
    public IActionResult Div(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var div = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
            return Ok(div.ToString());
        }

        return BadRequest("Inavlid Input");
    }

    [HttpGet("media/{firstNumber}/{secondNumber}")]
    public IActionResult Media(string firstNumber, string secondNumber)
    {
        if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
        {
            var media = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
            return Ok(media.ToString());
        }

        return BadRequest("Inavlid Input");
    }

    [HttpGet("square/{Number}")]
    public IActionResult Square(string Number)
    {
        if (IsNumeric(Number))
        {
            var square = Math.Sqrt((double)ConvertToDecimal(Number));
            return Ok(square.ToString());
        }

        return BadRequest("Inavlid Input");
    }

    private decimal ConvertToDecimal(string strNumber)
    {
        decimal decimalValue;
        if(decimal.TryParse(strNumber, out decimalValue))
        {
            return decimalValue;
        }
        return 0;
    }

    private bool IsNumeric(string strNumber)
    {
        double number;
        bool isNumber = double.TryParse(
            strNumber, System.Globalization.NumberStyles.Any, 
            System.Globalization.NumberFormatInfo.InvariantInfo, 
            out number);
        return isNumber;
    }
}
